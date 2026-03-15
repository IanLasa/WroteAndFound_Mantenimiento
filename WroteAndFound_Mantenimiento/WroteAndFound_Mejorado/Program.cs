namespace WroteAndFound
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger.Info("Aplicación WroteAndFound iniciada.");

            // Crear la libreria
            Libreria WroteAndFound = new Libreria("WroteAndFound", "Ian Lasa", new DateOnly(2025, 12, 17));

            // Añadir Libros principales
            WroteAndFound.AgregarLibro(new Libro("Cien años de soledad", "Gabriel García Márquez", 1967, "Realismo mágico"));
            WroteAndFound.AgregarLibro(new Libro("1984", "George Orwell", 1949, "Distopía"));
            WroteAndFound.AgregarLibro(new Libro("El Principito", "Antoine de Saint-Exupéry", 1943, "Fábula"));
            WroteAndFound.AgregarLibro(new Libro("Don Quijote de la Mancha", "Miguel de Cervantes", 1605, "Novela clásica"));
            WroteAndFound.AgregarLibro(new Libro("Orgullo y prejuicio", "Jane Austen", 1813, "Romance"));
            WroteAndFound.AgregarLibro(new Libro("Moby Dick", "Herman Melville", 1851, "Aventura"));
            WroteAndFound.AgregarLibro(new Libro("La Metamorfosis", "Franz Kafka", 1915, "Existencialismo"));
            WroteAndFound.AgregarLibro(new Libro("Crimen y castigo", "Fiódor Dostoyevski", 1866, "Filosófica"));
            WroteAndFound.AgregarLibro(new Libro("El Gran Gatsby", "F. Scott Fitzgerald", 1925, "Novela moderna"));
            WroteAndFound.AgregarLibro(new Libro("Harry Potter y la piedra filosofal", "J.K. Rowling", 1997, "Fantasía"));

            // Para saber si se han cargado los libros, y cuantos.
            Logger.Info($"Catálogo inicial cargado con {Libreria.NumeroDeLibros()} libros.");

            Bienvenida();

            Logger.Info("Aplicación WroteAndFound cerrada!!!");
        }

        static void Bienvenida()
        {
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine("------- ¡Bienvenido a la biblioteca Wrote&Found! -------");
            Console.WriteLine("");
            Console.WriteLine("              --------------------");
            Console.WriteLine("              |                | |");
            Console.WriteLine("              |                | |");
            Console.WriteLine("              |      Wrote     | |");
            Console.WriteLine("              |        &       | |");
            Console.WriteLine("              |      Found     | |");
            Console.WriteLine("              |                | |");
            Console.WriteLine("              |                | |");
            Console.WriteLine("              --------------------");
            Console.WriteLine("");
            Console.WriteLine("        Presiona una tecla para continuar");
            Console.ReadKey();

            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine("        Buena suerte entendiendo este código...");
            Console.ReadKey();
            Console.WriteLine("");
            Console.WriteLine("        Ya verás porqué...");
            Console.ReadKey();

            Menus.VerMenu();
        }
    }
}
