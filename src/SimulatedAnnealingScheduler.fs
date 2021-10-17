namespace Scheduling

module SimulatedAnnealingScheduler =

    open System
    open Utilities
    open Types

    let calculateInitialSchedule (shifts: Shift[]) (staff: StaffMember[]) =
        // TODO: ensure we are not mutating inputs (maybe wait until we have changed collections to be immutable)
        let staffWithShiftCounts = staff |> Array.map (fun sm -> sm, 0)

        let pick shift =
            staffWithShiftCounts
            // TODO: assign and sort by weighting according existing number of shifts, shift preferences etc.
            |> Array.sortBy (fun smsc -> snd smsc)
            |> Array.truncate shift.MaximumNumberOfStaff

        [|
            for i in 0..shifts.Length - 1 ->
                let picked = pick shifts.[i]
                // TODO: make this code more elegant / performant? Maybe use more mutability.
                for smsc in picked do
                    let ix = staffWithShiftCounts |> Array.findIndex (fun curr -> curr = smsc)
                    staffWithShiftCounts.[ix] <- fst staffWithShiftCounts.[ix], snd staffWithShiftCounts.[ix] + 1

                { shifts.[i] with Staff = picked |> Array.map (fun smsc -> fst smsc) }
        |]

    // TODO: pass in calculateMeanShiftsPerStaffMember
    let calculateCost shifts =
        calculateVariance calculateMeanShiftsPerStaffMember shifts

    let moveToNeighbour nextRandom shifts (staff: StaffMember[]) =
        match staff with
        | [||] ->
            shifts
        | _ ->
            let shiftsWithStaffIndices =
                shifts
                |> Array.mapi (fun i s -> if Array.isEmpty s.Staff then None else Some i)
                |> Array.choose id
            match shiftsWithStaffIndices with
            | [||] ->
                shifts
            | _ ->
                // TODO: possibly we should clone every shift object as well to ensure the
                // Staff on input shifts cannot possibly be modified via the output shifts
                // but we plan to use an immutable collections for Staff anyway
                let shiftsCopy = Array.copy shifts
                let shiftIndicesIx = nextRandom (Array.length shiftsWithStaffIndices)
                let shiftIx = shiftsWithStaffIndices.[shiftIndicesIx]
                let shiftCopy =  { shiftsCopy.[shiftIx] with Staff = Array.copy shiftsCopy.[shiftIx].Staff }
                let nonMatchingStaff = staff |> Array.filter (fun sm -> not (shiftCopy.Staff |> Array.exists (fun sm2 -> sm.Id = sm2.Id)))
                match nonMatchingStaff with
                | [||] ->
                    shiftsCopy
                | _ ->
                    let shiftStaffMemberIx = nextRandom (Array.length shiftCopy.Staff)
                    let staffMemberIx = nonMatchingStaff |> Array.length |> nextRandom
                    shiftCopy.Staff.[shiftStaffMemberIx] <- nonMatchingStaff.[staffMemberIx]
                    shiftsCopy.[shiftIx] <- shiftCopy
                    shiftsCopy

    let satisfiesHardConstraints shifts =
        // TODO: include possibility of same staff member on shift more than once
        failwith "Not implemented"

    // if solution fails to satisfy hard constraints we apply a penalty to the cost
    let applyPenalty cost =
        // TODO
        failwith "Not implemented"

    let calculateSchedule nextRandom shifts staff =
        if Array.isEmpty shifts then
            Error NoShifts
        else if Array.isEmpty staff then
            Error NoStaff
        else
            let mutable temp = 1500.0
            let alpha = 0.9
            let numOfTempReductions = 250
            let numOfNeighboursToSearch = 20

            let mutable currSchedule = calculateInitialSchedule shifts staff

            for _ in 0..numOfTempReductions do
                for _ in 0..numOfNeighboursToSearch do
                    let currCost = calculateCost currSchedule

                    let newSchedule = moveToNeighbour nextRandom currSchedule staff
                    let newCost = calculateCost newSchedule

                    if newCost <= currCost then
                        currSchedule <- newSchedule
                    else
                        let random = nextRandom 100 |> float |> (/) <| 100.0
                        let acceptanceProbability = 1.0 / Math.Exp((newCost - currCost) / temp)

                        if random <= acceptanceProbability then
                            currSchedule <- newSchedule
                temp <- temp * alpha
            Ok currSchedule