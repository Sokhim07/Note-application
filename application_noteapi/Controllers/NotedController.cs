using System.Data;
using Application_noteapi.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;

namespace Application_noteapi.Controller;

[Route("api/[controller]")]
[ApiController]
public class NotedController(IDbConnection con) : ControllerBase
{
    [HttpGet, Route("viewdata")]
    public IActionResult viewData([FromQuery] LOGIN dto)
    {

        try
        {
            var param = new DynamicParameters();
            param.Add("USER_NAME", dto.USER_NAME);
             param.Add("FILTER", dto.FILTER);

            var query = con.Query("NT.V_NOTED", param, commandType: CommandType.StoredProcedure);
            return Ok(query);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    [HttpGet, Route("viewdetail")]
    public IActionResult viewDetail([FromQuery] LOGIN dto)
    {

        try
        {
            var param = new DynamicParameters();
            param.Add("NT_ID", dto.NT_ID);

            var query = con.Query("NT.VD_NOTED", param, commandType: CommandType.StoredProcedure);
            return Ok(query);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet, Route("save")]
    public IActionResult Save([FromQuery] LOGIN dto)
    {
        try
        {
            var param = new DynamicParameters();
            param.Add("NT_ID", dto.NT_ID);
            param.Add("TITLE", dto.TITLE);
            param.Add("CONTENT", dto.CONTENT);
            param.Add("USER_NAME", dto.USER_NAME);
            

            var query = con.Query("NT.SV_NOTE_SAVE", param, commandType: CommandType.StoredProcedure);
            return Ok(query);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    [HttpGet, Route("delete")]
    public IActionResult Delete([FromQuery]LOGIN dto)
    {
       try
        {
                var param = new DynamicParameters();
                param.Add("NT_ID", dto.NT_ID);
                var query = con.Query("NT.NOTED_DELETE", param, commandType: CommandType.StoredProcedure);
                return Ok(query);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}