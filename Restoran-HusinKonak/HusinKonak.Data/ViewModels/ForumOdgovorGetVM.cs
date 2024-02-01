using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HusinKonak.Data.ViewModels
{
    public class ForumOdgovorGetVM
    {
        public int id { get; set; }
        public string odgovor { get; set; }
        public string datum_kreiranja { get; set; }
        public string autor_name { get; set; }
    }
}
