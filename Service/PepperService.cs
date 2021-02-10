using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Common;
using Repository.Common;
using Repository;
using Model.Common;
using Model;

namespace Service
{
    public class PepperService: IPepperService
    {
        protected IPepperRepository Repository { get; set; }

        public PepperService(IPepperRepository pepperRepository)
        {
            Repository = pepperRepository;
        }

        public async Task<bool> SavePepperAsync(IPepperModel model)
        {
            return await Repository.SavePepperAsync(model);
        }

        public async Task<List<IPepperModel>> GetAllPeppersAsync(int pageSize, int  pageNumber, string sort, string filterBy)
        {
            return await Repository.GetAllPeppersAsync(pageSize, pageNumber, sort, filterBy);
        }

        public async Task<bool> UpdatePepperAsync(IPepperModel model)
        {
            return await Repository.UpdatePepperAsync(model);
        }

        public async Task<bool> DeletePepperAsync(int id)
        {
            return await Repository.DeletePepperAsync(id);
        }
    }
}
