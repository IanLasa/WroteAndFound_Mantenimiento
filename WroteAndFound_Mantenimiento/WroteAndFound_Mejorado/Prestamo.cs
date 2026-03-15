class Prestamo
{
    // Diccionario con key "Nombre de un Usuario" y una lista como value
    static Dictionary<string, List<string>> Prestamos = new Dictionary<string, List<string>>();

    static bool Salir = false;

    public static Dictionary<string, List<string>> DiccionarioPrestamos()
    {
        return Prestamos;
    }

    // Coger prestado un libro
    public static void CogerPrestadoLibro(ref List<Libro> ListaDeLibros)
    {
        Console.Clear();
        if (Usuario.HayUsuarioActual == false)
        {
            Console.WriteLine("");
            Console.WriteLine("No estás iniciado como un usario, por favor crea uno o cambia a uno");
            Console.ReadKey();
        }
        else
        {
            // En el caso de que no se cree bien el usuario en el diccionario... KeyNotFoundException.
            try
            {
                if (!Prestamos.ContainsKey(Usuario.UsuarioActual))
                {
                    Prestamos[Usuario.UsuarioActual] = new List<string>();
                    Logger.Info($"Se creó entrada de préstamos para el usuario '{Usuario.UsuarioActual}' porque no existía.");
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Error al verificar la entrada de préstamos del usuario '{Usuario.UsuarioActual}': {ex.Message}");
                Console.WriteLine("Ha ocurrido un error al acceder a tus préstamos. Por favor, reinicia el programa.");
                Console.ReadKey();
                return;
            }

            while (!Salir)
            {
                Console.Clear();
                if (Comprobaciones.HayLibros())
                {
                    Menus.MenuListaDeLibros("LIBROS", Libreria.ListaDeLibros(), "Escribe el número del libro que quieres coger prestado", 0, Libreria.NumeroDeLibros(), 50);
                    if (Menus.Opcion != 0)
                    {
                        Console.Clear();
                        Console.WriteLine("");
                        Console.WriteLine("---------------------------------------");
                        Console.WriteLine("");

                        // Por si acaso se eliminase algún libro y no lo detectase o algo, es por añadir algo.
                        try
                        {
                            // Si ya tiene el libro prestado
                            if (Prestamos[Usuario.UsuarioActual].Contains(ListaDeLibros[Menus.Opcion - 1].Titulo))
                            {
                                Console.Clear();
                                Console.WriteLine("Ya tienes este libro prestado...");
                                Logger.Info($"El usuario '{Usuario.UsuarioActual}' intentó coger prestado '{ListaDeLibros[Menus.Opcion - 1].Titulo}' pero ya lo tenía.");
                                Console.ReadKey();
                            }
                            else if (Prestamos[Usuario.UsuarioActual].Count >= 3)
                            {
                                Console.WriteLine("Ya tienes el máximo de 3 libros prestados. Devuelve alguno antes de coger más.");
                                Logger.Info($"El usuario '{Usuario.UsuarioActual}' ha alcanzado el límite de 3 préstamos.");
                                Console.ReadKey();
                            }
                            // Si lo tiene prestado otro usuario
                            else if (!ListaDeLibros[Menus.Opcion - 1].Disponible)
                            {
                                Console.Clear();
                                Console.WriteLine("Este libro lo tiene prestado otro usuario...");
                                Logger.Info($"El usuario '{Usuario.UsuarioActual}' intentó coger '{ListaDeLibros[Menus.Opcion - 1].Titulo}' pero no estaba disponible.");
                                Console.ReadKey();
                            }
                            // Si puede coger prestado el libro
                            else
                            {
                                Console.Clear();
                                ListaDeLibros[Menus.Opcion - 1].Disponible = false;
                                Prestamos[Usuario.UsuarioActual].Add(ListaDeLibros[Menus.Opcion - 1].Titulo);
                                Logger.Info($"El usuario '{Usuario.UsuarioActual}' cogió prestado '{ListaDeLibros[Menus.Opcion - 1].Titulo}'.");
                                Console.WriteLine("¡Libro cogido prestado correctamente!");
                                Console.ReadKey();
                            }
                        }
                        catch (ArgumentOutOfRangeException ex)
                        {
                            Logger.Error($"Índice fuera de rango al coger un libro prestado. Opcion={Menus.Opcion}. Detalle: {ex.Message}");
                            Console.WriteLine("Error: el libro seleccionado ya no existe en la lista.");
                            Console.ReadKey();
                        }
                        catch (Exception ex)
                        {
                            Logger.Error($"Error inesperado en CogerPrestadoLibro: {ex.Message}");
                            Console.WriteLine("Ha ocurrido un error inesperado.");
                            Console.ReadKey();
                        }
                    }
                    Salir = true;
                }
            }
            Salir = false;
        }
    }

    // Función para devolver un libro prestado.
    // CORRECCIÓN DE ERROR LÓGICO: antes, se devolvían todos los libros prestádos cuando elegías devolver uno,
    // ya lo he solucionado...
    public static void DevolverLibro(ref List<Libro> ListaDeLibros)
    {
        Console.Clear();
        if (Usuario.HayUsuarioActual == false)
        {
            Console.WriteLine("");
            Console.WriteLine("No estás iniciado como un usario, por favor crea uno o cambia a uno");
            Console.ReadKey();
        }
        else
        {
            // Para el mismo caso que coger prestado un libro, si no existiese un usuario.
            try
            {
                if (!Prestamos.ContainsKey(Usuario.UsuarioActual))
                {
                    Prestamos[Usuario.UsuarioActual] = new List<string>();
                    Logger.Info($"Se creó entrada de préstamos para '{Usuario.UsuarioActual}'.");
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Error al verificar préstamos para devolver del usuario '{Usuario.UsuarioActual}': {ex.Message}");
                Console.WriteLine("Ha ocurrido un error. Por favor, reinicia el programa.");
                Console.ReadKey();
                return;
            }

            while (!Salir)
            {
                Console.Clear();
                if (Comprobaciones.HayLibros())
                {
                    if (Prestamos[Usuario.UsuarioActual].Count > 0)
                    {
                        int Contador = 0;
                        Console.WriteLine("----------------------------------------------------");
                        foreach (var LibroPrestado in Prestamos[Usuario.UsuarioActual])
                        {
                            Contador += 1;
                            Console.WriteLine("");
                            Console.WriteLine($"   {Contador}. {LibroPrestado}");
                        }
                        Console.WriteLine("----------------------------------------------------");
                        Console.WriteLine("");
                        Menus.Opcion = Comprobaciones.PedirNumero("Que libro quieres devolver", 1, Prestamos[Usuario.UsuarioActual].Count, false);

                        // Más de lo mismo, en el caso de que no se borre bien o algo.
                        try
                        {
                            // Para saber que libro hay que devolver y de quién.
                            string tituloADevolver = Prestamos[Usuario.UsuarioActual][Menus.Opcion - 1];

                            // CORRECCIÓN: marcar disponible solo el libro elegido, no todos...
                            foreach (var Libro in ListaDeLibros)
                            {
                                if (Libro.Titulo == tituloADevolver)
                                {
                                    Libro.Disponible = true;
                                    break;
                                }
                            }

                            Prestamos[Usuario.UsuarioActual].RemoveAt(Menus.Opcion - 1);
                            // Más loggs...
                            Logger.Info($"El usuario '{Usuario.UsuarioActual}' devolvió el libro '{tituloADevolver}'.");
                            Console.Clear();
                            Console.WriteLine("");
                            Console.WriteLine("¡Muy bien, ya se ha devuelto el libro!");
                            Salir = true;
                            Console.ReadKey();
                        }
                        catch (ArgumentOutOfRangeException ex)
                        {
                            Logger.Error($"Índice fuera de rango al devolver libro. Opcion={Menus.Opcion}. Detalle: {ex.Message}");
                            Console.WriteLine("Error: la opción seleccionada no es válida.");
                            Console.ReadKey();
                            Salir = true;
                        }
                        catch (Exception ex)
                        {
                            Logger.Error($"Error inesperado en DevolverLibro: {ex.Message}");
                            Console.WriteLine("Ha ocurrido un error inesperado.");
                            Console.ReadKey();
                            Salir = true;
                        }
                    }
                    else
                    {
                        Console.WriteLine("No tienes Libros prestados, ve y toma uno prestado...");
                        Salir = true;
                        Console.ReadKey();
                    }
                }
            }
            Salir = false;
        }
    }

    // Función para saber qué libros prestados tiene el usuario actual
    public static void LibrosPrestados()
    {
        Console.Clear();
        if (Usuario.HayUsuarioActual == false)
        {
            Console.WriteLine("");
            Console.WriteLine("No estás iniciado como un usario, por favor crea uno o cambia a uno");
            Console.ReadKey();
        }
        else
        {
            // Más de lo mismo, por que si no existiese el usuario en el diccionario...
            try
            {
                if (!Prestamos.ContainsKey(Usuario.UsuarioActual))
                {
                    Prestamos[Usuario.UsuarioActual] = new List<string>();
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Error al acceder a los libros prestados de '{Usuario.UsuarioActual}': {ex.Message}");
                Console.WriteLine("Ha ocurrido un error al leer tus préstamos.");
                Console.ReadKey();
                return;
            }

            while (!Salir)
            {
                Console.Clear();
                if (Comprobaciones.HayLibros())
                {
                    if (Prestamos[Usuario.UsuarioActual].Count > 0)
                    {
                        int Contador = 0;
                        Console.WriteLine("----------------------------------------------------");
                        foreach (var LibroPrestado in Prestamos[Usuario.UsuarioActual])
                        {
                            Contador += 1;
                            Console.WriteLine("");
                            Console.WriteLine($"   {Contador}. {LibroPrestado}");
                        }
                        Console.WriteLine("----------------------------------------------------");
                        Console.WriteLine("");
                        Console.WriteLine("Presiona una tecla para volver");
                        // Más loggs.
                        Logger.Info($"El usuario '{Usuario.UsuarioActual}' consultó sus libros prestados ({Contador} libros).");
                        Salir = true;
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("No tienes Libros prestados, ve y toma uno prestado...");
                        Salir = true;
                        Console.ReadKey();
                    }
                }
            }
            Salir = false;
        }
    }
}
