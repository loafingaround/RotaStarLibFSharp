module CalculateAverageShiftsPerStaffMemberTests

open Xunit
open Swensen.Unquote
open Scheduling.Scheduler
open Scheduling.Types
open Common

[<Fact>]
let ``calculates for single shift single staff member correctly``() =
    let shifts = [|
        { assassins with Staff = [| nancey |] }
    |]

    test <@ (calculateAverageShiftsPerStaffMember shifts) = 1 @>

[<Fact>]
let ``calculates for single shift two staff correctly``() =
    let shifts = [|
        { assassins with Staff =
                            [|
                                nancey
                                cheryl
                            |] }
    |]

    test <@ (calculateAverageShiftsPerStaffMember shifts) = 1 @>

[<Fact>]
let ``calculates for two shifts single staff member correctly``() =
    let shifts = [|
        { assassins with Staff = [| nancey |] }
        { dixie with Staff = [| nancey |] }
    |]

    test <@ (calculateAverageShiftsPerStaffMember shifts) = 2 @>

[<Fact>]
let ``calculates for two shifts with staff overlap correctly``() =
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
            nancey with Shifts = set [| assassins |]
        }
        {
            cheryl with Shifts =
                        set [|
                            assassins
                            dixie
                        |]
        }
        {
            britte with Shifts = set [| dixie |]
        }
    |]

    test <@ (calculateAverageShiftsPerStaffMember shifts) = 4/3 @>