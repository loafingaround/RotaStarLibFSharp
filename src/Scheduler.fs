namespace Scheduling

module Scheduler =

    open System
    open Types

    let calculateMeanShiftsPerStaffMember shifts =
        let shiftStaff =
            shifts
            |> Array.collect (fun s -> s.Staff)
        let uniqueStaff = Array.distinct shiftStaff
        (float) shiftStaff.Length / (float) uniqueStaff.Length

    let calculateVariance shifts =
        let mean = (float) (calculateMeanShiftsPerStaffMember shifts)
        let shiftCounts =
            shifts
            |> Array.collect (fun s -> s.Staff)
            |> Array.groupBy id
            |> Array.map (fun g -> (float) (snd g).Length)

        let total =
            shiftCounts
            |> Array.sumBy (fun c -> (c - mean) ** 2.0)

        total / (float) shiftCounts.Length
    
    let invertShifts shifts =
        shifts
        |> Array.collect (fun s -> s.Staff |> Array.map (fun sm -> sm, { s with Staff = Array.empty }))
        |> Array.groupBy fst
        |> Array.map (fun g -> { fst g with Shifts = snd g |> Array.map (fun ss -> snd ss) })

    let calculateCost shifts =
        calculateVariance shifts

    // TODO: do we need this? Surely if hard constraints are not met these should just be flagged to
    // the user afterwards (so they can find a solution later) rather than refusing to calculate a
    // schedule because, for example, there are not enough available staff for 1 shift.
    // Or are there constraints that should stop us in our tracks?
    let meetsHardConstraints shifts staff =
        // TODO: implement this properly with checks for different kinds of failure to meet requirements
        if false then
            Error (MinimumNumberOfStaff(required = 2, actual = 1))
        else
            Ok (shifts, staff)

    let calculateInitialSchedule (shifts: Shift[]) (staff: StaffMember[]) =
        let pick shift =
            staff
            // TODO: assign and sort by weighting according existing number of shifts, shift preferences etc.
            |> Array.sortBy (fun s -> s.Shifts.Length)
            |> Array.truncate shift.MaximumNumberOfStaff

        Ok [|
            for i in 0..shifts.Length - 1 ->
                let picked = pick shifts.[i]

                for sm in picked do
                    let ix = staff |> Array.findIndex (fun curr -> curr = sm)
                    staff.[ix] <- {staff.[ix] with Shifts = Array.append staff.[ix].Shifts [|shifts.[i]|]}

                { shifts.[i] with Staff = picked }
        |]

    let getDifferentRandomNumbers nextRandom exclMax =
        if exclMax <= 1 then
            0, 0
        else if exclMax = 2 then
            0, 1
        else
            let first = nextRandom(0, exclMax)
            let mutable second = first
            while second = first do
                second <- nextRandom(0, exclMax)
            first, second

    let allButOneIsSubsetOfOthers (arrays: 'a[][]) =
        let sets = Array.map set arrays
        let subsets = Array.filter (fun s -> true) sets // TODO
        true

    let swapStaffMembers (nextRandom: int * int -> int) getDiffRandoms (shifts: Shift[]) =
        // TODO: ensure we do not end with same staff member on shift more than once
        // TODO: try again if randomly selected shift has no staff
        let clone = Array.map (fun s -> { s with Staff = Array.copy s.Staff }) shifts
        let shiftCount = Array.length clone

        let getIndices () =
            let shift1Ix, shift2Ix = getDiffRandoms nextRandom shiftCount
            let shift1StaffCount = Array.length shifts.[shift1Ix].Staff
            let shift2StaffCount = Array.length shifts.[shift2Ix].Staff
            let shift1StaffIx = nextRandom(0, shift1StaffCount)
            let shift2StaffIx = nextRandom(0, shift2StaffCount)
            (shift1Ix, shift1StaffIx), (shift2Ix, shift2StaffIx)

        let swap (shifts: Shift[]) =
            // TODO: keep trying until 2 selected staff are not the same
            // TODO: introduce safety limit to prevent inifinite loop???
            let mutable ((shift1Ix, shift1StaffIx), (shift2Ix, shift2StaffIx)) = getIndices()
            while shifts.[shift1Ix].Staff.[shift1StaffIx] = shifts.[shift2Ix].Staff.[shift2StaffIx] do
                match getIndices() with
                    | ((shift1Ix, shift1StaffIx), (shift2Ix, shift2StaffIx)) ->
                        // TODO: return shifts unchanged if each staff lists is a subset of at least one other                
                        // TODO: can we swap two random shift staff members more elegantly?
                        let temp = shifts.[shift2Ix].Staff.[shift2StaffIx]
                        shifts.[shift2Ix].Staff.[shift2StaffIx] <- shifts.[shift1Ix].Staff.[shift1StaffIx]
                        shifts.[shift1Ix].Staff.[shift1StaffIx] <- temp
            shifts


        match clone with
        | [||] ->
            clone
        | [|_|] ->
            clone
        | _ ->
            if allButOneIsSubsetOfOthers (Array.map (fun s -> s.Staff) clone) then
                clone
            else
                swap clone

    // move operator
    // TODO: improve: for ex. rather than only swapping staff between shifts, could replace individual
    // staff from available staff (is it possible there could be staff that have not been assigned to a
    // shift in the initial schedule?)
    let moveToNeigbour nextRandom getDiffRandoms (shifts: Shift[]) =
        swapStaffMembers nextRandom getDiffRandoms shifts

    let calculateSchedule shifts staff =
        match meetsHardConstraints shifts staff with
        | Error requirement ->
            Error requirement
        | Ok (shifts, staff) ->
            match calculateInitialSchedule shifts staff with
            | Error err ->
                Error err
            | Ok initialSchedule ->
                let shiftCount = Array.length shifts
                
                if shiftCount <= 0 then
                    Ok initialSchedule
                else
                    let mutable temp = 1500.0
                    let alpha = 0.9
                    let numOfTempReductions = 250
                    let numOfNeighboursToSearch = 20

                    let rand = Random();

                    let mutable currSchedule = initialSchedule

                    for i in 0..numOfTempReductions do
                        for j in 0..numOfNeighboursToSearch do
                            let currCost = calculateCost currSchedule

                            let newSchedule = moveToNeigbour rand.Next getDifferentRandomNumbers currSchedule
                            let newCost = calculateCost newSchedule

                            printfn "Current schedule: %s" (String.Join(", ", currSchedule |> Array.map (fun s -> Array.length s.Staff)))

                            printfn "New cost %f <= current cost %f?" newCost currCost
                            if newCost <= currCost then
                                printfn "\tYes"
                                currSchedule <- newSchedule
                            else
                                printfn "\tNo"
                                let r = rand.Next(0, 100) |> float |> (/) <| 100.0

                                let value = 1.0 / Math.Exp((newCost - currCost) / temp)

                                printfn "Random no. %f <= value %f?" r value
                                if r <= value then
                                    printfn "\tYes"
                                    currSchedule <- newSchedule
                                else
                                    printfn "\tNo"

                        temp <- temp * alpha
                    Ok currSchedule
