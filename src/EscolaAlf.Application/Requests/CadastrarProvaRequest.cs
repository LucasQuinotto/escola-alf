using System;
using System.Collections.Generic;
using System.Linq;
using EscolaAlf.Application.Bean;
using EscolaAlf.Application.Dtos;
using EscolaAlf.Application.Entities;
using EscolaAlf.Application.Interfaces;

namespace EscolaAlf.Application.Requests
{
    public class CadastrarProvaRequest : ICadastrarProvaRequest
    {
        public Prova Executar(CadastrarProvaDto cadastrarProvaDto)
        {
            Console.WriteLine("Cadastrando prova...");

            var questoes = cadastrarProvaDto.Questoes;

            var questoesValidadas = ValidarQuestoes(questoes)
                .Select(questao => new Questao
                {
                    Numero = questao.Numero,
                    Peso = questao.Peso
                }).ToList();

            var pesoTotalProva = questoesValidadas.Sum(questaoProva => questaoProva.Peso);
            var valorPeso = (double) 10 / pesoTotalProva;

            var prova = new Prova
            {
                Id = GerarIdExterno(),
                ValorPeso = valorPeso,
                Questoes = questoesValidadas,
            };
            Dados.Provas.Add(prova);

            Console.WriteLine("Prova cadastrada com sucesso");
            return prova;
        }

        private IEnumerable<QuestaoDto> ValidarQuestoes(IList<QuestaoDto> questoes)
        {
            if (questoes.Select(x => x.Peso).ToList().Exists(x => x == 0))
                throw new Exception("Peso da questão não pode ser zero");

            var numerosQuestoes = questoes.Select(x => x.Numero).ToList();
            var numerosQuestoesDistintos = numerosQuestoes.Distinct().ToList();

            if (numerosQuestoes.Count != numerosQuestoesDistintos.Count)
                throw new Exception("Numero da questão não pode ser duplicado");

            var questoesOrdenadas = questoes.OrderBy(x => x.Numero).ToList();
            var primeiraQuestao = questoesOrdenadas.FirstOrDefault();

            if (primeiraQuestao?.Numero < 1)
                throw new Exception("A sequencia de questões deve começar em 1");

            var ultimaQuestao = questoesOrdenadas.LastOrDefault();

            if (questoes.Count != ultimaQuestao?.Numero)
                throw new Exception("Nem todos os numeros de questões foram encontrados");

            return questoesOrdenadas;
        }

        private string GerarIdExterno() => $"ALF-{Dados.Provas.Count + 1}";
    }
}