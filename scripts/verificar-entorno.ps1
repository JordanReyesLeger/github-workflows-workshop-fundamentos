<#
.SYNOPSIS
    Comprueba que tu entorno esta listo para el taller de GitHub Actions.
#>

$ErrorActionPreference = 'Continue'
$correctas = 0
$fallidas = 0

function Test-Paso {
    param([string]$Etiqueta, [scriptblock]$Comando)

    & $Comando *> $null
    if ($LASTEXITCODE -eq 0) {
        Write-Host "  [OK]    $Etiqueta" -ForegroundColor Green
        $script:correctas++
    }
    else {
        Write-Host "  [FALLA] $Etiqueta" -ForegroundColor Red
        $script:fallidas++
    }
}

Write-Host ""
Write-Host "Verificando el entorno del taller de GitHub Actions" -ForegroundColor Cyan
Write-Host "---------------------------------------------------"

Test-Paso "Git instalado"          { git --version }
Test-Paso "SDK de .NET instalado"  { dotnet --version }
Test-Paso "GitHub CLI instalado"   { gh --version }
Test-Paso "Sesion de GitHub CLI"   { gh auth token }
Test-Paso "Restaurar dependencias" { dotnet restore TallerWorkflows.sln }
Test-Paso "Compilar la solucion"   { dotnet build TallerWorkflows.sln --configuration Release --no-restore }
Test-Paso "Ejecutar las pruebas"   { dotnet test TallerWorkflows.sln --configuration Release --no-build }

Write-Host "---------------------------------------------------"
Write-Host "  Correctas: $correctas   Fallidas: $fallidas"
Write-Host ""

if ($fallidas -gt 0) {
    Write-Host "Revisa el Modulo 0 del README antes de continuar." -ForegroundColor Yellow
    exit 1
}

Write-Host "Todo listo. Sigue con el Modulo 1 del README." -ForegroundColor Green