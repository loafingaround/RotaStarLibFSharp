namespace Scheduling

module Scheduler =

    open Types

    let CalculateSchedule (shifts: Shift[]) (staff: Person[]) =
       shifts
