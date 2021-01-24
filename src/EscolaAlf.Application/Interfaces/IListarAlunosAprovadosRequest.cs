using System.Collections.Generic;
using EscolaAlf.Application.Entities;

namespace EscolaAlf.Application.Interfaces
{
    public interface IListarAlunosAprovadosRequest
    {
        public List<Aluno> Executar();
    }
}