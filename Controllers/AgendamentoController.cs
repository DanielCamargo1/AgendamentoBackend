using AgendamentoBackend.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AgendamentoBackend.Model;
using AgendamentoBackend.Model;

namespace BackEndPlanejadorDeViagem.Controllers
{

    //Agenda

    [Route("api/[controller]")]
    [ApiController]
    public class AgendamentoController : ControllerBase
    {
        private readonly AgendaDbContext _contextAgenda;
        public AgendamentoController(AgendaDbContext context)
        {
            _contextAgenda = context;
        }

        [HttpGet]
        public async Task<ActionResult<Agendamento>> GetAgenda()
        {
            // -> Eager Loading
            var clients = await _contextAgenda.Agendamento.Include(a => a.Client).Include(a => a.Servico).ToListAsync();
            return Ok(clients);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Agendamento>> GetAgendaForId(int id)
        {
            var clients = await _contextAgenda.Agendamento.FindAsync(id);
            return Ok(clients);
        }

        [HttpPost]
        public async Task<ActionResult<Agendamento>> PostAgenda(Agendamento agendamento)
        {
            if (agendamento != null)
            {
                try
                {
                    _contextAgenda.Agendamento.Add(agendamento);
                    await _contextAgenda.SaveChangesAsync();
                    return CreatedAtAction("GetAgenda", new { id = agendamento.Id }, agendamento);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, "Mensagem de erro: " + ex.Message);
                }
            }
            else
            {
                return BadRequest("Agendamento inválido");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Agendamento>> PutAgendamento(int id, Agendamento agendamento)
        {
            try
            {
                var agenda = await _contextAgenda.Agendamento.FindAsync(id);
                agenda.Servico = agendamento.Servico;
                agenda.Client = agendamento.Client;
                agenda.Descricao = agendamento.Descricao;
                await _contextAgenda.SaveChangesAsync();
                return Ok(agendamento);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Agendamento>> DeleteAgendamento(int id)
        {
            try
            {
                var agenda = await _contextAgenda.Agendamento.FindAsync(id);
                _contextAgenda.Agendamento.Remove(agenda);
                _contextAgenda.SaveChanges();
                return (agenda);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}