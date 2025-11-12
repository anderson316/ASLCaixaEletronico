namespace ASLCaixaEletronico.Service
{
    public class SaqueService
    {
        private readonly decimal[] notasDisponiveis = new decimal[] { 200, 100, 50, 20, 10, 5, 2 };

        public List<Dictionary<decimal, int>> CombinacoesNotas(decimal valorSaque)
        {
            if (valorSaque <= 0)
                throw new ArgumentException("O valor do saque deve ser maior que zero.");
            if (valorSaque % 1 != 0)
                throw new ArgumentException("O valor informado não pode ser distruibuido pelas cedulas disponiveis");

            var resultados = new List<Dictionary<decimal, int>>();
            CalcularCombinacoes(valorSaque, 0, new Dictionary<decimal, int>(), resultados);
            return resultados.OrderBy(r => r.Sum(x => x.Value)).Take(3).ToList();
        }

        private void CalcularCombinacoes(decimal valorRestante, int indiceNota, Dictionary<decimal, int> combinacaoAtual, List<Dictionary<decimal, int>> resultados)
        {
            if (valorRestante == 0)
            {
                resultados.Add(new Dictionary<decimal, int>(combinacaoAtual));
                return;
            }
            if (indiceNota >= notasDisponiveis.Length)
                return;
            decimal notaAtual = notasDisponiveis[indiceNota];
            int quantidadeMaxima = (int)(valorRestante / notaAtual);
            for (int quantidade = quantidadeMaxima; quantidade >= 0; quantidade--)
            {
                if (quantidade > 0)
                    combinacaoAtual[notaAtual] = quantidade;
                CalcularCombinacoes(valorRestante - (quantidade * notaAtual), indiceNota + 1, combinacaoAtual, resultados);
                if (quantidade > 0)
                    combinacaoAtual.Remove(notaAtual);
            }
        }
    }
}
