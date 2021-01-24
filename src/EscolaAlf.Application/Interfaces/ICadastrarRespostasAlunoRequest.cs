using EscolaAlf.Application.Dtos;
using EscolaAlf.Application.Entities;

namespace EscolaAlf.Application.Interfaces
{
    public interface ICadastrarRespostasAlunoRequest
    {
        public NotaAlunoProva Executar(CadastrarRespostasAlunoDto respostasAlunoDto);
    }
}