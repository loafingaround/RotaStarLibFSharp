namespace Scheduling

module Scheduler =

    open Types

    let CalculateIntialSchedule (shifts: Shift[]) (staff: Person[]) =
       shifts

    // TODO let CalculateCost(shifts: Shift[]) =

    let invertShifts(shifts: Shift[]) =
        let staff = Set.empty<Person>
        shifts
        |> Array.collect (fun s -> s.Staff)
        |> Array.fold (fun ss person -> if (Set.contains person ss) then ss else (Set.add person ss)) staff
