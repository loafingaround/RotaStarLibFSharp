module InvertShiftsTests

open Xunit
open Swensen.Unquote
open Utilities
open Scheduling.Types
open Common

let sortAll staffShifts =
    Array.sort (Array.map (fun s -> { s with Shifts = Array.sort s.Shifts }) staffShifts)

[<Fact>]
let ``inverts no shifts correctly``() =
    let shifts = Array.empty<Shift>

    let expected = Array.empty<StaffMember>

    test <@ sortAll (invertShifts shifts) = sortAll expected @>

[<Fact>]
let ``inverts single shift no staff correctly``() =
    let shifts = [|
        { assassins with Staff = Array.empty }
    |]

    let expected = Array.empty<StaffMember>

    test <@ sortAll (invertShifts shifts) = sortAll expected @>

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

[<Fact>]
let ``inverts three shifts with staff overlap correctly``() =
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
                                cheryl
                            |]
        }
    |]

    let expected = [|
        {
            nancey with Shifts =
                        [|
                            assassins
                            priscilla
                        |]
        }
        {
            cheryl with Shifts =
                        [|
                            dixie
                            assassins
                            priscilla
                        |]
        }
        {
            britte with Shifts = [| dixie |]
        }
    |]

    test <@ sortAll (invertShifts shifts) = sortAll expected @>

[<Fact>]
let ``inverts shifts with staff that in turn have shifts that are inconsistent with the shifts they are on correctly``() =
    let shifts = [|
        {
            assassins with Staff =
                            [|
                                {
                                    nancey with Shifts =
                                                [|
                                                    assassins
                                                    dixie
                                                |]
                                }
                                {
                                    britte with Shifts =
                                                [|
                                                    assassins
                                                    dixie
                                                |]
                                }
                            |]
        }
        {
            dixie with Staff =
                        [|
                            {
                                cheryl with Shifts =
                                            [|
                                                assassins
                                            |]
                            }
                            {
                                britte with Shifts =
                                            [|
                                                priscilla
                                            |]
                            }
                            {
                                nancey with Shifts =
                                            [|
                                                priscilla
                                            |]
                            }
                        |]
        }
    |]

    let expected = [|
        {
            nancey with Shifts =
                        [|
                            assassins
                            dixie
                        |]
        }
        {
            britte with Shifts =
                        [|
                            assassins
                            dixie
                        |]
        }
        {
            cheryl with Shifts =
                        [|
                            dixie
                        |]
        }
    |]

    test <@ sortAll (invertShifts shifts) = sortAll expected @>
