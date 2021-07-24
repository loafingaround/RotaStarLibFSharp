#r "/general/Development/Projects/Rotaring/RotaStarLibFSharp/src/bin/Debug/netstandard2.0/Scheduling.dll"
#r "/general/Development/Projects/Rotaring/RotaStarLibFSharp/tests/bin/Debug/netcoreapp3.1/Scheduling.Tests.dll"

open Scheduling.Scheduler
open Scheduling.Types
open Common

// data

let shifts = [| assassins; dixie; priscilla |]

let staff = [| nancey; britte; cheryl |]

// display

let showSchedule shifts =
    printfn "<DISPLAY SCHEDULE GRAPHICALLY>"

// logic

match calculateInitialSchedule shifts staff with
| Error err ->
    printfn "Something went wrong"
| Ok initialSchedule ->
    showSchedule initialSchedule
