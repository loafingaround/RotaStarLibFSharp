// #r "/general/Development/Projects/Rotaring/RotaStarLibFSharp/src/bin/Debug/netstandard2.0/Scheduling.dll";;
// #r "/general/Development/Projects/Rotaring/RotaStarLibFSharp/tests/bin/Debug/netcoreapp3.1/Scheduling.Tests.dll";;

// open Scheduling.Types;;
// open Utilities;;
// open Common;;

// let shifts = [|
//     {
//         assassins with Staff =
//                         [|
//                             nancey
//                             cheryl   
//                         |]
//     }
//     {
//         dixie with Staff =
//                     [|
//                         britte
//                         cheryl
//                     |]
//     }
//     {
//         priscilla with Staff =
//                         [|
//                             nancey
//                             britte
//                         |]
//     }
// |];;

// shiftsToCsvFile shifts "/home/thomas/Desktop/shifts.csv";;

#I "XPlot.GoogleCharts/XPlot.GoogleCharts.3.0.1/lib/netstandard2.0"
#I "NewtonSoft.Json/Newtonsoft.Json.12.0.3/lib/netstandard2.0"
#I "Google.DataTable.Net.Wrapper/Google.DataTable.Net.Wrapper.4.0.0/lib/netstandard2.0"
#r "XPlot.GoogleCharts.dll"
open XPlot.GoogleCharts

let chart = Chart.Line [ for x in 0 .. 10 -> x, x*x ]
chart.Show()

let Bolivia = ["2004/05", 165.; "2005/06", 135.; "2006/07", 157.; "2007/08", 139.; "2008/09", 136.]
let Ecuador = ["2004/05", 938.; "2005/06", 1120.; "2006/07", 1167.; "2007/08", 1110.; "2008/09", 691.]
let Madagascar = ["2004/05", 522.; "2005/06", 599.; "2006/07", 587.; "2007/08", 615.; "2008/09", 629.]
let Average = ["2004/05", 614.6; "2005/06", 682.; "2006/07", 623.; "2007/08", 609.4; "2008/09", 569.6]

let series = [ "bars"; "bars"; "bars"; "lines" ]
let inputs = [ Bolivia; Ecuador; Madagascar; Average ]

let chart2 =
    inputs
    |> Chart.Combo
    |> Chart.WithOptions 
         (Options(title = "Coffee Production", series = 
            [| for typ in series -> Series(typ) |]))
    |> Chart.WithLabels 
         ["Bolivia"; "Ecuador"; "Madagascar"; "Average"]
    |> Chart.WithLegend true
    |> Chart.WithSize (600, 250)

chart2.Show()