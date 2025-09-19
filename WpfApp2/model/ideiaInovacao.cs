using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2.model
{
    internal class ideiaInovacao
    {
        public String Area { get; set; }
        public String Ideia { get; set; }
        public float Custo { get; set; }

        public Boolean CadastrarII(ideiaInovacao ii)
        {
            BD.BD.SalvarBD(ii);

            return true;
        }
        public override string ToString()
        {
            return $"{Area} / {Ideia} / {Custo}";
        }
    }
}
