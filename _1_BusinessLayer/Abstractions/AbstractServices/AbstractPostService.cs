using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.AbstractServices.IServices;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.ProxyResult;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Abstractions.AbstractServices
{
    public abstract class AbstractPostService : IPostService
    {
        protected readonly AbstractPostRepository _postRepository;
        protected readonly AbstractLikeRepository _likeRepository;
        protected AbstractPostService(AbstractLikeRepository likeRepository, AbstractPostRepository postRepository)
        {
            _postRepository = postRepository;
            _likeRepository = likeRepository;
        }

    }
}
