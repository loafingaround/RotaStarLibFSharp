namespace Scheduling

module Scheduler =

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
            calculateInitialSchedule shifts staff
