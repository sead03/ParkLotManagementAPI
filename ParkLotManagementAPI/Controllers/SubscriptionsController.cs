using Microsoft.AspNetCore.Mvc;
using ParkLotManagementAPI.EfCore;
using ParkLotManagementAPI.Models;

namespace ParkLotManagementAPI.Controllers
{
    [ApiController]
    public class SubscriptionsController : ControllerBase
    {
        private readonly DbHelper _db;
        public SubscriptionsController(EF_DataContext eF_DataContext, DbHelper db)
        {
            _db = db;
        }
        [HttpGet]
        [Route("api/[controller]/GetSubscription")]
        public IActionResult GetSubscriptions(int code)
        {
            ResponseType type = ResponseType.Success;
            try
            {
                IEnumerable<Subscriptions> data = _db.GetSubscriptions();

                if (!data.Any())
                {
                    type = ResponseType.NotFound;
                }

                return Ok(ResponseHandler.GetAppResponse(type, data));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpPut]
        [Route("api/[controller]/UpdateSubscription")]
        public IActionResult Put([FromBody] Subscriptions subscriptions)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                _db.UpdateSubscription(subscriptions);
                return Ok(ResponseHandler.GetAppResponse(type, subscriptions));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpDelete]
        [Route("api/[controller]/DeleteSubscription/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                _db.DeleteSubscription(id);
                return Ok(ResponseHandler.GetAppResponse(type, "Delete Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
    }
}
