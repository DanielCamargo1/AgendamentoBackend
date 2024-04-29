using System.ComponentModel.DataAnnotations;

namespace AgendamentoBackend.Model
{
   
    [Serializable]
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
    
}
