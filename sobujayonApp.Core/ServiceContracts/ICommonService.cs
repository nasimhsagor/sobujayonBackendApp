using System.Collections.Generic;
using System.Threading.Tasks;
using sobujayonApp.Core.DTO;

namespace sobujayonApp.Core.ServiceContracts
{
    public interface ICommonService
    {
        Task<IEnumerable<NavItemResponse>> GetNavItems();
        Task<IEnumerable<DeliveryAreaResponse>> GetDeliveryAreas();
    }
}
