class Libro{

    public string Titulo {get; private set;}
    public string Autor {get; private set;}
    public int Anyo {get; private set;}
    public string Genero {get; private set;}
    public bool Disponible {get; set;}

    public List<Resenya> Resenyas = new List<Resenya>();

    // Constructor de Libro
    public Libro(string titulo, string autor = "Desconocido", int anyo = 0, string genero = "Desconocido")
    {
        this.Titulo = titulo;
        this.Autor = autor;
        this.Anyo = anyo;
        this.Genero = genero;
        this.Disponible = true;
        Resenyas = new List<Resenya>();
    }
}
