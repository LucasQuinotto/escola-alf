using System;
using System.Collections.Generic;
using System.Linq;
using EscolaAlf.Application.Bean;
using EscolaAlf.Application.Entities;
using EscolaAlf.Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace EscolaAlf.Application.Controllers
{
    public class ProvasController : ApiController
    {
        [HttpPost]
        public IList<Prova> CadastrarProva([FromBody] CadastrarProvaRequest request)
        {
            Console.WriteLine("Cadastrando prova...");

            var questoes = request.Questoes;

            var questoesValidadas = ValidarQuestoes(questoes)
                .Select(questao => new Questao
                {
                    Numero = questao.Numero,
                    Peso = questao.Peso
                }).ToList();

            Dados.Provas.Add(new Prova
            {
                Id = GerarIdExterno(),
                Questoes = questoesValidadas,
            });
            
            Console.WriteLine("Prova cadastrada com sucesso");

            return Dados.Provas;
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
            var ultimaQuestao = questoesOrdenadas.LastOrDefault();

            if (questoes.Count != ultimaQuestao?.Numero)
                throw new Exception("Nem todos os numeros de questões foram encontrados");

            return questoesOrdenadas;
        }

        public string GerarIdExterno() => $"ALF-{Dados.Provas.Count + 1}";
    }
}