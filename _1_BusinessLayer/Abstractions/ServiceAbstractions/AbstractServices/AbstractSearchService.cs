using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.ServiceAbstractions.IServices;
using _1_BusinessLayer.Concrete.Dtos.BotDtos;
using _1_BusinessLayer.Concrete.Dtos.PostDtos;
using _1_BusinessLayer.Concrete.Dtos.UserDtos;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.ProxyResult;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Abstractions.AbstractClasses;

namespace _1_BusinessLayer.Abstractions.ServiceAbstractions.AbstractServices
{
    public abstract class AbstractSearchService : ISearchService
    {
        protected readonly AbstractUserQueryHandler _userQueryHandler;
        protected readonly AbstractPostQueryHandler _postQueryHandler;
        protected readonly AbstractBotQueryHandler _botQueryHandler;
        protected readonly AbstractTrendingPostQueryHandler _trendingPostQueryHandler;

        protected AbstractSearchService(
            AbstractUserQueryHandler userQueryHandler,
            AbstractPostQueryHandler postQueryHandler,
            AbstractBotQueryHandler botQueryHandler,
            AbstractTrendingPostQueryHandler trendingPostQueryHandler)
        {
            _userQueryHandler = userQueryHandler;
            _postQueryHandler = postQueryHandler;
            _botQueryHandler = botQueryHandler;
            _trendingPostQueryHandler = trendingPostQueryHandler;
        }

        public abstract Task<ObjectIdentityResult<List<MinimalPostDto>>> FilterPosts(string query, string OrderType, DateTime? startDate, DateTime? endDate, ClaimsPrincipal claims);
        public abstract Task<ObjectIdentityResult<List<MinimalPostDto>>> GetMostLikedPosts(DateTime date, ClaimsPrincipal claims);
        public abstract Task<ObjectIdentityResult<List<MinimalPostDto>>> GetTrendingPosts(ClaimsPrincipal claims);
        public abstract Task<ObjectIdentityResult<List<MinimalBotDto>>> SearchBotsByProfileNameAsync(string query, int maxCandidates);
        public abstract Task<ObjectIdentityResult<List<MinimalPostDto>>> SearchPostsByTitleAsync(string query, int maxCandidates);
        public abstract Task<ObjectIdentityResult<List<MinimalUserDto>>> SearchUserByProfileNameAsync(string query, int maxCandidates);
    }
}
