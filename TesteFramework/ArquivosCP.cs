using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SzarFramework.Attributes;
using SzarFramework;

namespace TesteFramework
{
    [Tables("BUMM_ARQUIVOSCP", "BUMM: ARQUIVOS CP", BoUTBTableType.bott_NoObject, false)]
    public class ArquivosCP : TableBase
    {

        [Fields("Tipo", "Tipo", 2, false, BoFldSubTypes.st_None)]
        [ValidValues("RM", "Remessa")]
        [ValidValues("RT", "Retorno")]
        public string Tipo { get; set; }

        [Fields("Nome", "Nome", 250, false, BoFldSubTypes.st_None)]
        public string Nome { get; set; }

        [Fields("Caminho", "Caminho", 250, false, BoFldSubTypes.st_None)]
        public string Caminho { get; set; }

        [Fields("Banco", "Banco", 100, false, BoFldSubTypes.st_None)]
        public string Banco { get; set; }

        [Fields("Agencia", "Agencia", 50, false, BoFldSubTypes.st_None)]
        public string Agencia { get; set; }

        [Fields("Conta", "Conta", 50, false, BoFldSubTypes.st_None)]
        public string Conta { get; set; }

        [Fields("IdArquivo", "IdArquivo", 100, false, BoFldSubTypes.st_None)]
        public string IdArquivo { get; set; }

        [Fields("Data", "Data", 200, false, BoFldSubTypes.st_None)]
        public DateTime Data { get; set; }

        [Fields("Hora", "Hora", 8, false, BoFldSubTypes.st_None)]
        public string Hora { get; set; }

        [Fields("BaseOrigem", "BaseOrigem", 250, false, BoFldSubTypes.st_None)]
        public string BaseOrigem { get; set; }

        [Fields("Usuario", "Usuario", 250, false, BoFldSubTypes.st_None)]
        public string Usuario { get; set; }

        [Fields("Arquivo", "Arquivo", 255, false, BoFldSubTypes.st_None)]
        public string Arquivo { get; set; }


    }
}
