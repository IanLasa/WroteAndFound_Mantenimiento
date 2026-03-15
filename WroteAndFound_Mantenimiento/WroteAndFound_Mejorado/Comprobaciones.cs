class Comprobaciones
{
    private static bool Salir = false;

    private static int OpcionInt;

    private static string OpcionString = "";
    public static int PedirNumero(string pregunta, int x, int y, bool especial)
    {
        do
        {
            Console.Write($"{pregunta}({x}-{y}): ");
            string entrada = Console.ReadLine() ?? "";

            // Para casos en los que quiero que puedan hacer intro
            if (especial && entrada == "")
            {
                return -1;
            }

            /* 
                Comprueba que lo que escriba el usuario se puede convertir en un int.
            */
            if (int.TryParse(entrada, out OpcionInt) && OpcionInt >= x && OpcionInt <= y)
            {
                Salir = true;
            }
            else
            {
                Console.WriteLine($"{entrada} no es un número válido, vuelve a intentarlo.");
                Console.ReadKey();
            }

        } while (!Salir);
        Salir = false;
        return OpcionInt;

    }

    // Funcion muy simple para comprobar que un string no esta vacío
    public static string PedirString(string pregunta, int maximo)
    {
        while (!Salir)
        {
            Console.Write($"{pregunta}: ");
            OpcionString = Console.ReadLine() ?? "";

            // Si el string que nos den es demasiado grande, da error y vuelve a pedir.
            if (OpcionString.Length > maximo)
            {
                Console.WriteLine("Demasiado largo, vuelve a intentarlo...");
                Console.ReadKey();

            }
            else { Salir = true; }
        }
        Salir = false;
        return OpcionString;
    }


    public static bool HayLibros()
    {
        int NumeroDeLibros = Libreria.NumeroDeLibros();
        if (NumeroDeLibros == 0)
        {
            Console.WriteLine("─────────────────────────────────────────────────────────");
            Console.WriteLine("     Actualmente no hay libros en la biblioteca...");
            Console.WriteLine("    ¡Parece que se han borrado todos! ");
            Console.WriteLine("");
            Console.WriteLine("    ¿Por qué no vas y añades uno nuevo? ");
            Console.WriteLine("─────────────────────────────────────────────────────────");
            Console.WriteLine("");
            Console.WriteLine("Presiona una tecla para volver...");
            Console.ReadKey();
            return false;
        }
        else
        {
            return true;
        }
    }

    // Comprueba si hay reseñas, no tiene mucho más.
    public static bool HayResenyas(List<Libro> libros)
    {
        int NumeroDeResenyas = Libreria.NumeroDeResenyas();
        if (NumeroDeResenyas == 0)
        {
            Console.WriteLine("─────────────────────────────────────────────────────────");
            Console.WriteLine("     Actualmente no hay Resenyas en la biblioteca...");
            Console.WriteLine("    ¡Parece que se han borrado todos! ");
            Console.WriteLine("");
            Console.WriteLine("    ¿Por qué no vas y añades uno nuevo? ");
            Console.WriteLine("─────────────────────────────────────────────────────────");
            Console.WriteLine("");
            Console.WriteLine("Presiona una tecla para volver...");
            Console.ReadKey();
            return false;
        }
        else
        {
            return true;
        }

    }

    // Comprobar que hay usuarios

    public static bool HayUsuarios()
    {
        int NumeroUsuarios = Usuario.NumeroDeUsuarios();
        if (NumeroUsuarios == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    // Comprueba si un libro en concreto está disponible
    public static string EstaDisponible(List<Libro> ListaLibros, int i)
    {
        if (ListaLibros[i].Disponible == true) 
        { 
            return "Disponible"; 
        }
        else 
        { 
            return "No Disponible"; 
        }
    }

}
