module InvertShiftsTests

open Xunit
open Swensen.Unquote
open Scheduling.Scheduler
open Scheduling.Types
open System

// shifts

let assassins =
    {
        Name = "Assassins"
        Start = new DateTime(2021, 3, 4, 18, 30, 00)
        End = new DateTime(2021, 3, 4, 21, 30, 00)
        Staff = Array.empty
    }

let dixie = 
    {
        Name = "Dixie Swim Club"
        Start = new DateTime(2021, 2, 15, 18, 30, 00)
        End = new DateTime(2021, 2, 15, 21, 30, 00)
        Staff = Array.empty
    }

let priscilla =
    {
        Name = "Priscilla queen of the desert"
        Start = new DateTime(2021, 1, 26, 13, 30, 00)
        End = new DateTime(2021, 1, 26, 17, 30, 00)
        Staff = Array.empty
    }

// staff

let nancey = 
    {
        Forename = "Nancey"
        Surname = "Fahy"
        UnavailableDates = Array.empty
        Shifts = Array.empty
    }

let cheryl =
    {
        Forename = "Cheryl"
        Surname = "Pentha"
        UnavailableDates = Array.empty
        Shifts = Array.empty
    }

let britte =
    {
        Forename = "Britte"
        Surname = "Lowery"
        UnavailableDates = Array.empty
        Shifts = Array.empty
    }

[<Fact>]
let ``outputs same as input``() =
    let shifts: Shift[] = [|
        assassins
        dixie
        priscilla
    |]

    let staff: Person[] = [||]
    test <@ (CalculateIntialSchedule shifts staff) = shifts @>



[<Fact>]
let ``inverts single shift single staff member correctly``() =
    let shifts = [|
        { assassins with Staff = [| nancey |] }
    |]

    let expected = set [|
        { nancey with Shifts = [| assassins |] }
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

    let expected = set [|
        { nancey with Shifts = [| assassins |] }
        { cheryl with Shifts = [| assassins |] }
    |]

    test <@ (invertShifts shifts) = expected @>

[<Fact>]
let ``inverts two shifts single staff member correctly``() =
    let shifts = [|
        { assassins with Staff = [| nancey |] }
        { dixie with Staff = [| nancey |] }
    |]

    let expected = set [|
        { nancey with Shifts =
                        [|
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

    let expected = set [|
        {
            nancey with Shifts = [| assassins |]
        }
        {
            cheryl with Shifts =
                        [|
                            assassins
                            dixie
                        |]
        }
        {
            britte with Shifts = [| dixie |]
        }
    |]

    test <@ (invertShifts shifts) = expected @>