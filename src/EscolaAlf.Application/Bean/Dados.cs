using System.Collections.Generic;
using EscolaAlf.Application.Entities;

namespace EscolaAlf.Application.Bean
{
    public static class Dados
    {
        public static IList<Aluno> Alunos { get; set; } = new List<Aluno>();

        public static IList<Prova> Provas { get; set; } = new List<Prova>();

        public static IList<Gabarito> Gabaritos { get; set; } = new List<Gabarito>();
    }
}