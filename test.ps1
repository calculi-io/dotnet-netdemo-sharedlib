# builds in debug, runs tests and generates reports
$testResultsDir = ".\TestResults"
$VSINSTALLDIR_2017 = "\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise"

if ($null -ne $env:VSINSTALLDIR_2017) {
	$VSINSTALLDIR_2017 = $env:VSINSTALLDIR_2017
}

MSBuild.exe /restore /t:Rebuild /p:Configuration=debug

$proc = Start-Process "$VSINSTALLDIR_2017\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe" -ArgumentList "/EnableCodeCoverage SharedLib\bin\Debug\SharedLib.dll /Logger:trx" -PassThru -NoNewWindow
$proc.WaitForExit()
"Exit code: {0}", $proc.ExitCode

$i = 0
$reportFiles = Get-ChildItem -Path TestResults *.coverage -Recurse | %{$_.FullName}
Foreach ($f in $reportFiles) {
	if ($f.Contains("\In\")) {
		$reportFiles = $reportFiles -ne $f
	}
}
Foreach ($f in $reportFiles) {
	Write-Output "Found: $f"
	$destName = "VisualStudio"
	$ext = "coveragexml"
	$cname = "coverage"
	if ($i -gt 0) {
		$destName += "_$i"
		$cname += "_$i"
	}
	Copy-Item -Path $f -destination "$testResultsDir\$destName.$ext"
	Write-Output "copying to $testResultsDir\$destName.$ext"
	$proc = Start-Process "$VSINSTALLDIR_2017\Team Tools\Dynamic Code Coverage Tools\CodeCoverage.exe" -ArgumentList "analyze /output:$testResultsDir\$cname.xml $testResultsDir\$destName.$ext" -PassThru -NoNewWindow
	$proc.WaitForExit()
	$i = $i + 1
}
