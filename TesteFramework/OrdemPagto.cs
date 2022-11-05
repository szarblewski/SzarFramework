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
    [Tables("BUMM_ORDEMPAGTO", "BUMM: ORDEM PGTOS", BoUTBTableType.bott_Document, false)]
    [Udo(tableName: "BUMM_ORDEMPAGTO",
        name: "ORDEM DE PAGAMENTOS",
        code: "BUMM_ORDEMPAGTO",
        cancel: BoYesNoEnum.tYES,
        close: BoYesNoEnum.tYES,
        createDefaultForm: BoYesNoEnum.tYES,
        delete: BoYesNoEnum.tYES,
        find: BoYesNoEnum.tYES,
        yearTransfer: BoYesNoEnum.tNO,
        manageSeries: BoYesNoEnum.tNO,
        objectType: BoUDOObjType.boud_Document,
        form: "OrdemPagtos",
        enableEnhancedform: BoYesNoEnum.tYES,
        rebuildEnhancedForm: BoYesNoEnum.tYES,
        log: true

        )]
    public class OrdemPagto 
    {

        [Fields("BplId", "Filial", 5, false, BoFldSubTypes.st_None)]
        public string BplId { get; set; }

        [Fields("BplName", "Nome Filial", 254, false, BoFldSubTypes.st_None)]
        public string BplName { get; set; }

        [Fields("CodBanco", "Código do Banco", 50, false, BoFldSubTypes.st_None)]
        public string CodBanco { get; set; }

        [Fields("TipoPgto", "Tipo pagamento cp", 50, false, BoFldSubTypes.st_None)]
        public string TipoPgto { get; set; }

        [Fields("Tributo", "Tributo", 50, false, BoFldSubTypes.st_None)]
        public string Tributo { get; set; }

        [Fields("CardCode", "Código do PN", 200, false, BoFldSubTypes.st_None)]
        public string CardCode { get; set; }

        [Fields("CardName", "Nome do PN", 200, false, BoFldSubTypes.st_None)]
        public string CardName { get; set; }

        [Fields("DataEmissao", "Data de Emissão", 200, false, BoFldSubTypes.st_None)]
        public DateTime DataEmissao { get; set; }

        [Fields("DtVencimento", "Data de Vencimento", 200, false, BoFldSubTypes.st_None)]
        public DateTime DataVencimento { get; set; }

        [Fields("DtAgendamento", "Data de Agendamento", 200, false, BoFldSubTypes.st_None)]
        public DateTime DtAgendamento { get; set; }

        [Fields("DtPagamento", "Data de Pagamento", 20, false, BoFldSubTypes.st_None)]
        public DateTime DataPagamento { get; set; }

        [Fields("NrDocumento", "Nr. Documento", 20, false, BoFldSubTypes.st_None)]
        public string NrDocumento { get; set; }

        [Fields("NossoNumero", "Nosso Número", 20, false, BoFldSubTypes.st_None)]
        public string NossoNumero { get; set; }

        [Fields("NrExtrato", "Ident. Extrato", 20, false, BoFldSubTypes.st_None)]
        public string NrExtrato { get; set; }

        [Fields("Autenticacao", "Autenticação Elet. Pagto", 70, false, BoFldSubTypes.st_None)]
        public string Autenticacao { get; set; }

        [Fields("VlOriginal", "Valor Original", 20, false, BoFldSubTypes.st_Sum)]
        public double VlOriginal { get; set; }

        [Fields("VlTitulo", "Valor da Ordem", 20, false, BoFldSubTypes.st_Sum)]
        public double VlTitulo { get; set; }

        [Fields("VlRetencao", "Valor Retenção no Pagto", 20, false, BoFldSubTypes.st_Sum)]
        public double VlRetencao { get; set; }

        [Fields("VlRetencaoB1", "Vl.Retenção do B1 Acumulado", 20, false, BoFldSubTypes.st_Sum)]
        public double VlRetencaoB1 { get; set; }

        [Fields("VlOutrEntid", "Vl. Outras Entidades", 20, false, BoFldSubTypes.st_Sum)]
        public double VlOutrEntid { get; set; }

        [Fields("VlAtMonet", "Vl. Atualização Monetaria", 20, false, BoFldSubTypes.st_Sum)]
        public double VlAtMonet { get; set; }

        [Fields("VlPercDARF", "Vl. % DARF", 20, false, BoFldSubTypes.st_Sum)]
        public double VlPercDARF { get; set; }

        [Fields("VlDesconto", "Valor do Desconto", 20, false, BoFldSubTypes.st_Sum)]
        public double VlDesconto { get; set; }

        [Fields("VlMora", "Valor Mora", 20, false, BoFldSubTypes.st_Sum)]
        public double VlMora { get; set; }

        [Fields("VlJuros", "Valor dos Juros", 20, false, BoFldSubTypes.st_Sum)]
        public double VlJuros { get; set; }

        [Fields("VlPago", "Valor Pago", 20, false, BoFldSubTypes.st_Sum)]
        public double VlPago { get; set; }

        [Fields("VlINSS", "Valor INSS", 20, false, BoFldSubTypes.st_Sum)]
        public double VlINSS { get; set; }

        [Fields("VlReceita", "Valor Receita", 20, false, BoFldSubTypes.st_Sum)]
        public double VlReceita { get; set; }

        [Fields("Status", "Status da Ordem", 2, false, BoFldSubTypes.st_None)]
        public string Status { get; set; }

        [Fields("Banco", "Banco", 3, false, BoFldSubTypes.st_None)]
        public string Banco { get; set; }

        [Fields("NomeBanco", "Nome Banco", 254, false, BoFldSubTypes.st_None)]
        public string NomeBanco { get; set; }

        [Fields("Agencia", "Agência", 5, false, BoFldSubTypes.st_None)]
        public string Agencia { get; set; }

        [Fields("DigAgencia", "Dig. Agência", 1, false, BoFldSubTypes.st_None)]
        public string DigAgencia { get; set; }

        [Fields("Conta", "C/Corrente", 12, false, BoFldSubTypes.st_None)]
        public string Conta { get; set; }

        [Fields("DigConta", "Dig. Conta", 2, false, BoFldSubTypes.st_None)]
        public string DigConta { get; set; }

        [Fields("LinhaDigitavel", "Linha Digitavel", 200, false, BoFldSubTypes.st_None)]
        public string LinhaDigitavel { get; set; }

        [Fields("CodigoBarras", "Código de Barras", 44, false, BoFldSubTypes.st_None)]
        public string CodigoBarras { get; set; }

        [Fields("CodReceita", "Código da Receita", 4, false, BoFldSubTypes.st_None)]
        public string CodReceita { get; set; }

        [Fields("TipoContrib", "Tipo do Contribuinte", 2, false, BoFldSubTypes.st_None)]
        public string TipoContrib { get; set; }

        [Fields("CodContrib", "Codigo do Contribuinte", 14, false, BoFldSubTypes.st_None)]
        public string CodContrib { get; set; }

        [Fields("NomeContrib", "Nome do Contribuinte", 254, false, BoFldSubTypes.st_None)]
        public string NomeContrib { get; set; }

        [Fields("EndContrib", "Endereço Contribuinte", 254, false, BoFldSubTypes.st_None)]
        public string EndContrib { get; set; }

        [Fields("CepContrib", "Cep Contribuinte", 254, false, BoFldSubTypes.st_None)]
        public string CepContrib { get; set; }

        [Fields("IdentTrib", "Código Identificação Tributo", 2, false, BoFldSubTypes.st_None)]
        public string IdentTrib { get; set; }

        [Fields("Competencia", "Competencia", 100, false, BoFldSubTypes.st_None)]
        public string Competencia { get; set; }

        [Fields("Referencia", "Referencia", 100, false, BoFldSubTypes.st_None)]
        public string Referencia { get; set; }

        [Fields("CodInscEMD", "Insc.Estadual/Munic/Nr.Declaração", 100, false, BoFldSubTypes.st_None)]
        public string CodInscEMD { get; set; }

        [Fields("NrDocOrigem", "Nr. Doc. Origem", 10, false, BoFldSubTypes.st_None)]
        public string NrDocOrigem { get; set; }

        [Fields("NrParcela", "Nr. Parcela", 20, false, BoFldSubTypes.st_None)]
        public string NrParcela { get; set; }

        [Fields("DAEtiqueta", "Dívida Ativa/N.Etiqueta", 10, false, BoFldSubTypes.st_None)]
        public int DAEtiqueta { get; set; }

        [Fields("Renavam", "Renavam", 10, false, BoFldSubTypes.st_None)]
        public int Renavam { get; set; }

        [Fields("UF", "Unidade Federação", 2, false, BoFldSubTypes.st_None)]
        public string UF { get; set; }

        [Fields("Municipio", "Municipio", 200, false, BoFldSubTypes.st_None)]
        public string Municipio { get; set; }

        [Fields("Placa", "Nr. Placa", 7, false, BoFldSubTypes.st_None)]
        public string Placa { get; set; }

        [Fields("OpcaoPgto", "Opcao Pagamento", 1, false, BoFldSubTypes.st_None)]
        public string OpcaoPgto { get; set; }

        [Fields("NRenavam", "Renavam", 10, false, BoFldSubTypes.st_None)]
        public int NRenavam { get; set; }

        [Fields("OpcaoRet", "Opção de Retirada do CRVL", 1, false, BoFldSubTypes.st_None)]
        public string OpcaoRet { get; set; }


        [Fields("BancoPN", "Banco Parceiro", 3, false, BoFldSubTypes.st_None)]
        public string BancoPN { get; set; }

        [Fields("NomeBancoPN", "Nome Banco Parceiro", 254, false, BoFldSubTypes.st_None)]
        public string NomeBancoPN { get; set; }

        [Fields("AgenciaPN", "Agência Parceiro", 5, false, BoFldSubTypes.st_None)]
        public string AgenciaPN { get; set; }

        [Fields("DigAgenciaPN", "Dig. Agência Parceiro", 1, false, BoFldSubTypes.st_None)]
        public string DigAgenciaPN { get; set; }

        [Fields("ContaPN", "C/Corrente Parceiro", 12, false, BoFldSubTypes.st_None)]
        public string ContaPN { get; set; }

        [Fields("DigContaPN", "Dig. Conta Parceiro", 2, false, BoFldSubTypes.st_None)]
        public string DigContaPN { get; set; }

        [Fields("TipoCtaBanc", "Tipo conta bancaria", 1, false, BoFldSubTypes.st_None)]
        [ValidValues("1", "Conta Corrente")]
        [ValidValues("2", "Conta Poupança")]
        public string TipoCtaBanc { get; set; }

        [Fields("TipoTrfBanc", "Tipo Transferência bancaria", 1, false, BoFldSubTypes.st_None)]
        [ValidValues("1", "DOC")]
        [ValidValues("2", "TED")]
        public string TipoTrfBanc { get; set; }

        [Fields("CpfCnpjSacador", "CPF/CNPJ Sacador", 18, false, BoFldSubTypes.st_None)]
        public string CpfCnpjSacador { get; set; }

        [Fields("NomeSacador", "Nome Sacador", 250, false, BoFldSubTypes.st_None)]
        public string NomeSacador { get; set; }

        [Fields("CpfCnpjBenef", "CPF/CNPJ Beneficiário", 18, false, BoFldSubTypes.st_None)]
        public string CpfCnpjBenef { get; set; }

        [Fields("NomeBenef", "Nome Beneficiário", 250, false, BoFldSubTypes.st_None)]
        public string NomeBenef { get; set; }

        [Fields("HabilitPagto", "Habilita Intermidiador pagamento", 1, "N", false, BoFldSubTypes.st_None)]
        [ValidValues("Y", "Sim")]
        [ValidValues("N", "Não")]
        public string HabilitPagto { get; set; }

        [Fields("VlMultaDarf", "Valor Multa Darf", 20, false, BoFldSubTypes.st_Sum)]
        public double VlMultaDarf { get; set; }

        [Fields("VlEncDarf", "Valor Encargos Darf", 20, false, BoFldSubTypes.st_Sum)]
        public double VlEncDarf { get; set; }

        [Fields("HabilitComp", "Habilita Comp. posterior", 1, "N", false, BoFldSubTypes.st_None)]
        [ValidValues("Y", "Sim")]
        [ValidValues("N", "Não")]
        public string HabilitComp { get; set; }

        [Fields("InfoCompl", "Info. Complementares", 250, false, BoFldSubTypes.st_None)]
        public string InfoCompl { get; set; }

        [Fields("LogradouroFav", "Logradouro Favorecido", 200, false, BoFldSubTypes.st_None)]
        public string LogradouroFav { get; set; }

        [Fields("NumLogFav", "Numero Logradouro Favorecido", 10, false, BoFldSubTypes.st_None)]
        public int NumLogFav { get; set; }

        [Fields("ComplementoFav", "Complemento Favorecido", 200, false, BoFldSubTypes.st_None)]
        public string ComplementoFav { get; set; }

        [Fields("BairroFav", "Bairro Favorecido", 200, false, BoFldSubTypes.st_None)]
        public string BairroFav { get; set; }

        [Fields("CidadeFav", "Cidade Favorecido", 200, false, BoFldSubTypes.st_None)]
        public string CidadeFav { get; set; }

        [Fields("CepFav", "Cep Favorecido", 10, false, BoFldSubTypes.st_None)]
        public int CepFav { get; set; }

        [Fields("CompCepFav", "Comp. CEP Favorecido", 3, false, BoFldSubTypes.st_None)]
        public string CompCepFav { get; set; }

        [Fields("EstadoFav", "Estado Favorecido", 2, false, BoFldSubTypes.st_None)]
        public string EstadoFav { get; set; }


    }

    [Tables(name: "BUMM_OPGTODOCS", description: "BUMM: ORDEM PGTOS DOCS", typeTable: BoUTBTableType.bott_DocumentLines, systemTable: false)]
    [UdoChild(tableName: "BUMM_OPGTODOCS", tabelaFather: "BUMM_ORDEMPAGTO")]
    public class DocsOrdemPagto 
    {
        [Fields("BplId", "Filial", 5, false, BoFldSubTypes.st_None)]
        public string BplId { get; set; }

        [Fields("BplName", "Nome Filial", 254, false, BoFldSubTypes.st_None)]
        public string BplName { get; set; }

        [Fields("TransID", "ID LCM", 15, false, BoFldSubTypes.st_None)]
        public string TransID { get; set; }

        [Fields("LineID", "Linha LCM", 5, false, BoFldSubTypes.st_None)]
        public string LineID { get; set; }

        [Fields("BaseType", "Tipo do Documento Base", 30, false, BoFldSubTypes.st_None)]
        public string BaseType { get; set; }

        [Fields("BaseEntry", "Documento Base", 15, false, BoFldSubTypes.st_None)]
        public string BaseEntry { get; set; }

        [Fields("BaseLine", "Linha do Docto Base", 5, false, BoFldSubTypes.st_None)]
        public string BaseLine { get; set; }

        [Fields("Serial", "Nr.Nota", 15, false, BoFldSubTypes.st_None)]
        public string Serial { get; set; }

        [Fields("VlOriginal", "Valor Original", 20, false, BoFldSubTypes.st_Sum)]
        public double VlOriginal { get; set; }

        [Fields("VlTitulo", "Valor da Ordem", 20, false, BoFldSubTypes.st_Sum)]
        public double VlTitulo { get; set; }

        [Fields("VlRetencao", "Valor Retencao no Pagto", 20, false, BoFldSubTypes.st_Sum)]
        public double VlRetencao { get; set; }

        [Fields("VlRetencaoB1", "Vl.Retencao do B1 Acumulado", 20, false, BoFldSubTypes.st_Sum)]
        public double VlRetencaoB1 { get; set; }


    }


    [Tables(name: "BUMM_OPGTOHIST", description: "BUMM: ORDEM PGTOS HISTORICO", typeTable: BoUTBTableType.bott_DocumentLines, systemTable: false)]
    [UdoChild(tableName: "BUMM_OPGTOHIST", tabelaFather: "BUMM_ORDEMPAGTO")]
    public class HistoricoOrdemPagto 
    {
        [Fields("Data", "Data da Ocorrência", 20, false, BoFldSubTypes.st_None)]
        public DateTime Data { get; set; }

        [Fields("Hora", "Hora da Ocorrência", 8, false, BoFldSubTypes.st_None)]
        public string Hora { get; set; }

        [Fields("Usuario", "Executado por", 25, false, BoFldSubTypes.st_None)]
        public string Usuario { get; set; }

        [Fields("Ocorrencia", "Ocorrencia", 8, false, BoFldSubTypes.st_None)]
        [ValidValues("00", "Envio")]
        [ValidValues("01", "Retorno")]
        public string Ocorrencia { get; set; }

        [Fields("IdArquivo", "Id Arquivo Gerado", 50, false, BoFldSubTypes.st_None)]
        public string IdArquivo { get; set; }

        [Fields("OcorrRet", "Ocorrencia Retorno", 10, false, BoFldSubTypes.st_None)]
        public string OcorrRet { get; set; }

        [Fields("IdContasPagar", "Id Contas a Pagar", 10, false, BoFldSubTypes.st_None)]
        public string IdContasPagar { get; set; }

        [Fields("DtHrPagto", "Dt./Hr. Pagamento", 20, false, BoFldSubTypes.st_None)]
        public string DtHrPagto { get; set; }

        [Fields("Obs", "Observações", 254, false, BoFldSubTypes.st_None)]
        public string Obs { get; set; }

        [Fields("BaseOrigem", "Base Origem", 100, false, BoFldSubTypes.st_None)]
        public string BaseOrigem { get; set; }

    }


    [Tables(name: "BUMM_OPGTORET", description: "BUMM: ORDEM PGTOS RETENÇÕES", typeTable: BoUTBTableType.bott_DocumentLines, systemTable: false)]
    [UdoChild(tableName: "BUMM_OPGTORET", tabelaFather: "BUMM_ORDEMPAGTO")]
    public class RetencaoOrdemPagto 
    {
        [Fields("BaseOrigem", "Base Origem", 100, false, BoFldSubTypes.st_None)]
        public string BaseOrigem { get; set; }

        [Fields("Usuario", "Executado por", 25, false, BoFldSubTypes.st_None)]
        public string Usuario { get; set; }

        [Fields("Data", "Data da Ocorrência", 20, false, BoFldSubTypes.st_None)]
        public DateTime Data { get; set; }

        [Fields("Hora", "Hora da Ocorrência", 8, false, BoFldSubTypes.st_None)]
        public string Hora { get; set; }

        [Fields("VlOriginal", "Valor Original", 10, false, BoFldSubTypes.st_Sum)]
        public double VlOriginal { get; set; }

        [Fields("VlRetencao", "Valor Retenção no Pagto", 10, false, BoFldSubTypes.st_Sum)]
        public double VlRetencao { get; set; }

        [Fields("TgtDocEntry", "Documento Destino", 15, false, BoFldSubTypes.st_None)]
        public string TgtDocEntry { get; set; }

        [Fields("TgtTransId", "Documento Destino", 15, false, BoFldSubTypes.st_None)]
        public string TgtTransId { get; set; }

        [Fields("TgtLineId", "Linha do Docto Destino", 5, false, BoFldSubTypes.st_None)]
        public string TgtLineId { get; set; }



    }
}
