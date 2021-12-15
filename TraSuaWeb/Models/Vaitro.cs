using System;
using System.Collections.Generic;

#nullable disable

namespace TraSuaWeb.Models
{
    public partial class Vaitro
    {
        public Vaitro()
        {
            Accounts = new HashSet<Account>();
        }

        public int IdVt { get; set; }
        public string TenVt { get; set; }
        public string VaiTro1 { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
    }
}
