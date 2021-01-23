using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace EscolaAlf.Application.Controllers
{
    public class GabaritosController
    {
        [HttpPost]
        public void Deve([FromBody] CadastrarGabaritoRequest request)
        {
            
        }
    }

    public class CadastrarGabaritoRequest
    {
        public string IdProva { get; set; }
        public IList<Alternativa> Alternativas { get; set; }
    }

    public enum Alternativa
    {
        A = 1,
        B = 2,
        C = 3,
        D = 4,
        E = 5,
    }
}