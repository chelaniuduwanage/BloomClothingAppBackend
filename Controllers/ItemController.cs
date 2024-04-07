using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Trendy_BackEnd.Config.EnumCollection;
using System.ComponentModel.DataAnnotations;
using Trendy_BackEnd.Models;
using Trendy_BackEnd.Services;
using Microsoft.AspNetCore.Authentication;
using System.Reflection.Metadata.Ecma335;

namespace Trendy_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        //Fixed custom authentication
        [HttpGet]
        [Route("ItemById")]
        public async Task<ModelItem> GetItemById([FromQuery, Required] string Id)
        {
            if (string.IsNullOrWhiteSpace(Id))
            {
                return new ModelItem()
                {
                    Id = 0,
                    Name = null
                };
            }
            else
            {
                ItemServices Service = new();

                ModelItem r;
                r = new()
                {
                    Id = 0,
                    Name = null
                };

                return await Service.GetItemById(Id);
            }

        }



        [HttpGet]
        [Route("SearchItems")]
        public async Task<ActionResult<List<ModelItem>>> SearchItems([FromQuery, Required] int nextPage, [FromQuery, Required] int rowCount, [FromQuery, Required] decimal MinPrice, [FromQuery, Required] decimal MaxPrice,
           [FromQuery] string? Name = "", [FromQuery] string? Category = "", [FromQuery] string? Color = "", [FromQuery] string? ItemSize = "")
        {
            if (string.IsNullOrWhiteSpace(Name)) { Name = ""; }
            if (string.IsNullOrWhiteSpace(Category)) { Category = ""; }
            if (string.IsNullOrWhiteSpace(Color)) { Color = ""; }
            if (string.IsNullOrWhiteSpace(ItemSize)) { ItemSize = ""; }

            ItemServices service = new();
            return await service.SearchItems(Name, Category, Color, ItemSize, nextPage, rowCount, MinPrice, MaxPrice);
        }

       

    }
}
