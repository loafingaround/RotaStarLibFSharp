module CalculateScheduleTests

open System
open Xunit
open Swensen.Unquote
open Scheduling.SimulatedAnnealingScheduler
open Scheduling.Types
open Common

let nextDummyRandom _ =
    0

[<Fact>]
let ``Returns appropriate error for 0 shifts``() =
    let shifts = Array.empty<Shift>

    let staff = [| nancey |]

    let expected: Shift[] = Array.empty

    test <@ calculateSchedule nextDummyRandom shifts staff = Error NoShifts @>

[<Fact>]
let ``Returns appropriate error for 0 staff members``() =
    let shifts = [| priscilla |]

    let staff = Array.empty<StaffMember>

    test <@ calculateSchedule nextDummyRandom shifts staff = Error NoStaff @>

[<Fact>]
let ``Does not blow up given 1 shift and 1 staff member``() =
    let shifts = [| priscilla |]

    let staff = [| nancey |]

    try
        calculateSchedule nextDummyRandom shifts staff |> ignore
    with
        | ex -> Assert.True(false, ex.Message)

[<Fact>]
let ``Returns Ok result given 1 shift and 1 staff member``() =
    let shifts = [| priscilla |]

    let staff = [| nancey |]

    match calculateSchedule nextDummyRandom shifts staff with
    | Ok _ ->
        Assert.True(true)
    | Error error ->
        match error with
        | NoShifts ->
            Assert.True(false, "No shifts")
        | NoStaff ->
            Assert.True(false, "No staff")
