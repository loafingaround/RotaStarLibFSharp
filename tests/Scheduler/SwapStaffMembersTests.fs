module SwapStaffMembersTests

open Xunit
open Swensen.Unquote
open Scheduling.Scheduler
open Scheduling.Types
open Common


[<Fact>]
let ``returns no shifts unchanged``() =

    let shifts = Array.empty<Shift>

    let staffRandomIndicesSeq = [|0; 0;|]
    let shiftRandomIndices = 0, 1
    let actual = swapStaffMembers (getNextRandomFunc staffRandomIndicesSeq) (fun _  _ -> shiftRandomIndices) shifts

    let expected = shifts

    test <@ actual = expected @>


[<Fact>]
let ``returns single shift with no staff members unchanged``() =

    let shifts = [|
            { assassins with Staff = [| |] }
        |]

    let staffRandomIndicesSeq = [|0; 0;|]
    let shiftRandomIndices = 0, 1
    let actual = swapStaffMembers (getNextRandomFunc staffRandomIndicesSeq) (fun _  _ -> shiftRandomIndices) shifts

    let expected = shifts

    test <@ actual = expected @>

[<Fact>]
let ``returns single shift with single staff member unchanged``() =

    let shifts = [|
            { assassins with Staff = [| nancey |] }
        |]

    let staffRandomIndicesSeq = [|0; 0;|]
    let shiftRandomIndices = 0, 1
    let actual = swapStaffMembers (getNextRandomFunc staffRandomIndicesSeq) (fun _  _ -> shiftRandomIndices) shifts

    let expected = shifts

    test <@ actual = expected @>

[<Fact>]
let ``returns single shift with 2 staff members unchanged``() =

    let shifts = [|
            { assassins with Staff = [| nancey; britte|] }
        |]

    let staffRandomIndicesSeq = [|0; 0;|]
    let shiftRandomIndices = 0, 1
    let actual = swapStaffMembers (getNextRandomFunc staffRandomIndicesSeq) (fun _  _ -> shiftRandomIndices) shifts

    let expected = shifts

    test <@ actual = expected @>

[<Fact(Skip="X")>]
let ``returns 2 shifts with same staff members unchanged``() =

    let shifts = [|
            { assassins with Staff = [| nancey |] }
            { dixie  with Staff = [| nancey |] }
        |]

    let staffRandomIndicesSeq = [|0; 0;|]
    let shiftRandomIndices = 0, 1
    let actual = swapStaffMembers (getNextRandomFunc staffRandomIndicesSeq) (fun _  _ -> shiftRandomIndices) shifts

    let expected = shifts

    test <@ actual = expected @>

[<Fact(Skip="X")>]
let ``returns 2 shifts where first staff list is subset of second unchanged``() =

    let shifts = [|
            { dixie  with Staff = [| nancey |] }
            { priscilla with Staff = [| nancey; lola |] }
        |]

    let mutable calledNextRandomFunc = false
    let mutable calledGetDiffRandoms = false

    let nextRandomFunc (_, _) =
        calledNextRandomFunc <- true
        0

    let getDiffRandoms _ _ =
        calledGetDiffRandoms <- true
        0, 0

    let actual = swapStaffMembers nextRandomFunc getDiffRandoms shifts

    let expected = shifts

    test <@ actual = expected @>

[<Fact(Skip="X")>]
let ``returns 2 shifts where second staff list is subset of first unchanged``() =

    let shifts = [|
            { dixie  with Staff = [| nancey; lola |] }
            { priscilla with Staff = [| nancey |] }
        |]

    let staffRandomIndicesSeq = [|0; 0|]
    let shiftRandomIndices = 0, 1
    let actual = swapStaffMembers (getNextRandomFunc staffRandomIndicesSeq) (fun _  _ -> shiftRandomIndices) shifts

    let expected = shifts

    test <@ actual = expected @>

[<Fact(Skip="X")>]
let ``returns 3 shifts where each staff lists is a subset of at least one other unchanged``() =

    let shifts = [|
            { dixie  with Staff = [| nancey; lola; britte |] }
            { assassins with Staff = [| nancey |] }
            { priscilla with Staff = [| nancey; lola |] }
        |]

    let staffRandomIndicesSeq = [|0; 0|]
    let shiftRandomIndices = 0, 1
    let actual = swapStaffMembers (getNextRandomFunc staffRandomIndicesSeq) (fun _  _ -> shiftRandomIndices) shifts

    let expected = shifts

    test <@ actual = expected @>

[<Fact(Skip="X")>]
let ``swaps different staff members``() =

    let shifts = [|
            { assassins with Staff = [| nancey |] }
            { dixie  with Staff = [| britte |] }
        |]

    let staffRandomIndicesSeq = [|0; 0;|]
    let shiftRandomIndices = 0, 1
    let actual = swapStaffMembers (getNextRandomFunc staffRandomIndicesSeq) (fun _  _ -> shiftRandomIndices) shifts

    let expected = [|
            { assassins with Staff = [| britte |] }
            { dixie  with Staff = [| nancey |] }
        |]

    test <@ actual = expected @>

[<Fact(Skip="X")>]
let ``swaps randomly selected staff members``() =

    let shifts = [|
            { assassins with Staff = [| cheryl |] }
            { dixie  with Staff = [| nancey; lola |] }
            { priscilla with Staff = [| nancey; cheryl; britte |] }
        |]

    let staffRandomIndicesSeq = [|1; 2;|]
    let shiftRandomIndices = 1, 2
    let actual = swapStaffMembers (getNextRandomFunc staffRandomIndicesSeq) (fun _  _ -> shiftRandomIndices) shifts

    let expected = [|
            { assassins with Staff = [| cheryl |] }
            { dixie  with Staff = [| nancey; britte |] }
            { priscilla with Staff = [| nancey; cheryl; lola |] }
        |]

    test <@ actual = expected @>

[<Fact(Skip="X")>]
let ``does not produce shift with duplicate staff members``() =

    let shifts = [|
            { dixie  with Staff = [| nancey; lola |] }
            { priscilla with Staff = [| cheryl; nancey; britte |] }
        |]

    let staffRandomIndicesSeq = [|0; 2; 1|]
    let shiftRandomIndices = 0, 1
    let actual = swapStaffMembers (getNextRandomFunc staffRandomIndicesSeq) (fun _  _ -> shiftRandomIndices) shifts

    let expected = [|
            { dixie  with Staff = [| nancey; britte |] }
            { priscilla with Staff = [| cheryl; nancey; lola |] }
        |]

    test <@ actual = expected @>