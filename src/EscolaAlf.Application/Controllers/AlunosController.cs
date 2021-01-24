using System.Collections.Generic;
using EscolaAlf.Application.Dtos;
using EscolaAlf.Application.Entities;
using EscolaAlf.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EscolaAlf.Application.Controllers
{
    public class AlunosController : ApiController
    {
        private readonly ICadastrarAlunoRequest _cadastrarRequest;
        private readonly IListarAlunosAprovadosRequest _listarRequest;

        public AlunosController(
            ICadastrarAlunoRequest cadastrarRequest,
            IListarAlunosAprovadosRequest listarRequest)
        {
            _cadastrarRequest = cadastrarRequest;
            _listarRequest = listarRequest;
        }

        [HttpPost]
        public Aluno CadastrarAluno([FromBody] CadastrarAlunoDto body)
        {
            return _cadastrarRequest.Executar(body);
        }

        [HttpGet]
        [Route("aprovados")]
        public List<Aluno> ListarAlunosAprovados()
        {
            return _listarRequest.Executar();
        }
    }
}