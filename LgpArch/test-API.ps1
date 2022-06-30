Install-Module -Name 'MSAL.PS'
$appsettings = Get-Content '.\LgpSampleApi\appsettings.json' | ConvertFrom-Json

$jwt = Get-MsalToken -TenantId $appsettings.AzureAd.TenantId -ClientId $appsettings.Swagger.ClientId -RedirectUri "https://localhost:49153/signin-oidc" -Scopes "api://$($appsettings.AzureAd.ClientId)/.default" 

Start-Process "https://jwt.ms/#access_token=$($jwt.AccessToken)"

$jwt.AccessToken | Set-Clipboard

Invoke-RestMethod -Headers @{Authorization = "$($jwt.TokenType) $($jwt.AccessToken)" } -Uri  "https://localhost:49153/WeatherForecast" -Method Get