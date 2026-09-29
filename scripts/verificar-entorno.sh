#!/usr/bin/env bash
# Comprueba que tu entorno está listo para el taller.
set -u

ok=0
fallos=0

comprobar() {
  local etiqueta="$1"
  local comando="$2"
  if eval "$comando" >/dev/null 2>&1; then
    echo "  [OK]    $etiqueta"
    ok=$((ok + 1))
  else
    echo "  [FALLA] $etiqueta"
    fallos=$((fallos + 1))
  fi
}

echo ""
echo "Verificando el entorno del taller de GitHub Actions"
echo "---------------------------------------------------"

comprobar "Git instalado"            "git --version"
comprobar "SDK de .NET instalado"    "dotnet --version"
comprobar "GitHub CLI instalado"     "gh --version"
comprobar "Sesion de GitHub CLI"     "gh auth token"
comprobar "Restaurar dependencias"   "dotnet restore TallerWorkflows.sln"
comprobar "Compilar la solucion"     "dotnet build TallerWorkflows.sln --configuration Release --no-restore"
comprobar "Ejecutar las pruebas"     "dotnet test TallerWorkflows.sln --configuration Release --no-build"

echo "---------------------------------------------------"
echo "  Correctas: $ok   Fallidas: $fallos"
echo ""

if [ "$fallos" -gt 0 ]; then
  echo "Revisa el Modulo 0 del README antes de continuar."
  exit 1
fi

echo "Todo listo. Sigue con el Modulo 1 del README."