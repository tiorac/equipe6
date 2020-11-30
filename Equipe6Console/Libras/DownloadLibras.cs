using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Linq;
using System.Net;

namespace Equipe6Console.Libras
{
    public static class DownloadLibras
    {
        //https://www.ines.gov.br/dicionario-de-libras/main_site/filme/
        public static void ObterVideos()
        {
            XmlDataDocument xmldoc = new XmlDataDocument();
            int i = 0;
            string str = null;
            FileStream fs = new FileStream(@"D:\Code\TioRAC\Equipe6\Equipe6Console\Libras\palavras.xml", FileMode.Open, FileAccess.Read);
            xmldoc.Load(fs);
            var xmlnode = xmldoc.GetElementsByTagName("banco")[0];

            foreach (XmlElement child in xmlnode)
            {
                Console.WriteLine("Downlod file: " + child.Attributes["p"].Value);
                var video = child.Attributes["f"].Value;

                using (var client = new WebClient())
                {
                    client.DownloadFile("https://www.ines.gov.br/dicionario-de-libras/main_site/filme/" + video, @"D:\Teste\LibrasVideos\ines\" + video);
                }
            }


        }
    }
}
