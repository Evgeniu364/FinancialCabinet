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
    public class BankController: ControllerBase
    {

        private readonly BankService bankService;

        public BankController(BankService bankService)
        {
            this.bankService = bankService;
        }

        [HttpGet("{ID}")]
        [ProducesResponseType(typeof(BankModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAsync(Guid ID)
        {
            BankModel model = await bankService.GetAsync(ID);

            return Ok(model);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<BankModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllAsync()
        {
            IEnumerable<BankModel> modelList = await bankService.GetAllAsync();
            return Ok(modelList);
        }

        [HttpGet("total")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTotalAsync()
        {
            int total = await bankService.GetTotalAsync();

            return Ok(total);
        }

        [HttpPost]
        [ProducesResponseType(typeof(List<BankModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> InsertAsync(BankModel model)
        {
            if (model == null)
                return BadRequest("model is null");
            model = await bankService.InsertAsync(model);

            return Ok(model);
        }

        [HttpPost("insertMany")]
        public async Task<IActionResult> InsertManyAsync(List<BankModel> modelList)
        {
            if (modelList == null)
                return BadRequest("model is null");

            await bankService.InsertManyAsync(modelList);

            return Ok();
        }

        [HttpPut]
        [ProducesResponseType(typeof(List<BankModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAsync(BankModel model)
        {
            if (model == null)
                return BadRequest("model is null");
            model = await bankService.UpdateAsync(model);

            return Ok(model);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteByIdAsync(Guid ID)
        {
            await bankService.DeleteByIdAsync(ID);
            return Ok();
        }
    }
}
