using EscolaAlf.Application.Dtos;
using EscolaAlf.Application.Entities;

namespace EscolaAlf.Application.Interfaces
{
    public interface ICadastrarProvaRequest
    {
        public Prova Executar(CadastrarProvaDto cadastrarProvaDto);
    }
}