namespace rec Scheduling

open System

module Types =
    
    type DateRange = {
        Start: DateTime
        End: DateTime
    }

    type StaffMember = {
        Id: int
        Forename: string
        Surname: string
        UnavailableDates: DateRange[]
        Shifts: Shift[]
    }
    
    type Shift = {
        Id: int
        Name: string
        // TODO: use DateRange
        Start: DateTime
        End: DateTime
        Staff: StaffMember[]
        MinimumNumberOfStaff: int
        MaximumNumberOfStaff: int
    }

    type ShiftRequirement =
        | MinimumNumberOfStaff of required: int * actual: int
        | MinimumNumberOfFirstAiders of reqired: int * actual: int
