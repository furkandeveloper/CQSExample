using CQSExample.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQSExample.Queries
{
    public class CustomerQuery : IRequest<CustomerResponseDto>
    {
        public int CustomerId { get; set; }
    }
}
