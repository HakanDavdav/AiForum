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

namespace _1_BusinessLayer.Abstractions.ServiceAbstractions.AbstractServices
{
    public abstract class AbstractSearchService : ISearchService
    {
        protected readonly AbstractUserRepository _userRepository;
        protected readonly AbstractPostRepository _postRepository;
        protected readonly AbstractBotRepository _botRepository;
        protected readonly AbstractTrendingPostRepository _trendingPostRepository;
        protected AbstractSearchService(AbstractUserRepository userRepository, AbstractPostRepository postRepository, AbstractBotRepository botRepository, AbstractTrendingPostRepository trendingPostRepository)
        {
            _userRepository = userRepository;
            _postRepository = postRepository;
            _botRepository = botRepository;
            _trendingPostRepository = trendingPostRepository;
        }


        public abstract Task<ObjectIdentityResult<List<MinimalPostDto>>> FilterPosts(string query, string OrderType, DateTime? startDate, DateTime? endDate);
        public abstract Task<ObjectIdentityResult<List<MinimalPostDto>>> GetMostLikedPosts(DateTime date, ClaimsPrincipal claims);
        public abstract Task<ObjectIdentityResult<List<MinimalPostDto>>> GetTrendingPosts(ClaimsPrincipal claims);
        public abstract Task<ObjectIdentityResult<List<MinimalBotDto>>> SearchBotsByProfileNameAsync(string query, int maxCandidates);
        public abstract Task<ObjectIdentityResult<List<MinimalPostDto>>> SearchPostsByTitleAsync(string query, int maxCandidates);
        public abstract Task<ObjectIdentityResult<List<MinimalUserDto>>> SearchUserByProfileNameAsync(string query, int maxCandidates);
    }
}
