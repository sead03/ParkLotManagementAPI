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
        public IActionResult GetSubscribers(string firstName, string lastName)
        {
            ResponseType type = ResponseType.Success;
            try
            {
                IEnumerable<Subscribers> data = _db.GetSubscriber();

                if (!string.IsNullOrEmpty(firstName))
                {
                    data = data.Where(subscriber => subscriber.firstName.Contains(firstName));
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
        public IActionResult Put([FromBody] Subscribers subscribers)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                _db.PostSubscriber(subscribers);
                return Ok(ResponseHandler.GetAppResponse(type, subscribers));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
        [HttpPost]
        [Route("api/[controller]/PostSubscriber")]
        public IActionResult Post([FromBody] Subscribers subscribers)
        {
            try
            {
                ResponseType type = ResponseType.Success;
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
                return Ok(ResponseHandler.GetAppResponse(type, "Delete Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
    }
}
