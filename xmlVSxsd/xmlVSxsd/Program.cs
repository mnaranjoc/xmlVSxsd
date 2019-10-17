using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;

namespace xmlVSxsd
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Presiona una tecla para comenzar...");
            //Console.ReadLine();

            try
            {
                /* Lectura de cfdi */
                string sPathXML = @"C:\Users\manuel.naranjo\Desktop\Nueva carpeta\ejemplo detecno.xml";
                byte[] yXml = File.ReadAllBytes(sPathXML);

                /* Llamado al método y verificación de resultado */
                string sResultado = Valida_con_XSD(yXml);
                if (string.IsNullOrWhiteSpace(sResultado))
                {
                    Console.WriteLine("Xml válido.");
                    Console.ReadLine();
                }

                Console.WriteLine("Xml inválido: " + sResultado);

            }
            catch (Exception ex)
            {

                throw;
            }

            Console.WriteLine("Presiona una tecla para terminar...");
            Console.ReadLine();
        }

        private static string Valida_con_XSD(byte[] yXML)
        {
            MemoryStream oMemoStream = null;
            XmlReader oXmlReader = null;
            XmlReaderSettings oXmlReadSettings = null;

            try
            {
                /* Configuración del validador */
                oXmlReadSettings = new XmlReaderSettings();
                oXmlReadSettings.ValidationType = ValidationType.Schema;

                /* Colección de archivos XSD */
                XmlSchemaSet oSchemaSet = new XmlSchemaSet();
                oSchemaSet.XmlResolver = null;
                /*oSchemaSet.Add(null, @".\xsd\cfdv33.xsd");
                oSchemaSet.Add(null, @".\xsd\catCFDI.xsd");
                oSchemaSet.Add(null, @".\xsd\tdCFDI.xsd");*/
                oSchemaSet.Add(null, @"C:\Users\manuel.naranjo\Desktop\Nueva carpeta\XML_V7.0.xsd");
                oSchemaSet.Compile();

                /* Asignación de shcemas */
                oXmlReadSettings.Schemas = oSchemaSet;

                /* Lectura y validación del CFDI*/
                oMemoStream = new MemoryStream(yXML);
                oXmlReader = XmlReader.Create(oMemoStream, oXmlReadSettings);
                while (oXmlReader.Read()) { }

                return string.Empty;
            }
            catch (XmlSchemaException XmlSchemaEx)
            {
                /* Captura de mensajes */
                return XmlSchemaEx.Message;
            }
            catch (XmlException XmlEx)
            {
                /* Captura de mensajes */
                return XmlEx.Message;
            }
            catch (Exception ex)
            {
                /* Algo salio muy mal 7n7 */
                throw;
            }
        }
    }
}
