#I "XPlot.GoogleCharts/XPlot.GoogleCharts.3.0.1/lib/netstandard2.0"
#r "XPlot.GoogleCharts.dll"
#r "/general/Development/Projects/Rotaring/RotaStarLibFSharp/src/bin/Debug/netstandard2.0/Scheduling.dll"
#r "/general/Development/Projects/Rotaring/RotaStarLibFSharp/tests/bin/Debug/netcoreapp3.1/Scheduling.Tests.dll"

open XPlot.GoogleCharts
open Scheduling.Types

// TODO: fix this: it's showing crosses (as opposed to ticks) everywhere
// Maybe it was relying on staff on shifts from initial schedule
// calculation in turn having shifts (which they don't now)?
let getStaffShiftsTable shifts staff =
    let staffShiftsMatrix =
        shifts
        |> Array.map (fun s ->
            staff
            |> Array.map (fun sm ->
                let working =
                    sm.Shifts
                    |> Array.exists (fun sh -> sh.Id = s.Id)
                sprintf "%s %s" sm.Forename sm.Surname, working))

    staffShiftsMatrix
    |> Chart.Table
    |> Chart.WithLabels (Array.append [|" "|] (Array.map (fun s -> s.Name) shifts))