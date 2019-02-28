git checkout .
git clean -f

foreach ($dir in Get-ChildItem -Path .\ -Include bin, obj -Directory -Recurse) {
    $path = $dir.FullName
    Write-Host $path

    Remove-Item -Path "$path" -Recurse -Force -Filter *
}

$project = ".\Workshop.App\"
$settingsFile = $project + "Properties\launchSettings.json"
if (Test-Path $settingsFile) {
    $json = Get-Content $settingsFile -Raw | ConvertFrom-Json
    $json.profiles.'Workshop.App'.commandLineArgs = ""
    $json | ConvertTo-Json -Depth 50 -Compress | Set-Content $settingsFile
}

dotnet build