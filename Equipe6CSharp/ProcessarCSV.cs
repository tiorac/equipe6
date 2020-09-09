using LumenWorks.Framework.IO.Csv;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Media.Animation;
using System.Xml;

namespace Equipe6
{
    public class ProcessarCSV
    {
        static string ArquivoTreino = @"D:\Teste\Webpages_Classification_train_data.csv";
        static string ArquivoTeste = @"D:\Teste\Webpages_Classification_test_data.csv";


        public static void LerArquivo(MLP mlp, bool t = false)
        {
            var listaTamanho = new List<string>();

            using (CsvReader csv = new CsvReader(new StreamReader(ArquivoTreino), true))
            {
                int fieldCount = csv.FieldCount;
                string[] headers = csv.GetFieldHeaders();
                int limite = 0;

                while (csv.ReadNextRecord())
                {
                    var urlLen = Convert.ToInt32(csv[2]);
                    var jsLen = Convert.ToDouble(csv[8], CultureInfo.InvariantCulture);
                    var jsObfLen = Convert.ToDouble(csv[9], CultureInfo.InvariantCulture);
                    var whoIs = (csv[6] == "complete") ? 1 : 0;
                    var https = (csv[7] == "yes") ? 1 : 0;
                    var label = (csv[11] == "good") ? true : false;

                    listaTamanho.Add(csv[5]);
                     /*“/”, “%”, “#”, “&”, “. “, “=”*/

                    mlp.AdicionarValoresParaEntradas(
                            (urlLen < 20) ? 1: 0,
                            (urlLen >= 20 && urlLen< 30) ? 1 : 0,
                            (urlLen >= 30 && urlLen < 40) ? 1 : 0,
                            (urlLen >= 40 && urlLen < 50) ? 1 : 0,
                            (urlLen >= 50 && urlLen < 60) ? 1 : 0,
                            (urlLen >= 60 && urlLen < 70) ? 1 : 0,
                            (urlLen >= 70 && urlLen < 80) ? 1 : 0,
                            (urlLen >= 80 && urlLen < 90) ? 1 : 0,
                            (urlLen >= 90 && urlLen < 100) ? 1 : 0,
                            (urlLen >= 100 && urlLen < 150) ? 1 : 0,
                            (urlLen >= 150 && urlLen < 200) ? 1 : 0,
                            (urlLen >= 200) ? 1 : 0,
                            (jsLen < 50) ? 1 : 0,
                            (jsLen >= 100 && jsLen < 150) ? 1 : 0,
                            (jsLen >= 150 && jsLen < 200) ? 1 : 0,
                            (jsLen >= 200 && jsLen < 250) ? 1 : 0,
                            (jsLen >= 250 && jsLen < 300) ? 1 : 0,
                            (jsLen >= 300 && jsLen < 350) ? 1 : 0,
                            (jsLen >= 350 && jsLen < 400) ? 1 : 0,
                            (jsLen >= 400 && jsLen < 450) ? 1 : 0,
                            (jsLen >= 450 && jsLen < 500) ? 1 : 0,
                            (jsLen >= 500 && jsLen < 550) ? 1 : 0,
                            (jsLen >= 550 && jsLen < 600) ? 1 : 0,
                            (jsLen >= 600 && jsLen < 650) ? 1 : 0,
                            (jsLen >= 650 && jsLen < 700) ? 1 : 0,
                            (jsLen >= 700 && jsLen < 750) ? 1 : 0,
                            (jsLen >= 750 && jsLen < 800) ? 1 : 0,
                            (jsLen >= 800 && jsLen < 850) ? 1 : 0,
                            (jsLen >= 850 && jsLen < 900) ? 1 : 0,
                            (jsLen >= 900 && jsLen < 950) ? 1 : 0,
                            (jsLen >= 950) ? 1 : 0,
                            (jsObfLen < 100) ? 1 : 0,
                            (jsObfLen >= 100 && jsObfLen < 200) ? 1 : 0,
                            (jsObfLen >= 200 && jsObfLen < 300) ? 1 : 0,
                            (jsObfLen >= 300 && jsObfLen < 400) ? 1 : 0,
                            (jsObfLen >= 400 && jsObfLen < 500) ? 1 : 0,
                            (jsObfLen >= 500 && jsObfLen < 600) ? 1 : 0,
                            (jsObfLen >= 600 && jsObfLen < 700) ? 1 : 0,
                            (jsObfLen >= 700 && jsObfLen < 800) ? 1 : 0,
                            (jsObfLen >= 800 && jsObfLen < 900) ? 1 : 0,
                            (jsObfLen >= 900 && jsObfLen < 1000) ? 1 : 0,
                            whoIs, 
                            https);
                    mlp.AdicionarValoresParaSaidas(Convert.ToDouble(label), Convert.ToDouble(!label));
                    mlp.FuncionarRede();
                    
                    if (t)
                        testar(mlp);
                    else
                        mlp.BackPropagation();

                    limite++;

                    /*if (limite >= 100000)
                        break;*/
                }
            }

            listaTamanho = listaTamanho.Distinct().ToList();

        }

        public static void TestarArquivo(MLP mlp)
        {
            acertoTotal = 0;
            erroTotal = 0;
            respostasErradas = 0;

            int goodtotal = 0;
            int eviltotal = 0;

            int goodErro = 0;
            int evilErro = 0;

            int goodAcerto = 0;
            int evilAcerto = 0;

            int count = 0;
            int oxe = 0;

            using (CsvReader csv = new CsvReader(new StreamReader(ArquivoTeste), true))
            {
                int fieldCount = csv.FieldCount;
                string[] headers = csv.GetFieldHeaders();

                while (csv.ReadNextRecord())
                {
                    count++;

                    var urlLen = Convert.ToInt32(csv[2]);
                    var jsLen = Convert.ToDouble(csv[8], CultureInfo.InvariantCulture);
                    var jsObfLen = Convert.ToDouble(csv[9], CultureInfo.InvariantCulture);
                    var whoIs = (csv[6] == "complete") ? 1 : 0;
                    var https = (csv[7] == "yes") ? 1 : 0;
                    var label = (csv[11] == "good") ? true : false;

                    if (label)
                        goodtotal++;
                    else
                        eviltotal++;

                    mlp.AdicionarValoresParaEntradas(
                           (urlLen < 20) ? 1 : 0,
                           (urlLen >= 20 && urlLen < 30) ? 1 : 0,
                           (urlLen >= 30 && urlLen < 40) ? 1 : 0,
                           (urlLen >= 40 && urlLen < 50) ? 1 : 0,
                           (urlLen >= 50 && urlLen < 60) ? 1 : 0,
                           (urlLen >= 60 && urlLen < 70) ? 1 : 0,
                           (urlLen >= 70 && urlLen < 80) ? 1 : 0,
                           (urlLen >= 80 && urlLen < 90) ? 1 : 0,
                           (urlLen >= 90 && urlLen < 100) ? 1 : 0,
                           (urlLen >= 100 && urlLen < 150) ? 1 : 0,
                           (urlLen >= 150 && urlLen < 200) ? 1 : 0,
                           (urlLen >= 200) ? 1 : 0,
                           (jsLen < 50) ? 1 : 0,
                           (jsLen >= 100 && jsLen < 150) ? 1 : 0,
                           (jsLen >= 150 && jsLen < 200) ? 1 : 0,
                           (jsLen >= 200 && jsLen < 250) ? 1 : 0,
                           (jsLen >= 250 && jsLen < 300) ? 1 : 0,
                           (jsLen >= 300 && jsLen < 350) ? 1 : 0,
                           (jsLen >= 350 && jsLen < 400) ? 1 : 0,
                           (jsLen >= 400 && jsLen < 450) ? 1 : 0,
                           (jsLen >= 450 && jsLen < 500) ? 1 : 0,
                           (jsLen >= 500 && jsLen < 550) ? 1 : 0,
                           (jsLen >= 550 && jsLen < 600) ? 1 : 0,
                           (jsLen >= 600 && jsLen < 650) ? 1 : 0,
                           (jsLen >= 650 && jsLen < 700) ? 1 : 0,
                           (jsLen >= 700 && jsLen < 750) ? 1 : 0,
                           (jsLen >= 750 && jsLen < 800) ? 1 : 0,
                           (jsLen >= 800 && jsLen < 850) ? 1 : 0,
                           (jsLen >= 850 && jsLen < 900) ? 1 : 0,
                           (jsLen >= 900 && jsLen < 950) ? 1 : 0,
                           (jsLen >= 950) ? 1 : 0,
                           (jsObfLen < 100) ? 1 : 0,
                           (jsObfLen >= 100 && jsObfLen < 200) ? 1 : 0,
                           (jsObfLen >= 200 && jsObfLen < 300) ? 1 : 0,
                           (jsObfLen >= 300 && jsObfLen < 400) ? 1 : 0,
                           (jsObfLen >= 400 && jsObfLen < 500) ? 1 : 0,
                           (jsObfLen >= 500 && jsObfLen < 600) ? 1 : 0,
                           (jsObfLen >= 600 && jsObfLen < 700) ? 1 : 0,
                           (jsObfLen >= 700 && jsObfLen < 800) ? 1 : 0,
                           (jsObfLen >= 800 && jsObfLen < 900) ? 1 : 0,
                           (jsObfLen >= 900 && jsObfLen < 1000) ? 1 : 0,
                           whoIs,
                           https);
                    
                    mlp.FuncionarRede();

                    if (mlp.o[0] > mlp.o[1] && label)
                    {
                        acertoTotal++;
                        goodAcerto++;
                    }
                    else if (mlp.o[1] > mlp.o[0] && !label)
                    {
                        acertoTotal++;
                        evilAcerto++;
                    }
                    else if (mlp.o[0] < mlp.o[1] && label)
                    {
                        erroTotal++;
                        goodErro++;
                    }
                    else if (mlp.o[1] < mlp.o[0] && !label)
                    {
                        erroTotal++;
                        evilErro++;
                    }
                    else
                    {
                        oxe++;
                    }
                }
            }
        }

        static double respostasErradas = 0;
        static double erroTotal = 0;
        static double acertoTotal = 0;

        private static void testar(MLP mlp)
        {
            for (int t = 0; t < mlp.C; t++)
            {
                if (mlp.y[t] == 1 && mlp.o[t] < 0.5) respostasErradas++;
                if (mlp.y[t] == 0 && mlp.o[t] > 0.5) respostasErradas++;

                erroTotal += Math.Abs(mlp.y[t] - mlp.o[t]);
            }
        }
    }
}
