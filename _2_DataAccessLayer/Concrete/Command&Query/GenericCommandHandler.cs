using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Abstractions.Generic;

namespace _2_DataAccessLayer.Concrete.Cqrs
{
    public class GenericCommandHandler : AbstractGenericCommandHandler 
    {
        public GenericCommandHandler(ApplicationDbContext context) : base(context)
        {
        }
    }

}
