using System.Data;
using Application_noteapi.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Application_noteapi.Utils;
using Application_noteapi.Utils.Security;
using System.Security.Cryptography;
using System.Text;

namespace Application_noteapi.Controller;

[Route("api/[controller]")]
[ApiController]
public class UserController(IDbConnection con) : ControllerBase
{
    public static byte[] HashPassword(string password)
{
    using var sha = SHA256.Create();
    return sha.ComputeHash(Encoding.UTF8.GetBytes(password));
}
[HttpGet]
[Route("login")]
public IActionResult Login([FromQuery] LOGIN dto)
{
    try
    {
        var param = new DynamicParameters();
        param.Add("USER_NAME", dto.USER_NAME);
        param.Add("PASSWORD", dto.PASSWORD);
        var result = con.Query("NT.US_LOGIN", param, commandType: CommandType.StoredProcedure).ToList();

        if (result.Count > 0)
        {
                return Ok(result);
        }

        return StatusCode(418, new { locale = "invalid_username_or_password" });
    }
    catch (Exception ex)
    {
        return StatusCode(500, new { error = ex.Message });
    }
}


    [HttpGet, Route("viewdata")]
    public IActionResult viewData([FromQuery] LOGIN dto)
    {

        try
        {
            var param = new DynamicParameters();
            param.Add("FILTER", dto.FILTER);

            var query = con.Query("NT.V_USER", param, commandType: CommandType.StoredProcedure);
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
            param.Add("US_ID", dto.US_ID);

            var query = con.Query("NT.VD_USER", param, commandType: CommandType.StoredProcedure);
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
            param.Add("US_ID", dto.US_ID);
            param.Add("USER_NAME", dto.USER_NAME);
            param.Add("PASS", dto.PASSWORD);
            param.Add("IS_LOCK", dto.IS_LOCK);
            param.Add("LLOG_IN", dto.LLOG_IN);
            param.Add("IS_ONLINE", dto.IS_ONLINE);
            param.Add("LLOG_OUT", dto.LLOG_OUT);	


            var query = con.Query("NT.USER_SAVE", param, commandType: CommandType.StoredProcedure);
            return Ok(query);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    
}