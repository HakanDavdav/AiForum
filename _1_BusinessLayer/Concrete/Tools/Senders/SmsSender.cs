using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete.Entities;
using _2_DataAccessLayer.Concrete.Enums.OtherEnums;
using Microsoft.AspNetCore.Identity;
using static _2_DataAccessLayer.Concrete.Enums.SmsTypes;

namespace _1_BusinessLayer.Concrete.Tools.Senders
{
    public class SmsSender
    {
        public async Task<IdentityResult> SendAuthenticationSms(Actor user, string token, SmsType? smsType )
        {
            throw new NotImplementedException();
        }
    }
}
