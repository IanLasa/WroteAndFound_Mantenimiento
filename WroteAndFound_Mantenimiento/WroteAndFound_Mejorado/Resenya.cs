class Resenya
{
    public string Autor { get; private set; }
    public string Calificacion { get; private set; }

    private static bool Salir = false;
    private static int CalificacionResenya;

    // Constructor Reseña
    Resenya(string autor, int calificacion)
    {
        this.Autor = autor;
        this.Calificacion = new string('⭐', calificacion);
    }



    public static void PonerResenyas()
    {
        Console.Clear();
        // Sale si no esta iniciado sesión
        if (!Usuario.HayUsuarioActual)
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
                    Menus.MenuListaDeLibros("LIBROS", Libreria.ListaDeLibros(), "Escribe el número del libro que quieres poner reseña", 0, Libreria.NumeroDeLibros(), 50);
                    if (Menus.Opcion != 0)
                    {
                        
                            foreach (var Us in Libreria.ListaDeLibros()[Menus.Opcion - 1].Resenyas)
                            {
                                if (Us.Autor == Usuario.UsuarioActual)
                                {
                                    // En el caso de que ya haya puesto antes una reseña
                                    Console.Clear();
                                    Console.WriteLine(" Este usuario ya le ha puesto una reseña a este libro...");
                                    Console.WriteLine("");
                                    Console.WriteLine("Presiona una tecla para volver a intentarlo...");
                                    Console.ReadKey();
                                    Salir = true;
                                }
                            }
                        
                        if (!Salir)
                        {
                            // Pedir y añadir Reseñá
                            Console.Clear();
                            Libreria.VerCaracteristicasLibro(Menus.Opcion - 1, true);
                            Console.WriteLine();
                            CalificacionResenya = Comprobaciones.PedirNumero("Que reseña le quieres poner", 1, 5, false);

                            Console.WriteLine();

                            Libreria.AgregarResenya(Menus.Opcion, new Resenya(Usuario.UsuarioActual, CalificacionResenya));
                            Console.Clear();
                            Console.WriteLine("Se ha añadido la reseña correctamente");
                            Console.WriteLine();
                            Console.WriteLine("Pulsa una tecla para continuar");
                            Salir = true;
                            Console.ReadKey();
                        }
                    }
                    else
                    {
                        Salir = true;
                    }
                }
                else
                {
                    Salir = true;
                }
            }
            Salir = false;
        }
    }


    public static int CaracteristicasResenyas(int l, int i, List<Libro> ListaLibros)
    {
        if (ListaLibros[l].Resenyas.Count == 0)
        {
            Console.WriteLine("     No hay Reseas...");
            return 6;
        }

        int Contador = 0;

        foreach (Resenya Res in ListaLibros[l].Resenyas)
        {
            Console.WriteLine($"      {Res.Autor}: {Res.Calificacion}");
            Contador++;
            i++;

            if (Contador % 2 == 0 && Contador != 0)
            {
                return i;
            }
        }

        Console.WriteLine("No hay más reseñas. Presiona una tecla para volver");
        Console.ReadKey();
        return 6;

    }
}


