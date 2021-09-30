using System;

namespace CrawlerCFO
{
    public class Dentista
    {
        public Dentista(string funcao, string inscricao, string nome, string situacao, string tipo, string inscricaoCRO, string registroCRO)
        {
            Funcao = funcao;
            Inscricao = inscricao;
            Nome = nome;
            Situacao = situacao;
            Tipo = tipo;

            var diaInsc = Int32.Parse(inscricaoCRO.Split("/")[0]);
            var mesInsc = Int32.Parse(inscricaoCRO.Split("/")[1]);
            var anoInsc = Int32.Parse(inscricaoCRO.Split("/")[2]);
            InscricaoCRO = new DateTime(anoInsc, mesInsc, diaInsc);

            var diaReg = Int32.Parse(registroCRO.Split("/")[0]);
            var mesReg = Int32.Parse(registroCRO.Split("/")[1]);
            var anoReg = Int32.Parse(registroCRO.Split("/")[2]);
            RegistroCRO = new DateTime(anoReg, mesReg, diaReg);
        }

        public string Funcao { get; set; }
        public string Inscricao { get; set; }
        public string Situacao { get; set; }
        public string Tipo { get; set; }
        public string Nome { get; set; }
        public DateTime InscricaoCRO { get; set; }
        public DateTime RegistroCRO { get; set; }
    }
}
