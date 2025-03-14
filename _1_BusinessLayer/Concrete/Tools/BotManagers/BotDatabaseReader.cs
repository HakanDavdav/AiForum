﻿using System;
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

        public async Task<(List<string> Data, string Response)> GetModelDataAsync()
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
                List<string> data = new List<string>();
                List<Post> posts = await _postRepository.GetRandomPosts(1);
                foreach (var post in posts)
                {
                    data.Add("Post Title:"+post.Title+"\nPost Context:"+post.Context);
                }
                return (data,"creatingEntry") ;

            }
            else if (ActionPossibility < probabilityCreatingEntry + probabilityCreatingOpposingEntry)
            {
               List <string> data = new List<string>();
               StringBuilder stringBuilder = new StringBuilder();
               List<Post> posts = await _postRepository.GetRandomPosts(1);
                foreach (var post in posts)
                {
                    List<Entry> entries = await _entryRepository.GetRandomEntriesByPostId(post.PostId,3);
                    stringBuilder.Append("Post Title:"+post.Title+"\nPost Context"+post.Context);
                    foreach (var entry in entries)
                    {
                        stringBuilder.AppendLine("Entry Context:"+entry.Context);
                    }
                    data.Add(stringBuilder.ToString());
                    stringBuilder.Clear();
                }
                return (data, "creatingOpposingEntry");

            }

            else if (ActionPossibility < probabilityCreatingEntry + probabilityCreatingOpposingEntry + probabilityCreatingPost)
            {
                 List<string> data = new List<string>();
                 List<News> news = await _newsRepository.GetRandomNews(1);
                foreach (var new_s in news)
                {
                    data.Add("News Title:"+new_s.title+"\nNews Context:"+new_s.context);
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
                        stringBuilder.AppendLine("Post Title:"+(await _postRepository.GetByEntryId(entry.EntryId)).Title
                                                 +"Post Context"+ (await _postRepository.GetByEntryId(entry.EntryId)).Context);
                        stringBuilder.AppendLine("Entry Context:"+entry.Context);
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
                    stringBuilder.Append("Bod Id:" + bot.BotId);
                    List<Entry> entries = await _entryRepository.GetRandomEntriesByBotId(bot.BotId, 3);
                    foreach (var entry in entries)
                    {
                        stringBuilder.AppendLine("Post Title:" + (await _postRepository.GetByEntryId(entry.EntryId)).Title
                                                + "Post Context" + (await _postRepository.GetByEntryId(entry.EntryId)).Context);
                        stringBuilder.AppendLine("Entry Context:" + entry.Context);
                    }
                    data.Add(stringBuilder.ToString());
                    stringBuilder.Clear();
                }
                return (data,"creatingBotFollowing");

            }
            else if (ActionPossibility < probabilityCreatingEntry + probabilityCreatingOpposingEntry + probabilityCreatingPost + probabilityUserFollowing + probabilityBotFollowing + probabilityLikePost)
            {
                List<string> data = new List<string>();
                List<Post> posts = await _postRepository.GetRandomPosts(3);
                foreach (var post in posts)
                {
                    data.Add("Post Title:" + post.Title + "\nPost context:" + post.Context);
                }
                return (data,"likePost");
            }
            else
            {
                List<string> data = new List<string>();
                StringBuilder stringBuilder = new StringBuilder();
                List<Post> posts = await _postRepository.GetRandomPosts(1);
                foreach (var post in posts)
                {
                    stringBuilder.Append("Post Title:" + post.Title + "\nPost Context:" + post.Context);
                    List<Entry> entries = await _entryRepository.GetRandomEntriesByPostId(post.PostId,3);
                    foreach (var entry in entries)
                    {
                        stringBuilder.AppendLine("Entry Context:"+entry.Context);
                    }
                    data.Add(stringBuilder.ToString());
                }
                return (data, "likeEntry");
            }
        }
    }      
}
