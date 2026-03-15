class Autoria
{

    public static void MiAutoria()
    {
        Console.Clear();
        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine("");
        }

        Console.WriteLine("Programa hecho habiendo dormido, por Ian Lasa.");
        Console.WriteLine("");
        Console.WriteLine("Finalizado el proyecto el 19/12/2025 a las 23:12");
        Console.WriteLine("");
        Console.WriteLine("¡Ni tan mal!");
        Console.WriteLine("");
        Console.WriteLine("Sinceramente, se que el código es un desmadre, pero ¡Hey!, he conseguido utilizar un diccionario");
        Console.WriteLine("");

        int nota = Comprobaciones.PedirNumero("Que nota le vas a poner a este extraño proyecto", 8, 10, false);
        Console.Clear();

        Console.Clear();
        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine("");
        }
        Console.WriteLine($"¡Gracias por el merecido {nota}!");
        Console.ReadKey();
    }
}