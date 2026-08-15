param(
    [Parameter(Mandatory = $true)]
    [ValidateSet("SqlServer", "Postgres")]
    [string]$Provider,

    [Parameter(Mandatory = $true)]
    [string]$MigrationName,

    [string]$Project = (Join-Path $PSScriptRoot "..\..\WebApp.Infrastructure"),
    [string]$StartupProject = (Join-Path $PSScriptRoot "..\..\WebApp")
)

$contextMap = @{
    "SqlServer" = "SqlServerAppDbContext"
    "Postgres"  = "PostgresAppDbContext"
}

$outputDirMap = @{
    "SqlServer" = "Persistence/Migrations/SqlServer"
    "Postgres"  = "Persistence/Migrations/Postgres"
}

$context = $contextMap[$Provider]
$outputDir = $outputDirMap[$Provider]

Write-Host "Project resolved to: $Project" -ForegroundColor DarkGray
Write-Host "StartupProject resolved to: $StartupProject" -ForegroundColor DarkGray
Write-Host "Output dir: $outputDir" -ForegroundColor DarkGray

Write-Host "Creating migration '$MigrationName' for provider '$Provider' (context: $context)..." -ForegroundColor Cyan

dotnet ef migrations add $MigrationName `
    --context $context `
    --output-dir $outputDir `
    --project $Project `
    --startup-project $StartupProject

if ($LASTEXITCODE -eq 0) {
    Write-Host "Migration created successfully in $outputDir" -ForegroundColor Green
} else {
    Write-Host "Migration failed." -ForegroundColor Red
    exit $LASTEXITCODE
}