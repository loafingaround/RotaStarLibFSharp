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

[<Fact>]
let ``Does not return shifts with same staff more than once on shift``() =
    let staff = [| nancey; britte; cheryl |]

    let shifts = [|
            { assassins with MaximumNumberOfStaff = 2 };
            { dixie  with MaximumNumberOfStaff = 3 };
            { priscilla with MaximumNumberOfStaff = 1 }
        |]

    let rand = Random(0)
    let nextRandom = fun max -> rand.Next(max)

    match calculateSchedule nextRandom shifts staff with
    | Ok schedule ->
        let withDuplicates =
            schedule
            |> Array.map (fun s ->
                let counts =
                    s.Staff
                    |> Array.countBy (fun sm -> sm.Id)
                    |> Array.where (fun c -> snd c > 1)
                s, counts)
            |> Array.where (fun sc -> Array.length (snd sc) > 0)
        let getStaffMemberById id =
            staff |> Array.find (fun s -> s.Id = id)
        let getStaffMemberNames idsAndCounts =
            let names =
                idsAndCounts
                |> Array.map (fun ic ->
                    match ic with
                    | id, count ->
                        let staffMember = getStaffMemberById id
                        sprintf "\t%s %s: %i times" staffMember.Forename staffMember.Surname count)
            String.Join("\n", names)
        let getMsg (withDuplicates: (Shift * (int * int)[])[]) =
            let names =
                withDuplicates
                |> Array.map (fun wd -> sprintf "%s\n%s" (fst wd).Name (getStaffMemberNames (snd wd)))
            "These shifts have the same staff multiple times:\n" + String.Join("\n", names)
        Assert.True(Array.isEmpty withDuplicates, getMsg withDuplicates)
    | Error _ ->
        ()
