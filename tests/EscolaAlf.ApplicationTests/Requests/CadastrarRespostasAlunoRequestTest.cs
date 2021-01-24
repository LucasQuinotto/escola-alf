using System;
using System.Collections.Generic;
using EscolaAlf.Application.Bean;
using EscolaAlf.Application.Dtos;
using EscolaAlf.Application.Entities;
using EscolaAlf.Application.Requests;
using Xunit;

namespace EscolaAlf.ApplicationTests.Requests
{
    public class CadastrarRespostasAlunoRequestTest
    {
        public CadastrarRespostasAlunoRequestTest()
        {
            Dados.Provas.Clear();
            Dados.Gabaritos.Clear();
            Dados.Alunos.Clear();
            Dados.Notas.Clear();
        }

        [Fact]
        public void Deve_ocorrer_exception_caso_prova_nao_for_encontrada()
        {
            var cadastrarRespostasDto = new CadastrarRespostasAlunoDto
            {
                IdAluno = "1",
                IdProva = "ALF-1",
                Respostas = new List<RespostasAlunoDto>()
            };
            var request = new CadastrarRespostasAlunoRequest();

            Func<NotaAlunoProva> func = () => request.Executar(cadastrarRespostasDto);

            Assert.Throws<Exception>(func);
            Assert.Equal(
                "Prova não encontrada",
                Assert.Throws<Exception>(func).Message
            );
            Assert.Equal(0, Dados.Notas.Count);
        }

        [Fact]
        public void Deve_ocorrer_exception_caso_gabarito_nao_for_encontrado()
        {
            Dados.Provas.Add(new Prova
            {
                Id = "ALF-1",
                ValorPeso = 30d,
                Questoes = new List<Questao>()
            });
            var cadastrarRespostasDto = new CadastrarRespostasAlunoDto
            {
                IdAluno = "1",
                IdProva = "ALF-1",
                Respostas = new List<RespostasAlunoDto>()
            };
            var request = new CadastrarRespostasAlunoRequest();

            Func<NotaAlunoProva> func = () => request.Executar(cadastrarRespostasDto);

            Assert.Throws<Exception>(func);
            Assert.Equal(
                "Gabarito não cadastrado para a prova ALF-1",
                Assert.Throws<Exception>(func).Message
            );
            Assert.Equal(0, Dados.Notas.Count);
        }

        [Fact]
        public void Deve_ocorrer_exception_caso_aluno_nao_for_encontrado()
        {
            Dados.Provas.Add(new Prova
            {
                Id = "ALF-1",
                ValorPeso = 30d,
                Questoes = new List<Questao>()
            });
            Dados.Gabaritos.Add(new Gabarito
            {
                IdProva = "ALF-1",
                Respostas = new List<Respostas>()
            });
            var cadastrarRespostasDto = new CadastrarRespostasAlunoDto
            {
                IdAluno = "1",
                IdProva = "ALF-1",
                Respostas = new List<RespostasAlunoDto>()
            };
            var request = new CadastrarRespostasAlunoRequest();

            Func<NotaAlunoProva> func = () => request.Executar(cadastrarRespostasDto);

            Assert.Throws<Exception>(func);
            Assert.Equal(
                "Aluno não encontrado",
                Assert.Throws<Exception>(func).Message
            );
            Assert.Equal(0, Dados.Notas.Count);
        }

        [Fact]
        public void Deve_ocorrer_exception_caso_resposta_do_aluno_ja_foi_cadastrada()
        {
            Dados.Provas.Add(new Prova
            {
                Id = "ALF-1",
                ValorPeso = 30d,
                Questoes = new List<Questao>()
            });
            Dados.Gabaritos.Add(new Gabarito
            {
                IdProva = "ALF-1",
                Respostas = new List<Respostas>()
            });
            Dados.Alunos.Add(new Aluno
            {
                Id = "e1f2cc2e-ead1-4478-9017-ef29d546287c",
                Nome = "FULANO"
            });
            Dados.Notas.Add(new NotaAlunoProva
            {
                IdProva = "ALF-1",
                IdAluno = "e1f2cc2e-ead1-4478-9017-ef29d546287c",
                NotaProva = 10d
            });
            var cadastrarRespostasDto = new CadastrarRespostasAlunoDto
            {
                IdAluno = "e1f2cc2e-ead1-4478-9017-ef29d546287c",
                IdProva = "ALF-1",
                Respostas = new List<RespostasAlunoDto>()
            };
            var request = new CadastrarRespostasAlunoRequest();

            Func<NotaAlunoProva> func = () => request.Executar(cadastrarRespostasDto);

            Assert.Throws<Exception>(func);
            Assert.Equal(
                "Resposta do aluno FULANO para a prova ALF-1 já cadastrada",
                Assert.Throws<Exception>(func).Message
            );
            Assert.Equal(1, Dados.Notas.Count);
        }

        [Fact]
        public void Deve_ocorrer_exception_caso_haja_respostas_duplicadas()
        {
            Dados.Provas.Add(new Prova
            {
                Id = "ALF-1",
                ValorPeso = 30d,
                Questoes = new List<Questao>()
            });
            Dados.Gabaritos.Add(new Gabarito
            {
                IdProva = "ALF-1",
                Respostas = new List<Respostas>()
            });
            Dados.Alunos.Add(new Aluno
            {
                Id = "e1f2cc2e-ead1-4478-9017-ef29d546287c",
                Nome = "FULANO"
            });
            var cadastrarRespostasDto = new CadastrarRespostasAlunoDto
            {
                IdAluno = "e1f2cc2e-ead1-4478-9017-ef29d546287c",
                IdProva = "ALF-1",
                Respostas = new List<RespostasAlunoDto>
                {
                    new RespostasAlunoDto {NumeroQuestao = 1,Alternativa = Alternativa.A},
                    new RespostasAlunoDto {NumeroQuestao = 1,Alternativa = Alternativa.A}
                }
            };
            var request = new CadastrarRespostasAlunoRequest();

            Func<NotaAlunoProva> func = () => request.Executar(cadastrarRespostasDto);

            Assert.Throws<Exception>(func);
            Assert.Equal(
                "Numero da resposta não pode ser duplicado",
                Assert.Throws<Exception>(func).Message
            );
            Assert.Equal(0, Dados.Notas.Count);
        }

        [Fact]
        public void Deve_ocorrer_exception_caso_nao_haja_questao_para_as_respostas_informadas()
        {
            Dados.Provas.Add(new Prova
            {
                Id = "ALF-1",
                ValorPeso = 30d,
                Questoes = new List<Questao>
                {
                    new Questao {Numero = 1,Peso = 1}
                }
            });
            Dados.Gabaritos.Add(new Gabarito
            {
                IdProva = "ALF-1",
                Respostas = new List<Respostas>
                {
                    new Respostas {NumeroQuestao = 1,Alternativa = Alternativa.A}
                }
            });
            Dados.Alunos.Add(new Aluno
            {
                Id = "e1f2cc2e-ead1-4478-9017-ef29d546287c",
                Nome = "FULANO"
            });
            var cadastrarRespostasDto = new CadastrarRespostasAlunoDto
            {
                IdAluno = "e1f2cc2e-ead1-4478-9017-ef29d546287c",
                IdProva = "ALF-1",
                Respostas = new List<RespostasAlunoDto>
                {
                    new RespostasAlunoDto {NumeroQuestao = 1,Alternativa = Alternativa.A},
                    new RespostasAlunoDto {NumeroQuestao = 2,Alternativa = Alternativa.A}
                }
            };
            var request = new CadastrarRespostasAlunoRequest();

            Func<NotaAlunoProva> func = () => request.Executar(cadastrarRespostasDto);

            Assert.Throws<Exception>(func);
            Assert.Equal(
                "Questão não encontrada para resposta",
                Assert.Throws<Exception>(func).Message
            );
            Assert.Equal(0, Dados.Notas.Count);
        }

        [Fact]
        public void Deve_ocorrer_exception_caso_nao_seja_informado_todas_as_respostas()
        {
            Dados.Provas.Add(new Prova
            {
                Id = "ALF-1",
                ValorPeso = 30d,
                Questoes = new List<Questao>
                {
                    new Questao {Numero = 1,Peso = 1},
                    new Questao {Numero = 2,Peso = 1}
                }
            });
            Dados.Gabaritos.Add(new Gabarito
            {
                IdProva = "ALF-1",
                Respostas = new List<Respostas>
                {
                    new Respostas {NumeroQuestao = 1,Alternativa = Alternativa.A},
                    new Respostas {NumeroQuestao = 2,Alternativa = Alternativa.B}
                }
            });
            Dados.Alunos.Add(new Aluno
            {
                Id = "e1f2cc2e-ead1-4478-9017-ef29d546287c",
                Nome = "FULANO"
            });
            var cadastrarRespostasDto = new CadastrarRespostasAlunoDto
            {
                IdAluno = "e1f2cc2e-ead1-4478-9017-ef29d546287c",
                IdProva = "ALF-1",
                Respostas = new List<RespostasAlunoDto>
                {
                    new RespostasAlunoDto {NumeroQuestao = 1,Alternativa = Alternativa.A},
                }
            };
            var request = new CadastrarRespostasAlunoRequest();

            Func<NotaAlunoProva> func = () => request.Executar(cadastrarRespostasDto);

            Assert.Throws<Exception>(func);
            Assert.Equal(
                "Quantidade de respostas não correspondente",
                Assert.Throws<Exception>(func).Message
            );
            Assert.Equal(0, Dados.Notas.Count);
        }

        [Fact]
        public void Deve_ocorrer_exception_caso_alternativa_informada_nao_exista()
        {
            Dados.Provas.Add(new Prova
            {
                Id = "ALF-1",
                ValorPeso = 30d,
                Questoes = new List<Questao>
                {
                    new Questao {Numero = 1,Peso = 1},
                }
            });
            Dados.Gabaritos.Add(new Gabarito
            {
                IdProva = "ALF-1",
                Respostas = new List<Respostas>
                {
                    new Respostas {NumeroQuestao = 1,Alternativa = Alternativa.A},
                }
            });
            Dados.Alunos.Add(new Aluno
            {
                Id = "e1f2cc2e-ead1-4478-9017-ef29d546287c",
                Nome = "FULANO"
            });
            var cadastrarRespostasDto = new CadastrarRespostasAlunoDto
            {
                IdAluno = "e1f2cc2e-ead1-4478-9017-ef29d546287c",
                IdProva = "ALF-1",
                Respostas = new List<RespostasAlunoDto>
                {
                    new RespostasAlunoDto {NumeroQuestao = 1,Alternativa = (Alternativa) 6},
                }
            };
            var request = new CadastrarRespostasAlunoRequest();

            Func<NotaAlunoProva> func = () => request.Executar(cadastrarRespostasDto);

            Assert.Throws<Exception>(func);
            Assert.Equal(
                "Alternativa inválida",
                Assert.Throws<Exception>(func).Message
            );
            Assert.Equal(0, Dados.Notas.Count);
        }

        [Fact]
        public void Deve_cadastrar_resposta_do_aluno_com_sucesso()
        {
            Dados.Provas.Add(new Prova
            {
                Id = "ALF-1",
                ValorPeso = 2.5d,
                Questoes = new List<Questao>
                {
                    new Questao {Numero = 1,Peso = 1},
                    new Questao {Numero = 2,Peso = 3},
                }
            });
            Dados.Gabaritos.Add(new Gabarito
            {
                IdProva = "ALF-1",
                Respostas = new List<Respostas>
                {
                    new Respostas {NumeroQuestao = 1,Alternativa = Alternativa.A},
                    new Respostas {NumeroQuestao = 2,Alternativa = Alternativa.A},
                }
            });
            Dados.Alunos.Add(new Aluno
            {
                Id = "e1f2cc2e-ead1-4478-9017-ef29d546287c",
                Nome = "FULANO"
            });
            var cadastrarRespostasDto = new CadastrarRespostasAlunoDto
            {
                IdAluno = "e1f2cc2e-ead1-4478-9017-ef29d546287c",
                IdProva = "ALF-1",
                Respostas = new List<RespostasAlunoDto>
                {
                    new RespostasAlunoDto {NumeroQuestao = 1,Alternativa = Alternativa.E},
                    new RespostasAlunoDto {NumeroQuestao = 2,Alternativa = Alternativa.A},
                }
            };
            var request = new CadastrarRespostasAlunoRequest();

            var notaAlunoProva = request.Executar(cadastrarRespostasDto);

            Assert.Equal("e1f2cc2e-ead1-4478-9017-ef29d546287c", notaAlunoProva.IdAluno);
            Assert.Equal("ALF-1", notaAlunoProva.IdProva);
            Assert.Equal(7.5d, notaAlunoProva.NotaProva);
            Assert.Equal(1, Dados.Notas.Count);
        }
    }
}