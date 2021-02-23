namespace rec Scheduling

open System

module Types =
    
    type DateRange = {
        Start: DateTime
        End: DateTime
    }

    type Person = {
        Forename: string
        Surname: string
        UnavailableDates: DateRange[]
        Shifts: Shift[]
    }
    
    type Shift = {
        Name: string
        // TODO: use DateRange
        Start: DateTime
        End: DateTime
        Staff: Person[]
        MinStaffCount: int
    }
