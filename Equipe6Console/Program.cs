using System;
using System.Linq;

namespace Equipe6Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var referencias = args.Select(a => Int32.Parse(a)).ToList();

            Equipe6.Fipe.Fipe.ObterJsonCarrosFipeOrg(referencias);
            
        }
    }
}
