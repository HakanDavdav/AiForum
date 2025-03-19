using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_BusinessLayer.Concrete.Dtos.EntryDtos
{
    public class EditEntryDto
    {
        public int EntryId { get; set; }
        public string Context { get; set; }
        public DateTime DateTime { get; set; }

    }
}
