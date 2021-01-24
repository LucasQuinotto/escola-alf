using System.Collections.Generic;

namespace EscolaAlf.Application.Dtos
{
    public class CadastrarProvaDto
    {
        public IList<QuestaoDto> Questoes { get; set; }
    }

    public class QuestaoDto
    {
        public int Numero { get; set; }
        public int Peso { get; set; }
    }
}