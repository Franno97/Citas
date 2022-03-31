Add-Migration "Inicial"  -Context CitaDbContext


Update-Database -Context  CitaDbContext


# Generar script migracion
# Generar script desde la primera migracion hasta la ultima
Script-Migration -Context CitaDbContext 0