Install-Module -Name 'MSAL.PS'
$appsettings = Get-Content '.\LgpSampleApi\appsettings.json' | ConvertFrom-Json

$jwt = Get-MsalToken -TenantId $appsettings.AzureAd.TenantId -ClientId $appsettings.Swagger.ClientId -RedirectUri "https://localhost:49153/signin-oidc" -Scopes "api://$($appsettings.AzureAd.ClientId)/.default" 

Start-Process "https://jwt.ms/#access_token=$($jwt.AccessToken)"

$jwt.AccessToken | Set-Clipboard

# Invoke-RestMethod -Headers @{Authorization = "$($jwt.TokenType) $($jwt.AccessToken)" } -Uri  "https://localhost:49153/WeatherForecast" -Method Get
$hc = [System.Net.Http.HttpClient]::new()
$hc.DefaultRequestVersion = [System.Net.HttpVersion]::Version30
$hc.DefaultRequestHeaders.Add('Authorization', "$($jwt.TokenType) $($jwt.AccessToken)")
$r = $hc.GetAsync('https://localhost:49157/WeatherForecast').GetAwaiter().GetResult()
$r
Write-Host 'Headers'
$r.Content.Headers
Write-Host 'Content'
$r.Content.ReadAsStringAsync().GetAwaiter().GetResult()