using System;
using System.Linq;
using EscolaAlf.Application.Bean;
using EscolaAlf.Application.Dtos;
using EscolaAlf.Application.Entities;
using EscolaAlf.Application.Requests;
using Xunit;

namespace EscolaAlf.ApplicationTests.Requests
{
    public class CadastrarAlunoRequestTest
    {
        public CadastrarAlunoRequestTest()
        {
            Dados.Alunos.Clear();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Deve_ocorrer_exception_com_campo_nome_nulo_ou_vazio(string campoNome)
        {
            var alunoDto = new CadastrarAlunoDto {Nome = campoNome};
            var request = new CadastrarAlunoRequest();

            Func<Aluno> func = () => request.Executar(alunoDto);

            Assert.Throws<Exception>(func);
            Assert.Equal(
                "Campo nome não pode ser branco ou nulo",
                Assert.Throws<Exception>(func).Message
            );
            Assert.Equal(0, Dados.Alunos.Count);
        }

        [Fact]
        public void Deve_ocorrer_exception_caso_haja_mais_de_cem_alunos_cadastrado()
        {
            var alunoDto = new CadastrarAlunoDto {Nome = "Aluno Teste"};
            var request = new CadastrarAlunoRequest();

            Enumerable.Range(1, 100).ToList()
                .ForEach(x => Dados.Alunos.Add(new Aluno
                {
                    Id = $"{x}",
                    Nome = $"Aluno Teste {x}"
                }));

            Func<Aluno> func = () => request.Executar(alunoDto);

            Assert.Throws<Exception>(func);
            Assert.Equal(
                "Limite máximo de 100 alunos cadastrados foi atingido",
                Assert.Throws<Exception>(func).Message
            );
            Assert.Equal(100, Dados.Alunos.Count);
        }

        [Fact]
        public void Deve_cadastrar_aluno()
        {
            var alunoDto = new CadastrarAlunoDto {Nome = "Aluno Teste"};
            var request = new CadastrarAlunoRequest();

            var aluno = request.Executar(alunoDto);

            Assert.NotNull(aluno.Id);
            Assert.NotEmpty(aluno.Id);
            Assert.Equal("ALUNO TESTE", aluno.Nome);
            Assert.Equal(1, Dados.Alunos.Count);
        }
    }
}