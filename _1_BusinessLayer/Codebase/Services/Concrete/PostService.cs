using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Services._Concrete;
using _2_DataAccessLayer.Abstractions.Generic;
using _2_DataAccessLayer.Concrete.Cqrs;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace _1_BusinessLayer.Codebase.Services.Concrete
{
    public class PostService
    {
        AbstractGenericQueryHandler _queryHandler;
        AbstractGenericCommandHandler _commandHandler;
        ILogger<PostService> _logger;

        public PostService(AbstractGenericCommandHandler commandHandler, AbstractGenericQueryHandler queryHandler, ILogger<PostService> logger)
        {
            _commandHandler = commandHandler;
            _queryHandler = queryHandler;
            _logger = logger;
        }

        public async Task<IdentityResult> CreatePost(CreatePos)
        {


        }

        public async Task<IdentityResult> DeletePost()
        {

        }

        public async Task<IdentityResult> EditPost()
        {

        }

        public async Task<IdentityResult> LoadPostEntryModules()
        {

        }

        public async Task<IdentityResult> LoadPostLikeModules()
        {

        }

        public async Task<IdentityResult> GetPost()
        {

        }
    }
}
