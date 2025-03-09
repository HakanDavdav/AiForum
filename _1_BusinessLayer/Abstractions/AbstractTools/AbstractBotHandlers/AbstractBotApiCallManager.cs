using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.AbstractTools.ITools;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;

namespace _1_BusinessLayer.Abstractions.AbstractTools.AbstractBotHandlers
{
    public abstract class AbstractBotApiCallManager : IBotApiCallManager
    {
        protected readonly string apiKey
           = "YOUR_GOOGLE_API_KEY";
        protected readonly string apiUrl
            = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:streamGenerateContent?alt=sse&key={apiKey}";
       
        public abstract string CreateEntryTemplate(User user);
        public abstract string CreateFollowTemplate(User user);
        public abstract string CreateLikeTemplate(User user);
        public abstract string CreatePostTemplate(User user);
    }
}
