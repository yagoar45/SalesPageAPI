using Microsoft.AspNetCore.Mvc;
using SalesPageAPI.Models;
using SalesPageAPI.Services.SalesListServices;

namespace SalesPageAPI.Controllers
{
    [ApiController]
    [Route("/api/SalesPage/[Controller]")]
    public class SalesListController : ControllerBase
    {

        private readonly SalesListServices _addSalesListService;

        public SalesListController(SalesListServices addSalesListService)
        {
            _addSalesListService = addSalesListService;
        }

        [HttpPost("AddSalesList")]
        public async Task<IActionResult> AddSalesList([FromBody] SalesListsModel salesLists)
        {
            await _addSalesListService.AddSalesList(salesLists);
            return Ok("Lista adicionada com sucesso !");
        }

        [HttpGet("SearchSalesLists")]
        public async Task<IActionResult> SearchSalesList()
        {
           var result =  await _addSalesListService.SearchSalesLists();
            return Ok(result);
        }
    }
}
