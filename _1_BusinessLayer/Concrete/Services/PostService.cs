using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.AbstractServices;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.Errors;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.ProxyResult;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Concrete.Services
{
    public class PostService : AbstractPostService
    {
        public PostService(AbstractLikeRepository likeRepository, AbstractPostRepository postRepository) : base(likeRepository, postRepository)
        {
        }

    }
}
