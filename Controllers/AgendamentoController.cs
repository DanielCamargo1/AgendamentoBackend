﻿using AgendamentoBackend.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AgendamentoBackend.Model;

namespace BackEndPlanejadorDeViagem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendamentoController : ControllerBase
    {
        private readonly AgendaDbContext _contextAgenda;
        public AgendamentoController(AgendaDbContext context)
        {
            _contextAgenda = context;
        }
        public Dictionary<string, string> NomeExame = new Dictionary<string, string>();

        [HttpGet]
        public async Task<ActionResult<Agendamento>> GetAgenda()
        {
            // -> Eager Loading
            var clients = await _contextAgenda.Agendamento.Include(a => a.Client).Include(a => a.Servico).ToListAsync();
            foreach(var client in clients)
            {
                if (DataInvalida(client) == null)
                {
                    return client;
                }
            }
            return Ok(clients);    
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Agendamento>> GetAgendaForId(int id)
        {
            var clients = await _contextAgenda.Agendamento.FindAsync(id);
            if(clients != null)
            {
                return Ok(clients);

            }
            else
            {
                return BadRequest("Id não localizado");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Agendamento>> PostAgenda(Agendamento agendamento, DateTime dataAgendamento)
        {
            if (agendamento != null)
            {
                try
                {
                    _contextAgenda.Agendamento.Add(agendamento);
                    await _contextAgenda.SaveChangesAsync();
                    agendamento.HorarioAgendado = dataAgendamento;
                    NomeExame.Add(agendamento.Client.Nome, agendamento.Servico.NomeService);
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

        public async Task<ActionResult<Agendamento>> DataInvalida(Agendamento client)
        {
            DateTime agora = DateTime.Now;
            int diaDeHoje = agora.Day;
            int mesAtual = agora.Month;
            int diaAgendado = client.HorarioAgendado.Day;
            int mesAgendado = client.HorarioAgendado.Month;
            if (diaAgendado < diaDeHoje )
            {
                if(mesAgendado < mesAtual)
                {
                    _contextAgenda.Remove(client);
                    await _contextAgenda.SaveChangesAsync();
                    return client;
                }
            }
            return Ok();
        }
    }
}