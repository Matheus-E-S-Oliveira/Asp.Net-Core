namespace GerenciadorTarefas.Models;

public class Tarefa
{
    public int Id { get; set;}
    public DateTime Data { get; set; }
    public string Titulo { get; set;}
    public bool Completo { get; set; }

    public Tarefa(DateTime data, string titulo, bool completo = false)
    {
        Data = data;
        Titulo = titulo;
        Completo = completo;
    }

    public Tarefa(int id, DateTime data, string titulo, bool completo = false)
    {
        Id = id;
        Data = data;
        Titulo = titulo;
        Completo = completo;
    }
 }