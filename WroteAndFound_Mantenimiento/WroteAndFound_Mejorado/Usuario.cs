class Usuario
{
    public string Nombre { get; private set; }
    private string Contrasenya { get; set; }

    static List<Usuario> ListaUsuarios = new List<Usuario>();

    static string NombreUsuario = "";
    static string ContrasenyaUsuario = "";
    public static string UsuarioActual = "";
    public static bool HayUsuarioActual = false;
    static bool Salir = false;

    public Usuario(string nombre, string contrasenya)
    {
        this.Nombre = nombre;
        this.Contrasenya = contrasenya;
    }

    public static int NumeroDeUsuarios()
    {
        return ListaUsuarios.Count;
    }

    public static List<Usuario> ListaDeUsuarios()
    {
        return ListaUsuarios;
    }

    // Opción crear cuenta de usuario
    public static void CrearUsuario()
    {
        Console.Clear();
        Console.WriteLine("        ╔════════════════════════╗");
        Console.WriteLine("        ║    [ CREAR CUENTA ]    ║");
        Console.WriteLine("        ╚════════════════════════╝");
        Console.WriteLine("");
        NombreUsuario = Comprobaciones.PedirString("    👤  Nombre (max 10 characteres)", 10);
        Console.WriteLine("");
        ContrasenyaUsuario = Comprobaciones.PedirString("    🔒  Contraseña", 10);

        // En los casos que falle el crear un usuario.
        try
        {
            if (Prestamo.DiccionarioPrestamos().ContainsKey(NombreUsuario))
            {
                Console.Clear();
                Console.WriteLine("Ya existe un usuario con ese nombre. Por favor elige otro.");
                Console.ReadKey();
                // Más Loggs.
                Logger.Info($"Intento de crear usuario duplicado: '{NombreUsuario}'.");
                return;
            }

            ListaUsuarios.Add(new Usuario(NombreUsuario, ContrasenyaUsuario));
            Prestamo.DiccionarioPrestamos()[NombreUsuario] = new List<string>();
            // Más loggs.
            Logger.Info($"Usuario '{NombreUsuario}' creado correctamente.");
            Console.Clear();
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("");
            Console.WriteLine("¡Usuario Creado Correctamente!");
            UsuarioActual = NombreUsuario;
            HayUsuarioActual = true;
            Console.WriteLine("");
            Console.WriteLine("Presiona una tecla para volver al menú...");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            Logger.Error($"Error al crear el usuario '{NombreUsuario}': {ex.Message}");
            Console.WriteLine("Ha ocurrido un error al crear el usuario.");
            Console.ReadKey();
        }
    }

    // Opción cambiar de usuario
    public static void CambiarUsuario()
    {
        Console.Clear();
        if (!Comprobaciones.HayUsuarios())
        {
            Console.WriteLine("");
            Console.WriteLine("No hay usuarios creados, ve y crea uno...");
            Console.WriteLine("");
            Console.ReadKey();
        }
        else
        {
            while (!Salir)
            {
                Console.Clear();
                Console.WriteLine("        ╔══════════════════════════════╗");
                Console.WriteLine("        ║    [ Cambiar de Usuario ]    ║");
                Console.WriteLine("        ╚══════════════════════════════╝");
                Console.WriteLine("");
                NombreUsuario = Comprobaciones.PedirString("    👤  Nombre del usuario", 10);
                ContrasenyaUsuario = Comprobaciones.PedirString("    🔒  Contraseña", 10);
                Console.WriteLine("-----------------------------------------");
                Console.WriteLine("");
                if (NombreUsuario == UsuarioActual)
                {
                    Console.WriteLine("Ya estas iniciado en este usuario, prueba con otro...");
                    Console.ReadKey();
                }
                else
                {
                    foreach (var Us in ListaUsuarios)
                    {
                        if (NombreUsuario == Us.Nombre && ContrasenyaUsuario == Us.Contrasenya)
                        {
                            UsuarioActual = NombreUsuario;
                            HayUsuarioActual = true;
                            // Más loggs.
                            Logger.Info($"Cambio de usuario: ahora activo '{UsuarioActual}'.");
                            Console.Clear();
                            Console.WriteLine("¡Usuario cambiado correctamente!");
                            Console.WriteLine("");
                            Console.WriteLine("Presiona una tecla para volver al menú...");
                            Console.ReadKey();
                            Salir = true;
                        }
                    }
                    if (!Salir)
                    {
                        Console.WriteLine("El usuario que has introducido no existe. \n \nOh la contraseña no es correcta");
                        Console.WriteLine("");
                        string StringSalir = Comprobaciones.PedirString("Vuelve a intentarlo, oh escribe \"salir\" para volver al menú", 10);
                        if (StringSalir == "salir")
                        {
                            Salir = true;
                        }
                    }
                }
            }
            Salir = false;
        }
    }

    // Opción eliminar usuario
    public static void EliminarUsuario()
    {
        Console.Clear();
        if (!Comprobaciones.HayUsuarios())
        {
            Console.WriteLine("");
            Console.WriteLine("No hay usuarios creados, ve y crea uno...");
            Console.WriteLine("");
            Console.ReadKey();
        }
        else
        {
            while (!Salir)
            {
                Console.Clear();
                Console.WriteLine("        ╔══════════════════════════════╗");
                Console.WriteLine("        ║    [ Eliminar un Usuario ]   ║");
                Console.WriteLine("        ╚══════════════════════════════╝");
                Console.WriteLine("");
                NombreUsuario = Comprobaciones.PedirString("    👤  Nombre del usuario", 10);
                ContrasenyaUsuario = Comprobaciones.PedirString("    🔒  Contraseña", 10);
                Console.WriteLine("-----------------------------------------");
                Console.WriteLine("");

                // POr si sucediese algo.
                try
                {
                    // Para saber si se ha encontrado
                    bool Encontrado = false;
                    for (int i = 0; i < ListaUsuarios.Count; i++)
                    {
                        Usuario Us = ListaUsuarios[i];
                        if (NombreUsuario == Us.Nombre && ContrasenyaUsuario == Us.Contrasenya)
                        {
                            ListaUsuarios.RemoveAt(i);
                            // Más loggs...
                            Logger.Info($"Usuario '{NombreUsuario}' eliminado.");
                            Console.WriteLine("¡Usuario borrado correctamente!");

                            if (NombreUsuario == UsuarioActual)
                            {
                                HayUsuarioActual = false;
                                UsuarioActual = "";
                            }

                            Console.ReadKey();
                            Salir = true;
                            Encontrado = true;
                            break;
                        }
                    }

                    if (!Encontrado)
                    {
                        Console.WriteLine("El usuario que has introducido no existe. \n \nOh la contraseña no es correcta");
                        Console.WriteLine("");
                        string StringSalir = Comprobaciones.PedirString("Vuelve a intentarlo, oh escribe \"salir\" para volver al menú", 10);
                        if (StringSalir == "salir")
                        {
                            Salir = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error($"Error al eliminar el usuario '{NombreUsuario}': {ex.Message}");
                    Console.WriteLine("Ha ocurrido un error al eliminar el usuario.");
                    Console.ReadKey();
                    Salir = true;
                }
            }
            Salir = false;
        }
    }

    // Te muestra cuál es el usuario actual
    public static void ActualUsuario()
    {
        Console.Clear();
        if (HayUsuarioActual == false)
        {
            Console.WriteLine("");
            Console.WriteLine("No estás iniciado como un usario, por favor crea uno o cambia a uno");
            Console.WriteLine("");
            Console.ReadKey();
        }
        else
        {
            // Más loggs.
            Logger.Info($"Consulta de usuario actual: '{UsuarioActual}'.");
            Console.WriteLine($"Actualmente estas iniciado como {UsuarioActual}");
            Console.WriteLine("");
            Console.WriteLine("Presiona una tecla para volver");
            Console.ReadKey();
        }
    }
}
