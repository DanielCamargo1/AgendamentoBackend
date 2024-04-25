using System.ComponentModel.DataAnnotations;

namespace AgendamentoBackend.Model
{
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

    public class Agendamento
    {
        public int Id { get; set; }
        public Service Servico { get; set; }
        public Client Client { get; set; }
        public DateTime Horario { get; set; } = DateTime.Now;
        [Required(ErrorMessage = " Digite um Horario Para Agendamento")]
        public DateTime HorarioAgendado { get; set; }
        public string Descricao { get; set; }
    }

    public class Service
    {
        public int Id { get; set; }
        [Required(ErrorMessage = " Digite um Nome Para  o serviço")]
        public string NomeService { get; set; }
        public string Descricao { get; set; }
        [Required(ErrorMessage = " Informe a duração")]
        public decimal Duracao { get; set; }
        [Required(ErrorMessage = " Digite um Valor")]
        public decimal valor { get; set; }
    }
}
