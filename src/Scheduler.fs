namespace Scheduling

module Scheduler =

    open Types

    let calculateMeanShiftsPerStaffMemberFromShifts shifts =
        let shiftStaff =
            shifts
            |> Array.collect (fun s -> s.Staff)
        let uniqueStaff = Array.distinct shiftStaff
        decimal shiftStaff.Length / decimal uniqueStaff.Length

    let calculateMeanShiftsPerStaffMemberFromStaff staff =
        let total = staff |> Array.sumBy (fun s -> s.Shifts.Length)
        (decimal) total / (decimal) staff.Length

    let calculateVariance shifts =
        let mean = calculateMeanShiftsPerStaffMemberFromShifts shifts
        let shiftsCounts =
            shifts
            |> Array.collect (fun s -> s.Staff)
            |> Array.groupBy id
            |> Array.map (fun g -> (snd g).Length)
        // TODO: implement the rest
        0M
    
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
