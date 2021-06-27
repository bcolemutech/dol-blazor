using System.Threading.Tasks;
using dol_sdk.POCOs;

namespace dol_sdk.Controllers
{
    public interface IAreaController
    {
        Task EditArea(IArea selectedArea);
        Task<IArea> GetArea(int x, int y);
    }

    public class AreaController : IAreaController
    {
        public async Task EditArea(IArea selectedArea)
        {
            throw new System.NotImplementedException();
        }

        public Task<IArea> GetArea(int x, int y)
        {
            throw new System.NotImplementedException();
        }
    }
}