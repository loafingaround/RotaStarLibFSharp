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
        MinStaffCount = 0
    }

let dixie =
    {
        Name = "Dixie Swim Club"
        Start = new DateTime(2021, 2, 15, 18, 30, 00)
        End = new DateTime(2021, 2, 15, 21, 30, 00)
        Staff = Array.empty
        MinStaffCount = 0
    }

let priscilla =
    {
        Name = "Priscilla queen of the desert"
        Start = new DateTime(2021, 1, 26, 13, 30, 00)
        End = new DateTime(2021, 1, 26, 17, 30, 00)
        Staff = Array.empty
        MinStaffCount = 0
    }

let succeedInBusiness =
    {
        Name = "How To Succeed in Business Without Really Trying"
        Start = new DateTime(2021, 4, 30, 14, 30, 00)
        End = new DateTime(2021, 4, 30, 16, 00, 00)
        Staff = Array.empty
        MinStaffCount = 0
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