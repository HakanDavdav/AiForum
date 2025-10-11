using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Codebase.Dtos.ActorDtos.OutputDtos;
using _1_BusinessLayer.Codebase.Dtos.ContenItemDtos.OutputDtos;
using _1_BusinessLayer.Codebase.Dtos.TribeDtos.Output;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.ProxyResult;

namespace _1_BusinessLayer.Concrete.Services._Concrete
{
    public class SearchService
    {

        public Task<ObjectIdentityResult<PostDto>> FilterPosts()
        {

        }

        public Task<ObjectIdentityResult<EntryDto>> GetTrendingPosts()
        {

        }

        public Task<ObjectIdentityResult<EntryDto>> GetMostLikedEntries()
        {

        }

        public Task<ObjectIdentityResult<EntryDto> GetMostDislikedEntries()
        {

        }


        public Task<ObjectIdentityResult<PostDto>> GetMostRecentPosts()
        {

        }

        public Task<ObjectIdentityResult<PostDto>> GetMostRelevantPosts()
        {
        }

        public Task<ObjectIdentityResult<EntryDto>> GeneralSearch()
        {
            Task<ObjectIdentityResult<List<PostDto>>> SearchPostByTitle()
            {
            }

            Task<ObjectIdentityResult<MinimalActorDto>> SearchActorByProfileName()
            {
            }

            Task<ObjectIdentityResult<MinimalActorDto>> SearchTribeByTribeName()
            {
            }

        }

        public Task<ObjectIdentityResult<TribeDto>> GetTribeLeaderboard()
        {

        }

        public Task<ObjectIdentityResult<MinimalActorDto>> GetActorLeaderboard()
        {
        }



    }


}
    










public static class StringHelperExtension
{
    public static int LevenshteinDistanceTo(this string source, string target)
    {
        if (string.IsNullOrEmpty(source))
            return target?.Length ?? 0;
        if (string.IsNullOrEmpty(target))
            return source.Length;

        var n = source.Length;
        var m = target.Length;
        var d = new int[n + 1, m + 1];

        for (int i = 0; i <= n; i++) d[i, 0] = i;
        for (int j = 0; j <= m; j++) d[0, j] = j;

        for (int i = 1; i <= n; i++)
        {
            for (int j = 1; j <= m; j++)
            {
                int cost = source[i - 1] == target[j - 1] ? 0 : 1;
                d[i, j] = Math.Min(
                    Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                    d[i - 1, j - 1] + cost);
            }
        }

        return d[n, m];
    }
}