using EscolaAlf.Application.Dtos;
using EscolaAlf.Application.Entities;
using EscolaAlf.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EscolaAlf.Application.Controllers
{
    public class AlunosController : ApiController
    {
        private readonly ICadastrarAlunoRequest _request;

        public AlunosController(ICadastrarAlunoRequest request)
        {
            _request = request;
        }

        [HttpPost]
        public Aluno CadastrarAluno([FromBody] CadastrarAlunoDto body)
        {
            return _request.Executar(body);
        }
    }
}