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
    public class DepositController : ControllerBase
    {

        private readonly DepositService depositService;

        public DepositController(DepositService DepositService)
        {
            this.depositService = DepositService;
        }

        [HttpGet("{ID}")]
        [ProducesResponseType(typeof(DepositModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAsync(Guid ID)
        {
            DepositModel model = await depositService.GetAsync(ID);

            return Ok(model);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<DepositModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllAsync()
        {
            IEnumerable<DepositModel> modelList = await depositService.GetAllAsync();
            return Ok(modelList);
        }

        [HttpGet("total")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTotalAsync()
        {
            int total = await depositService.GetTotalAsync();

            return Ok(total);
        }

        [HttpPost]
        [ProducesResponseType(typeof(List<DepositModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> InsertAsync(DepositModel model)
        {
            if (model == null)
                return BadRequest("model is null");
            model = await depositService.InsertAsync(model);

            return Ok(model);
        }

        [HttpPost("insertMany")]
        public async Task<IActionResult> InsertManyAsync(List<DepositModel> modelList)
        {
            if (modelList == null)
                return BadRequest("model is null");

            await depositService.InsertManyAsync(modelList);

            return Ok();
        }

        [HttpPut]
        [ProducesResponseType(typeof(List<DepositModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAsync(DepositModel model)
        {
            if (model == null)
                return BadRequest("model is null");
            model = await depositService.UpdateAsync(model);

            return Ok(model);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteByIdAsync(Guid ID)
        {
            await depositService.DeleteByIdAsync(ID);
            return Ok();
        }
    }
}
