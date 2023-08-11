using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Kiota.Abstractions;
using ParkLotManagementAPI.EfCore;
using ParkLotManagementAPI.Models;

namespace ParkLotManagementAPI.Controller
{
    [ApiController]
    public class ParkSpotsController : ControllerBase
    {
        private readonly DbHelper _db;
        public ParkSpotsController(EF_DataContext eF_DataContext, DbHelper db)
        {
            _db = db;
        }
        [HttpGet]
        [Route("api/[controller]/GetParkSpots")]
        public IActionResult Get()
        {
            ResponseType type = ResponseType.Success;
            try
            {            
                IEnumerable<ParkSpots> data = _db.GetParkSpots();
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
        [Route("api/[controller]/UpdateParkSpots")]
        public IActionResult Put([FromBody] ParkSpotsDto parkSpots)
        {

            try
            {
                ResponseType type = ResponseType.Success;
                _db.SaveParkSpots(parkSpots);
                return Ok(ResponseHandler.GetAppResponse(type, parkSpots));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
        [HttpGet]
        [Route("api/[controller]/GetWeekdayPricingPlans")]
        public IActionResult GetWeekdayPricePlan()
        {
            ResponseType type = ResponseType.Success;
            try
            {
                IEnumerable<WeekdayPricePlan> data = _db.GetWeekdayPricePlans();
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
        [Route("api/[controller]/UpdateWeekdayPricePlan")]
        public IActionResult PutWeekdayPricePlan([FromBody] WeekdayPricePlanDto weekdayPricePlan)
        {

            try
            {
                ResponseType type = ResponseType.Success;
                _db.SaveWeekdayPricePlans(weekdayPricePlan);
                return Ok(ResponseHandler.GetAppResponse(type, weekdayPricePlan));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
        [HttpGet]
        [Route("api/[controller]/GetWeekendPricingPlans")]
        public IActionResult GetWeekendPricePlan()
        {
            ResponseType type = ResponseType.Success;
            try
            {
                IEnumerable<WeekendPricePlan> data = _db.GetWeekendPricePlans();
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
        [Route("api/[controller]/UpdateWeekendPricePlan")]
        public IActionResult PutWeekendPricePlan([FromBody] WeekendPricePlanDto weekendPricePlan)
        {

            try
            {
                ResponseType type = ResponseType.Success;
                _db.SaveWeekendPricePlans(weekendPricePlan);
                return Ok(ResponseHandler.GetAppResponse(type, weekendPricePlan));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
    }
}
