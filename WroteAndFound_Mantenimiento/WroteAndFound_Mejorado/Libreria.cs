class Libreria
{

    private string Nombre { get; set; }
    private string Creador { get; set; }
    private DateOnly CreadoEl { get; set; }
    static List<Libro> Libros = new List<Libro>();

    static bool Salir = false;

    // Constructor de Libreria
    public Libreria(string nombre, string creador, DateOnly creadoEn)
    {
        this.Nombre = nombre;
        this.Creador = creador;
        this.CreadoEl = creadoEn;
    }

    // Poder obtener la lista de libros, ya que es privada
    public static List<Libro> ListaDeLibros()
    {
        return Libros;
    }

    // Saber el número de libros que tiene la lista
    public static int NumeroDeLibros()
    {
        return Libros.Count;
    }

    // Saber el número de reseñas que hay
    public static int NumeroDeResenyas()
    {
        int NumeroDeResenyas = 0;
        foreach (Libro Li in Libros)
        {
            NumeroDeResenyas += Li.Resenyas.Count;
        }
        return NumeroDeResenyas;
    }

    public void AgregarLibro(Libro Libro)
    {
        Libros.Add(Libro);
    }

    public static void AgregarResenya(int Opcion, Resenya Resenya)
    {
        Libros[Opcion - 1].Resenyas.Add(Resenya);
    }

    public static void VerLibros()
    {
        Console.Clear();
        if (Comprobaciones.HayLibros())
        {
            do
            {
                Menus.MenuListaDeLibros("LIBROS", Libros, "Escribe el número del libro que quieres ver más", 0, Libros.Count, 50);
                if (Menus.Opcion > 0 && Menus.Opcion <= Libros.Count)
                {
                    VerCaracteristicasLibro(Menus.Opcion - 1, false);
                }
                else
                {
                    Salir = true;
                }

            } while (!Salir);
        }
        Salir = false;
    }

    // Ver características del libro que se pida
    public static void VerCaracteristicasLibro(int l, bool special)
    {
        // No se como, pero me había salido el error ArgumentOutOfRangeException probando a lo loco el código,
        // pero no he podido replicarlo... Así que he hecho un try-catch por si acaso.
        try
        {
            string Disponible = Comprobaciones.EstaDisponible(Libros, l);

            int i = 5;
            while (!Salir)
            {
                Console.Clear();
                Console.WriteLine("---------------------------------------");
                Console.WriteLine($"   Libro {l + 1}:");
                Console.WriteLine($"   Título: {Libros[l].Titulo}");
                Console.WriteLine($"   Autor:  {Libros[l].Autor}");
                Console.WriteLine($"   Año:    {Libros[l].Anyo}");
                Console.WriteLine($"   Género: {Libros[l].Genero}");
                Console.WriteLine("");
                Console.WriteLine($"   Estado: " + Disponible);
                i = Resenya.CaracteristicasResenyas(l, i, Libros);
                Console.WriteLine("---------------------------------------");
                Console.WriteLine("");
                if (i != 6 && !special)
                {
                    string OpcionString = Comprobaciones.PedirString("Escribe \"Si\" para volver atras", 50);
                    if (OpcionString == "Si")
                    {
                        Salir = true;
                    }
                }
                else if (i == 6 && !special)
                {
                    Console.WriteLine("Presiona una tecla para volver...");
                    Console.ReadKey();
                    Salir = true;
                }
                else
                {
                    Salir = true;
                }
            }
        }
        // ex es la variable que le he puesto yo (es común), para guardar el error del sistema.
        catch (ArgumentOutOfRangeException ex)
        {
            // ex.message es para que tambien guarde la información del error que da el sistema... 
            Logger.Error($"{l} está fuera del. Detalle: {ex.Message}");
            Console.WriteLine("Error: el libro seleccionado ya no está disponible.");
            Console.ReadKey();
        }
        // Cualquier otra excepción...
        catch (Exception ex)
        {
            Logger.Error($"Error inesperado en VerCaracteristicasLibro: {ex.Message}");
            Console.WriteLine("Ha ocurrido un error inesperado al mostrar el libro.");
            Console.ReadKey();
        }
        // Lo que pasa si o si...
        finally
        {
            Salir = false;
        }
    }

    // Función añadir libro
    public static void AnhadirLibro()
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
            while (!Salir)
            {
                Console.Clear();
                Console.WriteLine("---------------------------------------");
                Console.WriteLine("   ¡Vamos a añadir un nuevo libro!");
                Console.WriteLine("");
                Console.WriteLine($"📘 Libro {Libros.Count + 1}:");

                string Titulo = Comprobaciones.PedirString("Título", 40);
                string Autor = Comprobaciones.PedirString("Autor", 40);
                int Anyo = Comprobaciones.PedirNumero("Año", -4000, 2025, false);
                string Genero = Comprobaciones.PedirString("Género", 40);

                Libros.Add(new Libro(Titulo, Autor, Anyo, Genero));
                //Más logs...
                Logger.Info($"Libro añadido: '{Titulo}' de '{Autor}' ({Anyo}), género '{Genero}'.");
                Console.WriteLine("---------------------------------------");
                Console.WriteLine("¡Libro añadido!");
                Console.WriteLine("");
                string StringSalir = Comprobaciones.PedirString("Escribe \"salir\" si no quieres seguir añadiendo", 10);
                if (StringSalir == "salir")
                {
                    Salir = true;
                }
            }
            Salir = false;
        }
    }

    // * IMPORTANTE * CORRECCIÓN DE UN ERROR QUE TENÍA: Antes, solo borraba el libro si estaba
    // en el diccionario de préstamos. Si no estaba prestado a nadie, el libro nunca
    // se eliminaba porque el RemoveAt estaba dentro del if de préstamo. Solucionado ahora...
    public static void EliminarLibro()
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
            while (!Salir)
            {
                Console.Clear();
                if (Comprobaciones.HayLibros())
                {
                    Menus.MenuListaDeLibros("LIBROS", Libros, "Escribe el número del libro que quieres eliminar", 0, Libros.Count, 50);
                    if (Menus.Opcion != 0)
                    {
                        Console.Clear();
                        Console.WriteLine("");
                        Console.WriteLine("---------------------------------------");
                        Console.WriteLine("");
                        Console.Write("Escribe \"si\" para confirmar o cualquier otra cosa para cancelar: ");
                        string confirmar = Console.ReadLine() ?? "";
                        Console.WriteLine("");
                        if (confirmar == "si")
                        {
                            // Este es más por si acaso? No me ha dado ningún error, pero seguro que podría fallar.
                            try
                            {
                                string tituloBorrar = Libros[Menus.Opcion - 1].Titulo;

                                // CORRECCIÓN!!! Primero quitar el libro de todos los diccionarios de préstamo,
                                // y luego eliminarlo siempre, da igual si está prestado oh no...
                                foreach (var usuarioPrestamo in Prestamo.DiccionarioPrestamos())
                                {
                                    usuarioPrestamo.Value.Remove(tituloBorrar);
                                }

                                Libros.RemoveAt(Menus.Opcion - 1);
                                // Más logs...
                                Logger.Info($"Libro '{tituloBorrar}' eliminado del catálogo.");
                                Console.WriteLine("!Libro borrado del catálogo!");
                            }
                            catch (ArgumentOutOfRangeException ex)
                            {
                                // Por si no lo has leído el comentario un poco antes, ex.message es para que se muestre
                                // también el error que te da el sistema.
                                Logger.Error($"Índice fuera de rango al eliminar libro. Opcion={Menus.Opcion}. Detalle: {ex.Message}");
                                Console.WriteLine("Error: el libro seleccionado ya no existe.");
                            }
                            // En el caso de que hubiese cualquier otro error.
                            catch (Exception ex)
                            {
                                Logger.Error($"Error inesperado en EliminarLibro: {ex.Message}");
                                Console.WriteLine("Ha ocurrido un error inesperado al eliminar el libro.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Has cancelado borrar un libro");
                        }

                        Console.WriteLine("");
                        Console.Write("Escribe \"si\", si quieres borrar otro");
                        string borrarOtro = Console.ReadLine() ?? "";
                        if (borrarOtro != "si")
                        {
                            Salir = true;
                        }
                    }
                    else
                    {
                        Salir = true;
                    }
                }
            }
            Salir = false;
        }
    }

    // Menu Buscar Libro por lo que sea
    public static void BuscarLibro()
    {
        do
        {
            Console.Clear();
            // MANTENIMIENTO EVOLUTIVO: He añadido la opción "Buscar por Disponibilidad".
            string[] menu = { "Buscar por Título", "Buscar por Autor", "Buscar por año", "Buscar por Genero", "Buscar por Reseñas", "Buscar por Disponibilidad" };
            Menus.MenuBonito("BUSCAR POR", menu, "Escribe el número de lo que quieres hacer", 0, 6, 37);
            switch (Menus.Opcion)
            {
                case 1:
                    BuscarLibroPor("Título");
                    Console.ReadKey();
                    break;
                case 2:
                    BuscarLibroPor("Autor");
                    Console.ReadKey();
                    break;
                case 3:
                    BuscarLibroPor("Anyo");
                    Console.ReadKey();
                    break;
                case 4:
                    BuscarLibroPor("Género");
                    Console.ReadKey();
                    break;
                case 5:
                    BuscarLibroPor("Reseñas");
                    Console.ReadKey();
                    break;
                case 6:
                    BuscarLibroPor("Disponibilidad");
                    Console.ReadKey();
                    break;
                case 0:
                    Salir = true;
                    break;
            }
        } while (!Salir);
        Salir = false;
    }

    // Función buscar en cada caso
    static void BuscarLibroPor(string tipo)
    {
        Console.Clear();

        int HayResultados = 0;
        int Estrellas = -1;
        string BuscarString = "";
        int BuscarInt = 0;
        // Añadido
        bool BuscarDisponible = true;

        // Si no hay reseñas, sale
        if (tipo == "Reseñas" && !Comprobaciones.HayResenyas(Libros))
        {
            Console.WriteLine("No hay reseñas en ningún libro.");
            Console.WriteLine("");
            Console.WriteLine("Presiona una tecla para volver atrás");
            return;
        }

        // Añadido lo de Disponibilidad.
        if (tipo == "Reseñas")
        {
            Estrellas = Comprobaciones.PedirNumero($"Escribe la {tipo} del libro", 1, 5, false);
        }
        else if (tipo == "Anyo")
        {
            BuscarInt = Comprobaciones.PedirNumero($"Escribe el {tipo} en el que se escribió el libro", -4000, 2025, false);
        }
        // Lo que se ha añadido.
        else if (tipo == "Disponibilidad")
        {
            int opcionDisp = Comprobaciones.PedirNumero("¿Qué libros quieres ver? (1=Disponibles, 2=No disponibles)", 1, 2, false);
            BuscarDisponible = (opcionDisp == 1);
        }
        else
        {
            BuscarString = Comprobaciones.PedirString($"Escribe el {tipo} del libro", 30);
        }

        Console.WriteLine("");
        Console.WriteLine("");

        foreach (Libro Lib in Libros)
        {
            if (tipo == "Título" && BuscarString == Lib.Titulo)
            {
                Console.WriteLine($"  {HayResultados + 1}. {Lib.Titulo}");
                Console.WriteLine("");
                HayResultados += 1;
            }
            else if (tipo == "Autor" && BuscarString == Lib.Autor)
            {
                Console.WriteLine($"  {HayResultados + 1}. {Lib.Titulo}");
                Console.WriteLine($"       {Lib.Autor}");
                Console.WriteLine("");
                HayResultados += 1;
            }
            else if (tipo == "Anyo" && BuscarInt == Lib.Anyo)
            {
                Console.WriteLine($"  {HayResultados + 1}. {Lib.Titulo}");
                Console.WriteLine($"       {Lib.Anyo}");
                Console.WriteLine("");
                HayResultados += 1;
            }
            else if (tipo == "Género" && BuscarString == Lib.Genero)
            {
                Console.WriteLine($"  {HayResultados + 1}. {Lib.Titulo}");
                Console.WriteLine($"       {Lib.Genero}");
                Console.WriteLine("");
                HayResultados += 1;
            }
            else if (tipo == "Reseñas")
            {
                foreach (Resenya Res in Lib.Resenyas)
                {
                    if (Estrellas == Res.Calificacion.Length)
                    {
                        Console.WriteLine($"  {HayResultados + 1}. {Lib.Titulo}");
                        Console.WriteLine($"       {Res.Calificacion}");
                        Console.WriteLine("");
                        HayResultados += 1;
                    }
                }
            }
            // Añadido...
            else if (tipo == "Disponibilidad" && Lib.Disponible == BuscarDisponible)
            {
                string Estado = Lib.Disponible ? "Disponible" : "No disponible";
                Console.WriteLine($"  {HayResultados + 1}. {Lib.Titulo}");
                Console.WriteLine($"       Estado: {Estado}");
                Console.WriteLine("");
                HayResultados += 1;
            }
        }

        // Si hay resultados, te dice cuántos
        if (HayResultados != 0)
        {
            if (tipo == "Reseñas")
            {
                Console.WriteLine($"Hay {HayResultados} resultado/s para {Estrellas} estrellas.");
                Console.WriteLine("");
            }
            else if (tipo == "Disponibilidad")
            {
                string estadoTexto = BuscarDisponible ? "disponibles" : "no disponibles";
                Console.WriteLine($"Hay {HayResultados} libro/s {estadoTexto}.");
                Console.WriteLine("");
                Logger.Info($"Búsqueda por disponibilidad ({estadoTexto}): {HayResultados} resultado/s.");
            }
            else
            {
                Console.WriteLine($"Hay {HayResultados} resultado/s para {BuscarString}.");
                Console.WriteLine("");
            }
        }
        else
        {
            Console.WriteLine("No hay resultados...");
            Console.WriteLine("");
            Console.WriteLine("Presiona una tecla para volver atrás");
        }
    }
}
