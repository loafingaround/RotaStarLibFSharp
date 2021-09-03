#I "XPlot.GoogleCharts/XPlot.GoogleCharts.3.0.1/lib/netstandard2.0"
#r "XPlot.GoogleCharts.dll"
#r "src/bin/Debug/netstandard2.0/Scheduling.dll"
#r "tests/bin/Debug/netcoreapp3.1/Scheduling.Tests.dll"

open XPlot.GoogleCharts
open Scheduling.Types

let getStaffShiftsTable shifts staff =
    let staffShiftsMatrix =
        shifts
        |> Array.map (fun s ->
            staff
            |> Array.map (fun sm ->
                let name = sprintf "%s %s" sm.Forename sm.Surname
                let isWorking =
                    s.Staff
                    |> Array.exists (fun ssm -> ssm.Id = sm.Id)
                name, isWorking))

    staffShiftsMatrix
    |> Chart.Table
    |> Chart.WithLabels (Array.append [|" "|] (Array.map (fun s -> s.Name) shifts))