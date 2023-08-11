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
    }
}
