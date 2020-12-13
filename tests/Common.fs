module Common

open System
open Scheduling.Types

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
        Shifts = Set.empty
    }

let cheryl =
    {
        Forename = "Cheryl"
        Surname = "Pentha"
        UnavailableDates = Array.empty
        Shifts = Set.empty
    }

let britte =
    {
        Forename = "Britte"
        Surname = "Lowery"
        UnavailableDates = Array.empty
        Shifts = Set.empty
    }