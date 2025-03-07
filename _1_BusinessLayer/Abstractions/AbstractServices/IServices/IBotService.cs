using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Abstractions.AbstractServices.IServices
{
    public interface IBotService
    {
        Task<IdentityResult> CreateBot();
        Task<IdentityResult> CustomizeBot();
        Task<IdentityResult> GetBotActivity();
        Task<IdentityResult> GetBotProfile();



    }
}
