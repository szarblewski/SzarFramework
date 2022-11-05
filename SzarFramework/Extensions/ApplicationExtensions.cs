using SAPbouiCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SzarFramework
{
    public static class ApplicationExtensions
    {
        /// <summary>
        /// Abre tela no SAP com base no XML '.srf' passado como parametro
        /// </summary>
        /// <param name="app"></param>
        /// <param name="screen">Tela XML</param>
        /// <param name="screenName">ID da tela XML</param>
        /// <returns></returns>
        public static Form OpenForm(this Application app, string screen, string screenName)
        {

            try
            {
                FormCreationParams creationPackage = (FormCreationParams)B1AppDomain.Application.CreateObject(BoCreatableObjectType.cot_FormCreationParams);
                XmlDocument document = new XmlDocument
                {
                    InnerXml = screen
                };
                creationPackage.XmlData = document.InnerXml;
                creationPackage.UniqueID = DateTime.Now.ToBinary().ToString();
                creationPackage.FormType = screenName;
                return B1AppDomain.Application.Forms.AddEx(creationPackage);
            }
            catch (Exception ex)
            {
                B1Exception.throwException("Erro ao carregar formularios XML :: ", ex);
                return null;
            }



            
        }





        
    }
}
