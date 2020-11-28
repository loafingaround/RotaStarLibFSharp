namespace Scheduling

module Types =
    
    type Person = {
        Forename: string
        Surname: string
    }

    type Shift = {
        Name: string
        Staff: Person[]
    }