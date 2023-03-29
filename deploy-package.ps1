$secret = Read-Host "Enter your Nuget API Key: " -AsSecureString
dotnet nuget push artifacts\Release\*.nupkg --source https://api.nuget.org/v3/index.json --api-key $secret
$secret = ""