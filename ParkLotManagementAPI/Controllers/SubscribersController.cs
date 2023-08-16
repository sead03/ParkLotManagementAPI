using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkLotManagementAPI.EfCore;
using ParkLotManagementAPI.Models;

namespace ParkLotManagementAPI.Controllers
{
    [ApiController]
    public class SubscribersController : ControllerBase
    {
        private readonly DbHelper _db;
        public SubscribersController(EF_DataContext eF_DataContext, DbHelper db)
        {
            _db = db;
        }
        [HttpGet]
        [Route("api/[controller]/GetSubscribers")]
        public IActionResult GetSubscribers()
        {
            ResponseType type = ResponseType.Success;
            try
            {
                IEnumerable<Subscribers> data = _db.GetSubscriber();
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
        [Route("api/[controller]/GetSubscriber")]
        public IActionResult GetSubscribers(string? firstName, string? lastName, int? cardNrId, string? email)
        {
            ResponseType type = ResponseType.Success;
            try
            {
                IEnumerable<Subscribers> data = _db.GetSubscriber();

                if (!string.IsNullOrEmpty(firstName))
                {
                    data = data.Where(subscriber => subscriber.firstName == firstName);
                }

                if (!string.IsNullOrEmpty(lastName))
                {
                    data = data.Where(subscriber => subscriber.lastName == lastName );
                }

                if (cardNrId.HasValue)
                {
                    data = data.Where(subscriber => subscriber.cardNumberId == cardNrId);
                }

                if (!string.IsNullOrEmpty(email))
                {
                    data = data.Where(subscriber => subscriber.email == email);
                }

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
        [Route("api/[controller]/UpdateSubscriber")]
        public IActionResult UpdateSubscriber([FromBody] Subscribers subscribers)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _db.UpdateSubscriber(subscribers);

                return Ok("Subscriber updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred: " + ex.Message);
            }
        }

        [HttpPost]
        [Route("api/[controller]/PostSubscriber")]
        public IActionResult Post([FromBody] Subscribers subscribers)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                ResponseType type = ResponseType.Success;

                // Check for duplicate ID card number
                bool isDuplicate = _db.CheckDuplicateIdCardNumber(subscribers.cardNumberId);
                if (isDuplicate)
                {
                    return BadRequest("A person with the same ID card number already exists.");
                }

                _db.PostSubscriber(subscribers);

                return Ok(ResponseHandler.GetAppResponse(type, subscribers));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
        [HttpDelete]
        [Route("api/[controller]/DeleteSubscriber/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _db.DeleteSubscriber(id);
                return Ok("Subscriber soft deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred: " + ex.Message);
            }
        }
    }
}
