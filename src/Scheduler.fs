namespace Scheduling

module Scheduler =

    open Types

    let CalculateIntialSchedule (shifts: Shift[]) (staff: Person[]) =
       shifts

    // TODO let CalculateCost(shifts: Shift[]) =

    let invertShifts(shifts: Shift[]) =
        let staff = Set.empty<Person>
        shifts
        |> Array.fold
            (fun staff' shift -> shift.Staff |> Array.fold
                                    (fun staff'' staffMember ->
                                        let matchingStaff = staff''
                                                            |> Set.filter (fun sm -> {sm with Shifts = Array.empty} = staffMember)
                                        let stafflessShift = { shift with Staff = Array.empty }
                                        if (Set.isEmpty matchingStaff) then
                                            Set.add {staffMember with Shifts = [| stafflessShift |]} staff''
                                        else
                                            // TODO: modify the matching staff member (in matchingStaff) that we have
                                            // just found instead of iterating through staff'' again
                                            staff''
                                            |> Set.map
                                                (fun sm ->
                                                    if {sm with Shifts = Array.empty} = staffMember then
                                                        { sm with Shifts = Array.ofList (stafflessShift :: (List.ofArray sm.Shifts))  }
                                                    else
                                                        sm))
                                    staff')
                                staff
