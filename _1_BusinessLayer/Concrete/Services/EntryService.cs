using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.AbstractServices.AbstractServices;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.Errors;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.ProxyResult;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Concrete.Services
{
    public class EntryService : AbstractEntryService
    {
        public EntryService(AbstractEntryRepository entryRepository, AbstractUserRepository userRepository) : base(entryRepository, userRepository)
        {
        }

        public override Task<ObjectIdentityResult<IdentityResult>> CreateEntry(int userId, int postId)
        {
            throw new NotImplementedException();
        }

        public override Task<IdentityResult> DeleteEntry(int userId, int entryId)
        {
            throw new NotImplementedException();
        }

        public override Task<IdentityResult> EditEntry(int userId, int entryId)
        {
            throw new NotImplementedException();
        }
    }
}
