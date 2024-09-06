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

    public IActionResult Delete(int id)
    {
        var tarefa = _context.Tarefas.Find(id);
        if(tarefa == null){
            return NotFound();
        }
        _context.Tarefas.Remove(tarefa);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Create()
    {
        ViewData["Title"] = "Cadastrar Tarefa";
        return View("Form");
    }

    [HttpPost]
    public IActionResult Create(FormTarefaViewModel date)
    {
        var tarefa = new Tarefa(date.Data, date.Titulo);
        _context.Add(tarefa);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(int id)
    {
        var tarefa = _context.Tarefas.Find(id);
        if(tarefa == null){
            return NotFound();
        }
        var viewModel = new FormTarefaViewModel { Titulo = tarefa.Titulo, Data = tarefa.Data};
        ViewData["Title"] = "Editar Tarefa";
        return View("Form", viewModel);
    }

    [HttpPost]
    public IActionResult Edit(int id, FormTarefaViewModel date){
        var tarefa = _context.Tarefas.Find(id);
        if(tarefa == null)
        {
            return NotFound();
        }
        tarefa.Titulo = date.Titulo;
        tarefa.Data = date.Data;
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    public IActionResult ToComplete(int id)
    {
        var tarefa = _context.Tarefas.Find(id);
        if(tarefa == null)
        {
            return NotFound();
        }
        tarefa.Completo = true;
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
}
