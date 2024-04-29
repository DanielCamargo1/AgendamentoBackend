using AgendamentoBackend.Data;
using AgendamentoBackend.DTOs.Cliente;
using AgendamentoBackend.Mapping;
using AgendamentoBackend.Model;
using AutoMapper;
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
        private readonly IMapper _mapper;
        public ClientController(AgendaDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;   
        }

        public List<string> Nomes = new List<string>(); // -> Esta aqui para ser usado no PDF

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
            if(clients == null)
            {
                   return BadRequest("Cliente não encontrado");
            }
            var clientsDto = _mapper.Map<ClientDTO>(clients);

            return Ok(clientsDto);
        }

        [HttpPost]
        public async Task<ActionResult<Client>> PostClient(Client client)
        {
            try
            {
                _context.Client.Add(client);
                _context.SaveChanges();
                Nomes.Add(client.Nome);
                return CreatedAtAction("GetClient", new { id = client.Id }, client);
            }
            catch 
            {
                return BadRequest("Ocorreu um erro");
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
