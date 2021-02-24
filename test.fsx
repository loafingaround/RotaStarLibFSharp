#r "/general/Development/Projects/Rotaring/RotaStarLibFSharp/src/bin/Debug/netstandard2.0/Scheduling.dll";;
#r "/general/Development/Projects/Rotaring/RotaStarLibFSharp/tests/bin/Debug/netcoreapp3.1/Scheduling.Tests.dll";;

open Scheduling.Types;;
open Utilities;;
open Common;;

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
|];;

shiftsToCsvFile shifts "/home/thomas/Desktop/shifts.csv";;