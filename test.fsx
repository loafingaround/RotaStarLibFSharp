#r "/general/Development/Projects/Rotaring/RotaStarLibFSharp/src/bin/Debug/netstandard2.0/Scheduling.dll"
#r "/general/Development/Projects/Rotaring/RotaStarLibFSharp/tests/bin/Debug/netcoreapp3.1/Scheduling.Tests.dll"

open Scheduling.Scheduler
open Scheduling.Types
open Utilities
open Common

let shifts = [|
    {
        assassins with Staff =
                        [|
                            nancey
                            cheryl   
                        |]
    }
    {
        dixie with Staff =
                    [|
                        britte
                        cheryl
                    |]
    }
    {
        priscilla with Staff =
                        [|
                            nancey
                            britte
                        |]
    }
|]

// shiftsToCsvFile shifts "/home/thomas/Desktop/shifts.csv";;

#I "XPlot.GoogleCharts/XPlot.GoogleCharts.3.0.1/lib/netstandard2.0"
#I "NewtonSoft.Json/Newtonsoft.Json.12.0.3/lib/netstandard2.0"
#I "Google.DataTable.Net.Wrapper/Google.DataTable.Net.Wrapper.4.0.0/lib/netstandard2.0"
#r "XPlot.GoogleCharts.dll"
open XPlot.GoogleCharts

let staff = invertShifts shifts

let shiftCountByStaffMember =
    staff
    |> Array.map (fun sm -> sprintf "%s %s" sm.Forename sm.Surname, sm.Shifts.Length)
    |> Array.toList

let pieChart =
    shiftCountByStaffMember
    |> Chart.Pie
    |> Chart.WithTitle "Shifts by staff member"
    |> Chart.WithLegend true
    |> Chart.WithSize (1000, 1000)

pieChart.Show()

let staffShiftsMatrix =
    shifts
    |> Array.map (fun s ->
        staff
        |> Array.map (fun sm ->
            let working =
                sm.Shifts
                |> Array.map (fun sh -> sh.Id)
                |> Array.contains s.Id
            sprintf "%s %s" sm.Forename sm.Surname, working))

let scheduleTable =
    staffShiftsMatrix
    |> Chart.Table
    |> Chart.WithLabels (Array.append [|" "|] (Array.map (fun s -> s.Name) shifts))

scheduleTable.Show()

// TODO: Would be nice to have both charts on the same page but this doesn't seem possible using Show() method
// We would probably have to create our own page and add the mark from GetHtml() / GetInlineHtml() / GetInlineJS()