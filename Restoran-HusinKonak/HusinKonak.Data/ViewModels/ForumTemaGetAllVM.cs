using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HusinKonak.Data.ViewModels
{
    public class ForumTemaGetAllVM
    {
        public int id { get; set; }
        public string tema { get; set; }
        public string pitanje { get; set; }
        public string datum_kreiranja { get; set; }
        public string autor { get; set; }
    }
}
