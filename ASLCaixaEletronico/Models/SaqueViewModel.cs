namespace ASLCaixaEletronico.Models
{
    public class SaqueViewModel
    {
        public decimal Valor{ get; set; }

        public List<Dictionary<decimal, int>>? CombinacoesDetalhadas { get; set; }

        public string? MensagemErro { get; set; }
    }
}
