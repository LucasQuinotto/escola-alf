using EscolaAlf.Application.Dtos;
using EscolaAlf.Application.Entities;
using EscolaAlf.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EscolaAlf.Application.Controllers
{
    public class ProvasController : ApiController
    {
        private readonly ICadastrarProvaRequest _request;

        public ProvasController(ICadastrarProvaRequest request)
        {
            _request = request;
        }

        [HttpPost]
        public Prova CadastrarProva([FromBody] CadastrarProvaDto body)
        {
            return _request.Executar(body);
        }
    }
}