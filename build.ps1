Push-Location
Get-ChildItem artifacts/*.nupkg -Recurse | foreach { Remove-Item -Path $_.FullName }
dotnet build -c Release