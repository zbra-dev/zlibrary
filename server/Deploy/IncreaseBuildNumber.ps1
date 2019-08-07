Param($Pat)

$project = "$env:SYSTEM_TEAMPROJECT"
$projecturi = "$env:SYSTEM_TEAMFOUNDATIONCOLLECTIONURI"
$definitionId = "$env:SYSTEM_DEFINITIONID"
$token = $Pat
$base64AuthInfo = [Convert]::ToBase64String([Text.Encoding]::ASCII.GetBytes(("{0}:{1}" -f "", $token)))
$headers = @{Authorization = ("Basic {0}" -f $base64AuthInfo)}  
$defurl = $projecturi + "DefaultCollection/" + $project + "/_apis/build/definitions/" + $definitionid + "?api-version=5.1-preview.7"
$definition = Invoke-RestMethod -Uri $defurl -headers $headers -Method Get
$buildNumber = ([int]$definition.variables.BuildNumber.value) + 1
$definition.variables.BuildNumber.value= [string]$buildNumber 
$json = @($definition) | ConvertTo-Json  -Depth 100 -Compress
Invoke-RestMethod -Uri $defurl -headers $headers -Method Put -Body ([System.Text.Encoding]::UTF8.GetBytes($json)) -ContentType "application/json" 
Write-Host "##vso[task.setvariable variable=BuildNumber;]$buildNumber"