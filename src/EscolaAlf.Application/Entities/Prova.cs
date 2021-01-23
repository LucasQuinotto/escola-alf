using System.Collections.Generic;

namespace EscolaAlf.Application.Entities
{
    public class Prova
    {
        public string Id { get; set; }
        public IList<Questao> Questoes { get; set; }
    }

    public class Questao
    {
        public int Numero { get; set; } 
        public int Peso { get; set; }
    }
}