using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jm.Azure.FunctionApps.QuickQuote.Core
{
    public interface IPolicyRepository
    {
        Task<ValidateUserResponse> ValidateUser(string token, string userId);
    }
}
