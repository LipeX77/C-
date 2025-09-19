using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WpfApp2.model;

namespace WpfApp2.control
{
    
    internal class IdeiaInovacaoControle
    {
        private ideiaInovacao ModeloPersistencia = new();


        public Boolean ControleCadastrarII(string area, string ideia, float custo)
        {

            ideia = ideia + "!!!!!!!";
            ideiaInovacao ii = new()
            {
                Area = area,
                Ideia = ideia,
                Custo = custo
            };
            ModeloPersistencia.CadastrarII(ii);

            if (ModeloPersistencia.CadastrarII(ii))
                return true;

            return false;
        }



    }
}
