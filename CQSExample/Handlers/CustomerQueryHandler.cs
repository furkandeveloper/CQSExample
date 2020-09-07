using CQSExample.Context;
using CQSExample.Dtos;
using CQSExample.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CQSExample.Handlers
{
    public class CustomerQueryHandler : IRequestHandler<CustomerQuery, CustomerResponseDto>
    {
        private readonly CQSSampleDbContext cQSSampleDbContext;

        public CustomerQueryHandler(CQSSampleDbContext cQSSampleDbContext)
        {
            this.cQSSampleDbContext = cQSSampleDbContext;
        }
        public async Task<CustomerResponseDto> Handle(CustomerQuery request, CancellationToken cancellationToken)
        {
            return cQSSampleDbContext.Customers.Where(a => a.CustomerId == request.CustomerId).Select(s => new CustomerResponseDto()
            {
                CustomerId = s.CustomerId,
                CreateDate = s.CreateDate,
                CustomerName = s.CustomerName
            }).FirstOrDefault();
        }
    }
}
