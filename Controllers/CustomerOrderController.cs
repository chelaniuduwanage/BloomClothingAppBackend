using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Trendy_BackEnd.Config.EnumCollection;
using System.ComponentModel.DataAnnotations;
using Trendy_BackEnd.Models;
using Trendy_BackEnd.Services;
using System.Security.Principal;

namespace Trendy_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerOrderController : ControllerBase
    {
        //Fixed custom authentication
        [HttpPost]
        [Route("Save")]
        public async Task<ResponseResult> SignUp([FromBody, Required] ModelCustomerOrder model)
        {
            if (string.IsNullOrWhiteSpace(model.Address) || (string.IsNullOrWhiteSpace(model.FirstName)) || string.IsNullOrWhiteSpace(model.PaymentMethod))
            {
                return new ResponseResult()
                {
                    Status = ApiRespond.Fail.ToString(),
                    Content = null,
                    Message = "PaymentMethod | First Name | Last Name | Contact Number cannot be empty , Please Recheck your Inputs"
                };
            }
            else if (model.UserId <= 0 || model.Subtotal <= 0 || model.FullTotal <= 0)
            {
                return new ResponseResult()
                {
                    Status = ApiRespond.Fail.ToString(),
                    Content = null,
                    Message = "UserId | Subtotal |  Discount | FullTotal cannot be empty , Please Recheck your Inputs"
                };
            }
            else
            {
                if (model.Items == null || (model.Items).Count == 0)
                {
                    return new ResponseResult()
                    {
                        Status = ApiRespond.Fail.ToString(),
                        Content = null,
                        Message = "No Customer Order, Products Provided. Please Provide Products Details."
                    };
                }
                foreach (var Items in model.Items)
                {
                    if (Items.ItemId <= 0)
                    {
                        return new ResponseResult()
                        {
                            Status = ApiRespond.Fail.ToString(),
                            Content = null,
                            Message = "Error when Inserting Customer Order Details. Product Id must be Valid One."
                        };
                    }
                    else if (Items.Qty <= 0 || Items.Price <= 0 || Items.Total <= 0)
                    {
                        return new ResponseResult()
                        {
                            Status = ApiRespond.Fail.ToString(),
                            Content = null,
                            Message = "Error when Inserting Customer Order Details. Please recheck your inputs.(Numbers)"
                        };
                    }
                    else if (string.IsNullOrWhiteSpace(Items.Name) || (string.IsNullOrWhiteSpace(Items.Link)) || string.IsNullOrWhiteSpace(Items.size) || string.IsNullOrWhiteSpace(Items.Color))
                    {
                        return new ResponseResult()
                        {
                            Status = ApiRespond.Fail.ToString(),
                            Content = null,
                            Message = "Error when Inserting Customer Order Details. Please recheck your inputs.(Strings)"
                        };
                    }
                }
                if (string.IsNullOrWhiteSpace(model.CardLastDigits))
                {
                    model.CardLastDigits = "";
                }

                CustomerOrderServices Service = new();
                return await Service.ManageCustomerOrderSave(model);
            }

        }
    }
}
