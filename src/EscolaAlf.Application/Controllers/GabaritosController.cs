using System;
using System.Linq;
using EscolaAlf.Application.Bean;
using EscolaAlf.Application.Entities;
using EscolaAlf.Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace EscolaAlf.Application.Controllers
{
    public class GabaritosController : ApiController
    {
        [HttpPost]
        public Gabarito CadastrarGabarito([FromBody] CadastrarGabaritoRequest request)
        {
            var gabaritosVinculados = Dados.Gabaritos.Where(x => request.IdProva == x.IdProva).ToList();

            if (gabaritosVinculados.Any())
                throw new Exception($"Gabarito já cadastrado para a prova {request.IdProva}");

            var prova = Dados.Provas.FirstOrDefault(x => request.IdProva == x.Id);

            if (prova == null)
                throw new Exception($"Prova {request.IdProva} não encontrada");

            var numerosQuestoes = request.Respostas.Select(x => x.NumeroQuestao).ToList();
            var numerosQuestoesDistintos = numerosQuestoes.Distinct().ToList();

            if (numerosQuestoes.Count != numerosQuestoesDistintos.Count)
                throw new Exception("Numero da questão não pode ser duplicado");

            var numerosCorrespondetes = numerosQuestoesDistintos
                .Select(numeroQuestao => prova.Questoes
                    .Select(questao => questao.Numero)
                    .Contains(numeroQuestao));

            if (numerosCorrespondetes.Any(contains => !contains))
                throw new Exception("Questão não encontrada para resposta");

            if (prova.Questoes.Count != request.Respostas.Count)
                throw new Exception("Quantidade de respostas não correspondente");

            var alternativasInvalidas = request.Respostas
                .Where(x => Enum.IsDefined(typeof(Alternativa), x.Alternativa) == false)
                .ToList();

            if (alternativasInvalidas.Any())
                throw new Exception("Alternativa inválida");

            var respostas = request.Respostas.Select(x => new Respostas
            {
                NumeroQuestao = x.NumeroQuestao,
                Alternativa = x.Alternativa,
            }).ToList();

            var gabarito = new Gabarito
            {
                IdProva = request.IdProva,
                Respostas = respostas
            };
            Dados.Gabaritos.Add(gabarito);

            return gabarito;
        }
    }
}