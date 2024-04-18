namespace AgendamentoBackend.Model
{
    public class Client
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
    }

    public class Agendamento
    {
        public int Id { get; set; }
        public Service Servico { get; set; }
        public Client Client { get; set; }
        public DateTime Horario { get; set; } = DateTime.Now;
        public DateTime HorarioAgendado { get; set; }
        public string Descricao { get; set; }
    }

    public class Service
    {
        public int Id { get; set; }
        public string NomeService { get; set; }
        public string Descricao { get; set; }
        public int Duracao { get; set; }
        public decimal valor { get; set; }
    }
}
