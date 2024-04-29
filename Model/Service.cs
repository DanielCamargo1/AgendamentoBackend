using System.ComponentModel.DataAnnotations;

namespace AgendamentoBackend.Model
{
    [Serializable]
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
