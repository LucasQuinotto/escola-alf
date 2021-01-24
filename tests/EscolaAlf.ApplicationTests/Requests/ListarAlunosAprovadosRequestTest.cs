using System.Collections.Generic;
using EscolaAlf.Application.Bean;
using EscolaAlf.Application.Dtos;
using EscolaAlf.Application.Entities;
using EscolaAlf.Application.Requests;
using Xunit;

namespace EscolaAlf.ApplicationTests.Requests
{
    public class ListarAlunosAprovadosRequestTest
    {
        public ListarAlunosAprovadosRequestTest()
        {
            Dados.Provas.Clear();
            Dados.Gabaritos.Clear();
            Dados.Alunos.Clear();
            Dados.Notas.Clear();

            CadastrarProvas();
            CadastarGabaritos();
            var (id1, id2, id3) = CadastrarAlunos();
            CadastrarRespostas(id1, id2, id3);
        }

        [Fact]
        public void Deve_listar_apenas_os_alunos_aprovados()
        {
            var requets = new ListarAlunosAprovadosRequest();
            var alunosAprovados = requets.Executar();

            Assert.Equal(2, alunosAprovados.Count);
            Assert.Equal("FULANO", alunosAprovados[0].Nome);
            Assert.Equal("CICLANO", alunosAprovados[1].Nome);
        }

        private void CadastrarProvas()
        {
            var prova1 = new CadastrarProvaDto
            {
                Questoes = new List<QuestaoDto>
                {
                    new QuestaoDto {Numero = 1, Peso = 1},
                    new QuestaoDto {Numero = 2, Peso = 3}
                }
            };
            var prova2 = new CadastrarProvaDto
            {
                Questoes = new List<QuestaoDto>
                {
                    new QuestaoDto {Numero = 1, Peso = 1},
                    new QuestaoDto {Numero = 2, Peso = 2},
                    new QuestaoDto {Numero = 3, Peso = 3}
                }
            };
            var cadastrarProva = new CadastrarProvaRequest();
            cadastrarProva.Executar(prova1);
            cadastrarProva.Executar(prova2);
        }

        private void CadastarGabaritos()
        {
            var gabarito1 = new CadastrarGabaritoDto
            {
                IdProva = "ALF-1",
                Respostas = new List<RespostaDto>
                {
                    new RespostaDto {NumeroQuestao = 1, Alternativa = Alternativa.A,},
                    new RespostaDto {NumeroQuestao = 2, Alternativa = Alternativa.A,}
                }
            };
            var gabarito2 = new CadastrarGabaritoDto
            {
                IdProva = "ALF-2",
                Respostas = new List<RespostaDto>
                {
                    new RespostaDto {NumeroQuestao = 1, Alternativa = Alternativa.A,},
                    new RespostaDto {NumeroQuestao = 2, Alternativa = Alternativa.A,},
                    new RespostaDto {NumeroQuestao = 3, Alternativa = Alternativa.A,}
                }
            };
            var cadastrarProva = new CadastrarGabaritoRequest();
            cadastrarProva.Executar(gabarito1);
            cadastrarProva.Executar(gabarito2);
        }

        private (string, string, string) CadastrarAlunos()
        {
            var aluno1 = new CadastrarAlunoDto {Nome = "Fulano"};
            var aluno2 = new CadastrarAlunoDto {Nome = "Ciclano"};
            var aluno3 = new CadastrarAlunoDto {Nome = "Beltrano"};

            var cadastrarAluno = new CadastrarAlunoRequest();
            var id1 = cadastrarAluno.Executar(aluno1).Id;
            var id2 = cadastrarAluno.Executar(aluno2).Id;
            var id3 = cadastrarAluno.Executar(aluno3).Id;

            return (id1, id2, id3);
        }

        private void CadastrarRespostas(string id1, string id2, string id3)
        {
            var resposta1 = new CadastrarRespostasAlunoDto
            {
                IdAluno = id1,
                IdProva = "ALF-1",
                Respostas = new List<RespostasAlunoDto>
                {
                    new RespostasAlunoDto {NumeroQuestao = 1,Alternativa = Alternativa.A,},
                    new RespostasAlunoDto {NumeroQuestao = 2,Alternativa = Alternativa.A,},
                }
            };
            var resposta2 = new CadastrarRespostasAlunoDto
            {
                IdAluno = id1,
                IdProva = "ALF-2",
                Respostas = new List<RespostasAlunoDto>
                {
                    new RespostasAlunoDto {NumeroQuestao = 1,Alternativa = Alternativa.A,},
                    new RespostasAlunoDto {NumeroQuestao = 2,Alternativa = Alternativa.A,},
                    new RespostasAlunoDto {NumeroQuestao = 3,Alternativa = Alternativa.A,}
                }
            };
            var resposta3 = new CadastrarRespostasAlunoDto
            {
                IdAluno = id2,
                IdProva = "ALF-1",
                Respostas = new List<RespostasAlunoDto>
                {
                    new RespostasAlunoDto {NumeroQuestao = 1,Alternativa = Alternativa.E,},
                    new RespostasAlunoDto {NumeroQuestao = 2,Alternativa = Alternativa.A,},
                }
            };
            var resposta4 = new CadastrarRespostasAlunoDto
            {
                IdAluno = id2,
                IdProva = "ALF-2",
                Respostas = new List<RespostasAlunoDto>
                {
                    new RespostasAlunoDto {NumeroQuestao = 1,Alternativa = Alternativa.A,},
                    new RespostasAlunoDto {NumeroQuestao = 2,Alternativa = Alternativa.E,},
                    new RespostasAlunoDto {NumeroQuestao = 3,Alternativa = Alternativa.A,},
                }
            };
            var resposta5 = new CadastrarRespostasAlunoDto
            {
                IdAluno = id3,
                IdProva = "ALF-1",
                Respostas = new List<RespostasAlunoDto>
                {
                    new RespostasAlunoDto {NumeroQuestao = 1,Alternativa = Alternativa.A,},
                    new RespostasAlunoDto {NumeroQuestao = 2,Alternativa = Alternativa.E,},
                }
            };
            var resposta6 = new CadastrarRespostasAlunoDto
            {
                IdAluno = id3,
                IdProva = "ALF-2",
                Respostas = new List<RespostasAlunoDto>
                {
                    new RespostasAlunoDto {NumeroQuestao = 1,Alternativa = Alternativa.E,},
                    new RespostasAlunoDto {NumeroQuestao = 2,Alternativa = Alternativa.E,},
                    new RespostasAlunoDto {NumeroQuestao = 3,Alternativa = Alternativa.A,}
                }
            };
            
            var cadastrarRespostas = new CadastrarRespostasAlunoRequest();
            cadastrarRespostas.Executar(resposta1);
            cadastrarRespostas.Executar(resposta2);
            cadastrarRespostas.Executar(resposta3);
            cadastrarRespostas.Executar(resposta4);
            cadastrarRespostas.Executar(resposta5);
            cadastrarRespostas.Executar(resposta6);
        }
    }
}