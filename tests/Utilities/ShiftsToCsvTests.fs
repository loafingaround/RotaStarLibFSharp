module ShiftsToCsvTests

open Xunit
open Swensen.Unquote
open Utilities
open Scheduling.Types
open Common

[<Fact>]
let ``converts no shifts correctly``() =
    let shifts = Array.empty<Shift>
    
    test <@ (shiftsToCsv shifts) = "" @>

[<Fact>]
let ``converts single shift no staff correctly``() =
    let shifts = [|
        { priscilla with Staff = Array.empty }
    |]

    test <@ (shiftsToCsv shifts) = "\r\n" +
            "Priscilla queen of the desert" @>

[<Fact>]
let ``converts single shift single staff member correctly``() =
    let shifts = [|
        { priscilla with Staff = [| nancey |] }
    |]

    test <@ (shiftsToCsv shifts) = ",Nancey Fahy\r\n" +
            "Priscilla queen of the desert,X" @>

[<Fact>]
let ``converts single shift two staff correctly``() =
    let shifts = [|
        { priscilla with Staff =
                            [|
                                nancey
                                cheryl
                            |] }
    |]

    test <@ (shiftsToCsv shifts) = ",Nancey Fahy,Cheryl Pentha\r\n" +
            "Priscilla queen of the desert,X,X" @>

[<Fact>]
let ``converts two shifts single staff member for 2nd shift correctly``() =
    let shifts = [|
        { priscilla with Staff = [| |] }
        { dixie with Staff = [| nancey |] }
    |]

    test <@ (shiftsToCsv shifts) = ",Nancey Fahy\r\n" +
            "Priscilla queen of the desert,\r\n" +
            "Dixie Swim Club,X" @>

[<Fact>]
let ``converts two shifts single staff member for both shifts correctly``() =
    let shifts = [|
        { priscilla with Staff = [| nancey |] }
        { dixie with Staff = [| nancey |] }
    |]

    test <@ (shiftsToCsv shifts) = ",Nancey Fahy\r\n" +
            "Priscilla queen of the desert,X\r\n" +
            "Dixie Swim Club,X" @>

[<Fact>]
let ``converts two shifts two staff with one shift each correctly``() =
    let shifts = [|
        { priscilla with Staff = [| britte |] }
        { dixie with Staff = [| nancey |] }
    |]

    test <@ (shiftsToCsv shifts) = ",Britte Lowery,Nancey Fahy\r\n" +
            "Priscilla queen of the desert,X,\r\n" +
            "Dixie Swim Club,,X" @>

[<Fact>]
let ``converts three shifts with staff overlap correctly``() =
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
                                nancey
                                britte
                            |]
        }
    |]

    test <@ (shiftsToCsv shifts) = ",Nancey Fahy,Cheryl Pentha,Britte Lowery\r\n" +
            "Assassins,X,X,\r\n" +
            "Dixie Swim Club,,X,X\r\n" +
            "Priscilla queen of the desert,X,,X" @>