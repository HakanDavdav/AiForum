using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.AbstractTools.AbstractBotHandlers;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using _2_DataAccessLayer.Concrete.Repositories;

namespace _1_BusinessLayer.Concrete.Tools.BotHandlers.BotManagers
{
    public class BotApiCallManager 
    {
       
        }

        public override string CreateEntryTemplate(User user)
        {
            
        }

        public override string CreateFollowTemplate(User user)
        {
            throw new NotImplementedException();
        }

        public override string CreateLikeTemplate(User user)
        {
            throw new NotImplementedException();
        }

        public override string CreatePostTemplate(User user)
        {
            _newsRepository.GetByIdAsync(user.Id);
        }
    }
}
