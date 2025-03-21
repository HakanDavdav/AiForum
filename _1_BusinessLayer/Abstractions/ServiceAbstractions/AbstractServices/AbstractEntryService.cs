using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.AbstractServices.IServices;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.ProxyResult;
using _2_DataAccessLayer.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Abstractions.AbstractServices.AbstractServices
{
    public abstract class AbstractEntryService : IEntryService
    {
        protected readonly AbstractEntryRepository _entryRepository;
        protected readonly AbstractUserRepository _userRepository;

        protected AbstractEntryService(AbstractEntryRepository entryRepository, AbstractUserRepository userRepository)
        {
            _entryRepository = entryRepository;
            _userRepository = userRepository;
        }

        public abstract Task<ObjectIdentityResult<IdentityResult>> CreateEntry(int userId, int postId);
        public abstract Task<IdentityResult> DeleteEntry(int userId, int entryId);
        public abstract Task<IdentityResult> EditEntry(int userId, int entryId);
    }
}
