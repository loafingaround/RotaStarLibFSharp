﻿namespace rec Scheduling

open System

module Types =
    
    type Person = {
        Forename: string
        Surname: string
        UnavailableDates: DateTime[]
        Shifts: Shift[]
    }
    
    type Shift = {
        Name: string
        Start: DateTime
        End: DateTime
        Staff: Person[]
    }
