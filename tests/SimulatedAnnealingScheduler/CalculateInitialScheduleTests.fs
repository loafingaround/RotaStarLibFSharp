module CalculateInitialScheduleTests

open System
open Xunit
open Swensen.Unquote
open Scheduling.SimulatedAnnealingScheduler
open Scheduling.Types
open Common

let isStaffValid availableStaff expectedStaffCount shift =
    shift.Staff.Length = expectedStaffCount
    && Array.forall
        (fun (sm: StaffMember) -> Array.exists (fun (asm: StaffMember) -> sm.Id = asm.Id) availableStaff)
        shift.Staff

let getIds items =
    Array.map (fun s -> s.Id) items

[<Fact>]
let ``Calculates expected schedule for 0 shifts with 1 available staff member``() =
    let shifts: Shift[] = Array.empty

    let staff = [|
        nancey
    |]

    let expected: Shift[] = Array.empty

    test <@ calculateInitialSchedule shifts staff = expected @>

[<Fact>]
let ``Calculates expected schedule for 1 shift with max 1 staff and 1 available staff member``() =
    let shift = { succeedInBusiness with MaximumNumberOfStaff = 1 }
    let shifts = [|
        shift
    |]

    let staff = [|
        nancey
    |]

    let expected = [|
        { shift with Staff = [|nancey|] }
    |]
    
    test <@ calculateInitialSchedule shifts staff = expected @>

[<Fact>]
let ``Calculates expected schedule for 1 shift with max 2 staff and 1 available staff member``() =
    let shift = { succeedInBusiness with MaximumNumberOfStaff = 2 }
    let shifts = [|
        shift
    |]

    let staff = [|
        nancey
    |]

    let expected = [|
        { shift with Staff = [|nancey|] }
    |]
    
    test <@ calculateInitialSchedule shifts staff = expected @>

[<Fact>]
let ``Calculates a valid schedule for 1 shift max 2 staff and 2 available staff members``() =
    let shift = { succeedInBusiness with MaximumNumberOfStaff = 2 }
    let shifts = [|
        shift
    |]

    let staff = [|
        britte
        nancey
    |]

    let actual = calculateInitialSchedule shifts staff

    let expectedStaffCount = 2
    
    test <@ Set (getIds actual) = Set (getIds shifts) @>

    let actualShift = actual.[0]
    
    test <@ isStaffValid staff expectedStaffCount actualShift @>

[<Fact>]
let ``Calculates a valid schedule for 1 shift with max 2 staff and 3 available staff members``() =
    let shift = { succeedInBusiness with MaximumNumberOfStaff = 2 }
    let shifts = [|
        shift
    |]

    let staff = [|
        cheryl
        nancey
        britte
    |]

    let actual = calculateInitialSchedule shifts staff

    let expectedStaffCount = 2
    
    test <@ Set (getIds actual) = Set (getIds shifts) @>

    let actualShift = actual.[0]
    
    test <@ isStaffValid staff expectedStaffCount actualShift @>

[<Fact>]
let ``Calculates a valid schedule for 1 shift max 2 staff members, 1 shift with max 1 staff members and 3 available staff members``() =
    let shift1 = { succeedInBusiness with MaximumNumberOfStaff = 2 }
    let shift2 = { assassins with MaximumNumberOfStaff = 1 }
    let shifts = [|
        shift2
        shift1
    |]

    let staff = [|
        britte
        cheryl
        nancey
    |]
    
    let actual = calculateInitialSchedule shifts staff

    let expectedStaffCountShift1 = 2
    let expectedStaffCountShift2 = 1

    test <@ Set (getIds actual) = Set (getIds shifts) @>

    let actualShift1 = Array.find (fun s -> s.Id = shift1.Id) actual
    let actualShift2 = Array.find (fun s -> s.Id = shift2.Id) actual

    test <@ isStaffValid staff expectedStaffCountShift1 actualShift1 @>
    test <@ isStaffValid staff expectedStaffCountShift2 actualShift2 @>

[<Fact>]
let ``Calculates a valid schedule for 20 shifts different maximum staff members and 16 staff members``() =
    let numbers = [|0..19|]

    let shifts = [|
        for i in numbers ->
        {
            Id = i
            Name = "Event " + i.ToString()
            Start = new DateTime(2021, 3, 4, 18, 30, 00)
            End = new DateTime(2021, 3, 4, 21, 30, 00)
            Staff = Array.empty
            MinimumNumberOfStaff = 0
            MaximumNumberOfStaff = i
        }
    |]

    let staff = [|
        for i in 0..16 ->
        {
            Id = i
            Forename = "Forename " + i.ToString()
            Surname = "Surname " + i.ToString()
            UnavailableDates = Array.empty
            Shifts = Array.empty
        }
    |]
    
    let actual = calculateInitialSchedule shifts staff

    test <@ Set (getIds actual) = Set (getIds shifts) @>

    for i in numbers do
        let actualShift = Array.find (fun s -> s.Id = i) actual
        test <@ isStaffValid staff (Math.Min(i, staff.Length)) actualShift @>
