using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sobujayonApp.Core.ServiceContracts
{
    public interface IJwtService
    {
        string GenerateJwtToken(Guid userId, string email, string personName);
    }
}
