param(
    [Parameter(Mandatory)][string]$StrongNameKeyBase64
)
$ErrorActionPreference = "Stop"

[IO.File]::WriteAllBytes("$PSScriptRoot/../51Degrees.snk", [Convert]::FromBase64String($StrongNameKeyBase64))
