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

    let moveToNeighbour nextRandom shifts availableStaff =
        // TODO: ensure we are not mutating inputs
        match availableStaff with
        | [||] ->
            shifts
        | _ ->
            let shiftsWithStaff =
                shifts
                |> Array.filter (fun s -> not (Array.isEmpty s.Staff))
            match shiftsWithStaff with
            | [||] ->
                shifts
            | _ ->
                let shiftIx = nextRandom (Array.length shiftsWithStaff)
                let shift = shiftsWithStaff.[shiftIx]
                let staffMemberIx = nextRandom (Array.length shift.Staff)
                // TODO: keep trying if selected available staff member is same as the existing staff member on the shift?
                let availableStaffMemberIx = nextRandom (Array.length availableStaff)
                shift.Staff.[staffMemberIx] <- availableStaff.[availableStaffMemberIx]
                shifts

    let satisfiesHardConstraints shifts =
        // TODO: include possibility of same staff member on shift more than once
        failwith "Not implemented"

    // if solution fails to satisfy hard constraints we apply a penalty to the cost
    let applyPenalty cost =
        // TODO
        failwith "Not implemented"

    let calculateSchedule shifts staff =
        failwith "Not implemented"
