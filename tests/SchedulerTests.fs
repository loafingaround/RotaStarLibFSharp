module SchedulerTests

open Xunit
open Swensen.Unquote
open Scheduling.Scheduler
open Scheduling.Types

[<Fact>]
let ``outputs same as input``() =
    let shifts: Shift[] = [||]
    let staff: Person[] = [||]
    test <@ (CalculateSchedule shifts staff) = shifts @>
