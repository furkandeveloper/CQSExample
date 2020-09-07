using CQSExample.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQSExample.Commands
{
    public class CustomerCommand : IRequest
    {
        public string CustomerName { get; set; }
    }
}
