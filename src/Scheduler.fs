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

    let calculateSchedule shifts staff =
        match meetsHardConstraints shifts staff with
        | Error requirement ->
            Error requirement
        | Ok (shifts, staff) ->
            match calculateInitialSchedule shifts staff with
            | Error err ->
                Error err
            | Ok initialSchedule ->
                let mutable temp = 1500.0
                let alpha = 0.9
                let numOfTempReductions = 250
                let numOfNeighboursToSearch = 20

                let rand = System.Random();

                let shiftCount = Array.length shifts

                let getDifferentRandomNumbers exclMax =
                    let first = rand.Next(0, exclMax)
                    let mutable second = first
                    while second = first do
                        second <- rand.Next(0, exclMax)
                    first, second

                // move operator
                let move (shifts: Shift[]) =
                    // TODO: can we swap two random shift staff members more elegantly?
                    let shift1Ix, shift2Ix = getDifferentRandomNumbers shiftCount
                    let shift1 = shifts.[shift1Ix]
                    let shift2 = shifts.[shift2Ix]
                    let shift1StaffIx = rand.Next(0, (Array.length shift1.Staff))
                    let shift2StaffIx = rand.Next(0, (Array.length shift2.Staff))
                    let temp = shift2.Staff.[shift2StaffIx]
                    shift2.Staff.[shift2StaffIx] <- shift1.Staff.[shift1StaffIx]
                    shift1.Staff.[shift1StaffIx] <- temp
                    shifts

                let mutable currSchedule = initialSchedule

                for i in 0..numOfNeighboursToSearch do
                    for j in 0..numOfNeighboursToSearch do
                        let currCost = calculateCost currSchedule

                        let newSchedule = move currSchedule
                        let newCost = calculateCost newSchedule

                        if newCost <= currCost then
                            currSchedule <- newSchedule
                        else
                            let r = rand.Next(0, 100) |> float |> (/) <| 100.0

                            let value = 1.0 / Math.Exp((newCost - currCost) / temp)

                            if r <= value then
                                currSchedule <- newSchedule

                        temp <- temp * alpha
                Ok currSchedule
