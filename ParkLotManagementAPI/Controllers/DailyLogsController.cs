using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkLotManagementAPI.EfCore;
using ParkLotManagementAPI.Models;

namespace ParkLotManagementAPI.Controllers
{
    [ApiController]
    public class DailyLogsController : ControllerBase
    {
        private readonly DbHelper _db;
        public DailyLogsController(EF_DataContext eF_DataContext, DbHelper db)
        {
            _db = db;
        }
        [HttpGet]
        [Route("api/[controller]/GetDailyLogs")]
        public IActionResult GetDailyLogs()
        {
            try
            {
                List<DailyLogs> logsWithCalculatedPrice = _db.GetLogsWithCalculatedPrice();
                return Ok(logsWithCalculatedPrice);
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpPost]
        [Route("api/[controller]/CreateLog")]
        public IActionResult CreateLog([FromBody] DailyLogs dailyLogs)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _db.CreateLogWithRandomCode(dailyLogs);

                return Ok("Log created successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred: " + ex.Message);
            }
        }

    }
}
