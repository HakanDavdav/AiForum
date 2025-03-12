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
                var randomizer = new Random();
                IQueryable<Post> posts = await _postRepository.GetAllAsync();
                IQueryable<Post> randomPost = posts.Skip(randomizer.Next(await posts.CountAsync())).Take(1);

                List<Post> randomPostList = await randomPost.ToListAsync();
                Post post = randomPostList.First();

                return new List<string> {"Post Title:"+post.Title+"\nPost Context:"+post.Context};
            }
            else if (ActionPossibility < probabilityCreatingEntry + probabilityCreatingOpposingEntry)
            {
                var randomizer = new Random();
                IQueryable<Post> posts = await _postRepository.GetAllWithInfoAsync();
                IQueryable<Post> randomPosts = posts.Skip(randomizer.Next(0,await posts.CountAsync()-2)).Take(3);
                
            }

            else if (ActionPossibility < probabilityCreatingEntry + probabilityCreatingOpposingEntry + probabilityCreatingPost)
            {
                var randomizer = new Random();
                IQueryable<News> news = await _newsRepository.GetAllAsync();
                IQueryable<News> randomNews = news.Skip(randomizer.Next(await news.CountAsync())).Take(1);

                List<News> randomNewsList = await randomNews.ToListAsync();
                News newss = randomNewsList.First();

                return new List<string> {"News Title:"+newss.title+"News Context:"+newss.context };

            }
            else if (ActionPossibility < probabilityCreatingEntry + probabilityCreatingOpposingEntry + probabilityCreatingPost + probabilityUserFollowing)
            {
                var randomizer = new Random();
                IQueryable<User> users = await _userRepository.GetAllWithInfoAsync();
                IQueryable<User> randomUsers = users.Skip(randomizer.Next(randomizer.Next(0,await users.CountAsync()-2))).Take(3);
                IQueryable<IGrouping<User, Entry>> randomUsersWithEntries = randomUsers.SelectMany(randomUsers => randomUsers.Entries).GroupBy(entry => entry.User);

                List<IGrouping<User, Entry>> randomUsersWithEntriesList = await randomUsersWithEntries.ToListAsync();
                var elements = new List<string>();
                var element = new StringBuilder();
                foreach (var item in randomUsersWithEntriesList)
                {
                    element.Append(item.Key.ProfileName);
                    foreach (var entry in item)
                    {
                        element.AppendLine("Entry:"+entry.Context);
                    }
                    elements.Add(element.ToString());
                }
                return elements;
            }
            else if (ActionPossibility < probabilityCreatingEntry + probabilityCreatingOpposingEntry + probabilityCreatingPost + probabilityUserFollowing + probabilityBotFollowing)
            {
                
            }
            else if (ActionPossibility < probabilityCreatingEntry + probabilityCreatingOpposingEntry + probabilityCreatingPost + probabilityUserFollowing + probabilityBotFollowing + probabilityLikePost)
            {
               
            }
            else
            {
             
            }
        }
    }      
}
