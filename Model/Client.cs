using System.ComponentModel.DataAnnotations;
namespace AgendamentoBackend.Model
{
    [Serializable]
    public class Client
    {
        public int Id { get; set; }
        [Required(ErrorMessage = " Digite um nome")]
        public string Nome { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = " Digite um Cpf")]
        [StringLength(11)]
        public string Cpf { get; set; }
    }
}
