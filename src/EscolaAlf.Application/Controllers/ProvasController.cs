using System;
using EscolaAlf.Application.Dtos;
using EscolaAlf.Application.Entities;
using EscolaAlf.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EscolaAlf.Application.Controllers
{
    public class ProvasController : ApiController
    {
        private readonly ICadastrarProvaRequest _provaRequest;
        private readonly ICadastrarRespostasAlunoRequest _respostasAlunoRequest;

        public ProvasController(
            ICadastrarProvaRequest provaRequest,
            ICadastrarRespostasAlunoRequest respostasAlunoRequest)
        {
            _provaRequest = provaRequest;
            _respostasAlunoRequest = respostasAlunoRequest;
        }

        [HttpPost]
        public Prova CadastrarProva([FromBody] CadastrarProvaDto body)
        {
            return _provaRequest.Executar(body);
        }

        [HttpPost]
        [Route("{idProva}/alunos/{idAluno}")]
        public NotaAlunoProva CadastrarRespostaAlunoProva(
            [FromRoute] string idProva,
            [FromRoute] string idAluno,
            [FromBody] CadastrarRespostasAlunoDto body)
        {
            body.IdProva = idProva;
            body.IdAluno = idAluno;
            return _respostasAlunoRequest.Executar(body);
        }
    }
}