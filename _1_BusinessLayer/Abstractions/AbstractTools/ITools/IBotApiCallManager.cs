using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete.Entities;

namespace _1_BusinessLayer.Abstractions.AbstractTools.ITools
{
    public interface IBotApiCallManager
    {
        public Task<String> CreateResponse(Bot bot);
    }
}
