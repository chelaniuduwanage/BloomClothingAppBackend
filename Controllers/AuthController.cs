using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Trendy_BackEnd.Config.EnumCollection;
using System.ComponentModel.DataAnnotations;
using Trendy_BackEnd.Models;
using Trendy_BackEnd.Services;

namespace Trendy_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {


        //Fixed custom authentication
        [HttpGet]
        [Route("Login")]
        public async Task<ResponseResult> SignIn([FromQuery, Required] string Email, [FromQuery, Required] string Password)
        {
            if (string.IsNullOrWhiteSpace(Email) || (string.IsNullOrWhiteSpace(Password)))
            {
                return new ResponseResult()
                {
                    Status = ApiRespond.Fail.ToString(),
                    Content = null,
                    Message = "Email or Password cannot be empty , Please Recheck your Inputs"
                };
            }
            else
            {
                AuthServices Service = new();
                return await Service.SignIn(Email, Password);
            }

        }

        //Fixed custom authentication
        [HttpPost]
        [Route("SignUp")]
        public async Task<ResponseResult> SignUp([FromBody, Required] ModelUser model)
        {
            if (string.IsNullOrWhiteSpace(model.Email) || (string.IsNullOrWhiteSpace(model.Password)) || string.IsNullOrWhiteSpace(model.Address1) || string.IsNullOrWhiteSpace(model.FirstName) || string.IsNullOrWhiteSpace(model.LastName) || string.IsNullOrWhiteSpace(model.ContactNumber))
            {
                return new ResponseResult()
                {
                    Status = ApiRespond.Fail.ToString(),
                    Content = null,
                    Message = "Email | Password | Address1 | First Name | Last Name | Contact Number cannot be empty , Please Recheck your Inputs"
                };
            }
            else
            {
                AuthServices Service = new();
                return await Service.SignUp(model);
            }

        }

        //Fixed custom authentication
        [HttpPut]
        [Route("UpdateAddress2")]
        public async Task<ResponseResult> UpdateAddress2([FromQuery, Required] string Id, [FromQuery, Required] string Address2)
        {
            if (string.IsNullOrWhiteSpace(Id) || string.IsNullOrWhiteSpace(Address2))
            {
                return new ResponseResult()
                {
                    Status = ApiRespond.Fail.ToString(),
                    Content = null,
                    Message = "Id or Address cannot be empty , Please Recheck your Inputs"
                };
            }
            else
            {
                AuthServices Service = new();
                return await Service.UpdateAddress2(Id, Address2);
            }

        }


    }
}
