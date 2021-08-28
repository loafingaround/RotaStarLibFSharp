namespace Scheduling

module SimulatedAnnealingScheduler =

    open System
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
        failwith "Not implemented"

    let moveToNeighbour shifts staff =
        failwith "Not implemented"

    let satisfiesHardConstraints shifts =
        failwith "Not implemented"

    // if solution fails to satisfy hard constraints we apply a penalty to the cost
    let applyPenalty cost =
        // TODO
        failwith "Not implemented"

    let calculateSchedule shifts staff =
        failwith "Not implemented"