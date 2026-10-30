namespace CasoATM_4_5;

internal class Program
{
    static void Main(string[] args)
    {
        // Datos de negocio precargados (Sección 4 del BRD)
        string nombreCliente = "Raynieri Nuñez";
        string numeroCuenta = "CTA-1001";
        bool cuentaActiva = true;
        decimal saldo = 10000m; // RD$10,000

        bool sesionActiva = true;

        // Bucle iterativo de sesión (RN-09)
        do
        {
            MostrarMenu(nombreCliente, numeroCuenta);
            byte opcion = LeerOpcion(); // Optimización con byte (1 byte)

            switch (opcion)
            {
                case 1: // CU-01: Consultar saldo
                    ConsultarSaldo(saldo);
                    break;

                case 2: // CU-02: Depositar
                    if (!CuentaEstaActiva(cuentaActiva))
                    {
                        Console.WriteLine(">> Error: Cuenta inactiva. No se permiten depósitos.");
                        break;
                    }

                    if (LeerMonto("Ingrese el monto a depositar: ", out decimal montoDeposito))
                    {
                        saldo = Depositar(saldo, montoDeposito);
                    }
                    break;

                case 3: // CU-03: Retirar
                    if (!CuentaEstaActiva(cuentaActiva))
                    {
                        Console.WriteLine(">> Error: Cuenta inactiva. No se permiten retiros.");
                        break;
                    }

                    if (LeerMonto("Ingrese el monto a retirar: ", out decimal montoRetiro))
                    {
                        saldo = Retirar(saldo, montoRetiro);
                    }
                    break;

                case 4: // CU-04 / CP-08: Salir
                    Console.WriteLine("\n>> Sesión finalizada. Gracias por utilizar nuestros servicios.");
                    sesionActiva = false;
                    break;

                default: // CP-07 / RN-08: Opción inválida
                    Console.WriteLine("\n>> Error: Opción no reconocida. Seleccione una opción entre 1 y 4.");
                    break;
            }

        } while (sesionActiva);
    }

    // ============================================================
    // MÉTODOS REFACTORIZADOS (Sección 9 - Diseño Modular Esperado)
    // ============================================================

    // RF-10: Presenta el menú sin modificar estado
    private static void MostrarMenu(string cliente, string cuenta)
    {
        Console.WriteLine("\n========================================");
        Console.WriteLine("       BANCO CENTRAL - ATM MODULAR      ");
        Console.WriteLine($" Cliente: {cliente} | Cuenta: {cuenta}");
        Console.WriteLine("========================================");
        Console.WriteLine("1. Consultar saldo");
        Console.WriteLine("2. Depositar dinero");
        Console.WriteLine("3. Retirar dinero");
        Console.WriteLine("4. Salir");
        Console.WriteLine("----------------------------------------");
    }

    // RF-09 / Optimización: Lectura defensiva de opción
    private static byte LeerOpcion()
    {
        Console.Write("Seleccione una opción: ");
        byte.TryParse(Console.ReadLine() ?? "", out byte opcion);
        return opcion;
    }

    // RF-11 / CU-01: Muestra el saldo recibido
    private static void ConsultarSaldo(decimal saldoActual)
    {
        Console.WriteLine($">> Su saldo actual es: RD${saldoActual:N2}");
    }

    // RF-14: Valida el estado de la cuenta
    private static bool CuentaEstaActiva(bool estado)
    {
        return estado;
    }

    // RF-13 / RN-11 / CP-10: Lectura y validación numérica defensiva
    private static bool LeerMonto(string mensaje, out decimal monto)
    {
        Console.Write(mensaje);
        string entrada = Console.ReadLine() ?? "";

        if (!decimal.TryParse(entrada, out monto))
        {
            Console.WriteLine(">> Error: Entrada inválida. El monto debe ser numérico.");
            return false;
        }

        return true;
    }

    // RF-12 / RN-02, RN-05, RN-07: Procesa depósito y devuelve el nuevo saldo
    private static decimal Depositar(decimal saldoActual, decimal monto)
    {
        if (monto <= 0)
        {
            Console.WriteLine(">> Error: El monto a depositar debe ser mayor que cero.");
            return saldoActual; // Mantiene el saldo intacto (RN-07)
        }

        decimal nuevoSaldo = saldoActual + monto;
        Console.WriteLine($">> Depósito exitoso de RD${monto:N2}. Saldo actual: RD${nuevoSaldo:N2}");
        return nuevoSaldo;
    }

    // RF-12 / RN-03, RN-04, RN-06, RN-07: Procesa retiro y devuelve el nuevo saldo
    private static decimal Retirar(decimal saldoActual, decimal monto)
    {
        if (monto <= 0)
        {
            Console.WriteLine(">> Error: El monto a retirar debe ser mayor que cero.");
            return saldoActual;
        }

        if (monto > saldoActual)
        {
            Console.WriteLine($">> Error: Fondos insuficientes. Su saldo disponible es: RD${saldoActual:N2}");
            return saldoActual; // Mantiene el saldo intacto (RN-07)
        }

        decimal nuevoSaldo = saldoActual - monto;
        Console.WriteLine($">> Retiro exitoso de RD${monto:N2}. Saldo actual: RD${nuevoSaldo:N2}");
        return nuevoSaldo;
    }
}
