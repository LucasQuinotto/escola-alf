using System;
using System.Linq;
using EscolaAlf.Application.Bean;
using EscolaAlf.Application.Dtos;
using EscolaAlf.Application.Entities;
using EscolaAlf.Application.Interfaces;

namespace EscolaAlf.Application.Requests
{
    public class CadastrarAlunoRequest : ICadastrarAlunoRequest
    {
        public Aluno Executar(CadastrarAlunoDto cadastrarAlunoDto)
        {
            Console.WriteLine("Cadastrando aluno...");

            ValidarCadastro(cadastrarAlunoDto.Nome);

            var aluno = new Aluno
            {
                Id = Guid.NewGuid().ToString(),
                Nome = cadastrarAlunoDto.Nome.ToUpper()
            };
            Dados.Alunos.Add(aluno);

            Console.WriteLine("Aluno cadastrado com sucesso");

            return aluno;
        }

        private void ValidarCadastro(string nome)
        {
            if (Dados.Alunos.ToList().Count >= 100)
                throw new Exception("Limite máximo de 100 alunos cadastrados foi atingido");

            if (string.IsNullOrEmpty(nome))
                throw new Exception("Campo nome não pode ser branco ou nulo");
        }
    }
}