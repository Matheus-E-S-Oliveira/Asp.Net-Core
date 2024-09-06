using GerenciadorTarefas.Context;
using GerenciadorTarefas.Models;
using GerenciadorTarefas.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorTarefas.Controllers;

public class TarefaController : Controller
{
    private readonly AppDbContext _context;
    public TarefaController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var tarefa = _context.Tarefas.ToList();
        var viewModel = new TarefaViewModel { Tarefas = tarefa };
        ViewData["Title"] = "Lista de Tarefas"; 
        return View(viewModel);
    }
}