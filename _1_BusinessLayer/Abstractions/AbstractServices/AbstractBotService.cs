using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.AbstractServices.IServices;
using _1_BusinessLayer.Concrete.Dtos.BotDtos;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.ProxyResult;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Abstractions.AbstractServices
{
    public abstract class AbstractBotService : IBotService
    {
        protected readonly AbstractBotRepository _botRepository;
        protected readonly AbstractUserRepository _userRepository;
        protected AbstractBotService(AbstractBotRepository botRepository,AbstractUserRepository userRepository)
        {
            _botRepository = botRepository;
            _userRepository = userRepository;
        }


    }
}
