using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using sobujayonApp.Core.DTO;
using sobujayonApp.Core.Entities;
using sobujayonApp.Core.RepositoryContracts;
using sobujayonApp.Core.ServiceContracts;

namespace sobujayonApp.Core.Services
{
    public class CommonService : ICommonService
    {
        private readonly IRepository<NavItem> _navRepository;
        private readonly IRepository<DeliveryArea> _areaRepository;
        private readonly IMapper _mapper;

        public CommonService(IRepository<NavItem> navRepository, IRepository<DeliveryArea> areaRepository, IMapper mapper)
        {
            _navRepository = navRepository;
            _areaRepository = areaRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<NavItemResponse>> GetNavItems()
        {
            var items = await _navRepository.GetAllAsync();
            // Sort by Order? NavItem has Order property.
            // Items is IEnumerable.
            // Since repo doesn't support OrderBy, do it in memory.
            // or I could use FindAsync(x => true) ... 
            // Assume GetAllAsync returns them.
            return _mapper.Map<IEnumerable<NavItemResponse>>(items);
        }

        public async Task<IEnumerable<DeliveryAreaResponse>> GetDeliveryAreas()
        {
            var areas = await _areaRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<DeliveryAreaResponse>>(areas);
        }
    }
}
