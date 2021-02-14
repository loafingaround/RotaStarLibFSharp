module calculateVarianceTests

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

    test <@ (calculateVariance shifts) = 0M @>

[<Fact>]
let ``calculates for single shift two staff correctly``() =
    let shifts = [|
        { assassins with Staff =
                            [|
                                nancey
                                cheryl
                            |] }
    |]

    test <@ (calculateVariance shifts) = 0M @>

[<Fact>]
let ``calculates for two shifts single staff member correctly``() =
    let shifts = [|
        { assassins with Staff = [| nancey |] }
        { dixie with Staff = [| nancey |] }
    |]

    test <@ (calculateVariance shifts) = 0M @>

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

    test <@ (calculateVariance shifts) = 2M/9M @>

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

    test <@ (calculateVariance shifts) = 2M/3M @>