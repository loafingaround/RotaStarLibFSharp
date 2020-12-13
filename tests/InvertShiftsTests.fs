module InvertShiftsTests

open Xunit
open Swensen.Unquote
open Scheduling.Scheduler
open Scheduling.Types
open Common

[<Fact>]
let ``inverts single shift single staff member correctly``() =
    let shifts = [|
        { assassins with Staff = [| nancey |] }
    |]

    let expected = [|
        { nancey with Shifts = set [| assassins |] }
    |]

    test <@ (invertShifts shifts) = expected @>

[<Fact>]
let ``inverts single shift two staff correctly``() =
    let shifts = [|
        { assassins with Staff =
                            [|
                                nancey
                                cheryl
                            |] }
    |]

    let expected = [|
        { nancey with Shifts = set [| assassins |] }
        { cheryl with Shifts = set [| assassins |] }
    |]

    test <@ Array.sort (invertShifts shifts) = Array.sort expected @>

[<Fact>]
let ``inverts two shifts single staff member correctly``() =
    let shifts = [|
        { assassins with Staff = [| nancey |] }
        { dixie with Staff = [| nancey |] }
    |]

    let expected = [|
        { nancey with Shifts =
                        set [|
                            assassins
                            dixie
                        |] }
    |]

    test <@ (invertShifts shifts) = expected @>

[<Fact>]
let ``inverts two shifts with staff overlap correctly``() =
    let shifts: Shift[] = [|
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
    |]

    let expected = [|
        {
            nancey with Shifts = set [| assassins |]
        }
        {
            cheryl with Shifts =
                        set [|
                            assassins
                            dixie
                        |]
        }
        {
            britte with Shifts = set [| dixie |]
        }
    |]

    test <@ Array.sort (invertShifts shifts) = Array.sort expected @>