namespace Scheduling

module Scheduler =

    open Types

    let CalculateIntialSchedule shifts staff =
       shifts

    // TODO let CalculateCost shifts =

    let calculateAverageShiftsPerStaffMember shifts =
        let shiftStaff =
            shifts
            |> Array.collect (fun s -> s.Staff)
        let uniqueStaff = Array.distinct shiftStaff
        decimal shiftStaff.Length / decimal uniqueStaff.Length

    let invertShifts shifts =
        shifts
        |> Array.collect (fun s -> s.Staff |> Array.map (fun sm -> sm, { s with Staff = Array.empty }))
        |> Array.groupBy fst
        |> Array.map (fun g -> { fst g with Shifts = snd g |> Array.map (fun ss -> snd ss) })
