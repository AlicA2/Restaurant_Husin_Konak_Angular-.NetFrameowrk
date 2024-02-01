using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HusinKonak.Data.ViewModels
{
    public class ForumTemaOdgovorGetVM
    {
        public string tema { get; set; }
        public string pitanje { get; set; }
        public string datum_kreiranja { get; set; }
        public string kreator { get; set; }
        public List<ForumOdgovorGetVM> forum_odgovori { get; set; }
    }

}
