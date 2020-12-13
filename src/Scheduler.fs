namespace Scheduling

module Scheduler =

    open Types

    let CalculateIntialSchedule (shifts: Shift[]) (staff: Person[]) =
       shifts

    // TODO let CalculateCost(shifts: Shift[]) =

    let calculateAverageShiftsPerStaffMember (shifts: Shift[]) =
        let shiftStaff =
            shifts
            |> Array.collect (fun s -> s.Staff)
        let uniqueStaff = Array.distinct shiftStaff
        decimal shiftStaff.Length / decimal uniqueStaff.Length

    let invertShifts(shifts: Shift[]) =
        let staff = Set.empty<Person>
        shifts
        |> Array.fold
            (fun staff' shift -> shift.Staff |> Array.fold
                                    (fun staff'' staffMember ->
                                        let matchingStaff = staff''
                                                            |> Set.filter (fun sm -> {sm with Shifts = Set.empty} = staffMember)
                                        let stafflessShift = { shift with Staff = Array.empty }
                                        if (Set.isEmpty matchingStaff) then
                                            Set.add {staffMember with Shifts = Set.singleton stafflessShift } staff''
                                        else
                                            // TODO: modify the matching staff member (in matchingStaff) that we have
                                            // just found instead of iterating through staff'' again
                                            staff''
                                            |> Set.map
                                                (fun sm ->
                                                    if {sm with Shifts = Set.empty} = staffMember then
                                                        { sm with Shifts = Set.ofList (stafflessShift :: (Set.toList sm.Shifts))  }
                                                    else
                                                        sm))
                                    staff')
                                staff
