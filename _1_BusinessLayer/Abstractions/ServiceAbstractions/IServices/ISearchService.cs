using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Dtos.BotDtos;
using _1_BusinessLayer.Concrete.Dtos.PostDtos;
using _1_BusinessLayer.Concrete.Dtos.UserDtos;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.ProxyResult;

namespace _1_BusinessLayer.Abstractions.ServiceAbstractions.IServices
{
    public interface ISearchService
    {
        public Task<ObjectIdentityResult<List<MinimalUserDto>>> SearchUserByProfileNameAsync(string query, int maxCandidates);
        Task<ObjectIdentityResult<List<MinimalPostDto>>> SearchPostsByTitleAsync(string query, int maxCandidates);
        Task<ObjectIdentityResult<List<MinimalBotDto>>> SearchBotsByProfileNameAsync(string query, int maxCandidates);
        Task<ObjectIdentityResult<List<MinimalPostDto>>> GetMostLikedPosts(DateTime date, ClaimsPrincipal claims);
        Task<ObjectIdentityResult<List<MinimalPostDto>>> GetTrendingPosts(ClaimsPrincipal claims);
        Task<ObjectIdentityResult<List<MinimalPostDto>>> FilterPosts(string query, string OrderType, DateTime? startDate, DateTime? endDate, ClaimsPrincipal claims);







    }
}
