using System;
using System.Linq;
using EscolaAlf.Application.Bean;
using EscolaAlf.Application.Dtos;
using EscolaAlf.Application.Entities;
using EscolaAlf.Application.Interfaces;

namespace EscolaAlf.Application.Requests
{
    public class CadastrarGabaritoRequest : ICadastrarGabaritoRequest
    {
        public Gabarito Executar(CadastrarGabaritoDto cadastrarGabaritoDto)
        {
            var prova = ValidarProva(cadastrarGabaritoDto);
            ValidarQuestoesRespostas(cadastrarGabaritoDto, prova);
            ValidarAlernativas(cadastrarGabaritoDto);

            var respostas = cadastrarGabaritoDto.Respostas.Select(x => new Respostas
            {
                NumeroQuestao = x.NumeroQuestao,
                Alternativa = x.Alternativa,
            }).ToList();

            var gabarito = new Gabarito
            {
                IdProva = cadastrarGabaritoDto.IdProva,
                Respostas = respostas
            };
            Dados.Gabaritos.Add(gabarito);

            return gabarito;
        }

        private Prova ValidarProva(CadastrarGabaritoDto gabaritoDto)
        {
            var gabaritosVinculados = Dados.Gabaritos
                .Where(x => gabaritoDto.IdProva == x.IdProva).ToList();

            if (gabaritosVinculados.Any())
                throw new Exception($"Gabarito já cadastrado para a prova {gabaritoDto.IdProva}");

            var prova = Dados.Provas.FirstOrDefault(x => gabaritoDto.IdProva == x.Id);

            if (prova == null)
                throw new Exception($"Prova {gabaritoDto.IdProva} não encontrada");

            return prova;
        }

        private void ValidarQuestoesRespostas(CadastrarGabaritoDto gabaritoDto, Prova prova)
        {
            var numerosQuestoes = gabaritoDto.Respostas.Select(x => x.NumeroQuestao).ToList();
            var numerosQuestoesDistintos = numerosQuestoes.Distinct().ToList();

            if (numerosQuestoes.Count != numerosQuestoesDistintos.Count)
                throw new Exception("Numero da questão não pode ser duplicado");

            var numerosCorrespondetes = numerosQuestoesDistintos
                .Select(numeroQuestao => prova.Questoes
                    .Select(questao => questao.Numero)
                    .Contains(numeroQuestao));

            if (numerosCorrespondetes.Any(contains => !contains))
                throw new Exception("Questão não encontrada para resposta");

            if (prova.Questoes.Count != gabaritoDto.Respostas.Count)
                throw new Exception("Quantidade de respostas não correspondente");
        }

        private void ValidarAlernativas(CadastrarGabaritoDto gabaritoDto)
        {
            var alternativasInvalidas = gabaritoDto.Respostas
                .Where(x => Enum.IsDefined(typeof(Alternativa), x.Alternativa) == false)
                .ToList();

            if (alternativasInvalidas.Any())
                throw new Exception("Alternativa inválida");
        }
    }
}