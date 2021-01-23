using System.Collections.Generic;
using EscolaAlf.Application.Entities;

namespace EscolaAlf.Application.Requests
{
    public class CadastrarGabaritoRequest
    {
        public string IdProva { get; set; }
        public IList<RespostaDto> Respostas { get; set; }
    }

    public class RespostaDto
    {
        public int NumeroQuestao { get; set; }
        public Alternativa Alternativa { get; set; }
    }
}