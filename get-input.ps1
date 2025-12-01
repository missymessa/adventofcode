param(
    [Parameter(Mandatory=$true)]
    [int]$Day,
    
    [Parameter(Mandatory=$true)]
    [int]$Year,
    
    [Parameter(Mandatory=$true)]
    [string]$SessionCookie
)

$url = "https://adventofcode.com/$Year/day/$Day/input"
$outputPath = "adventofcode$Year\input\day_$($Day.ToString('00')).txt"

try {
    Write-Host "Fetching input for Year $Year, Day $Day..."
    
    # Create directory if it doesn't exist
    $outputDir = Split-Path -Parent $outputPath
    if (!(Test-Path $outputDir)) {
        New-Item -ItemType Directory -Path $outputDir -Force | Out-Null
    }
    
    $session = New-Object Microsoft.PowerShell.Commands.WebRequestSession
    $cookie = New-Object System.Net.Cookie
    $cookie.Name = "session"
    $cookie.Value = $SessionCookie
    $cookie.Domain = ".adventofcode.com"
    $session.Cookies.Add($cookie)
    
    $response = Invoke-WebRequest -Uri $url -WebSession $session -UseBasicParsing
    
    if ($response.StatusCode -eq 200) {
        $response.Content | Out-File -FilePath $outputPath -Encoding utf8 -NoNewline
        Write-Host "Input saved to $outputPath" -ForegroundColor Green
    }
}
catch {
    Write-Host "Error: $($_.Exception.Message)" -ForegroundColor Red
    Write-Host "Make sure your session cookie is valid and the puzzle is available." -ForegroundColor Yellow
}
