module CalculateMeanShiftsPerStaffMemberTests

open Xunit
open Swensen.Unquote
open Utilities
open Scheduling.Types
open Common

[<Fact>]
let ``Calculates for single shift single staff member correctly``() =
    let shifts = [|
        { assassins with Staff = [| nancey |] }
    |]

    test <@ (calculateMeanShiftsPerStaffMember shifts) = 1.0 @>

[<Fact>]
let ``Calculates for single shift two staff correctly``() =
    let shifts = [|
        { assassins with Staff =
                            [|
                                nancey
                                cheryl
                            |] }
    |]

    test <@ (calculateMeanShiftsPerStaffMember shifts) = 1.0 @>

[<Fact>]
let ``Calculates for two shifts single staff member correctly``() =
    let shifts = [|
        { assassins with Staff = [| nancey |] }
        { dixie with Staff = [| nancey |] }
    |]

    test <@ (calculateMeanShiftsPerStaffMember shifts) = 2.0 @>

[<Fact>]
let ``Calculates for two shifts with staff overlap correctly``() =
    let shifts = [|
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

    test <@ (calculateMeanShiftsPerStaffMember shifts) = 4.0/3.0 @>

[<Fact>]
let ``Calculates for three shifts with staff overlap correctly``() =
    let shifts = [|
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

    test <@ (calculateMeanShiftsPerStaffMember shifts) = 2.0 @>
