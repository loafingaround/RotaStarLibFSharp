module MoveToNeighbourTests

open Xunit
open Swensen.Unquote
open Scheduling.Scheduler
open Scheduling.Types
open Common

[<Fact>]
let ``changes schedule``() =

    let shifts = [|
            { assassins with Staff = [| nancey |] };
            { dixie  with Staff = [| britte |] };
        |]

    let staffIndices = [|0; 0;|]
    let shiftIndices = 0, 1
    let neighbour = moveToNeigbour (getNextRandomFunc staffIndices) (fun _  _ -> shiftIndices) shifts

    test <@ shifts <> neighbour @>