Param([ValidateSet("Shift", "Person")] $type, [int] $number_to_generate)

Enum TypeToGenerate
{
    Shift
    Person
}

$typeToGenerate = [TypeToGenerate]$type

$r = New-Object System.Random

$time_ranges = @(10, 00, 12, 00), @(13, 30, 17, 30), @(18, 30, 21, 30), @(18, 30, 0, 0)
$time_ranges_max_ix = $time_ranges.Length - 1

$first_names_count = 4945
$last_names_count = 21985
$plays_and_musicals = 342

function generateShifts()
{
    $objCode = ""

    foreach ($i in 1..$number_to_generate) {
        $yr = $r.Next(2021, 2022)
        $mon = $r.Next(1, 12)
        $day = $r.Next(1, 28)
        $time_range = $time_ranges[$r.Next(1, $time_ranges_max_ix)]
        
        $name = getRandomEntry "plays_and_musicals.txt" $plays_and_musicals

        $objCode += "        {
                Name = ""$name""
                Start = new DateTime($yr, $mon, $day, $($time_range[0]), $($time_range[1]), 00)
                End = new DateTime($yr, $mon, $day, $($time_range[2]), $($time_range[3]), 00)
                Staff = Array.empty
            }`n"
    }

    $objCode
}

function generatePersons()
{
    $objCode = ""

    foreach ($i in 1..$number_to_generate) {
        $first_name = getRandomEntry "first-names.txt" $first_names_count
        $last_name = getRandomEntry "last-names.txt" $last_names_count

        $objCode += "        {
            Forename = ""$first_name""
            Surname = ""$last_name""
            UnavailableDates = Array.empty
            Shifts = Array.empty
        }`n"
    }

    $objCode
}

function getRandomEntry($fileName, $totalEntriesCount)
{
    $tc = $r.Next(1, $totalEntriesCount)
    $entries = Get-Content $fileName -TotalCount $tc
    return $entries[$tc - 1]
}

if ($typeToGenerate -eq [TypeToGenerate]::Shift)
{
    generateShifts
}
else
{
    generatePersons
}