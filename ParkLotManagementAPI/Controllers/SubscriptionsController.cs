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
        public IActionResult GetSubscription()
        {
            ResponseType type = ResponseType.Success;
            try
            {
                IEnumerable<Subscriptions> data = _db.GetSubscription();

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
        [HttpGet]
        [Route("api/[controller]/GetSubscription{code}")]
        public IActionResult GetSubscriptionByCode(int code)
        {
            try
            {
                Subscriptions subscription = _db.GetSubscriptionByCode(code);

                if (subscription == null)
                {
                    return NotFound("Subscription not found.");
                }

                return Ok(ResponseHandler.GetAppResponse(ResponseType.Success, subscription));
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred: " + ex.Message);
            }
        }

        [HttpPost]
        [Route("api/[controller]/CreateSubscription")]
        public IActionResult CreateSubscription([FromBody] Subscriptions subscription)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _db.CreateSubscription(subscription);

                return Ok("Subscription created successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred: " + ex.Message);
            }
        }

        [HttpPut]
        [Route("api/[controller]/UpdateSubscription")]
        public IActionResult UpdateSubscription([FromBody] Subscriptions subscription)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _db.UpdateSubscription(subscription);

                return Ok("Subscription updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred: " + ex.Message);
            }
        }

        [HttpDelete]
        [Route("api/[controller]/DeleteSubscription/{code}")]
        public IActionResult Delete(int code)
        {
            try
            {
                _db.DeleteSubscription(code);
                return Ok("Subscriber soft deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred: " + ex.Message);
            }
        }
    }
}
