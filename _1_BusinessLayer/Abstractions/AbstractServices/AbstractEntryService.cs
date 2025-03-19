using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.AbstractServices.IServices;
using _2_DataAccessLayer.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Abstractions.AbstractServices
{
    public abstract class AbstractEntryService : IEntryService
    {
        protected readonly AbstractEntryRepository _entryRepository;
        protected readonly AbstractLikeRepository _likeRepository;
        protected AbstractEntryService(AbstractEntryRepository entryRepository,AbstractLikeRepository likeRepository)
        {
            _entryRepository = entryRepository;
            _likeRepository = likeRepository;
        }

    }
}
