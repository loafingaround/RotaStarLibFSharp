module Tests

open Xunit
open Swensen.Unquote
open Scheduling.SimulatedAnnealingScheduler
open Scheduling.Types
open Utilities
open Common

[<Fact>]
let ``Check schedule output for shifts per staff member`` () =
    let staff = [| nancey; britte; cheryl |]

    let shifts = [|
            { assassins with MaximumNumberOfStaff = 2 };
            { dixie  with MaximumNumberOfStaff = 3 };
            { priscilla with MaximumNumberOfStaff = 1 }
        |]

    let schedule = calculateSchedule nextRandom shifts staff

    match schedule with
    | Error _ ->
        printfn "Something went wrong"
    | Ok schedule ->
        let inverted = invertShifts schedule

        for s in schedule do
            printfn "%s:" s.Name
            for sm in s.Staff do
                printfn "\t%s" sm.Forename

        for sm in inverted do
            printfn "No. of shifts: %i" (Array.length sm.Shifts)
