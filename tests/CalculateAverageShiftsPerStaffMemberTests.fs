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

    test <@ (calculateAverageShiftsPerStaffMember shifts) = 1M @>

[<Fact>]
let ``calculates for single shift two staff correctly``() =
    let shifts = [|
        { assassins with Staff =
                            [|
                                nancey
                                cheryl
                            |] }
    |]

    test <@ (calculateAverageShiftsPerStaffMember shifts) = 1M @>

[<Fact>]
let ``calculates for two shifts single staff member correctly``() =
    let shifts = [|
        { assassins with Staff = [| nancey |] }
        { dixie with Staff = [| nancey |] }
    |]

    test <@ (calculateAverageShiftsPerStaffMember shifts) = 2M @>

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

    test <@ (calculateAverageShiftsPerStaffMember shifts) = 4M/3M @>


[<Fact>]
let ``calculates for _ correctly``() =
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
        {
            priscilla with Staff =
                            [|
                                cheryl
                                britte
                            |]
        }
    |]

    test <@ (calculateAverageShiftsPerStaffMember shifts) = 2M @>