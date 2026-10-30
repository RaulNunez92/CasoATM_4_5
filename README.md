# Caso ATM — Refactorización Mediante Métodos (Laboratorio 4.5)

Evolución del sistema Mini ATM V1 hacia una arquitectura modular en C#.

## Responsabilidades de los Métodos (Sección 9 BRD)
- \MostrarMenu()\: Presentación visual en consola sin alteración de datos.
- \ConsultarSaldo(decimal)\: Lectura del saldo actual sin efectos secundarios.
- \CuentaEstaActiva(bool)\: Validación desacoplada del estado de la cuenta.
- \LeerMonto(string, out decimal)\: Captura defensiva contra entradas no numéricas.
- \Depositar(decimal, decimal)\: Aplica reglas de negocio y retorna saldo actualizado.
- \Retirar(decimal, decimal)\: Aplica validación de fondos y retorna saldo actualizado.

## Trazabilidad y Calidad
- Cumplimiento estricto de las reglas RN-01 a RN-10 y casos CP-01 a CP-10 sin regresiones.
