using System;
using System.Linq;
using EscolaAlf.Application.Bean;
using EscolaAlf.Application.Dtos;
using EscolaAlf.Application.Entities;
using EscolaAlf.Application.Interfaces;

namespace EscolaAlf.Application.Requests
{
    public class CadastrarRespostasAlunoRequest : ICadastrarRespostasAlunoRequest
    {
        public NotaAlunoProva Executar(CadastrarRespostasAlunoDto respostasAlunoDto)
        {
            var respostasAlunoOrdenadas = respostasAlunoDto.Respostas
                .OrderBy(x => x.NumeroQuestao).ToList();

            var (prova, gabarito) = ValidarProvaGabaritoAluno(respostasAlunoDto);
            ValidarRespostas(respostasAlunoDto, prova);
            ValidarAlernativas(respostasAlunoDto);

            var notaFinalAlunoProva = 0d;

            foreach (var respostaGabarito in gabarito.Respostas)
            {
                var respostaAluno = respostasAlunoOrdenadas
                    .First(x => x.NumeroQuestao == respostaGabarito.NumeroQuestao);

                var questaoProva = prova.Questoes
                    .First(x => x.Numero == respostaGabarito.NumeroQuestao);

                if (respostaGabarito.Alternativa == respostaAluno.Alternativa)
                    notaFinalAlunoProva += prova.ValorPeso * questaoProva.Peso;
            }

            var notaAlunoProva = new NotaAlunoProva
            {
                IdProva = respostasAlunoDto.IdProva,
                IdAluno = respostasAlunoDto.IdAluno,
                NotaProva = notaFinalAlunoProva,
            };
            Dados.Notas.Add(notaAlunoProva);

            return notaAlunoProva;
        }

        private (Prova, Gabarito) ValidarProvaGabaritoAluno(CadastrarRespostasAlunoDto respostasAlunoDto)
        {
            var prova = Dados.Provas.FirstOrDefault(x => x.Id == respostasAlunoDto.IdProva);

            if (prova == null)
                throw new Exception("Prova não encontrada");

            var gabarito = Dados.Gabaritos.FirstOrDefault(x => x.IdProva == respostasAlunoDto.IdProva);

            if (gabarito == null)
                throw new Exception($"Gabarito não cadastrado para a prova {respostasAlunoDto.IdProva}");

            var aluno = Dados.Alunos.FirstOrDefault(x => x.Id == respostasAlunoDto.IdAluno);

            if (aluno == null)
                throw new Exception("Aluno não encontrado");

            var notas = Dados.Notas.FirstOrDefault(x =>
                x.IdProva == respostasAlunoDto.IdProva && x.IdAluno == respostasAlunoDto.IdAluno);

            if (notas != null)
                throw new Exception($"Resposta do aluno {aluno.Nome} para a prova {prova.Id} já cadastrada");

            return (prova, gabarito);
        }

        private void ValidarRespostas(CadastrarRespostasAlunoDto respostasAlunoDto, Prova prova)
        {
            var numerosQuestoes = respostasAlunoDto.Respostas
                .Select(x => x.NumeroQuestao).ToList();
            var numerosQuestoesDistintos = numerosQuestoes.Distinct().ToList();

            if (numerosQuestoes.Count != numerosQuestoesDistintos.Count)
                throw new Exception("Numero da resposta não pode ser duplicado");

            var numerosCorrespondetes = numerosQuestoesDistintos
                .Select(numeroQuestao => prova.Questoes
                    .Select(questao => questao.Numero)
                    .Contains(numeroQuestao));

            if (numerosCorrespondetes.Any(contains => !contains))
                throw new Exception("Questão não encontrada para resposta");

            if (prova.Questoes.Count != respostasAlunoDto.Respostas.Count)
                throw new Exception("Quantidade de respostas não correspondente");
        }

        private void ValidarAlernativas(CadastrarRespostasAlunoDto respostasAlunoDto)
        {
            var alternativasInvalidas = respostasAlunoDto.Respostas
                .Where(x => Enum.IsDefined(typeof(Alternativa), x.Alternativa) == false)
                .ToList();

            if (alternativasInvalidas.Any())
                throw new Exception("Alternativa inválida");
        }
    }
}