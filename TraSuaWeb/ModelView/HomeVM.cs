using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraSuaWeb.Models;

namespace TraSuaWeb.ModelView
{
    public class HomeVM
    {
        public List<ProductHomeVM> sanphams { get; set; }
        public List<Tintuc> tintucs { get; set; }
    }
}