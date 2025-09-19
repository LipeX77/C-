using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp2.model;

namespace WpfApp2.BD
{
    internal class BD
    {
        public static List<ideiaInovacao> mybd = new();

        public static void SalvarBD(ideiaInovacao ii) => mybd.Add(ii);

        public static List<ideiaInovacao> RetornarBD() => mybd;
    }
}
