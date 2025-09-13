using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.ServiceAbstractions.AbstractServices;
using _1_BusinessLayer.Concrete.Dtos.BotDtos;
using _1_BusinessLayer.Concrete.Dtos.PostDtos;
using _1_BusinessLayer.Concrete.Dtos.UserDtos;
using _1_BusinessLayer.Concrete.Tools.Algorithms;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.ProxyResult;
using _1_BusinessLayer.Concrete.Tools.Extensions.Mappers;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using _2_DataAccessLayer.Concrete.Repositories;
using Microsoft.EntityFrameworkCore;

namespace _1_BusinessLayer.Concrete.Services
{
    public class SearchService : AbstractSearchService
    {
        public SearchService(AbstractUserRepository userRepository, AbstractPostRepository postRepository, AbstractBotRepository botRepository) : base(userRepository, postRepository, botRepository)
        {
        }

        public override Task<ObjectIdentityResult<List<MinimalPostDto>>> FilterPosts(string query, string OrderType, DateTime? startDate, DateTime? endDate)
        {
            throw new NotImplementedException();
        }

        public override async Task<ObjectIdentityResult<List<MinimalPostDto>>> GetMostLikedPosts(DateTime date, ClaimsPrincipal claims)
        {
            var postCount = claims.FindFirst("PostPerPage") != null ? int.Parse(claims.FindFirst("PostPerPage").Value) : 30;
            List<MinimalPostDto> minimalPostDtos = new List<MinimalPostDto>();
            List<Post> posts = await _postRepository.GetWithCustomSearchAsync(q =>
                q.Where(p => p.DateTime.Date == date.Date)
                .OrderByDescending(p => p.Likes)
                .Take(postCount)
                 );
            foreach (var post in posts)
            {
                minimalPostDtos.Add(post.Post_To_MinimalPostDto());
            }
            return ObjectIdentityResult<List<MinimalPostDto>>.Succeded(minimalPostDtos);
        }

        public override async Task<ObjectIdentityResult<List<MinimalPostDto>>> GetTrendingPosts(ClaimsPrincipal claims)
        {
            var postCount = claims.FindFirst("PostPerPage") != null ? int.Parse(claims.FindFirst("PostPerPage").Value) : 30;
            List<MinimalPostDto> minimalPostDtos = new List<MinimalPostDto>();
            List<TrendingPost> posts = await _trendingPostRepository.GetWithCustomSearchAsync(q => q.OrderBy(t => t.HotScore).Take(postCount));
            minimalPostDtos.Add(posts.Select(p => new MinimalPostDto
            {
                PostId = p.PostId,
                Title = p.PostTitle,
            }));

        }

        public override async Task<ObjectIdentityResult<List<MinimalBotDto>>> SearchBotsByProfileNameAsync(string query, int maxCandidates = 100)
        {
            var candidateBots = await _botRepository.GetWithCustomSearchAsync(q => q.Where(b => EF.Functions.Like(b.BotProfileName, $"%{query}%")).Take(maxCandidates));
            var candidateBotDtos = candidateBots.Select(c => new MinimalBotDto
            {
                BotId = c.Id,
                ProfileName = c.BotProfileName,
            }).OrderBy(c => c.ProfileName.LevenshteinDistanceTo(query))
            .Take(3).ToList();
            return ObjectIdentityResult<List<MinimalBotDto>>.Succeded(candidateBotDtos);
        }

        public override async Task<ObjectIdentityResult<List<MinimalPostDto>>> SearchPostsByTitleAsync(string query, int maxCandidates = 100)
        {
            var candidatePosts = await _postRepository.GetWithCustomSearchAsync(q => q.Where(p => EF.Functions.Like(p.Title, $"%{query}%")).Take(maxCandidates));
            var candidatePostDtos = candidatePosts.Select(c => new MinimalPostDto
            {
                PostId = c.PostId,
                Title = c.Title,
                EntryCount = c.EntryCount,
            }).OrderBy(c => c.Title.LevenshteinDistanceTo(query)).Take(5).ToList();
            return ObjectIdentityResult<List<MinimalPostDto>>.Succeded(candidatePostDtos);
        }

        public override async Task<ObjectIdentityResult<List<MinimalUserDto>>> SearchUserByProfileNameAsync(string query, int maxCandidates = 100)
        {
            var candidateUsers = await _userRepository.GetWithCustomSearchAsync(q => q.Where(u => EF.Functions.Like(u.ProfileName, $"%{query}%")).Take(maxCandidates));
            var minimalCandidateUserDtos = candidateUsers.Select(c => new MinimalUserDto
            {
                UserId = c.Id,
                ProfileName = c.ProfileName,
                ImageUrl = c.ImageUrl
            }).OrderBy(candidateUser => candidateUser.ProfileName.LevenshteinDistanceTo(query))
              .Take(3).ToList();
            return ObjectIdentityResult<List<MinimalUserDto>>.Succeded(minimalCandidateUserDtos);

        }
    }
}
