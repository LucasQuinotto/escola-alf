using System;
using System.Collections.Generic;
using System.Linq;
using EscolaAlf.Application.Bean;
using EscolaAlf.Application.Entities;
using EscolaAlf.Application.Interfaces;

namespace EscolaAlf.Application.Requests
{
    public class ListarAlunosAprovadosRequest : IListarAlunosAprovadosRequest
    {
        public List<Aluno> Executar()
        {
            Console.WriteLine("Consultando alunos aprovados...");

            var alunos = Dados.Alunos.ToList();
            var alunosAprovados = new List<Aluno>();

            foreach (var aluno in alunos)
            {
                var notasAluno = Dados.Notas
                    .Where(x => x.IdAluno == aluno.Id).ToList();

                var quantidadeDeProvas = notasAluno.Count;

                var somaTotalProvas = notasAluno.Select(x => x.NotaProva).Sum();
                var media = somaTotalProvas / quantidadeDeProvas;

                Console.WriteLine($"Aluno: {aluno.Nome} => media: {media}");

                if (media >= 7d)
                    alunosAprovados.Add(new Aluno
                    {
                        Id = aluno.Id,
                        Nome = aluno.Nome
                    });
            }

            return alunosAprovados;
        }
    }
}