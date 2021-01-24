using System.Collections.Generic;
using EscolaAlf.Application.Entities;

namespace EscolaAlf.Application.Dtos
{
    public class CadastrarRespostasAlunoDto
    {
        public string IdAluno { get; set; }
        public string IdProva { get; set; }
        public IList<RespostasAlunoDto> Respostas { get; set; }
    }

    public class RespostasAlunoDto
    {
        public int NumeroQuestao { get; set; }
        public Alternativa Alternativa { get; set; }
    }
}