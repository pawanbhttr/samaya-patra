using System;
using System.Collections.Generic;
using System.Text;

namespace SamayaPatra.Common
{
    public class UserRole
    {
        public int UserRoleID { get; set; }
        public string UserRoles { get; set; }
        public int Hierarchy { get; set; }
        public bool IsActive { get; set; }
    }
}
