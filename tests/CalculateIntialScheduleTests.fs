module CalculateIntialScheduleTests

open Xunit
open Swensen.Unquote
open Scheduling.Scheduler
open Scheduling.Types
open Common
open System

[<Fact>]
let ``outputs same as input``() =
    let shifts = [|
        assassins
        dixie
        priscilla
    |]

    let staff: StaffMember[] = [||]
    
    test <@ (calculateIntialSchedule shifts staff) = shifts @>

[<Fact>]
let ``returns error if feasible schedule is impossible`` =
    let shifts = [|
        assassins
        dixie
        priscilla
    |]

    let staff = [|
        { nancey with UnavailableDates = [|
                    {
                        Start = new DateTime(2021, 3, 1)
                        End = new DateTime(2021, 3, 6)
                    }
        |] } 
    |]
    ()