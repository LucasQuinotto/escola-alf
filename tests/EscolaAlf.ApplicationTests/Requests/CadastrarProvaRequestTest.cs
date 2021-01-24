using System;
using System.Collections.Generic;
using EscolaAlf.Application.Bean;
using EscolaAlf.Application.Dtos;
using EscolaAlf.Application.Entities;
using EscolaAlf.Application.Requests;
using Xunit;

namespace EscolaAlf.ApplicationTests.Requests
{
    public class CadastrarProvaRequestTest
    {
        public CadastrarProvaRequestTest()
        {
            Dados.Provas.Clear();
        }

        [Fact]
        public void Deve_ocorrer_exception_com_peso_zero()
        {
            var provaDto = new CadastrarProvaDto
            {
                Questoes = new List<QuestaoDto>
                {
                    new QuestaoDto {Numero = 1, Peso = 0},
                }
            };
            var request = new CadastrarProvaRequest();

            Func<Prova> func = () => request.Executar(provaDto);

            Assert.Throws<Exception>(func);
            Assert.Equal(
                "Peso da questão não pode ser zero",
                Assert.Throws<Exception>(func).Message
            );
            Assert.Equal(0, Dados.Provas.Count);
        }

        [Fact]
        public void Deve_ocorrer_exception_caso_haja_questoes_duplicadas()
        {
            var provaDto = new CadastrarProvaDto
            {
                Questoes = new List<QuestaoDto>
                {
                    new QuestaoDto {Numero = 1, Peso = 1},
                    new QuestaoDto {Numero = 1, Peso = 2},
                }
            };
            var request = new CadastrarProvaRequest();

            Func<Prova> func = () => request.Executar(provaDto);

            Assert.Throws<Exception>(func);
            Assert.Equal(
                "Numero da questão não pode ser duplicado",
                Assert.Throws<Exception>(func).Message
            );
            Assert.Equal(0, Dados.Provas.Count);
        }

        [Fact]
        public void Deve_ocorrer_exception_caso_sequencia_de_questoes_nao_comece_em_1()
        {
            var provaDto = new CadastrarProvaDto
            {
                Questoes = new List<QuestaoDto>
                {
                    new QuestaoDto {Numero = 0, Peso = 1},
                    new QuestaoDto {Numero = 1, Peso = 2},
                }
            };
            var request = new CadastrarProvaRequest();

            Func<Prova> func = () => request.Executar(provaDto);

            Assert.Throws<Exception>(func);
            Assert.Equal(
                "A sequencia de questões deve começar em 1",
                Assert.Throws<Exception>(func).Message
            );
            Assert.Equal(0, Dados.Provas.Count);
        }

        [Fact]
        public void Deve_ocorrer_exception_caso_sequencia_de_questoes_nao_seja_completa()
        {
            var provaDto = new CadastrarProvaDto
            {
                Questoes = new List<QuestaoDto>
                {
                    new QuestaoDto {Numero = 1, Peso = 1},
                    new QuestaoDto {Numero = 2, Peso = 2},
                    new QuestaoDto {Numero = 4, Peso = 3},
                }
            };
            var request = new CadastrarProvaRequest();

            Func<Prova> func = () => request.Executar(provaDto);

            Assert.Throws<Exception>(func);
            Assert.Equal(
                "Nem todos os numeros de questões foram encontrados",
                Assert.Throws<Exception>(func).Message
            );
            Assert.Equal(0, Dados.Provas.Count);
        }

        [Fact]
        public void Deve_cadastrar_prova_com_sucesso()
        {
            var provaDto = new CadastrarProvaDto
            {
                Questoes = new List<QuestaoDto>
                {
                    new QuestaoDto {Numero = 1, Peso = 1},
                    new QuestaoDto {Numero = 2, Peso = 2},
                    new QuestaoDto {Numero = 3, Peso = 3},
                    new QuestaoDto {Numero = 4, Peso = 1},
                }
            };
            var request = new CadastrarProvaRequest();

            var prova = request.Executar(provaDto);

            Assert.Contains("ALF-", prova.Id);
            Assert.Equal(4, prova.Questoes.Count);
            Assert.Equal(1.429, prova.ValorPeso, 3);
            Assert.Equal(1, Dados.Provas.Count);
        }
    }
}