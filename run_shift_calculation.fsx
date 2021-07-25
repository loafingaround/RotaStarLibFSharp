#r "/general/Development/Projects/Rotaring/RotaStarLibFSharp/src/bin/Debug/netstandard2.0/Scheduling.dll"
#r "/general/Development/Projects/Rotaring/RotaStarLibFSharp/tests/bin/Debug/netcoreapp3.1/Scheduling.Tests.dll"
#I "XPlot.GoogleCharts/XPlot.GoogleCharts.3.0.1/lib/netstandard2.0"
#I "NewtonSoft.Json/Newtonsoft.Json.12.0.3/lib/netstandard2.0"
#I "Google.DataTable.Net.Wrapper/Google.DataTable.Net.Wrapper.4.0.0/lib/netstandard2.0"
#r "XPlot.GoogleCharts.dll"
#load "shift_display.fsx"

open Shift_display
open Scheduling.Scheduler
open Scheduling.Types
open Common

// data

let staff = [| nancey; britte; cheryl |]

let shifts = [|
        { assassins with MaximumNumberOfStaff = 2 };
        { dixie  with MaximumNumberOfStaff = 3 };
        { priscilla with MaximumNumberOfStaff = 1 }
    |]

// display

let showSchedule shifts =
    printfn "Displaying shifts..."
    let scheduleTable = getStaffShiftsTable shifts staff
    scheduleTable.Show()

// logic

match calculateSchedule shifts staff with
| Error err ->
    printfn "Something went wrong"
| Ok initialSchedule ->
    showSchedule initialSchedule
