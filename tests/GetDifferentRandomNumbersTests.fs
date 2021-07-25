module GetDifferentRandomNumbersTests

open Xunit
open Swensen.Unquote
open Scheduling.Scheduler
open Common

[<Fact>]
let ``numbers both zero when exclusive max is 0``() =
    let maxExcl = 0
    let first, second = getDifferentRandomNumbers (getNextRandomFunc [||]) maxExcl
    test <@ first = 0 && second = 0 @>

[<Fact>]
let ``numbers both zero when exclusive max is 1``() =
    let maxExcl = 1
    let first, second = getDifferentRandomNumbers (getNextRandomFunc [||]) maxExcl
    test <@ first = 0 && second = 0 @>

[<Fact>]
let ``numbers are returned successfully``() =
    let randoms = [|1; 2;|]
    let first, second = getDifferentRandomNumbers (getNextRandomFunc randoms) 2
    test <@ first = 1 && second = 2 @>

[<Fact>]
let ``numbers are different``() =
    let randoms = [|1; 1; 1; 2|]
    let first, second = getDifferentRandomNumbers (getNextRandomFunc randoms) 2
    test <@ first = 1 && second = 2 @>
