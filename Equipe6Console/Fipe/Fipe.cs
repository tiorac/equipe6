using Equipe6Console.Fipe;
using FileHelpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Equipe6.Fipe
{
    public static class Fipe
    {
        public static void ObterJsonCarrosFipeOrg(List<int> referencias)
        {
            var carros = new List<FipeCarroLinha>();


            using (var wclient = new WebClient())
            {
                var mesReferenciaJson = wclient.UploadString(new Uri("https://veiculos.fipe.org.br/api/veiculos//ConsultarTabelaDeReferencia"), "");
                var mesReferencias = JsonSerializer.Deserialize<JsonElement>(mesReferenciaJson);

                foreach (var mesReferencia in mesReferencias.EnumerateArray())
                {
                    carros.Clear();
                    var codigoAtual = Int32.Parse(mesReferencia.GetProperty("Codigo").ToString());

                    if (!referencias.Contains(codigoAtual))
                        continue;

                    Console.WriteLine($"Processando Mes {mesReferencia.GetProperty("Mes")}.");

                    try
                    {
                        wclient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                        var marcasJson = wclient.UploadString(new Uri($"https://veiculos.fipe.org.br/api/veiculos//ConsultarMarcas"), $"codigoTabelaReferencia={mesReferencia.GetProperty("Codigo")}&codigoTipoVeiculo=1");
                        var marcas = JsonSerializer.Deserialize<JsonElement>(marcasJson);

                        foreach (var marca in marcas.EnumerateArray())
                        {
                            try
                            {
                                Console.WriteLine($"Processando marca {marca.GetProperty("Label")}.");

                                wclient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                                var modelosJson = wclient.UploadString(new Uri($"https://veiculos.fipe.org.br/api/veiculos//ConsultarModelos"), $"codigoTipoVeiculo=1&codigoTabelaReferencia={mesReferencia.GetProperty("Codigo")}&codigoModelo=&codigoMarca={marca.GetProperty("Value")}&ano=&codigoTipoCombustivel=&anoModelo=&modeloCodigoExterno=");
                                var modelos = JsonSerializer.Deserialize<JsonElement>(modelosJson).GetProperty("Modelos");

                                foreach (var modelo in modelos.EnumerateArray())
                                {
                                    try
                                    {
                                        Console.WriteLine($"Processando modelo {modelo.GetProperty("Label")}.");

                                        wclient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                                        var anosModeloJson = wclient.UploadString(new Uri($"https://veiculos.fipe.org.br/api/veiculos//ConsultarAnoModelo"), $"codigoTipoVeiculo=1&codigoTabelaReferencia={mesReferencia.GetProperty("Codigo")}&codigoModelo={modelo.GetProperty("Value")}&codigoMarca={marca.GetProperty("Value")}&ano=&codigoTipoCombustivel=&anoModelo=&modeloCodigoExterno=");
                                        var anosModelo = JsonSerializer.Deserialize<JsonElement>(anosModeloJson);

                                        foreach (var anoModelo in anosModelo.EnumerateArray())
                                        {
                                            try
                                            {
                                                var ano = anoModelo.GetProperty("Value").ToString().Split('-')[0];
                                                var tipoCombustivel = anoModelo.GetProperty("Value").ToString().Split('-')[1];

                                                wclient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                                                var carroJson = wclient.UploadString(new Uri($"https://veiculos.fipe.org.br/api/veiculos//ConsultarValorComTodosParametros"), $"codigoTabelaReferencia={mesReferencia.GetProperty("Codigo")}&codigoMarca={marca.GetProperty("Value")}&codigoModelo={modelo.GetProperty("Value")}&codigoTipoVeiculo=1&anoModelo={ano}&codigoTipoCombustivel={tipoCombustivel}&tipoVeiculo=carro&modeloCodigoExterno=&tipoConsulta=tradicional");
                                                var carro = JsonSerializer.Deserialize<JsonElement>(carroJson);

                                                carros.Add(new FipeCarroLinha
                                                {
                                                    Ano = ano,
                                                    Combustivel = carro.GetProperty("Combustivel").ToString(),
                                                    Marca = carro.GetProperty("Marca").ToString(),
                                                    Modelo = carro.GetProperty("Modelo").ToString(),
                                                    Valor = carro.GetProperty("Valor").ToString()
                                                });
                                            }
                                            catch (Exception ex)
                                            {
                                                Console.WriteLine($"Deu ruim pegar carro detalhe {ex}");
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine($"Deu ruim pegar ano modelo {ex}");
                                    }

                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Deu rum a obter dados da marca: {ex}");
                            }
                        }


                        //codigoTipoVeiculo=1&codigoTabelaReferencia=262&codigoModelo=437&codigoMarca=21&ano=&codigoTipoCombustivel=&anoModelo=&modeloCodigoExterno=

                        //codigoTabelaReferencia=262&codigoMarca=21&codigoModelo=7133&codigoTipoVeiculo=1&anoModelo=2016&codigoTipoCombustivel=1&tipoVeiculo=carro&modeloCodigoExterno=&tipoConsulta=tradicional


                        /*var jsonCarroDocument = JsonSerializer.Deserialize<JsonElement>(jsonCarro);

                        foreach (var carroDetalhe in jsonCarroDocument.EnumerateArray())
                        {
                            var valoresAno = carroDetalhe.GetProperty("tipo").ToString().Split(" ").ToList();
                            var combustivel = valoresAno.Last();
                            valoresAno.Remove(valoresAno.Last());
                            var ano = string.Join(" ", valoresAno);

                            carros.Add(new FipeCarroLinha
                            {
                                Modelo = carro.GetProperty("modelo").ToString(),
                                Marca = carro.GetProperty("marca").ToString(),
                                Ano = ano,
                                Combustivel = combustivel,
                                Valor = Convert.ToInt32(carroDetalhe.GetProperty("valor").ToString()),
                            });
                        }*/


                        var arquivo = Path.Combine(Directory.GetCurrentDirectory(), $"fipe-{mesReferencia.GetProperty("Mes").ToString().Replace("/", "-")}.csv");
                        var engine = new FileHelperEngine(typeof(FipeCarroLinha));
                        engine.WriteFile(arquivo, carros);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Deu ruim. :-( \t {ex}");
                    }
                }
            }
        }


        public static void ObterJsonCarrosG1Globo()
        {
            var carros = new List<FipeCarroLinha>();


            using (var wclient = new WebClient())
            {
                var jsonCarros = wclient.DownloadString(new Uri("http://g1.globo.com/static/fipe/json/carro.json"));
                var jsonDocument = JsonSerializer.Deserialize<JsonElement>(jsonCarros);

                foreach (var carro in jsonDocument.EnumerateArray())
                {
                    Console.WriteLine($"Processando carro {carro.GetProperty("modelo")}.");

                    try
                    {
                        var jsonCarro = wclient.DownloadString(new Uri($"http://g1.globo.com/static/fipe/json/carro/{carro.GetProperty("cod_fipe")}.json"));
                        var jsonCarroDocument = JsonSerializer.Deserialize<JsonElement>(jsonCarro);

                        foreach (var carroDetalhe in jsonCarroDocument.EnumerateArray())
                        {
                            var valoresAno = carroDetalhe.GetProperty("tipo").ToString().Split(" ").ToList();
                            var combustivel = valoresAno.Last();
                            valoresAno.Remove(valoresAno.Last());
                            var ano = string.Join(" ", valoresAno);

                            carros.Add(new FipeCarroLinha
                            {
                                Modelo = carro.GetProperty("modelo").ToString(),
                                Marca = carro.GetProperty("marca").ToString(),
                                Ano = ano,
                                Combustivel = combustivel,
                                //Valor = Convert.ToInt32(carroDetalhe.GetProperty("valor").ToString()),
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Deu ruim. :-( \t {ex}");
                    }
                }

                var arquivo = @"D:\Teste\fipe-out-2020.csv";
                var engine = new FileHelperEngine(typeof(FipeCarroLinha));
                engine.WriteFile(arquivo, carros);
            }
        }


        public static void CorrigirTudo()
        {
            foreach (var csv in Directory.GetFiles(@"D:\Teste\Fipe Todos", "*.csv"))
            {
                var arquivoDados = File.ReadAllText(csv);
                arquivoDados = arquivoDados.Replace("6.0, 6.2", "6.0[] 6.2");
                arquivoDados = arquivoDados.Replace("7,5kW", "7[]5kW");
                arquivoDados = arquivoDados.Replace("10,8m", "10[]8m");
                arquivoDados = arquivoDados.Replace("7,3m", "7[]3m");

                arquivoDados = arquivoDados.Replace(",", "\t");
                arquivoDados = arquivoDados.Replace("6.0[] 6.2", "6.0, 6.2");
                arquivoDados = arquivoDados.Replace("7[]5kW", "7,5kW");
                arquivoDados = arquivoDados.Replace("10[]8m", "10,8m");
                arquivoDados = arquivoDados.Replace("7[]3m", "7,3m");

                File.WriteAllText(csv, arquivoDados);
            }
        }


        public static void GerarTsvzao()
        {
            var listaTudo = new List<FipeCarroLinhaTudo>();

            foreach (var csv in Directory.GetFiles(@"D:\Teste\Fipe Todos", "*.csv"))
            {
                var engine = new FileHelperEngine(typeof(FipeCarroLinha));
                var lista = engine.ReadFile(csv).Cast<FipeCarroLinha>().ToList();
                var mesRef = Path.GetFileNameWithoutExtension(csv).Split("-")[1];
                var anoRef = Convert.ToInt32(Path.GetFileNameWithoutExtension(csv).Split("-")[2]);

                foreach (var linha in lista)
                {
                    var valor = Double.Parse(linha.Valor.Replace("R$ ", "").Replace(".", ""));

                    listaTudo.Add(new FipeCarroLinhaTudo
                    {
                        Ano = linha.Ano,
                        AnoReferencia = anoRef,
                        Combustivel = linha.Combustivel,
                        Marca = linha.Marca,
                        MesReferencia = mesRef,
                        Modelo = linha.Modelo,
                        Valor = valor
                    });
                }
            }

            var arquivo = Path.Combine(@"D:\Teste\Fipe Todos", "TabelaFipe.tsv");
            var engine2 = new FileHelperEngine(typeof(FipeCarroLinhaTudo));
            engine2.WriteFile(arquivo, listaTudo);
        }

        public static Dictionary<DateTime, double> ObterDolares()
        {
            var dolares = new Dictionary<DateTime, double>();

            var engine = new FileHelperEngine(typeof(DolarRef));
            var lista = engine.ReadFile(@"D:\Teste\Outros Sobre carros\Dolar1993_2020.csv").Cast<DolarRef>().ToList();

            foreach (var dolar in lista)
            {
                var mesAnterior = lista.FirstOrDefault(d => d.MesAno == dolar.MesAno.AddMonths(-1));
                var diff = 0.0;

                if (mesAnterior != null)
                    diff = (((dolar.Opening * 100) / mesAnterior.Opening) - 100) / 100;

                dolares.Add(dolar.MesAno, diff);
            }

            return dolares;
        }

        public static Dictionary<DateTime, double> ObterIGPM()
        {
            var dolares = new Dictionary<DateTime, double>();

            var engine = new FileHelperEngine(typeof(IGPMRef));
            var lista = engine.ReadFile(@"D:\Teste\Outros Sobre carros\f36e-serie-historica-igp-m-fgv.CSV").Cast<IGPMRef>().ToList();

            foreach (var dolar in lista)
            {
                dolares.Add(dolar.MesAno, dolar.Variacao / 100);
            }

            return dolares;
        }

        public static void GerarDiff()
        {
            var listaTudoDiff = new List<FipeCarroLinhaTudoDiff>();
            var engine = new FileHelperEngine(typeof(FipeCarroLinhaTudo));
            var lista = engine.ReadFile(@"D:\Teste\Fipe Todos\TabelaFipe.tsv").Cast<FipeCarroLinhaTudo>().ToList();
            var dolares = ObterDolares();
            var igpm = ObterIGPM();

            int linha = 0;

            //foreach (var modelo in lista)
            Parallel.ForEach(lista, modelo =>
            {
                var mesRefAtual = DateTime.ParseExact($"{modelo.AnoReferencia} {modelo.MesReferencia}", "yyyy MMMM", CultureInfo.CurrentCulture);
                var mesAnterior = mesRefAtual.AddMonths(-1);
                var diff = 0.0;

                var modeloAnterior = lista.FirstOrDefault(m => m.Modelo == modelo.Modelo
                                                && m.Marca == modelo.Marca
                                                && m.Ano == modelo.Ano
                                                && m.Combustivel == modelo.Combustivel
                                                && m.MesReferencia.Trim().ToLower() == mesAnterior.ToString("MMMM").Trim().ToLower()
                                                && m.AnoReferencia == mesAnterior.Year);

                if (modeloAnterior != null)
                    diff = (((modelo.Valor * 100) / modeloAnterior.Valor) - 100) / 100;

                var dolarAtual = 0.0;
                if (dolares.ContainsKey(mesRefAtual))
                    dolarAtual = dolares[mesRefAtual];

                var igpmAtual = 0.0;
                if (igpm.ContainsKey(mesRefAtual))
                    igpmAtual = igpm[mesRefAtual];

                listaTudoDiff.Add(new FipeCarroLinhaTudoDiff
                {
                    Ano = modelo.Ano,
                    AnoReferencia = modelo.AnoReferencia,
                    Combustivel = modelo.Combustivel,
                    Marca = modelo.Marca,
                    MesReferencia = modelo.MesReferencia,
                    Modelo = modelo.Modelo,
                    Valor = modelo.Valor,
                    Variacao = diff,
                    VariacaoDolar = dolarAtual,
                    VariacaoIGPM = igpmAtual
                });

                linha++;
                Console.WriteLine(linha);
            });

            var arquivo = Path.Combine(@"D:\Teste\Fipe Todos", "TabelaFipeDiff.tsv");
            var engine2 = new FileHelperEngine(typeof(FipeCarroLinhaTudoDiff));
            engine2.WriteFile(arquivo, listaTudoDiff);
        }


        public static void GerarDiff2()
        {
            var arquivo = Path.Combine(@"D:\Teste\Fipe Todos", "TabelaFipeDiff.tsv");
            var engine2 = new FileHelperEngine(typeof(FipeCarroLinhaTudoDiff));
            var listaAtual = engine2.ReadFile(arquivo).Cast<FipeCarroLinhaTudoDiff>().ToList();

            var engine = new FileHelperEngine(typeof(FipeCarroLinhaTudo));
            var listaAntigo = engine.ReadFile(@"D:\Teste\Fipe Todos\TabelaFipe.tsv").Cast<FipeCarroLinhaTudo>().ToList();
            var dolares = ObterDolares();
            var igpm = ObterIGPM();

            int linha = 0;


            /*lista.RemoveAll(modelo => listaAtual.Exists(m => m.Modelo == modelo.Modelo
                                                && m.Marca == modelo.Marca
                                                && m.Ano == modelo.Ano
                                                && m.Combustivel == modelo.Combustivel
                                                && m.MesReferencia == modelo.MesReferencia
                                                && m.AnoReferencia == modelo.AnoReferencia));*/

            /*var meses = listaAntigo.Select(m => m.MesReferencia).Distinct().ToList();
            var anos = listaAntigo.Select(m => m.AnoReferencia).Distinct().ToList();*/

            var meseseAnoAtual = (from modelo in listaAtual
                                  group modelo by new { modelo.AnoReferencia, modelo.MesReferencia }).ToList();

            var mesesAnoAntigo = (from modelo in listaAntigo
                                  group modelo by new { modelo.AnoReferencia, modelo.MesReferencia }).ToList();

            var listaFalta = new List<FipeCarroLinhaTudo>();

            foreach (var mesAnoAtual in meseseAnoAtual)
            {
                Console.WriteLine($"Mês {mesAnoAtual.Key.MesReferencia} de {mesAnoAtual.Key.AnoReferencia}");
                var mesAnoAntigo = mesesAnoAntigo.FirstOrDefault(a => a.Key.AnoReferencia == mesAnoAtual.Key.AnoReferencia && a.Key.MesReferencia == mesAnoAtual.Key.MesReferencia);

                foreach (var modelo in mesAnoAntigo)
                {
                    var modeloProcessado = mesAnoAtual.FirstOrDefault(m => m.Modelo == modelo.Modelo
                                                                && m.Marca == modelo.Marca
                                                                && m.Ano == modelo.Ano
                                                                && m.Combustivel == modelo.Combustivel
                                                                && m.MesReferencia == modelo.MesReferencia
                                                                && m.AnoReferencia == modelo.AnoReferencia);

                    if (modeloProcessado == null)
                        listaFalta.Add(modelo);
                }
            }




            /*foreach (var modelo in listaAtual)
            {
                var modeloProcessado = lista.FirstOrDefault(m => m.Modelo == modelo.Modelo
                                                && m.Marca == modelo.Marca
                                                && m.Ano == modelo.Ano
                                                && m.Combustivel == modelo.Combustivel
                                                && m.MesReferencia == modelo.MesReferencia
                                                && m.AnoReferencia == modelo.AnoReferencia);



                lista.Remove(modeloProcessado);
                linha++;
                Console.WriteLine(linha);
            }*/

            linha = 0;

            foreach (var modelo in listaFalta)
            {
                var mesRefAtual = DateTime.ParseExact($"{modelo.AnoReferencia} {modelo.MesReferencia}", "yyyy MMMM", CultureInfo.CurrentCulture);
                var mesAnterior = mesRefAtual.AddMonths(-1);
                var diff = 0.0;

                var modeloAnterior = listaAntigo.FirstOrDefault(m => m.Modelo == modelo.Modelo
                                                && m.Marca == modelo.Marca
                                                && m.Ano == modelo.Ano
                                                && m.Combustivel == modelo.Combustivel
                                                && m.MesReferencia.Trim().ToLower() == mesAnterior.ToString("MMMM").Trim().ToLower()
                                                && m.AnoReferencia == mesAnterior.Year);

                if (modeloAnterior != null)
                    diff = (((modelo.Valor * 100) / modeloAnterior.Valor) - 100) / 100;

                var dolarAtual = 0.0;
                if (dolares.ContainsKey(mesRefAtual))
                    dolarAtual = dolares[mesRefAtual];

                var igpmAtual = 0.0;
                if (igpm.ContainsKey(mesRefAtual))
                    igpmAtual = igpm[mesRefAtual];

                listaAtual.Add(new FipeCarroLinhaTudoDiff
                {
                    Ano = modelo.Ano,
                    AnoReferencia = modelo.AnoReferencia,
                    Combustivel = modelo.Combustivel,
                    Marca = modelo.Marca,
                    MesReferencia = modelo.MesReferencia,
                    Modelo = modelo.Modelo,
                    Valor = modelo.Valor,
                    Variacao = diff,
                    VariacaoDolar = dolarAtual,
                    VariacaoIGPM = igpmAtual
                });

                linha++;
                Console.WriteLine(linha);
            }


            engine2.WriteFile(arquivo, listaAtual);
        }

        public static void OrderBy()
        {
            var engine = new FileHelperEngine(typeof(FipeCarroLinhaTudo));
            var arquivo = Path.Combine(@"D:\Teste\Fipe Todos", "TabelaFipe.tsv");
            var lista = engine.ReadFile(arquivo).Cast<FipeCarroLinhaTudo>().ToList();
            lista = lista.OrderBy(l => l.AnoReferencia).ThenBy(l => l.MesReferencia).ThenBy(l => l.Marca).ThenBy(l => l.Modelo).ThenBy(l => l.Ano).ToList();
            engine.WriteFile(arquivo, lista);
        }

        public static Dictionary<DateTime, double> ObterDolares2()
        {
            var dolares = new Dictionary<DateTime, double>();

            var engine = new FileHelperEngine(typeof(DolarRef));
            var lista = engine.ReadFile(@"D:\Teste\Outros Sobre carros\Dolar1993_2020.csv").Cast<DolarRef>().ToList();

            foreach (var dolar in lista)
            {
                //var mesAnterior = lista.FirstOrDefault(d => d.MesAno == dolar.MesAno.AddMonths(-1));
                /*var diff = 0.0;

                if (mesAnterior != null)
                    diff = (((dolar.Opening * 100) / mesAnterior.Opening) - 100) / 100;*/

                dolares.Add(dolar.MesAno, dolar.Opening);
            }

            return dolares;
        }

        public static Dictionary<DateTime, double> ObterIGPM2()
        {
            var dolares = new Dictionary<DateTime, double>();

            var engine = new FileHelperEngine(typeof(IGPMRef));
            var lista = engine.ReadFile(@"D:\Teste\Outros Sobre carros\f36e-serie-historica-igp-m-fgv.CSV").Cast<IGPMRef>().ToList();

            foreach (var dolar in lista)
            {
                //dolares.Add(dolar.MesAno, dolar.Variacao / 100);
                dolares.Add(dolar.MesAno, dolar.Variacao);
            }

            return dolares;
        }

        public static List<PrecoPetroleo> ObterPetroleo()
        {
            var engine = new FileHelperEngine(typeof(PrecoPetroleo));
            var lista = engine.ReadFile(@"D:\Teste\Outros Sobre carros\BrentOilPrices.CSV").Cast<PrecoPetroleo>().ToList();
            var listaRetorno = new List<PrecoPetroleo>();

            lista.ForEach(p =>
            {
                DateTime data;

                if (p.Data.Length == 8)
                    p.Data = "0" + p.Data;

                if (p.Data.Length == 9)
                    DateTime.TryParseExact(p.Data, "dd-MMM-yy", new CultureInfo("en-US"), DateTimeStyles.None, out data);
                else
                    DateTime.TryParseExact(p.Data, "MMM dd yyyy", new CultureInfo("en-US"), DateTimeStyles.None, out data);

                data = new DateTime(data.Year, data.Month, 1);
                p.DataFormatada = data;

                if (!listaRetorno.Any(p2 => p2.DataFormatada == data))
                    listaRetorno.Add(p);
            });

            listaRetorno.ForEach(p =>
            {
                var mesAnterior = listaRetorno.FirstOrDefault(d => d.DataFormatada == p.DataFormatada.AddMonths(-1));
                var diff = 0.0;

                if (mesAnterior != null)
                    diff = (p.Valor / mesAnterior.Valor - 1);

                p.Variacao = diff;
            });

            return listaRetorno;
        }

        public static void MaisDados()
        {
            var dolares = ObterDolares2();
            var igpms = ObterIGPM2();
            var petroleo = ObterPetroleo();
            var listaFinal = new List<FipeCarroPetroleo>();

            var engineBase = new FileHelperEngine(typeof(FipeCarroLinhaTudoDiff));
            var arquivoBase = Path.Combine(@"D:\Teste\Fipe Todos", "TabelaFipeDiff.tsv");
            var listaInicial = engineBase.ReadFile(arquivoBase).Cast<FipeCarroLinhaTudoDiff>().ToList();

            var carroZero = listaInicial.Where(m => m.Ano == "32000").ToList();
            carroZero.ForEach(m => m.Ano = m.AnoReferencia.ToString());

            listaInicial.ForEach(modelo =>
            {
                var mesRefAtual = DateTime.ParseExact($"{modelo.AnoReferencia} {modelo.MesReferencia}", "yyyy MMMM", CultureInfo.CurrentCulture);

                var dolarAtual = 0.0;
                if (dolares.ContainsKey(mesRefAtual))
                    dolarAtual = dolares[mesRefAtual];

                var igpmAtual = 0.0;
                if (igpms.ContainsKey(mesRefAtual))
                    igpmAtual = igpms[mesRefAtual];


                var petroleoAtual = petroleo.FirstOrDefault(p2 => p2.DataFormatada == mesRefAtual);


                listaFinal.Add(new FipeCarroPetroleo
                {
                    Ano = modelo.Ano,
                    AnoReferencia = modelo.AnoReferencia,
                    Combustivel = modelo.Combustivel,
                    Dolar = dolarAtual,
                    IGPM = igpmAtual,
                    Marca = modelo.Marca,
                    MesReferencia = modelo.MesReferencia,
                    Modelo = modelo.Modelo,
                    Valor = modelo.Valor,
                    VariacaoDolar = modelo.VariacaoDolar,
                    VariacaoIGPM = modelo.VariacaoIGPM,
                    Variacao = modelo.Variacao,
                    Petroleo = petroleoAtual?.Valor ?? 0,
                    VariacaoPetroleo = petroleoAtual?.Variacao ?? 0
                });
            });

            listaFinal = listaFinal.OrderBy(l => l.AnoReferencia).ThenBy(l => l.MesReferencia).ThenBy(l => l.Marca).ThenBy(l => l.Modelo).ThenBy(l => l.Ano).ToList();

            var arquivo = Path.Combine(@"D:\Teste\Fipe Todos", "TabelaFipeTudo.tsv");
            var engine2 = new FileHelperEngine(typeof(FipeCarroPetroleo));
            engine2.WriteFile(arquivo, listaFinal);

            //var zero2 = lista.Where(m => m.Variacao == 0).ToList();
        }

        public static void TrocarMes()
        {
            var engineBase = new FileHelperEngine(typeof(FipeCarroPetroleo));
            var arquivoBase = Path.Combine(@"D:\Teste\Fipe Todos", "TabelaFipeTudo.tsv");
            var lista = engineBase.ReadFile(arquivoBase).Cast<FipeCarroPetroleo>().ToList();


            lista.ForEach(modelo =>
            {
                if (modelo.MesReferencia.ToLower() == "janeiro") modelo.MesReferencia = "01";
                if (modelo.MesReferencia.ToLower() == "fevereiro") modelo.MesReferencia = "02";
                if (modelo.MesReferencia.ToLower() == "março") modelo.MesReferencia = "03";
                if (modelo.MesReferencia.ToLower() == "abril") modelo.MesReferencia = "04";
                if (modelo.MesReferencia.ToLower() == "maio") modelo.MesReferencia = "05";
                if (modelo.MesReferencia.ToLower() == "junho") modelo.MesReferencia = "06";
                if (modelo.MesReferencia.ToLower() == "julho") modelo.MesReferencia = "07";
                if (modelo.MesReferencia.ToLower() == "agosto") modelo.MesReferencia = "08";
                if (modelo.MesReferencia.ToLower() == "setembro") modelo.MesReferencia = "09";
                if (modelo.MesReferencia.ToLower() == "outubro") modelo.MesReferencia = "10";
                if (modelo.MesReferencia.ToLower() == "novembro") modelo.MesReferencia = "11";
                if (modelo.MesReferencia.ToLower() == "dezembro") modelo.MesReferencia = "12";
            });

            lista = lista.OrderBy(l => l.AnoReferencia).ThenBy(l => l.MesReferencia).ThenBy(l => l.Marca).ThenBy(l => l.Modelo).ThenBy(l => l.Ano).ToList();
            engineBase.WriteFile(arquivoBase, lista);
        }

        public static void MesAnoRef()
        {
            var engineBase = new FileHelperEngine(typeof(FipeCarroPetroleo));
            var arquivoBase = Path.Combine(@"D:\Teste\Fipe Todos", "TabelaFipeTudo.tsv");
            var lista = engineBase.ReadFile(arquivoBase).Cast<FipeCarroPetroleo>().ToList();
            var listaFinal = new List<FipeCarroMesAnoRef>();

            lista.ForEach(modelo =>
            {
                listaFinal.Add(
                    new FipeCarroMesAnoRef
                    {
                        Ano = modelo.Ano,
                        AnoMesReferencia =$"{modelo.AnoReferencia}-{modelo.MesReferencia}",
                        Combustivel = modelo.Combustivel,
                        Dolar = modelo.Dolar,
                        IGPM = modelo.IGPM,
                        Marca = modelo.Marca,
                        Modelo = modelo.Modelo,
                        Petroleo = modelo.Petroleo,
                        Valor = modelo.Valor,
                        Variacao = modelo.Variacao,
                        VariacaoDolar = modelo.VariacaoDolar,
                        VariacaoIGPM = modelo.VariacaoIGPM,
                        VariacaoPetroleo = modelo.VariacaoPetroleo
                    });
            });

            listaFinal = listaFinal.OrderBy(l => l.AnoMesReferencia).ThenBy(l => l.Marca).ThenBy(l => l.Modelo).ThenBy(l => l.Ano).ToList();
            var engine2 = new FileHelperEngine(typeof(FipeCarroMesAnoRef));
            engine2.WriteFile(arquivoBase, listaFinal);
        }
    }
}
