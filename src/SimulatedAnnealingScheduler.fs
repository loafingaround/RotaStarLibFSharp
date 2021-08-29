namespace Scheduling

module SimulatedAnnealingScheduler =

    open Utilities
    open Types

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

    let calculateCost shifts =
        calculateVariance shifts

    let moveToNeighbour nextRandom shifts staff =
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
                let shiftsCopy = Array.copy shifts
                let shiftIndicesIx = nextRandom (Array.length shiftsWithStaffIndices)
                let shiftIx = shiftsWithStaffIndices.[shiftIndicesIx]
                let shiftCopy =  { shiftsCopy.[shiftIx] with Staff = shiftsCopy.[shiftIx].Staff }
                let shiftStaffMemberIx = nextRandom (Array.length shiftCopy.Staff)
                let staffMemberIx = nextRandom (Array.length staff)
                shiftCopy.Staff.[shiftStaffMemberIx] <- staff.[staffMemberIx]
                shiftsCopy

    let satisfiesHardConstraints shifts =
        // TODO: include possibility of same staff member on shift more than once
        failwith "Not implemented"

    // if solution fails to satisfy hard constraints we apply a penalty to the cost
    let applyPenalty cost =
        // TODO
        failwith "Not implemented"

    let calculateSchedule shifts staff =
        failwith "Not implemented"
