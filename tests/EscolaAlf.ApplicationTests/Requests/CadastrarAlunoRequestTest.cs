using System;
using EscolaAlf.Application.Dtos;
using EscolaAlf.Application.Entities;
using EscolaAlf.Application.Requests;
using Xunit;

namespace EscolaAlf.ApplicationTests.Requests
{
    public class CadastrarAlunoRequestTest
    {
        [Fact]
        public void Test1()
        {
            var alunoDto = new CadastrarAlunoDto();
            var request = new CadastrarAlunoRequest();

            Func<Aluno> func = () => request.Executar(alunoDto);

            Assert.Throws<Exception>(func);
            Assert.Equal("Campo nome não pode ser branco ou nulo", Assert.Throws<Exception>(func).Message);
        }
    }
}