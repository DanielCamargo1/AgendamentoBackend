using AgendamentoBackend.Data;
using AgendamentoBackend.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgendamentoBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicosController : ControllerBase
    {
        private readonly AgendaDbContext _context;
        public ServicosController(AgendaDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<Service>> GetServices()
        {
            var conteudo = _context.Servico.ToList();
            if (conteudo.Count > 0)
            {
                return Ok(conteudo);
            }
            return BadRequest("não possui itens");
        }

        [HttpPost]
        public async Task<ActionResult<Service>> PostService(Service service)
        {
            var Oservice = _context.Servico.ToList();
            if (Oservice is object)
            {
                _context.Servico.Add(service);
                _context.SaveChanges();
                return CreatedAtAction("GetServices", new { id = service.Id }, service);
            }
            return BadRequest("Erro, verifique e tente novamente");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Service>> PutServices(int id, Service service)
        {
            var services = await _context.Servico.FindAsync(id);
            if (services != null)
            {
                try
                {
                    services.Nome = service.Nome;
                    services.valor = service.valor;
                    services.Duracao = service.Duracao;
                    services.Descricao = service.Descricao;

                    _context.SaveChanges();
                    return Ok(service);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, "Mensagem de erro :" + ex.Message);
                }
            }
            return BadRequest("id Não encontrado");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Service>> DeleteClients(int id)
        {
            var selectservice = await _context.Client.FindAsync(id);
            _context.Client.Remove(selectservice);
            _context.SaveChanges();
            return Ok(selectservice);
        }
    }
}
