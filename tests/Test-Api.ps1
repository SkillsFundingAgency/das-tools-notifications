cd $PSScriptRoot/..
docker-compose up --detach

# Retry mechanism with increased timeout
$maxRetries = 20 # Increase retries
$retry = 0
while ($retry -lt $maxRetries) {
    try {
        Invoke-RestMethod -Method GET -Uri "http://localhost:8000/health"
        break
    } catch {
        Start-Sleep -Seconds 5 # Adjust delay between retries
        $retry++
        if ($retry -eq $maxRetries) {
            docker-compose logs web # Print logs for debugging
            throw "Service did not start in time"
        }
    }
}

$Body = @"
{
    "title": "test1",
    "description": "test2",
    "enabled": true
}
"@
Invoke-RestMethod -Method POST -Uri "http://localhost:8000/" -Headers @{ "Content-Type" = "application/json" } -Body $Body
$TestResponse = Invoke-RestMethod -Method GET -Uri "http://localhost:8000/"
Write-Host $TestResponse
$Assert = Compare-Object $TestResponse ($Body | ConvertFrom-Json)
if ($null -ne $Assert) {
    throw "Test assertion failed"
}
