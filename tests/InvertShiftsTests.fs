module InvertShiftsTests

open Xunit
open Swensen.Unquote
open Scheduling.Scheduler
open Scheduling.Types
open Common

let sortAll staffShifts =
    Array.sort (Array.map (fun s -> { s with Shifts = Array.sort s.Shifts }) staffShifts)

[<Fact>]
let ``inverts single shift single staff member correctly``() =
    let shifts = [|
        { assassins with Staff = [| nancey |] }
    |]

    let expected = [|
        { nancey with Shifts = [| assassins |] }
    |]

    test <@ sortAll (invertShifts shifts) = sortAll expected @>

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
        { nancey with Shifts = [| assassins |] }
        { cheryl with Shifts = [| assassins |] }
    |]

    test <@ sortAll (invertShifts shifts) = sortAll expected @>

[<Fact>]
let ``inverts two shifts single staff member correctly``() =
    let shifts = [|
        { assassins with Staff = [| nancey |] }
        { dixie with Staff = [| nancey |] }
    |]

    let expected = [|
        { nancey with Shifts =
                        [|
                            dixie
                            assassins
                        |] }
    |]

    test <@ sortAll (invertShifts shifts) = sortAll expected @>

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
            nancey with Shifts = [| assassins |]
        }
        {
            cheryl with Shifts =
                        [|
                            dixie
                            assassins
                        |]
        }
        {
            britte with Shifts = [| dixie |]
        }
    |]

    test <@ sortAll (invertShifts shifts) = sortAll expected @>