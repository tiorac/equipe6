using System;
using System.Linq;

namespace Equipe6Console
{
    class Program
    {
        static void Main(string[] args)
        {
            /*var referencias = args.Select(a => Int32.Parse(a)).ToList();
            Equipe6.Fipe.Fipe.ObterJsonCarrosFipeOrg(referencias);*/


            //Equipe6.Fipe.Fipe.CorrigirTudo();
            //Equipe6.Fipe.Fipe.GerarTsvzao();

            //Equipe6.Fipe.Fipe.ObterDolares();
            //Equipe6.Fipe.Fipe.ObterIGPM();

            //Equipe6.Fipe.Fipe.GerarDiff();
            //Equipe6.Fipe.Fipe.GerarDiff2();
            //Equipe6.Fipe.Fipe.MaisDados();
            //Equipe6.Fipe.Fipe.MesAnoRef();

            Libras.DownloadLibras.ObterVideos();
        }
    }
}
