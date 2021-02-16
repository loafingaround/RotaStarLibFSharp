namespace Scheduling

module Scheduler =

    open Types

    let calculateMeanShiftsPerStaffMemberFromShifts shifts =
        let shiftStaff =
            shifts
            |> Array.collect (fun s -> s.Staff)
        let uniqueStaff = Array.distinct shiftStaff
        (float) shiftStaff.Length / (float) uniqueStaff.Length

    let calculateMeanShiftsPerStaffMemberFromStaff staff =
        let total = staff |> Array.sumBy (fun s -> s.Shifts.Length)
        (float) total / (float) staff.Length

    let calculateVariance shifts =
        let mean = (float) (calculateMeanShiftsPerStaffMemberFromShifts shifts)
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
        // TODO: make this more sophisticated
        calculateMeanShiftsPerStaffMemberFromShifts shifts

    let calculateIntialSchedule shifts staff =
        shifts
