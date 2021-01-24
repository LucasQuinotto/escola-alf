using EscolaAlf.Application.Dtos;
using EscolaAlf.Application.Entities;

namespace EscolaAlf.Application.Interfaces
{
    public interface ICadastrarAlunoRequest
    {
        public Aluno Executar(CadastrarAlunoDto cadastrarAlunoDto);
    }
}