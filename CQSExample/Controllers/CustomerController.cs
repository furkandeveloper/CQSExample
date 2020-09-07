using CQSExample.Commands;
using CQSExample.Dtos;
using CQSExample.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQSExample.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator mediator;

        public CustomerController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetCustomerAsync(int customerId)
        {
            return Ok(mediator.Send<CustomerResponseDto>
                (new CustomerQuery() { CustomerId = customerId }));
        }

        [HttpPost]
        public async Task<IActionResult> InsertCustomerAsync([FromBody] CustomerCommand customerCommand)
        {
            await mediator.Send(customerCommand);
            return NoContent();
        }
    }
}
