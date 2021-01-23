using System.Collections.Generic;

namespace EscolaAlf.Application.Entities
{
    public class Gabarito
    {
        public string IdProva { get; set; }
        public IList<Respostas> Respostas { get; set; }
    }

    public class Respostas
    {
        public int NumeroQuestao { get; set; }
        public Alternativa Alternativa { get; set; }
    }
}