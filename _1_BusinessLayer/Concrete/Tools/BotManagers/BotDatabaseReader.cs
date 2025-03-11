using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.AbstractTools.AbstractBotHandlers;
using _2_DataAccessLayer.Abstractions;

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
        public BotDatabaseReader(AbstractBotRepository abstractBotRepository,AbstractEntryRepository entryRepository,AbstractNewsRepository newsRepository,
            AbstractUserRepository userRepository,AbstractPostRepository postRepository,AbstractBotApiCallManager botApiCallManager)
        {
            _botRepository = abstractBotRepository;
            _entryRepository = entryRepository;
            _newsRepository = newsRepository;
            _userRepository = userRepository;
            _postRepository = postRepository;
            _botApiCallManager = botApiCallManager;
        }

        public async Task<List<string>> GetOpposingData()
        {
            Random random = new Random();
            double randomValue = random.NextDouble(); // 0 ile 1 arasında rastgele sayı üretir

            double probabilityCreatingEntry = 0.2; // %30 ihtimal
            double probabilityCreatingOpposingEntry = 0.75; // %50 ihtimal
            double probabilityCreatingPost = 0.05; // %20 ihtimal

            if (randomValue < probabilityCreatingEntry)
            {
                var post = await _postRepository.GetRandomPostAsyncWithInfo();
                return new List<string> { "Post Title:"+post.Title+"\nPost Context:"+post.Context };
                
            }
            else if (randomValue < probabilityCreatingEntry + probabilityCreatingOpposingEntry)
            {
                var post = await _postRepository.GetRandomPostAsyncWithInfo();
                post.Entries.Skip();
            }
            else
            {
                Console.WriteLine("Olasılık C gerçekleşti! (%20)");
            }
        }

        public async Task<List<string>> GetDefaultData()
        {
            Random random = new Random();
            double randomValue = random.NextDouble(); // 0 ile 1 arasında rastgele sayı üretir

            double probabilityCreatingEntry = 0.6; // %30 ihtimal
            double probabilityCreatingOpposingEntry = 0.2; // %50 ihtimal
            double probabilityCreatingPost = 0.2; // %20 ihtimal

            if (randomValue < probabilityCreatingEntry)
            {
                Console.WriteLine("Olasılık A gerçekleşti! (%30)");
            }
            else if (randomValue < probabilityCreatingEntry + probabilityCreatingOpposingEntry)
            {
                Console.WriteLine("Olasılık B gerçekleşti! (%50)");
            }
            else
            {
                Console.WriteLine("Olasılık C gerçekleşti! (%20)");
            }

        }

        public async Task<List<string>> GetIndependentData()
        {
            Random random = new Random();
            double randomValue = random.NextDouble(); // 0 ile 1 arasında rastgele sayı üretir

            double probabilityCreatingEntry = 0.8; // %30 ihtimal
            double probabilityCreatingOpposingEntry = 0.1; // %50 ihtimal
            double probabilityCreatingPost = 0.2; // %20 ihtimal

            if (randomValue < probabilityCreatingEntry)
            {
                Console.WriteLine("Olasılık A gerçekleşti! (%30)");
            }
            else if (randomValue < probabilityCreatingEntry + probabilityCreatingOpposingEntry)
            {
                Console.WriteLine("Olasılık B gerçekleşti! (%50)");
            }
            else
            {
                Console.WriteLine("Olasılık C gerçekleşti! (%20)");
            }

        }
    }
}
