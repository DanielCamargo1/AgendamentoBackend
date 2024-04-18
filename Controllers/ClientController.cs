using AgendamentoBackend.Data;
using AgendamentoBackend.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgendamentoBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly AgendaDbContext _context;
        public ClientController(AgendaDbContext context)
        {
            _context = context;
        }

        // Cliente
        [HttpGet]
        public async Task<ActionResult<Client>> Getclient()
        {
            var clients = await _context.Client.ToListAsync();
            return Ok(clients);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetclientId(int id)
        {
            var clients = await _context.Client.FindAsync(id);
            return Ok(clients);
        }

        [HttpPost]
        public async Task<ActionResult<Client>> PostClient(Client client)
        {
            try
            {
                var cpf = client.Cpf.ToList();
                if (cpf.Count == 11)
                {
                    _context.Client.Add(client);
                    _context.SaveChanges();
                    return CreatedAtAction("GetClient", new { id = client.Id }, client);

                }
                return BadRequest($"Cpf Inválido, necessita-se ter 11 números digistados. vocêdigitou apenas {cpf.Count}");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Mensagem de erro :" + e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Client>> PutClient(int id, Client client)
        {
            var clients = await _context.Client.FindAsync(id);
            if (clients != null)
            {
                try
                {
                    clients.Nome = client.Nome;
                    clients.Email = client.Email;
                    clients.Cpf = client.Cpf;
                    _context.SaveChanges();
                    return Ok(client);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, "Mensagem de erro :" + ex.Message);
                }
            }
            return BadRequest("id Não encontrado");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Client>> DeleteClients(int id)
        {
            var selectClient = await _context.Client.FindAsync(id);
            _context.Client.Remove(selectClient);
            _context.SaveChanges();
            return Ok(selectClient);
        }
    }
}
