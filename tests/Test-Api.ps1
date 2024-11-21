cd $PSScriptRoot/..
docker-compose up --detach
Start-Sleep -Seconds 5 # do better
$Body = @"
{
    "title": "test1",
    "description": "test2",
    "enabled": true
}
"@

try {
  Invoke-RestMethod -Method POST -Uri "http://127.0.0.1:80/" -Headers @{ "Content-Type"="application/json" } -Body $Body
}
Catch {
  if($_.ErrorDetails.Message) {
    #WebResponseError
    Write-Host $_.ErrorDetails.Message;
  } else {
    #UsualException
    $_
  }
}

$TestResponse = Invoke-RestMethod -Method GET -Uri "http://127.0.0.1:80/"
Write-Host $TestResponse
$Assert = Compare-Object $TestResponse ($Body | ConvertFrom-Json)
if($null -ne $Assert){
    throw "Test assertion failed"
}