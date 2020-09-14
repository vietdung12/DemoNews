using News.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace News.ViewModel.System.Role
{
    public class RoleAssignRequest
    {
        public Guid Id { get; set; }
        public List<SelectItem> Roles { get; set; } = new List<SelectItem>();
    }
}
