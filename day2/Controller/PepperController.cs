using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Service.Common;
using Service;
using Model;
using System.Threading.Tasks;
using Model.Common;
using AutoMapper;

namespace day2.Controller
{
    public class PepperController : ApiController
    {
        protected IPepperService _service;
        public PepperController(IPepperService service)
        {
            _service = service;
        }
        
        [HttpPost]
        public async Task<HttpResponseMessage> SavePepperAsync(PepperModel model)
        {
            bool response = await _service.SavePepperAsync(model);
            
            if(response)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Successfully updated database.");
            } else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Unsuccessul!");
            }
        }
                
        [Route("show/{id}")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetAllPeppersAsync([FromUri]int pageSize, [FromUri]int pageNumber, [FromUri]string sort, [FromUri]string filterBy)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<IPepperModel, PepperViewModel>());

            List<PepperViewModel> viewModel = new List<PepperViewModel>();
                     
            List<IPepperModel> response = await _service.GetAllPeppersAsync(pageSize, pageNumber, sort, filterBy);

            IMapper iMapper = config.CreateMapper();

            foreach (IPepperModel model in response)
            {               
                var newPepper = iMapper.Map<IPepperModel, PepperViewModel>(model);
                viewModel.Add(newPepper);
            }
                       
            if (viewModel != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, viewModel);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Unsuccessful!");
            }
        }

        [Route("update/{id}/{name}")]
        [HttpPut]
        public async Task<HttpResponseMessage> UpdatePepperAsync(PepperModel model)
        {
            bool response = await _service.UpdatePepperAsync(model);

            if (response)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Successfully updated database.");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Unsuccessful!");
            }
        }

        [Route("delete/{id}/{option}")]
        [HttpDelete]
        public async Task<HttpResponseMessage> DeletePepperAsync([FromUri]int id)
        {
            bool response = await _service.DeletePepperAsync(id);

            if (response)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Successfully deleted record.");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Unsuccessful!");
            }
        }
    }
}
