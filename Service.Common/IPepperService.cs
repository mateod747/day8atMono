using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Common;
using Model;

namespace Service.Common
{
    public interface IPepperService
    {
        Task<bool> SavePepperAsync(IPepperModel model);
        Task<List<IPepperModel>> GetAllPeppersAsync(int pageSize, int pageNumber, string sort, string filterBy);
        Task<bool> UpdatePepperAsync(IPepperModel model);
        Task<bool> DeletePepperAsync(int id);
    }
}
