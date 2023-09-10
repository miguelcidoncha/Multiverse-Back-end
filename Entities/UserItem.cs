using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class UserItem
    {
        public int IdUsuario { get; set; }
        public string UserName { get; set; }
        public ushort IdRoll { get; set; } // Cambiar el tipo de datos de int a ushort
        public string Password { get; set; }
        public string Email { get; set; }

    }
}
