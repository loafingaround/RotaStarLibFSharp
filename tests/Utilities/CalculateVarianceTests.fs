module CalculateVarianceTests

open Xunit
open Swensen.Unquote
open Utilities
open Scheduling.Types
open Common

// TODO: pass into calculateVariance function returning constant mean value

[<Fact>]
let ``calculates for single shift single staff member correctly``() =
    let shifts = [|
        { assassins with Staff = [| nancey |] }
    |]

    test <@ (calculateVariance calculateMeanShiftsPerStaffMember shifts) = 0.0 @>

[<Fact>]
let ``calculates for single shift two staff correctly``() =
    let shifts = [|
        { assassins with Staff =
                            [|
                                nancey
                                cheryl
                            |] }
    |]

    test <@ (calculateVariance calculateMeanShiftsPerStaffMember shifts) = 0.0 @>

[<Fact>]
let ``calculates for two shifts single staff member correctly``() =
    let shifts = [|
        { assassins with Staff = [| nancey |] }
        { dixie with Staff = [| nancey |] }
    |]

    test <@ (calculateVariance calculateMeanShiftsPerStaffMember shifts) = 0.0 @>

[<Fact>]
let ``calculates for two shifts with staff overlap correctly``() =
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

    test <@ (calculateVariance calculateMeanShiftsPerStaffMember shifts) = 2.0/9.0 @>

[<Fact>]
let ``calculates for three shifts with staff overlap correctly``() =
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

    test <@ (calculateVariance calculateMeanShiftsPerStaffMember shifts) = 2.0/3.0 @>

[<Fact>]
let ``calculates for four shifts with very unfair staffing correctly``() =
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
                                cheryl
                            |]
        }
        {
            priscilla with Staff =
                            [|
                                cheryl
                            |]
        }
        {
            succeedInBusiness with Staff =
                                    [|
                                        cheryl
                                    |]
        }
    |]

    test <@ (calculateVariance calculateMeanShiftsPerStaffMember shifts) = 2.25 @>