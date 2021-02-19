$ver = "1.0.10"
if ($null -ne $env:VERSION_FOR_NUGET) {
	$ver = $env:VERSION_FOR_NUGET
}

Write-Host "Using version $ver"

nuget sources add -name local -Source $env:TEMP
MSBuild.exe /restore /t:Rebuild /p:Configuration=Release
nuget pack .\SharedLib.csproj -Prop Configuration=Release
nuget add .\SharedLib.1.0.10.nupkg -source $env:TEMP
