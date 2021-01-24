using System;
using System.Collections.Generic;
using EscolaAlf.Application.Bean;
using EscolaAlf.Application.Dtos;
using EscolaAlf.Application.Entities;
using EscolaAlf.Application.Requests;
using Xunit;

namespace EscolaAlf.ApplicationTests.Requests
{
    public class CadastrarGabaritoRequestTest
    {
        public CadastrarGabaritoRequestTest()
        {
            Dados.Provas.Clear();
            Dados.Gabaritos.Clear();
        }

        [Fact]
        public void Deve_ocorrer_exception_caso_haja_outro_gabarito_cadastrado()
        {
            Dados.Gabaritos.Add(new Gabarito
            {
                IdProva = "ALF-1",
                Respostas = new List<Respostas>
                {
                    new Respostas {NumeroQuestao = 1, Alternativa = Alternativa.A,}
                }
            });
            var gabaritoDto = new CadastrarGabaritoDto
            {
                IdProva = "ALF-1",
                Respostas = new List<RespostaDto>
                {
                    new RespostaDto {NumeroQuestao = 1, Alternativa = Alternativa.A,}
                }
            };
            var request = new CadastrarGabaritoRequest();

            Func<Gabarito> func = () => request.Executar(gabaritoDto);

            Assert.Throws<Exception>(func);
            Assert.Equal(
                "Gabarito já cadastrado para a prova ALF-1",
                Assert.Throws<Exception>(func).Message
            );
            Assert.Equal(1, Dados.Gabaritos.Count);
        }

        [Fact]
        public void Deve_ocorrer_exception_caso_nao_haja_prova_informada()
        {
            var gabaritoDto = new CadastrarGabaritoDto
            {
                IdProva = "ALF-1",
                Respostas = new List<RespostaDto>
                {
                    new RespostaDto {NumeroQuestao = 1, Alternativa = Alternativa.A,}
                }
            };
            var request = new CadastrarGabaritoRequest();

            Func<Gabarito> func = () => request.Executar(gabaritoDto);

            Assert.Throws<Exception>(func);
            Assert.Equal(
                "Prova ALF-1 não encontrada",
                Assert.Throws<Exception>(func).Message
            );
            Assert.Equal(0, Dados.Gabaritos.Count);
        }

        [Fact]
        public void Deve_ocorrer_exception_caso_haja_respostas_duplicadas()
        {
            Dados.Provas.Add(new Prova
            {
                Id = "ALF-1",
                Questoes = new List<Questao>
                {
                    new Questao {Numero = 1, Peso = 1,}
                }
            });
            var gabaritoDto = new CadastrarGabaritoDto
            {
                IdProva = "ALF-1",
                Respostas = new List<RespostaDto>
                {
                    new RespostaDto {NumeroQuestao = 1, Alternativa = Alternativa.A,},
                    new RespostaDto {NumeroQuestao = 1, Alternativa = Alternativa.B,}
                }
            };
            var request = new CadastrarGabaritoRequest();

            Func<Gabarito> func = () => request.Executar(gabaritoDto);

            Assert.Throws<Exception>(func);
            Assert.Equal(
                "Numero da questão não pode ser duplicado",
                Assert.Throws<Exception>(func).Message
            );
            Assert.Equal(0, Dados.Gabaritos.Count);
        }

        [Fact]
        public void Deve_ocorrer_exception_caso_nao_haja_questao_para_as_respostas_informadas()
        {
            Dados.Provas.Add(new Prova
            {
                Id = "ALF-1",
                Questoes = new List<Questao>
                {
                    new Questao {Numero = 1, Peso = 1,},
                }
            });
            var gabaritoDto = new CadastrarGabaritoDto
            {
                IdProva = "ALF-1",
                Respostas = new List<RespostaDto>
                {
                    new RespostaDto {NumeroQuestao = 1, Alternativa = Alternativa.A,},
                    new RespostaDto {NumeroQuestao = 2, Alternativa = Alternativa.B,},
                }
            };
            var request = new CadastrarGabaritoRequest();

            Func<Gabarito> func = () => request.Executar(gabaritoDto);

            Assert.Throws<Exception>(func);
            Assert.Equal(
                "Questão não encontrada para resposta",
                Assert.Throws<Exception>(func).Message
            );
            Assert.Equal(0, Dados.Gabaritos.Count);
        }

        [Fact]
        public void Deve_ocorrer_exception_caso_nao_seja_informado_todas_as_respostas()
        {
            Dados.Provas.Add(new Prova
            {
                Id = "ALF-1",
                Questoes = new List<Questao>
                {
                    new Questao {Numero = 1, Peso = 1,},
                    new Questao {Numero = 2, Peso = 1,},
                }
            });
            var gabaritoDto = new CadastrarGabaritoDto
            {
                IdProva = "ALF-1",
                Respostas = new List<RespostaDto>
                {
                    new RespostaDto {NumeroQuestao = 1, Alternativa = Alternativa.A,},
                }
            };
            var request = new CadastrarGabaritoRequest();

            Func<Gabarito> func = () => request.Executar(gabaritoDto);

            Assert.Throws<Exception>(func);
            Assert.Equal(
                "Quantidade de respostas não correspondente",
                Assert.Throws<Exception>(func).Message
            );
            Assert.Equal(0, Dados.Gabaritos.Count);
        }

        [Fact]
        public void Deve_ocorrer_exception_caso_alternativa_informada_nao_exista()
        {
            Dados.Provas.Add(new Prova
            {
                Id = "ALF-1",
                Questoes = new List<Questao>
                {
                    new Questao {Numero = 1, Peso = 1,},
                    new Questao {Numero = 2, Peso = 1,},
                }
            });
            var gabaritoDto = new CadastrarGabaritoDto
            {
                IdProva = "ALF-1",
                Respostas = new List<RespostaDto>
                {
                    new RespostaDto {NumeroQuestao = 1, Alternativa = Alternativa.A,},
                    new RespostaDto {NumeroQuestao = 2, Alternativa = (Alternativa) 6,},
                }
            };
            var request = new CadastrarGabaritoRequest();

            Func<Gabarito> func = () => request.Executar(gabaritoDto);

            Assert.Throws<Exception>(func);
            Assert.Equal(
                "Alternativa inválida",
                Assert.Throws<Exception>(func).Message
            );
            Assert.Equal(0, Dados.Gabaritos.Count);
        }

        [Fact]
        public void Deve_cadastrar_gabarito_com_sucesso()
        {
            Dados.Provas.Add(new Prova
            {
                Id = "ALF-1",
                Questoes = new List<Questao>
                {
                    new Questao {Numero = 1, Peso = 1,},
                    new Questao {Numero = 2, Peso = 2,},
                }
            });
            var gabaritoDto = new CadastrarGabaritoDto
            {
                IdProva = "ALF-1",
                Respostas = new List<RespostaDto>
                {
                    new RespostaDto {NumeroQuestao = 1, Alternativa = Alternativa.A,},
                    new RespostaDto {NumeroQuestao = 2, Alternativa = Alternativa.B,},
                }
            };
            var request = new CadastrarGabaritoRequest();

            var gabarito = request.Executar(gabaritoDto);

            Assert.Equal("ALF-1", gabarito.IdProva);
            Assert.Equal(2, gabarito.Respostas.Count);
            Assert.Equal(1, Dados.Gabaritos.Count);
        }
    }
}