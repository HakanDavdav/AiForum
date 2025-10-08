using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_DataAccessLayer.Concrete.Entities
{
    public class OutboxMessage
    {
        public Guid OutboxMessageId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? Type { get; set; }
        public string? Payload { get; set; }
        public DateTime? ProcessedOn { get; set; }
        public int RetryCount { get; set; }
    }
}
