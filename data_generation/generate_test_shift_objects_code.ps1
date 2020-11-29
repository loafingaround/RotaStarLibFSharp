Param([int] $number_to_generate)

$r = New-Object System.Random

$time_ranges = @(10, 00, 12, 00), @(13, 30, 17, 30), @(18, 30, 21, 30), @(18, 30, 0, 0)
$time_ranges_max_ix = $time_ranges.Length - 1

$first_names_count = 4945
$last_names_count = 21985
$plays_and_musicals = 342

$objCode = ""

foreach ($i in 1..$number_to_generate) {
    $yr = $r.Next(2021, 2022)
    $mon = $r.Next(1, 12)
    $day = $r.Next(1, 28)
    $time_range = $time_ranges[$r.Next(1, $time_ranges_max_ix)]

    #$start = New-Object System.DateTime 
    #$end = New-Object System.DateTime $yr, $mon, $day, $time_range[2], $time_range[3]
    
    $name = getRandomEntry "plays_and_musicals.txt" 342

    $objCode += "        {
            Name = ""$name""
            Start = new DateTime($yr, $mon, $day, $($time_range[0]), $($time_range[1]), 00)
            End = new DateTime($yr, $mon, $day, $($time_range[2]), $($time_range[3]), 00)
            Staff = Array.empty
        }`n"
}

$objCode

function getRandomEntry($fileName, $totalEntriesCount) {
    $tc = $r.Next(1, $totalEntriesCount)
    $entries = Get-Content $fileName -TotalCount $tc
    return $entries[$tc - 1]
}