module SchedulerTests

open Xunit
open Swensen.Unquote
open Scheduling.Scheduler
open Scheduling.Types
open System

[<Fact>]
let ``outputs same as input``() =
    let shifts: Shift[] = [|
        {
            Name = "Assassins"
            Start = new DateTime(2021, 3, 4, 18, 30, 00)
            End = new DateTime(2021, 3, 4, 21, 30, 00)
            Staff = Array.empty
        }
        {
            Name = "Dixie Swim Club"
            Start = new DateTime(2021, 2, 15, 18, 30, 00)
            End = new DateTime(2021, 2, 15, 21, 30, 00)
            Staff = Array.empty
        }
        {
            Name = "The Life Of A Tiktoker"
            Start = new DateTime(2021, 9, 13, 13, 30, 00)
            End = new DateTime(2021, 9, 13, 17, 30, 00)
            Staff = Array.empty
        }
        {
            Name = "Priscilla queen of the desert"
            Start = new DateTime(2021, 1, 26, 13, 30, 00)
            End = new DateTime(2021, 1, 26, 17, 30, 00)
            Staff = Array.empty
        }
        {
            Name = "Oh, Coward!"
            Start = new DateTime(2021, 10, 17, 13, 30, 00)
            End = new DateTime(2021, 10, 17, 17, 30, 00)
            Staff = Array.empty
        }
    |]

    let staff: Person[] = [||]
    test <@ (CalculateIntialSchedule shifts staff) = shifts @>

[<Fact>]
let ``inverts shift correctly``() =
    let shifts: Shift[] = [|
        {
            Name = "Assassins"
            Start = new DateTime(2021, 3, 4, 18, 30, 00)
            End = new DateTime(2021, 3, 4, 21, 30, 00)
            Staff = [|
                {
                    Forename = "Nancey"
                    Surname = "Fahy"
                    UnavailableDates = Array.empty
                    Shifts = Array.empty
                }
                {
                    Forename = "Cheryl"
                    Surname = "Pentha"
                    UnavailableDates = Array.empty
                    Shifts = Array.empty
                }
            |]
        }
        {
            Name = "Dixie Swim Club"
            Start = new DateTime(2021, 2, 15, 18, 30, 00)
            End = new DateTime(2021, 2, 15, 21, 30, 00)
            Staff = [|
                {
                    Forename = "Cheryl"
                    Surname = "Pentha"
                    UnavailableDates = Array.empty
                    Shifts = Array.empty
                }
                {
                    Forename = "Britte"
                    Surname = "Lowery"
                    UnavailableDates = Array.empty
                    Shifts = Array.empty
                }
            |]
        }
    |]

    let expected = set [|
        {
            Forename = "Nancey"
            Surname = "Fahy"
            UnavailableDates = Array.empty
            Shifts = Array.empty
        }
        {
            Forename = "Cheryl"
            Surname = "Pentha"
            UnavailableDates = Array.empty
            Shifts = Array.empty
        }
        {
            Forename = "Britte"
            Surname = "Lowery"
            UnavailableDates = Array.empty
            Shifts = Array.empty
        }
    |]

    test <@ (invertShifts shifts) = expected @>