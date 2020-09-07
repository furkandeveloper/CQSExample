# CQS Example For .Net Core

Heeey, with this repo, you can learn the CQS principle using MediatR library on .Net Core.
Follow me;
  - Turn on your computer.
  - Prepare your coffee.
  - And sit back.

# What is CQS?

 If a method changes the state of the object, it should not return a result. If a result does return, then it should not change the state of the object.


# What is CQRS?

CQRS is a more advanced version of CQS. Although their goals are the same, the two are handled very differently from each other. CQRS says that we need to separate Command and Query objects at application level.

When CQS is implemented, your Command and Query objects are separated from each other. CQRS wants these two objects to be separated at the application level, so not only the methods but the applications and even the databases should be different.

### What are the requirements for using it?

* As always, the mental approach of the developers in the team is very important. First of all, this mental leap must be made within the team, that is, you should brainstorm as a team to determine which services are in imbalance between Write and Read operations.
* Too much of everything is harmful :) CQRS should not be applied in the whole system, a good analysis should be done on the system and this pattern should be applied on the necessary objects.
* CQRS allows you to separate the load from reads and writes so you can scale each independently. This pattern is very useful if your application has a large disparity between reading and writing. Even without this, you can apply different optimization strategies to both sides. For example, using different database access techniques for reading and updating.
* If you have ever-changing business rules and a complex system, this pattern can ease your workload.

### MediatR
![mediator](https://user-images.githubusercontent.com/47147484/92412229-c81aca80-f153-11ea-9506-2e77eec65b65.png)

#### Startup.cs
```csharp
public void ConfigureServices(IServiceCollection services)
{
  services.AddMediatR(typeof(Startup));
}
```
#### CustomerCommand.cs
```csharp
public class CustomerCommand : IRequest
{
    public string CustomerName { get; set; }
}
```

#### CustomerQuery.cs
```csharp
public class CustomerQuery : IRequest<CustomerResponseDto>
{
    public int CustomerId { get; set; }
}
```

### CustomerCommandHandler.cs
```csharp
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
```

### CustomerQueryHandler.cs
```csharp
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
```
### CustomerController.cs
```csharp
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
```

For Turkish article: https://medium.com/mobiroller-tech/teknik-muhabbetler-3-cqrs-dfc32f44280e
