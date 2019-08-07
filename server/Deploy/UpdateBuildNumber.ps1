function Main()
{
	Param($sourceDirectory, $BuildNumber, $CommitId)
	Write-Host $sourceDirectory
	Write-Host $BuildNumber
	Write-Host $CommitId
	$GlobalVersionInfo = "$sourceDirectory/PackageInfo/CommonVersionInfo.props"
	Write-Host $GlobalVersionInfo
	$AssemblyVersionMatch = '<Version>([0-9.]+)</Version>'
	$AssemblyFileVersionMatch = '<FileVersion>([0-9.]+)</FileVersion>'
	$ApplicationVersionMatch = '<ApplicationVersion>([0-9.]+)</ApplicationVersion>'
	$ApplicationRevisionMatch = '<ApplicationRevision>([0-9.]*)</ApplicationRevision>'
	$Revision = $BuildNumber
	 
	$VersionSuffix = $CommitId.SubString(0, 8)
	$VersionPrefix = GetCurrentVersionPrefix
	$NewVersion = "$VersionPrefix.$Revision"
	Write-Output $("New Version: " + $NewVersion)
	Write-Output $("Suffix: " + $VersionSuffix)

	Set-ItemProperty $GlobalVersionInfo -name IsReadOnly -value $false
	   
	# Update the global info
	ReplaceValues $GlobalVersionInfo $Revision $NewVersion $VersionSuffix
}
   
function ReplaceValues($filename, $revision, $version, $suffix)
{
	[xml] $xml = (Select-Xml -Path $filename -XPath /).Node
	$node = $xml.SelectSingleNode("//PropertyGroup")
	$node.RemoveAll()

	$node.AppendChild($xml.CreateTextNode("`n`t`t"))

	$versionChild = $xml.CreateElement("Version")
	$versionChild.InnerText = "$version-$suffix"
	$node.AppendChild($versionChild)

	$node.AppendChild($xml.CreateTextNode("`n`t`t"))

	$fileVersion = $xml.CreateElement("FileVersion")
	$fileVersion.InnerText = $version
	$node.AppendChild($fileVersion)

	$node.AppendChild($xml.CreateTextNode("`n`t`t"))

	$applicationVersion = $xml.CreateElement("ApplicationVersion")
	$applicationVersion.InnerText = $version
	$node.AppendChild($applicationVersion)

	$node.AppendChild($xml.CreateTextNode("`n`t`t"))

	$applicationRevision = $xml.CreateElement("ApplicationRevision")
	$applicationRevision.InnerText = $revision
	$node.AppendChild($applicationRevision)

	$node.AppendChild($xml.CreateTextNode("`n`t"))

	$xml.Save($filename)
}
   
function GetCurrentVersionPrefix()
{
	$version = GetCurrentVersion
	return $version -replace '([0-9]+[.][0-9]+[.][0-9]+)[.][0-9]+', '$1' # if version is 3.8.0.270, returns 3.8.0
}
   
function GetCurrentVersion()
{
	return Get-Content $GlobalVersionInfo -Raw -Encoding UTF8 | % { [regex]::matches($_, $AssemblyVersionMatch) } | %{ $_.Groups[1].Value }
}
   
Main -sourceDirectory $args[0] -BuildNumber $args[1] -CommitId $args[2]