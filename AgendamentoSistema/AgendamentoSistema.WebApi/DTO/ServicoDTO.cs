namespace AgendamentoSistema.WebApi.DTO
{
    public class ServicoDTO
    {
        public string NomeServico { get; set; }
        public string Descricao { get; set; }
        public TimeSpan DuracaoMinutos { get; set; }

        public decimal Preco {  get; set; }
    }
}
