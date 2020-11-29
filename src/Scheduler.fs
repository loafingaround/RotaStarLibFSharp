namespace Scheduling

module Scheduler =

    open Types

    let CalculateIntialSchedule (shifts: Shift[]) (staff: Person[]) =
       shifts
