class Logger
{
    // Ruta del archivo de log
    private static string RutaLog = "wroteAndFound.log";

    // Escribe una línea en el archivo con fecha, hora y nivel
    private static void Escribir(string nivel, string mensaje)
    {
        string linea = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{nivel}] {mensaje}";
        try
        {
            File.AppendAllText(RutaLog, linea + Environment.NewLine);
        }
        catch
        {
            // Si no se puede escribir el log, no se interrumpe el programa
        }
    }

    // Si es de información
    public static void Info(string mensaje)
    {
        Escribir("INFO", mensaje);
    }

    // Si es un error
    public static void Error(string mensaje)
    {
        Escribir("ERROR", mensaje);
    }
}
