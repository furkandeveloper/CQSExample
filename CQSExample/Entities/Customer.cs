using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQSExample.Entities
{
    public class Customer
    {
        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string CreateDate { get; set; } = DateTime.UtcNow;
    }
}
