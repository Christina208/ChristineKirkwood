using Sabio.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sabio.Services.Interfaces
{
    public interface IAdminDashServices
    {
        public List<AdminDash> Get();
    }
}
