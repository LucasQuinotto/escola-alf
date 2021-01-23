using System.Collections.Generic;

namespace EscolaAlf.Application.Requests
{
    public class CadastrarProvaRequest
    {
        public IList<QuestaoDto> Questoes { get; set; }
    }

    public class QuestaoDto
    {
        public int Numero { get; set; } 
        public int Peso { get; set; }
    }
}