using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.EntityFrameworkCore;


namespace _1_BusinessLayer.Concrete.Tools.Managers.BotManagers
{
    public class BotDatabaseReader
    {
        public readonly AbstractPostRepository _postRepository;
        public readonly AbstractEntryRepository _entryRepository;
        public readonly AbstractNewsRepository _newsRepository;
        public readonly AbstractUserRepository _userRepository;
        public readonly AbstractBotRepository _botRepository;
        public BotDatabaseReader(AbstractBotRepository abstractBotRepository, AbstractEntryRepository entryRepository, AbstractNewsRepository newsRepository,
            AbstractUserRepository userRepository, AbstractPostRepository postRepository)
        {
            _botRepository = abstractBotRepository;
            _entryRepository = entryRepository;
            _newsRepository = newsRepository;
            _userRepository = userRepository;
            _postRepository = postRepository;
        }

        public async Task<(List<string> Data, string dataResponseType)> GetModelDataAsync(ProbabilitySet probabilitySet)
        {
            Random random = new Random();
            double ActionPossibility = random.NextDouble(); // 0 ile 1 arasında rastgele sayı üretir

            double probabilityCreatingEntry = probabilitySet.probabilityCreatingEntry;
            double probabilityCreatingOpposingEntry = probabilitySet.probabilityCreatingOpposingEntry;
            double probabilityCreatingPost = probabilitySet.probabilityCreatingPost;
            double probabilityUserFollowing = probabilitySet.probabilityUserFollowing;
            double probabilityBotFollowing = probabilitySet.probabilityBotFollowing;
            double probabilityLikePost = probabilitySet.probabilityLikePost;
            double probabilityLikeEntry = probabilitySet.probabilityLikeEntry;


            if (ActionPossibility < probabilityCreatingEntry)
            {
                List<string> data = new List<string>();
                List<Post> posts = await _postRepository.GetRandomPosts(1);
                foreach (var post in posts)
                {
                    data.Add("Post Id:" + post.PostId + "\nPost Title:" + post.Title + "\nPost NotificationContext:" + post.Context);
                }
                return (data, "creatingEntry");

            }
            else if (ActionPossibility < probabilityCreatingEntry + probabilityCreatingOpposingEntry)
            {
                List<string> data = new List<string>();
                StringBuilder stringBuilder = new StringBuilder();
                List<Post> posts = await _postRepository.GetRandomPosts(1);
                foreach (var post in posts)
                {
                    List<Entry> entries = await _entryRepository.GetRandomEntriesByPostId(post.PostId, 3);
                    stringBuilder.Append("Post Id:" + post.PostId + "\nPost Title:" + post.Title + "\nPost NotificationContext" + post.Context);
                    foreach (var entry in entries)
                    {
                        stringBuilder.AppendLine("Entry NotificationContext:" + entry.Context);
                    }
                    data.Add(stringBuilder.ToString());
                    stringBuilder.Clear();
                }
                return (data, "creatingOpposingEntry");

            }

            else if (ActionPossibility < probabilityCreatingEntry + probabilityCreatingOpposingEntry + probabilityCreatingPost)
            {
                List<string> data = new List<string>();
                List<TrendingPosts> news = await _newsRepository.GetRandomNews(1);
                foreach (var new_s in news)
                {
                    data.Add("News Title:" + new_s.Title + "\nNews NotificationContext:" + new_s.Context);
                }
                return (data, "creatingPost");

            }
            else if (ActionPossibility < probabilityCreatingEntry + probabilityCreatingOpposingEntry + probabilityCreatingPost + probabilityUserFollowing)
            {
                List<string> data = new List<string>();
                StringBuilder stringBuilder = new StringBuilder();
                List<User> users = await _userRepository.GetRandomUsers(1);
                foreach (var user in users)
                {
                    stringBuilder.Append("User Id:" + user.Id);
                    List<Entry> entries = await _entryRepository.GetRandomEntriesByUserId(user.Id, 3);
                    foreach (var entry in entries)
                    {
                        stringBuilder.AppendLine("Post Title:" + (await _postRepository.GetByEntryId(entry.EntryId)).Title
                                                 + "Post NotificationContext" + (await _postRepository.GetByEntryId(entry.EntryId)).Context);
                        stringBuilder.AppendLine("Entry NotificationContext:" + entry.Context);
                    }
                    data.Add(stringBuilder.ToString());
                    stringBuilder.Clear();
                }
                return (data, "creatingUserFollowing");


            }
            else if (ActionPossibility < probabilityCreatingEntry + probabilityCreatingOpposingEntry + probabilityCreatingPost + probabilityUserFollowing + probabilityBotFollowing)
            {
                List<string> data = new List<string>();
                StringBuilder stringBuilder = new StringBuilder();
                List<Bot> bots = await _botRepository.GetRandomBots(1);
                foreach (var bot in bots)
                {
                    stringBuilder.Append("Bot Id:" + bot.BotId);
                    List<Entry> entries = await _entryRepository.GetRandomEntriesByBotId(bot.BotId, 3);
                    foreach (var entry in entries)
                    {
                        stringBuilder.AppendLine("Post Title:" + (await _postRepository.GetByEntryId(entry.EntryId)).Title
                                                + "Post NotificationContext" + (await _postRepository.GetByEntryId(entry.EntryId)).Context);
                        stringBuilder.AppendLine("Entry NotificationContext:" + entry.Context);
                    }
                    data.Add(stringBuilder.ToString());
                    stringBuilder.Clear();
                }
                return (data, "creatingBotFollowing");

            }
            else if (ActionPossibility < probabilityCreatingEntry + probabilityCreatingOpposingEntry + probabilityCreatingPost + probabilityUserFollowing + probabilityBotFollowing + probabilityLikePost)
            {
                List<string> data = new List<string>();
                List<Post> posts = await _postRepository.GetRandomPosts(3);
                foreach (var post in posts)
                {
                    data.Add("Post Id:" + post.PostId + "\nPost Title:" + post.Title + "\nPost NotificationContext:" + post.Context);
                }
                return (data, "likePost");
            }
            else
            {
                List<string> data = new List<string>();
                StringBuilder stringBuilder = new StringBuilder();
                List<Post> posts = await _postRepository.GetRandomPosts(1);
                foreach (var post in posts)
                {
                    stringBuilder.Append("Post Title:" + post.Title + "\nPost NotificationContext:" + post.Context);
                    List<Entry> entries = await _entryRepository.GetRandomEntriesByPostId(post.PostId, 3);
                    foreach (var entry in entries)
                    {
                        stringBuilder.AppendLine("Entry Id:" + entry.EntryId + "\nEntry NotificationContext:" + entry.Context);
                    }
                    data.Add(stringBuilder.ToString());
                }
                return (data, "likeEntry");
            }
        }
    }
}
