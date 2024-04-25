using System.ComponentModel.DataAnnotations;

namespace AgendamentoBackend.DTOs.Cliente
{
    public class ClientDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = " Digite um nome")]
        public string Nome { get; set; }
        public string Email { get; set; }
    }
}
