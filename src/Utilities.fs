module Utilities

open System
open System.IO
open Scheduling.Types
open System.Collections.Generic

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