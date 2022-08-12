$Projects = Get-ChildItem -Path src/**/*.csproj -Recurse
$ErrorFound = $false
foreach ($Project in $Projects) {
  dotnet list $Project package --deprecated --include-transitive | Tee-Object -Variable Deprecated
  dotnet list $Project package --vulnerable --include-transitive | Tee-Object -Variable Vulnerable
  $Errors = $Deprecated + $Vulnerable | Select-String '>'
  if ($Errors.Count -gt 0) {
    Write-Host "##vso[task.logissue type=warning]Package issues discovered, review output above"
    $ErrorFound = $true
  }
}
$ErrorFound ? $(exit 1) : $(exit 0)