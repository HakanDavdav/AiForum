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
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.ProxyResult;
using _1_BusinessLayer.Concrete.Tools.Extensions;
using _1_BusinessLayer.Concrete.Tools.Extensions.Mappers;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Abstractions.AbstractClasses;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Concrete.Services
{
    public class SearchService : AbstractSearchService
    {
        public SearchService(
            AbstractUserQueryHandler userQueryHandler,
            AbstractPostQueryHandler postQueryHandler,
            AbstractBotQueryHandler botQueryHandler,
            AbstractTrendingPostQueryHandler trendingPostQueryHandler)
            : base(userQueryHandler, postQueryHandler, botQueryHandler, trendingPostQueryHandler)
        {
        }

        public override async Task<ObjectIdentityResult<List<MinimalPostDto>>> FilterPosts(string query, string OrderType, DateTime? startDate, DateTime? endDate, ClaimsPrincipal claims)
        {
            var postCount = claims.FindFirst("PostPerPage") != null ? int.Parse(claims.FindFirst("PostPerPage").Value) : 30;
            var validOrderTypes = new[] { "MostLiked", "Oldest", "Newest" };
            if (!validOrderTypes.Contains(OrderType))
            {
                return ObjectIdentityResult<List<MinimalPostDto>>.Failed(null, new[] { new IdentityError { Description = $"Invalid OrderType: {OrderType}" } });
            }
            if ((OrderType == "Oldest" || OrderType == "Newest") && (startDate == null || endDate == null))
            {
                return ObjectIdentityResult<List<MinimalPostDto>>.Failed(null, new[] { new IdentityError { Description = "StartDate and EndDate must be provided for Oldest/Newest order types." } });
            }

            Func<IQueryable<Post>, IQueryable<Post>> queryModifier = q =>
            {
                var filtered = q.Where(p => EF.Functions.Like(p.Title, $"%{query}%"));
                if (startDate != null)
                    filtered = filtered.Where(p => p.DateTime.Date >= startDate.Value.Date);
                if (endDate != null)
                    filtered = filtered.Where(p => p.DateTime.Date <= endDate.Value.Date);

                switch (OrderType)
                {
                    case "MostLiked":
                        filtered = filtered.OrderByDescending(p => p.LikeCount);
                        break;
                    case "Oldest":
                        filtered = filtered.OrderBy(p => p.DateTime);
                        break;
                    case "Newest":
                        filtered = filtered.OrderByDescending(p => p.DateTime);
                        break;
                }
                return filtered.Take(postCount);
            };

            var posts = await _postQueryHandler.GetWithCustomSearchAsync(queryModifier);
            var minimalPostDtos = posts.Select(post => new MinimalPostDto
            {
                PostId = post.PostId,
                Title = post.Title,
                EntryCount = post.EntryCount
            }).ToList();
            return ObjectIdentityResult<List<MinimalPostDto>>.Succeded(minimalPostDtos);
        }

        public override async Task<ObjectIdentityResult<List<MinimalPostDto>>> GetMostLikedPosts(DateTime date, ClaimsPrincipal claims)
        {
            var postCount = claims.FindFirst("PostPerPage") != null ? int.Parse(claims.FindFirst("PostPerPage").Value) : 30;
            var posts = await _postQueryHandler.GetWithCustomSearchAsync(q =>
                q.Where(p => p.DateTime.Date == date.Date)
                 .OrderByDescending(p => p.LikeCount)
                 .Take(postCount));
            var minimalPostDtos = posts.Select(post => new MinimalPostDto
            {
                PostId = post.PostId,
                Title = post.Title,
                EntryCount = post.EntryCount
            }).ToList();
            return ObjectIdentityResult<List<MinimalPostDto>>.Succeded(minimalPostDtos);
        }

        public override async Task<ObjectIdentityResult<List<MinimalPostDto>>> GetTrendingPosts(ClaimsPrincipal claims)
        {
            var postCount = claims.FindFirst("PostPerPage") != null ? int.Parse(claims.FindFirst("PostPerPage").Value) : 30;
            var trendingPosts = await _trendingPostQueryHandler.GetWithCustomSearchAsync(q => q.OrderByDescending(t => t.HotScore).Take(postCount));
            var minimalPostDtos = trendingPosts
                .Select(t => new MinimalPostDto
                {
                    PostId = t.PostId,
                    Title = t.PostTitle,
                    EntryCount = t.EntryCount
                })
                .ToList();
            return ObjectIdentityResult<List<MinimalPostDto>>.Succeded(minimalPostDtos);
        }

        public override async Task<ObjectIdentityResult<List<MinimalBotDto>>> SearchBotsByProfileNameAsync(string query, int maxCandidates = 100)
        {
            var candidateBots = await _botQueryHandler.GetWithCustomSearchAsync(q => q.Where(b => EF.Functions.Like(b.BotProfileName, $"%{query}%")).Take(maxCandidates));
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
            var candidatePosts = await _postQueryHandler.GetWithCustomSearchAsync(q => q.Where(p => EF.Functions.Like(p.Title, $"%{query}%")).Take(maxCandidates));
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
            var candidateUsers = await _userQueryHandler.GetWithCustomSearchAsync(q => q.Where(u => EF.Functions.Like(u.ProfileName, $"%{query}%")).Take(maxCandidates));
            var minimalCandidateUserDtos = candidateUsers.Select(c => new MinimalUserDto
            {
                UserId = c.ActorId,
                ProfileName = c.ProfileName,
                ImageUrl = c.ImageUrl
            }).OrderBy(candidateUser => candidateUser.ProfileName.LevenshteinDistanceTo(query))
              .Take(3).ToList();
            return ObjectIdentityResult<List<MinimalUserDto>>.Succeded(minimalCandidateUserDtos);
        }
    }
}
