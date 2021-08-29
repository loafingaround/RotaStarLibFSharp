module Utilities

open System
open System.IO
open Scheduling.Types
open System.Collections.Generic

// convenience function for clients of library
let rand = Random();
let nextRandom maxExcl =
    rand.Next(0, maxExcl)

let invertShifts shifts =
    shifts
    |> Array.collect (fun s -> s.Staff |> Array.map (fun sm -> sm, { s with Staff = Array.empty }))
    |> Array.groupBy fst
    |> Array.map (fun g -> { fst g with Shifts = snd g |> Array.map (fun ss -> snd ss) })

let calculateMeanShiftsPerStaffMember shifts =
    let shiftStaff =
        shifts
        |> Array.collect (fun s -> s.Staff)
    let uniqueStaff = Array.distinct shiftStaff
    (float) shiftStaff.Length / (float) uniqueStaff.Length

let calculateVariance (calculateMeanShiftsPerStaffMember: Shift[] -> float) shifts =
    let mean = (float) (calculateMeanShiftsPerStaffMember shifts)
    let shiftCounts =
        shifts
        |> Array.collect (fun s -> s.Staff)
        |> Array.groupBy id
        |> Array.map (fun g -> (float) (snd g).Length)

    let total =
        shiftCounts
        |> Array.sumBy (fun c -> (c - mean) ** 2.0)

    total / (float) shiftCounts.Length

let shiftsToCsv shifts =
    let staff = Dictionary<int, string>()
    let shiftLines = Dictionary<string, string>()

    for s in shifts do
        for sm in s.Staff do
            if not (staff.ContainsKey(sm.Id)) then
                let keys = Array.zeroCreate(shiftLines.Count)
                shiftLines.Keys.CopyTo(keys, 0)
                for k in keys do
                    shiftLines.[k] <- shiftLines.[k] + ","
                staff.Add(sm.Id, sprintf "%s %s" sm.Forename sm.Surname)
        let mutable line = ""
        let staffIds = s.Staff |> Array.map (fun sm -> sm.Id)
        for kvp in staff do
            line <- line + ","
            if Array.contains kvp.Key staffIds then line <- line + "X"
        shiftLines.Add(s.Name, line)
    let mutable csv = ""
    for kvp in staff do csv <- csv + "," + kvp.Value
    for kvp in shiftLines do
        csv <- csv + "\n" + kvp.Key + kvp.Value
    csv

let shiftsToCsvFile shifts filePath =
    let csv = shiftsToCsv shifts
    File.WriteAllText(filePath, csv)