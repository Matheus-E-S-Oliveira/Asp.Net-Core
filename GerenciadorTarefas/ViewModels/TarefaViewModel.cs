using GerenciadorTarefas.Models;

namespace GerenciadorTarefas.ViewModels;

public class TarefaViewModel
{
    public ICollection<Tarefa> Tarefas{ get; set; } = new List<Tarefa>();
}
