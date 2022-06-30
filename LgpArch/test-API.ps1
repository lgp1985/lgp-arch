Install-Module -Name 'MSAL.PS'
$appsettings = Get-Content '.\LgpArch\LgpSampleApi\appsettings.json' | ConvertFrom-Json

$jwt = Get-MsalToken -TenantId $appsettings.AzureAd.TenantId -ClientId '447a6a31-04e2-410d-a5ce-3a9d227fdba9' -RedirectUri "https://localhost:49153/signin-oidc" -Scopes "api://$($appsettings.AzureAd.ClientId)/.default" 

Start-Process "https://jwt.ms/#access_token=$($jwt.AccessToken)"

Invoke-RestMethod -Headers @{Authorization = "$($jwt.TokenType) $($jwt.AccessToken)" } -Uri  "https://localhost:49153/WeatherForecast" -Method Get