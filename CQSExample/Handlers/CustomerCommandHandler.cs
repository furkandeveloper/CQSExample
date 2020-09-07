using CQSExample.Commands;
using CQSExample.Context;
using CQSExample.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CQSExample.Handlers
{
    public class CustomerCommandHandler : IRequestHandler<CustomerCommand>
    {
        private readonly CQSSampleDbContext cQSSampleDbContext;

        public CustomerCommandHandler(CQSSampleDbContext cQSSampleDbContext)
        {
            this.cQSSampleDbContext = cQSSampleDbContext;
        }

        public Task<Unit> Handle(CustomerCommand request, CancellationToken cancellationToken)
        {
            cQSSampleDbContext.Customers.Add(new Entities.Customer()
            {
                CustomerName = request.CustomerName
            });
            cQSSampleDbContext.SaveChanges();
            return Task.FromResult(Unit.Value);
        }
    }
}
