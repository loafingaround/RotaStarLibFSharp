module GetDifferentRandomNumbersTests

open Xunit
open Swensen.Unquote
open Scheduling.Scheduler
open Common

[<Fact>]
let ``numbers both 0 when exclusive max is 0``() =
    let maxExcl = 0
    let first, second = getDifferentRandomNumbers (getNextRandomFunc [||]) maxExcl
    test <@ first = 0 && second = 0 @>

[<Fact>]
let ``numbers both 0 when exclusive max is 1``() =
    let maxExcl = 1
    let first, second = getDifferentRandomNumbers (getNextRandomFunc [||]) maxExcl
    test <@ first = 0 && second = 0 @>

[<Fact>]
let ``numbers are 0 and 1 when exclusive max is 2``() =
    let maxExcl = 2
    let first, second = getDifferentRandomNumbers (getNextRandomFunc [||]) maxExcl
    test <@ first = 0 && second = 1 @>

[<Fact>]
let ``numbers from random number func are returned successfully``() =
    let randoms = [|1; 2; 3|]
    let first, second = getDifferentRandomNumbers (getNextRandomFunc randoms) 3
    test <@ first = 1 && second = 2 @>

[<Fact>]
let ``numbers are different when random number func produces same value twice``() =
    let randoms = [|1; 1; 2|]
    let first, second = getDifferentRandomNumbers (getNextRandomFunc randoms) 3
    test <@ first = 1 && second = 2 @>

[<Fact>]
let ``numbers are different when random number func produces same value thrice``() =
    let randoms = [|1; 1; 1; 2|]
    let first, second = getDifferentRandomNumbers (getNextRandomFunc randoms) 4
    test <@ first = 1 && second = 2 @>
