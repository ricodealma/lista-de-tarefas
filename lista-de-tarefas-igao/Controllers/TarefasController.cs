using lista_de_tarefas_igao.Models;
using Microsoft.AspNetCore.Mvc;

namespace lista_de_tarefas_igao.Controllers
{
    public class TarefasController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult Index_SelecionarFiltro(string filtroNome, bool filtroCompleta)
        {
            Tarefa tarefa = new Tarefa();
            List<Tarefa> tarefas = new List<Tarefa>();

            if (string.IsNullOrEmpty(filtroNome) && filtroCompleta == false)
            {
                tarefas = tarefa.SelectTarefas();
            }
            else if (string.IsNullOrEmpty(filtroNome) && filtroCompleta == true)
            {
                tarefas = tarefa.SelectTarefasCompletas(filtroCompleta);
            }
            else if (!string.IsNullOrEmpty(filtroNome) && filtroCompleta == false)
            {
                tarefas = tarefa.SelectTarefasFiltradas(filtroNome);
            }
            else if (!string.IsNullOrEmpty(filtroNome) && filtroCompleta == true)
            {
                tarefas = tarefa.SelectTarefasCompletasFiltradas(filtroCompleta, filtroNome);
            }

            return Json(new JsonResult(tarefas)); 
        }
        public ActionResult Delete([FromRoute]int id)
        {
            Tarefa tarefa = new Tarefa();
            tarefa.DeleteTarefaById(id);
            return RedirectToAction("Index");
        }
        public ActionResult Editar([FromRoute] int? id) 
        {
            Tarefa tarefa = new Tarefa();
            if (id.HasValue)
            {
                int idInt = id.GetValueOrDefault();
                tarefa = tarefa.SelectTarefaById(idInt);
            }            

            return View(tarefa);
        }
        [HttpPost]
        public JsonResult Editar(Tarefa tarefa)
        {
            if (tarefa.Id != 0)
            {
                tarefa = new Tarefa { Id = tarefa.Id, Nome = tarefa.Nome, Descricao = tarefa.Descricao, Completa = tarefa.Completa };
                tarefa.UpdateTarefaById(tarefa);
            }
            else
            {
                tarefa = new Tarefa { Nome = tarefa.Nome, Descricao = tarefa.Descricao, Completa = tarefa.Completa };
                tarefa.CreateTarefa(tarefa);
            }

            return Json(new JsonResult(tarefa));
        }

    }
}
