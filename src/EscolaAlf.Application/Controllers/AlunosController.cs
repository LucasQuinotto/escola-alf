using System;
using System.Collections.Generic;
using System.Linq;
using EscolaAlf.Application.Bean;
using EscolaAlf.Application.Entities;
using EscolaAlf.Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace EscolaAlf.Application.Controllers
{
    public class AlunosController : ApiController
    {
        [HttpPost]
        public IList<Aluno> CadastrarAluno([FromBody] CadastrarAlunoRequest request)
        {
            Console.WriteLine("Cadastrando aluno...");

            ValidarAluno(request);

            var id = Guid.NewGuid().ToString();

            Dados.Alunos.Add(new Aluno
            {
                Id = id,
                Nome = request.Nome.ToUpper()
            });

            Console.WriteLine("Aluno cadastrado com sucesso");

            return Dados.Alunos;
        }

        public void ValidarAluno(CadastrarAlunoRequest alunos)
        {
            if (Dados.Alunos.ToList().Count >= 100)
                throw new Exception("Limite máximo de 100 alunos cadastrados foi atingido");

            if (string.IsNullOrEmpty(alunos.Nome))
                throw new Exception("Campo nome não pode ser branco ou nulo");
            
        }
    }
}