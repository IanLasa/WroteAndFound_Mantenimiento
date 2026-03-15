class Menus
{
    private static bool Salir = false;
    public static int Opcion;

    static string[] menu = new string[10];



    // Función para hacer el menú bonito
    public static void MenuBonito(string nombreMenu, string[] menu, string pregunta, int x, int y, int ancho)
    {
        string textoSalir = "0. Salir";

        // Centrar el título

        // Restar a ancho - 2 (por los ║), y luego dividirlo en dos, para los dos lados.
        int centrar = (ancho - 2 - nombreMenu.Length) / 2;

        // Crear el string que dice cuantos espacios a la izquierda, y como de grande ha de ser a lo máximo.
        string titulo = nombreMenu.PadLeft(nombreMenu.Length + centrar).PadRight(ancho - 2);

        while (!Salir)
        {
            Console.Clear();
            // Línea Superior
            /*
                new string('═', int), es para que rellele un texto con un caracter, unas ciertas veces.
            */
            Console.WriteLine("╔" + new string('═', ancho - 2) + "╗");

            // Poner el Título
            Console.WriteLine($"║{titulo}║");

            // Línea de espacio
            Console.WriteLine("║" + new string(' ', ancho - 2) + "║");

            for (int i = 0; i < menu.Length; i++)
            {
                string opcionMenu = $"{i + 1}. {menu[i]}";
                opcionMenu = opcionMenu.PadRight(ancho - 5);
                Console.WriteLine($"║   {opcionMenu}║");

                // Si el menú sin contar el título, es mas grande que 5, lo va dividiendo en partes de 5.
                // Que se pueden ver gracias a lo de especial, y presionando intro.
                if ((i + 1) % 5 == 0 && menu.Length > 5)
                {
                    // Ver más
                    string verMas = "Presiona intro para ver mas...";
                    Console.WriteLine($"║   {verMas.PadRight(ancho - 5)}║");

                    // Otro espacio
                    Console.WriteLine("║" + new string(' ', ancho - 2) + "║");

                    // Salir
                    Console.WriteLine($"║   {textoSalir.PadRight(ancho - 5)}║");

                    // Última linea
                    Console.WriteLine("╚" + new string('═', ancho - 2) + "╝");
                    Console.WriteLine("");

                    // Pide automáticamente, para mayor facilidad, el propio menu el número.
                    Opcion = Comprobaciones.PedirNumero(pregunta, x, y, true);

                    if (Opcion >= x && Opcion <= y)
                    {
                        return;
                    }
                    Console.Clear();

                    // Línea Superior
                    Console.WriteLine("╔" + new string('═', ancho - 2) + "╗");

                    // Poner el Título
                    Console.WriteLine($"║{titulo}║");

                    // Línea de espacio
                    Console.WriteLine("║" + new string(' ', ancho - 2) + "║");
                }
            }

            // Si el menu, sin contar el título, es más pequeño que cinco, no divide nada ni deja hacer intro.
            if (menu.Length <= 5)
            {
                // Otro espacio
                Console.WriteLine("║" + new string(' ', ancho - 2) + "║");

                // Salir
                Console.WriteLine($"║   {textoSalir.PadRight(ancho - 5)}║");

                // Última linea
                Console.WriteLine("╚" + new string('═', ancho - 2) + "╝");

                // Poner un espacio
                Console.WriteLine("");
                Opcion = Comprobaciones.PedirNumero(pregunta, x, y, false);
                if (Opcion >= x && Opcion <= y)
                {
                    return;
                }
            }
            else
            {
                // Si llegas al último grupo de 5, te pone "No hay mas", y con intro, vuelve al primer grupo de 5.
                string noHayMas = "No hay mas...";
                Console.WriteLine($"║   {noHayMas.PadRight(ancho - 5)}║");

                // Otro espacio
                Console.WriteLine("║" + new string(' ', ancho - 2) + "║");

                // Volver Atrás
                string volverAtras = "Presiona intro para volve atras";
                Console.WriteLine($"║   {volverAtras.PadRight(ancho - 5)}║");

                // Otro espacio
                Console.WriteLine("║" + new string(' ', ancho - 2) + "║");

                // Salir
                Console.WriteLine($"║   {textoSalir.PadRight(ancho - 5)}║");

                // Última linea
                Console.WriteLine("╚" + new string('═', ancho - 2) + "╝");

                // Poner un espacio
                Console.WriteLine("");
                Opcion = Comprobaciones.PedirNumero(pregunta, x, y, true);
                if (Opcion >= x && Opcion <= y)
                {
                    return;
                }
            }
        }
    }


    public static void MenuListaDeLibros(string nombreMenu, List<Libro> ListaLibros, string pregunta, int x, int y, int ancho)
    {
        string textoSalir = "0. Salir";

        // Centrar el título
        int centrar = (ancho - 2 - nombreMenu.Length) / 2;
        string titulo = nombreMenu.PadLeft(nombreMenu.Length + centrar).PadRight(ancho - 2);

        while (!Salir)
        {
            Console.Clear();
            // Línea Superior
            Console.WriteLine("╔" + new string('═', ancho - 2) + "╗");

            // Poner el Título
            Console.WriteLine($"║{titulo}║");

            // Línea de espacio
            Console.WriteLine("║" + new string(' ', ancho - 2) + "║");

            for (int i = 0; i < ListaLibros.Count; i++)
            {
                string opcionMenu = $"{i + 1}. {ListaLibros[i].Titulo}";
                opcionMenu = opcionMenu.PadRight(ancho - 5); // Rellena con espacios a la derecha        
                Console.WriteLine($"║   {opcionMenu}║");
                if ((i + 1) % 5 == 0 && ListaLibros.Count > 5)
                {
                    // Ver más
                    string verMas = "Presiona intro para ver mas...";
                    Console.WriteLine($"║   {verMas.PadRight(ancho - 5)}║");

                    // Otro espacio
                    Console.WriteLine("║" + new string(' ', ancho - 2) + "║");

                    // Salir
                    Console.WriteLine($"║   {textoSalir.PadRight(ancho - 5)}║");

                    // Última linea
                    Console.WriteLine("╚" + new string('═', ancho - 2) + "╝");
                    Console.WriteLine("");

                    Opcion = Comprobaciones.PedirNumero(pregunta, x, y, true);

                    if (Opcion >= x && Opcion <= y)
                    {
                        return;
                    }
                    Console.Clear();

                    // Línea Superior
                    Console.WriteLine("╔" + new string('═', ancho - 2) + "╗");

                    // Poner el Título
                    Console.WriteLine($"║{titulo}║");

                    // Línea de espacio
                    Console.WriteLine("║" + new string(' ', ancho - 2) + "║");
                }
            }

            if (ListaLibros.Count <= 5)
            {
                // Otro espacio
                Console.WriteLine("║" + new string(' ', ancho - 2) + "║");

                // Salir
                Console.WriteLine($"║   {textoSalir.PadRight(ancho - 5)}║");

                // Última linea
                Console.WriteLine("╚" + new string('═', ancho - 2) + "╝");

                // Poner un espacio
                Console.WriteLine("");
                Opcion = Comprobaciones.PedirNumero(pregunta, x, y, false);
                if (Opcion >= x && Opcion <= y)
                {
                    return;
                }
            }
            else
            {
                // Ver más
                string noHayMas = "No hay mas...";
                Console.WriteLine($"║   {noHayMas.PadRight(ancho - 5)}║");

                // Otro espacio
                Console.WriteLine("║" + new string(' ', ancho - 2) + "║");

                // Volver Atrás
                string volverAtras = "Presiona intro para volve atras";
                Console.WriteLine($"║   {volverAtras.PadRight(ancho - 5)}║");

                // Otro espacio
                Console.WriteLine("║" + new string(' ', ancho - 2) + "║");

                // Salir
                Console.WriteLine($"║   {textoSalir.PadRight(ancho - 5)}║");

                // Última linea
                Console.WriteLine("╚" + new string('═', ancho - 2) + "╝");

                // Poner un espacio
                Console.WriteLine("");
                Opcion = Comprobaciones.PedirNumero(pregunta, x, y, true);
                if (Opcion >= x && Opcion <= y)
                {
                    return;
                }
            }
        }
    }






    public static void VerMenu()
    {
        while (!Salir)
        {
            Console.Clear();

            menu = new string[] { "Libros", "Usuarios", "Prestamos", "Autoría" };
            MenuBonito("MENÚ", menu, "Escribe el número de lo que quieres hacer", 0, 4, 40);
            switch (Opcion)
            {
                case 1:
                    MenuLibros();
                    break;
                case 2:
                    MenuUsuario();
                    break;
                case 3:
                    MenuPrestamos();
                    break;
                case 4:
                    Autoria.MiAutoria();
                    break;
                case 0:
                    Console.Clear();
                    for (int i = 1; i <= 5; i++)
                    {
                        Console.WriteLine("");
                    }
                    Console.WriteLine("¡Gracias por venir a Wrote&Found! ¡Esperamos verte de nuevo!");
                    Salir = true;
                    break;
            }
        }
    }


    // Menú de libros
    static void MenuLibros()
    {
        do
        {
            Console.Clear();
            // Hacer que el array empieze desde cero, para luego rellenarlo.
            menu = new string[] { "Ver todos los libros", "Añadir nuevo libro", "Eliminar un libro", "Buscar Libro por", "Poner Reseñas" };
            MenuBonito("OPCIONES LIBROS", menu, "Escribe el número de lo que quieres hacer", 0, 5, 37);
            switch (Opcion)
            {
                case 1:
                    Libreria.VerLibros();
                    break;
                case 2:
                    Libreria.AnhadirLibro();
                    break;
                case 3:
                    Libreria.EliminarLibro();
                    break;
                case 4:
                    Libreria.BuscarLibro();
                    break;
                case 5:
                    Resenya.PonerResenyas();
                    break;
                case 0:
                    Console.WriteLine("Volviendo al menú principal...");
                    Salir = true;
                    break;
            }
        } while (!Salir);
        Salir = false;
    }


    static void MenuUsuario()
    {
        while (!Salir)
        {
            Console.Clear();
            menu = new string[] { "Crear Usuario", "Cambiar Usuario", "Eliminar Usuario", "Usuario Actual" };
            MenuBonito("USUARIOS", menu, "Escribe el número de la opción que quieras", 0, 4, 40);
            switch (Opcion)
            {
                case 1:
                    Usuario.CrearUsuario();
                    break;
                case 2:
                    Usuario.CambiarUsuario();
                    break;
                case 3:
                    Usuario.EliminarUsuario();
                    break;
                case 4:
                    Usuario.ActualUsuario();
                    break;
                case 0:
                    Salir = true;
                    break;
            }
        }
        Salir = false;
    }

    // Menú prestamos
    static void MenuPrestamos()
    {
        do
        {
            List<Libro> ListaLibros = Libreria.ListaDeLibros();
            Console.Clear();
            menu = new string[] { "Coger prestado un libro", "Devolver un libro", "Libros prestados" };
            MenuBonito("OPCIONES LIBROS", menu, "Escribe el número de lo que quieres hacer", 0, 3, 37);
            switch (Opcion)
            {
                case 1:
                    Prestamo.CogerPrestadoLibro(ref ListaLibros);
                    break;
                case 2:
                    Prestamo.DevolverLibro(ref ListaLibros);
                    break;
                case 3:
                    Prestamo.LibrosPrestados();
                    break;
                case 0:
                    Salir = true;
                    break;
            }
        } while (!Salir);
        Salir = false;
    }





}
