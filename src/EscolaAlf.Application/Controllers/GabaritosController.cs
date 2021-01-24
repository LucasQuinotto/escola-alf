using EscolaAlf.Application.Dtos;
using EscolaAlf.Application.Entities;
using EscolaAlf.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EscolaAlf.Application.Controllers
{
    public class GabaritosController : ApiController
    {
        private readonly ICadastrarGabaritoRequest _request;

        public GabaritosController(ICadastrarGabaritoRequest request)
        {
            _request = request;
        }

        [HttpPost]
        public Gabarito CadastrarGabarito([FromBody] CadastrarGabaritoDto body)
        {
            return _request.Executar(body);
        }
    }
}