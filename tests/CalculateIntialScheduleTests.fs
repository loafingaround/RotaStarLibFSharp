module CalculateIntialScheduleTests

open Xunit
open Swensen.Unquote
open Scheduling.Scheduler
open Scheduling.Types
open Common

[<Fact>]
let ``outputs same as input``() =
    let shifts = [|
        assassins
        dixie
        priscilla
    |]

    let staff: Person[] = [||]
    
    test <@ (CalculateIntialSchedule shifts staff) = shifts @>