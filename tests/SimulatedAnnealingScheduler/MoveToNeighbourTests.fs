module MoveToNeighbourTests

open Xunit
open Swensen.Unquote
open Scheduling.SimulatedAnnealingScheduler
open Scheduling.Types
open Common

[<Fact>]
let ``Does not move for 0 shifts and 0 available staff members``() =
    let shifts = Array.empty<Shift>

    let staff = Array.empty<StaffMember>

    let expected = shifts

    let nextRandom = (fun _ -> 0)

    let actual = moveToNeighbour nextRandom shifts staff

    test <@ actual = expected @>

[<Fact>]
let ``Does not move for 0 shifts and 1 available staff member``() =
    let shifts = Array.empty<Shift>

    let staff = [|
        nancey
    |]

    let expected = shifts

    let nextRandom = (fun _ -> 0)

    let actual = moveToNeighbour nextRandom shifts staff

    test <@ actual = expected @>

[<Fact>]
let ``Does not move for 1 shift with 0 staff members and 0 available staff members``() =
    let shifts = [|
            { assassins with Staff = [| |] }
        |]

    let staff = Array.empty<StaffMember>

    let expected = shifts

    let nextRandom = (fun _ -> 0)

    let actual = moveToNeighbour nextRandom shifts staff

    test <@ actual = expected @>

[<Fact>]
let ``Does not move for 1 shift with 1 staff member and 0 available staff members``() =
    let shifts = [|
            { assassins with Staff = [| britte |] }
        |]

    let staff = Array.empty<StaffMember>

    let expected = shifts

    let nextRandom = (fun _ -> 0)

    let actual = moveToNeighbour nextRandom shifts staff

    test <@ actual = expected @>

[<Fact>]
let ``Does not move for 1 shift with 0 staff members and 1 available staff member``() =
    let shifts = [|
            { assassins with Staff = [| |] }
        |]

    let staff = [|
        nancey
    |]

    let expected = shifts

    let nextRandom = (fun _ -> 0)

    let actual = moveToNeighbour nextRandom shifts staff

    test <@ actual = expected @>

[<Fact>]
let ``Changes staff member for 1 shift with 1 staff member and 1 available staff member``() =
    let shifts = [|
            { assassins with Staff = [| britte |] }
        |]

    let staff = [|
        nancey
    |]

    let expected = [|
            { assassins with Staff = [| nancey |] }
        |]

    let nextRandom = (fun _ -> 0)

    let actual = moveToNeighbour nextRandom shifts staff

    test <@ actual = expected @>

[<Fact>]
let ``Does not move for 2 shifts both with 0 staff members and 0 available staff members``() =
    let shifts = [|
            { assassins with Staff = [| |] }
            { dixie with Staff = [| |] }
        |]

    let staff = Array.empty<StaffMember>

    let expected = shifts

    let nextRandom = (fun _ -> 0)

    let actual = moveToNeighbour nextRandom shifts staff

    test <@ actual = expected @>

[<Fact>]
let ``Does not move for 2 shifts both with 0 staff members and 1 available staff members``() =
    let shifts = [|
            { assassins with Staff = [| |] }
            { dixie with Staff = [| |] }
        |]

    let staff = [| nancey |]

    let expected = shifts

    let nextRandom = (fun _ -> 0)

    let actual = moveToNeighbour nextRandom shifts staff

    test <@ actual = expected @>

[<Fact>]
let ``Changes expected staff member for 2 shifts including 1 with 1 staff members and 1 available staff members``() =
    let shifts = [|
            { assassins with Staff = [| britte |] }
            { dixie with Staff = [| |] }
        |]

    let staff = [| nancey |]

    let expected = [|
            { assassins with Staff = [| nancey |] }
            { dixie with Staff = [| |] }
        |]

    let nextRandom = (fun _ -> 0)

    let actual = moveToNeighbour nextRandom shifts staff

    test <@ actual = expected @>

[<Fact>]
let ``Changes expected staff member for 2 shifts including 1 with 1 staff members and 2 available staff members``() =
    let shifts = [|
            { assassins with Staff = [| britte |] }
            { dixie with Staff = [| |] }
        |]

    let staff = [| nancey; cheryl |]

    let expected = [|
            { assassins with Staff = [| cheryl |] }
            { dixie with Staff = [| |] }
        |]

    let nextRandom = getNextRandomFuncFromSeq [|0; 0; 1|]

    let actual = moveToNeighbour nextRandom shifts staff

    test <@ actual = expected @>

[<Fact>]
let ``Changes expected staff member for 2 shifts each with 1 staff member and 2 available staff members``() =
    let shifts = [|
            { assassins with Staff = [| britte |] }
            { dixie with Staff = [| lola |] }
        |]

    let staff = [| nancey; cheryl |]

    let expected = [|
            { assassins with Staff = [| britte |] }
            { dixie with Staff = [| cheryl |] }
        |]

    let nextRandom = getNextRandomFuncFromSeq [|1; 0; 1|]

    let actual = moveToNeighbour nextRandom shifts staff

    test <@ actual = expected @>

[<Fact>]
let ``Changes expected staff member for 2 shifts including 1 with 1 staff and 1 with 2 staff members and 2 available staff members``() =
    let shifts = [|
            { assassins with Staff = [| britte |] }
            { dixie with Staff = [| lola; cheryl |] }
        |]

    let staff = [| nancey; mary |]

    let expected = [|
            { assassins with Staff = [| britte |] }
            { dixie with Staff = [| lola; mary |] }
        |]

    let nextRandom = getNextRandomFuncFromSeq [|1; 1; 1|]

    let actual = moveToNeighbour nextRandom shifts staff

    test <@ actual = expected @>
