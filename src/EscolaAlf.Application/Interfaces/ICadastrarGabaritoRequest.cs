using EscolaAlf.Application.Dtos;
using EscolaAlf.Application.Entities;

namespace EscolaAlf.Application.Interfaces
{
    public interface ICadastrarGabaritoRequest
    {
        public Gabarito Executar(CadastrarGabaritoDto cadastrarGabaritoDto);
    }
}