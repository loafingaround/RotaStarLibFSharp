module Common

open System
open Scheduling.Types

// shifts

let assassins =
    {
        Id = 1
        Name = "Assassins"
        Start = new DateTime(2021, 3, 4, 18, 30, 00)
        End = new DateTime(2021, 3, 4, 21, 30, 00)
        Staff = Array.empty
        MinimumNumberOfStaff = 0
        MaximumNumberOfStaff = 0
    }

let dixie =
    {
        Id = 2
        Name = "Dixie Swim Club"
        Start = new DateTime(2021, 2, 15, 18, 30, 00)
        End = new DateTime(2021, 2, 15, 21, 30, 00)
        Staff = Array.empty
        MinimumNumberOfStaff = 0
        MaximumNumberOfStaff = 0
    }

let priscilla =
    {
        Id = 3
        Name = "Priscilla queen of the desert"
        Start = new DateTime(2021, 1, 26, 13, 30, 00)
        End = new DateTime(2021, 1, 26, 17, 30, 00)
        Staff = Array.empty
        MinimumNumberOfStaff = 0
        MaximumNumberOfStaff = 0
    }

let succeedInBusiness =
    {
        Id = 4
        Name = "How To Succeed in Business Without Really Trying"
        Start = new DateTime(2021, 4, 30, 14, 30, 00)
        End = new DateTime(2021, 4, 30, 16, 00, 00)
        Staff = Array.empty
        MinimumNumberOfStaff = 0
        MaximumNumberOfStaff = 0
    }

// staff

let nancey =
    {
        Id = 1
        Forename = "Nancey"
        Surname = "Fahy"
        UnavailableDates = Array.empty
        Shifts = Array.empty
    }

let cheryl =
    {
        Id = 2
        Forename = "Cheryl"
        Surname = "Pentha"
        UnavailableDates = Array.empty
        Shifts = Array.empty
    }

let britte =
    {
        Id = 3
        Forename = "Britte"
        Surname = "Lowery"
        UnavailableDates = Array.empty
        Shifts = Array.empty
    }