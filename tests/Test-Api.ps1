cd $PSScriptRoot/..
docker-compose up --detach
Start-Sleep -Seconds 15 # do better
$Body = @"
{
    "title": "test1",
    "description": "test2",
    "enabled": true
}
"@
Invoke-RestMethod -Method POST -Uri "http://localhost:8000/" -Headers @{ "Content-Type"="application/json" } -Body $Body
$TestResponse = Invoke-RestMethod -Method GET -Uri "http://localhost:8000/"
Write-Host $TestResponse
$Assert = Compare-Object $TestResponse ($Body | ConvertFrom-Json)
if($null -ne $Assert){
    throw "Test assertion failed"
}