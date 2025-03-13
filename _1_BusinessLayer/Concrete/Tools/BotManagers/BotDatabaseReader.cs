using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.AbstractTools.AbstractBotHandlers;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.EntityFrameworkCore;


namespace _1_BusinessLayer.Concrete.Tools.BotManagers
{
    public class BotDatabaseReader
    {
        public readonly AbstractPostRepository _postRepository;
        public readonly AbstractEntryRepository _entryRepository;
        public readonly AbstractNewsRepository _newsRepository;
        public readonly AbstractUserRepository _userRepository;
        public readonly AbstractBotRepository _botRepository;
        public readonly AbstractBotApiCallManager _botApiCallManager;
        public BotDatabaseReader(AbstractBotRepository abstractBotRepository, AbstractEntryRepository entryRepository, AbstractNewsRepository newsRepository,
            AbstractUserRepository userRepository, AbstractPostRepository postRepository, AbstractBotApiCallManager botApiCallManager)
        {
            _botRepository = abstractBotRepository;
            _entryRepository = entryRepository;
            _newsRepository = newsRepository;
            _userRepository = userRepository;
            _postRepository = postRepository;
            _botApiCallManager = botApiCallManager;
        }

        public async Task<List<string>> GetOpposingModeData()
        {
            Random random = new Random();
            double ActionPossibility = random.NextDouble(); // 0 ile 1 arasında rastgele sayı üretir

            double probabilityCreatingEntry = 0.1; // %10 ihtimal
            double probabilityCreatingOpposingEntry = 0.60; // %65 ihtimal
            double probabilityCreatingPost = 0.05; // %5 ihtimal
            double probabilityUserFollowing = 0.05; //%5 ihtimal
            double probabilityBotFollowing = 0.05; //%5 ihtimal
            double probabilityLikePost = 0.075; //%7.5 ihtimal
            double probabilityLikeEntry = 0.075; //%7.5 ihtimal


            if (ActionPossibility < probabilityCreatingEntry)
            {
                 List<string> strings = new List<string>();
                 StringBuilder stringBuilder = new StringBuilder();
                 IQueryable<Post> Posts = await _postRepository.GetAllWithInfoAsync();
                 IQueryable<Post> randomPosts =  Posts.OrderBy(post => Guid.NewGuid()).Take(3);
                 List<Post> randomPostsList = await randomPosts.ToListAsync();
                foreach (var post in randomPostsList)
                {
                    strings.Add("Post Title:"+post.Title+"\nPost Context:"+post.Context);
                }
                return strings;

            }
            else if (ActionPossibility < probabilityCreatingEntry + probabilityCreatingOpposingEntry)
            {
                List<string> strings = new List<string>();
                StringBuilder stringBuilder = new StringBuilder();
                IQueryable<Post> Posts = await _postRepository.GetAllWithInfoAsync();
                IQueryable<Post> randomPosts = Posts.OrderBy(post => Guid.NewGuid()).Take(3);
                IQueryable<IGrouping<Post, Entry>> randomPostsWithRandomEntries =
                   randomPosts.SelectMany(post => post.Entries.OrderBy(entry => Guid.NewGuid()).Take(3)).GroupBy(entry => entry.Post);
                List<IGrouping<Post, Entry>> randomPostsWithRandomEntriesList = await randomPostsWithRandomEntries.ToListAsync();
                foreach (var post in randomPostsWithRandomEntriesList)
                {
                    stringBuilder.AppendLine("Post Title:" + post.Key.Title + "\nPost Context:" + post.Key.Context);
                    foreach (var entry in post)
                    {
                        stringBuilder.AppendLine("Entry Context:" + entry.Context);
                    }
                    strings.Add(stringBuilder.ToString());
                    stringBuilder.Clear();
                }
                return strings;

            }

            else if (ActionPossibility < probabilityCreatingEntry + probabilityCreatingOpposingEntry + probabilityCreatingPost)
            {
                 List<string> strings = new List<string>();
                 IQueryable<News> news_s = await _newsRepository.GetAllWithInfoAsync();
                 IQueryable<News> randomNews_s = news_s.OrderBy(news => Guid.NewGuid()).Take(3);
                 List<News> randomNews_sList = await randomNews_s.ToListAsync();
                foreach (var news in randomNews_sList)
                {
                    strings.Add("News Title:" + news.title + "\nNews Context:" + news.context);
                }
                return strings;

            }
            else if (ActionPossibility < probabilityCreatingEntry + probabilityCreatingOpposingEntry + probabilityCreatingPost + probabilityUserFollowing)
            {
                List<string> strings = new List<string>();
                StringBuilder stringBuilder = new StringBuilder();
                IQueryable<User> users = await _userRepository.GetAllWithInfoAsync();
                IQueryable<User> randomUsers = users.OrderBy(user => Guid.NewGuid()).Take(3);
                IQueryable<IGrouping<User, Entry>> randomUsersWithRandomEntries =
                    randomUsers.SelectMany(user => user.Entries.OrderBy(entry => Guid.NewGuid()).Take(3)).GroupBy(entry => entry.User);
                List<IGrouping<User, Entry>> randomUsersWithRandomEntriesList = await randomUsersWithRandomEntries.ToListAsync();
                foreach (var user in randomUsersWithRandomEntriesList)
                {
                    stringBuilder.Append("User Id:"+user.Key.Id);
                    foreach (var entry in user)
                    {
                        stringBuilder.AppendLine("Post Title:" + entry.Post.Title + "\nPost context:" + entry.Post.Context);
                        stringBuilder.AppendLine("Entry Context:"+entry.Context);
                    }
                    strings.Add(stringBuilder.ToString());
                    stringBuilder.Clear();
                }
                return strings;
            }
            else if (ActionPossibility < probabilityCreatingEntry + probabilityCreatingOpposingEntry + probabilityCreatingPost + probabilityUserFollowing + probabilityBotFollowing)
            {
                List<string> strings = new List<string>();
                StringBuilder stringBuilder = new StringBuilder();
                IQueryable<Bot> bots = await _botRepository.GetAllWithInfoAsync();
                IQueryable<Bot> randomBots = bots.OrderBy(bot => Guid.NewGuid()).Take(3);
                IQueryable<IGrouping<Bot, Entry>> randomBotsWithRandomEntries =
                    randomBots.SelectMany(bot => bot.Entries.OrderBy(entry => Guid.NewGuid()).Take(3)).GroupBy(entry => entry.Bot);
                List<IGrouping<Bot,Entry>> randomBotWithRandomEntriesList = await randomBotsWithRandomEntries.ToListAsync();
                foreach (var bot in randomBotWithRandomEntriesList)
                {
                    stringBuilder.Append("Bot Id:" + bot.Key.BotId);
                    foreach (var entry in bot)
                    {
                        stringBuilder.AppendLine("Post Title:" + entry.Post.Title + "Post Context" + entry.Post.Context);
                        stringBuilder.AppendLine("Entry Context"+entry.Context);
                    }
                    strings.Add(stringBuilder.ToString());
                    stringBuilder.Clear();
                }
                return strings;

            }
            else if (ActionPossibility < probabilityCreatingEntry + probabilityCreatingOpposingEntry + probabilityCreatingPost + probabilityUserFollowing + probabilityBotFollowing + probabilityLikePost)
            {
                List<string> strings = new List<string>();
                IQueryable<Post> posts = await _postRepository.GetAllWithInfoAsync();
                IQueryable<Post> randomPosts = posts.OrderBy(post => Guid.NewGuid()).Take(3);
                List<Post> randomPostsList = await randomPosts.ToListAsync();
                foreach (var post in randomPostsList)
                {
                    strings.Add("Post Title:" + post.Title + "Post Context:" + post.Context);
                }
                return strings;
            }
            else
            {
                List<string> strings = new List<string>();
                StringBuilder stringBuilder = new StringBuilder();
                IQueryable<Post> posts = await _postRepository.GetAllWithInfoAsync();
                IQueryable<Post> randomPosts = posts.OrderBy(post => Guid.NewGuid()).Take(3);
                IQueryable<IGrouping<Post,Entry>> randomPostsWithRandomEntries = 
                    randomPosts.SelectMany(post => post.Entries.OrderBy(entry => Guid.NewGuid()).Take(3)).GroupBy(entry => entry.Post);
                List<IGrouping<Post, Entry>> randomPostWithRandomEntriesList = await randomPostsWithRandomEntries.ToListAsync();
                foreach (var item in randomPostWithRandomEntriesList)
                {
                    
                }

            }
        }
    }      
}
