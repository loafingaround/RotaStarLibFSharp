#r "src/bin/Debug/netstandard2.0/Scheduling.dll"
#r "tests/bin/Debug/netcoreapp3.1/Scheduling.Tests.dll"
#I "XPlot.GoogleCharts/XPlot.GoogleCharts.3.0.1/lib/netstandard2.0"
#I "NewtonSoft.Json/Newtonsoft.Json.12.0.3/lib/netstandard2.0"
#I "Google.DataTable.Net.Wrapper/Google.DataTable.Net.Wrapper.4.0.0/lib/netstandard2.0"
#r "XPlot.GoogleCharts.dll"
#load "shift_display.fsx"

open XPlot.GoogleCharts
open Shift_display
open Scheduling.SimulatedAnnealingScheduler
open Scheduling.Types
open Common
open Utilities

// data

let staff = [| nancey; britte; cheryl |]

let shifts = [|
        { assassins with MaximumNumberOfStaff = 2 };
        { dixie  with MaximumNumberOfStaff = 3 };
        { priscilla with MaximumNumberOfStaff = 1 }
    |]

// display

let showSchedule shifts =
    printfn "Displaying shifts table..."
    let scheduleTable = getStaffShiftsTable shifts staff
    scheduleTable.Show()
    printfn "Shifts table rendered."

// logic

match calculateSchedule nextRandom shifts staff with
| Error _ ->
    printfn "Something went wrong"
| Ok schedule ->
    showSchedule schedule

    let staff = invertShifts schedule

    printfn "Showing shifts per staff pie chart..."
    let shiftCountByStaffMember =
        staff
        |> Array.map (fun sm -> sprintf "%s %s (%i shifts)" sm.Forename sm.Surname sm.Shifts.Length, sm.Shifts.Length)
        |> Array.toList

    let pieChart =
        shiftCountByStaffMember
        |> Chart.Pie
        |> Chart.WithTitle "Shifts by staff member"
        |> Chart.WithLegend true
        |> Chart.WithSize (1000, 1000)

    pieChart.Show()
    printfn "Shifts per staff pie chart rendered."
