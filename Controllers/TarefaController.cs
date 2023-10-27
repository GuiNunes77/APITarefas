using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tarefas.Infra;
using Tarefas.Models;

namespace Tarefas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefaController : ControllerBase
    {
        private readonly ConnectionContext _context;

        public TarefaController(ConnectionContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tarefa>> ObterPorId(int id)
        {
            if (_context.Tarefas == null)
            {
                return NotFound();
            }
            var tarefa = await _context.Tarefas.FindAsync(id);

            if (tarefa == null)
            {
                return NotFound();
            }

            return tarefa;
        }

        [HttpGet("ObterTodos")]
        public async Task<ActionResult<IEnumerable<Tarefa>>> ObterTodas()
        {
          if (_context.Tarefas == null)
          {
              return NotFound();
          }
            return await _context.Tarefas.ToListAsync();
        }

        [HttpGet("ObterPorTitulo")]
        public async Task<ActionResult<IEnumerable<Tarefa>>> ObterPorTitulo (string titulo)
        {
            var tarefas = _context.Tarefas.Where(x => x.titulo.Contains(titulo)).ToList();
            return Ok(tarefas);
        }

        [HttpGet("ObterPorData")]
        public async Task<ActionResult<IEnumerable<Tarefa>>> ObterPorData (DateTime data)
        {
            var tarefas = _context.Tarefas.Where(x => x.data.Date == data.Date).ToList();
            return Ok(tarefas);
        }

        [HttpGet("ObterPorStatus")]
        public async Task<ActionResult<IEnumerable<Tarefa>>> ObterPorStatus (string status)
        {
            var tarefas = _context.Tarefas.Where(x => x.status == status).ToList();
            return Ok(tarefas);
        }

        [HttpPost]
        public async Task<ActionResult<Tarefa>> Criar(Tarefa tarefa)
        {
            if (tarefa.data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            _context.Tarefas.Add(tarefa);
            _context.SaveChanges();
            return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.id }, tarefa);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTarefa(int id, Tarefa tarefa)
        {
            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco == null)
                return NotFound();

            if (tarefa.data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            tarefaBanco.titulo = tarefa.titulo;
            tarefaBanco.descricao = tarefa.descricao;
            tarefaBanco.data = tarefa.data;
            tarefaBanco.status = tarefa.status;

            _context.Tarefas.Update(tarefaBanco);
            _context.SaveChanges();

            return Ok();
        }

        // DELETE: api/Tarefa/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarefa(int id)
        {
            if (_context.Tarefas == null)
            {
                return NotFound();
            }
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa == null)
            {
                return NotFound();
            }

            _context.Tarefas.Remove(tarefa);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
