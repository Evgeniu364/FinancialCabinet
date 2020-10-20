using FinancialCabinet.Model;
using FinancialCabinet.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialCabinet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CreditController : ControllerBase
    {

        private readonly CreditService creditService;

        public CreditController(CreditService CreditService)
        {
            this.creditService = CreditService;
        }

        [HttpGet("{ID}")]
        [ProducesResponseType(typeof(CreditModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAsync(Guid ID)
        {
            CreditModel model = await creditService.GetAsync(ID);

            return Ok(model);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<CreditModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllAsync()
        {
            IEnumerable<CreditModel> modelList = await creditService.GetAllAsync();
            return Ok(modelList);
        }

        [HttpGet("total")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTotalAsync()
        {
            int total = await creditService.GetTotalAsync();

            return Ok(total);
        }

        [HttpPost]
        [ProducesResponseType(typeof(List<CreditModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> InsertAsync(CreditModel model)
        {
            if (model == null)
                return BadRequest("model is null");
            model = await creditService.InsertAsync(model);

            return Ok(model);
        }

        [HttpPost("insertMany")]
        public async Task<IActionResult> InsertManyAsync(List<CreditModel> modelList)
        {
            if (modelList == null)
                return BadRequest("model is null");

            await creditService.InsertManyAsync(modelList);

            return Ok();
        }

        [HttpPut]
        [ProducesResponseType(typeof(List<CreditModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAsync(CreditModel model)
        {
            if (model == null)
                return BadRequest("model is null");
            model = await creditService.UpdateAsync(model);

            return Ok(model);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteByIdAsync(Guid ID)
        {
            await creditService.DeleteByIdAsync(ID);
            return Ok();
        }
    }
}
