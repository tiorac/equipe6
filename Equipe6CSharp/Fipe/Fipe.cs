using FileHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Equipe6.Fipe
{
    public static class Fipe
    {
        public static void ObterJsonCarrosFipeOrg()
        {
            var carros = new List<FipeCarroLinha>();


            using (var wclient = new WebClient())
            {
                var mesReferenciaJson = wclient.UploadString(new Uri("https://veiculos.fipe.org.br/api/veiculos//ConsultarTabelaDeReferencia"), "");
                var mesReferencias = JsonSerializer.Deserialize<JsonElement>(mesReferenciaJson);

                foreach (var mesReferencia in mesReferencias.EnumerateArray())
                {
                    Console.WriteLine($"Processando Mes {mesReferencia.GetProperty("Mes")}.");

                    try
                    {
                        var marcasJson = wclient.UploadString(new Uri($"https://veiculos.fipe.org.br/api/veiculos//ConsultarMarcas"), $"codigoTabelaReferencia={mesReferencia.GetProperty("Codigo")}&codigoTipoVeiculo=1");
                        var marcas = JsonSerializer.Deserialize<JsonElement>(marcasJson);


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


                        var arquivo = $"D:\\Teste\\fipe-{mesReferencia.GetProperty("Mes").ToString().Replace("/", "-")}.csv";
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
                                Valor = Convert.ToInt32(carroDetalhe.GetProperty("valor").ToString()),
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


        //JsonSerializer.
    }
}
