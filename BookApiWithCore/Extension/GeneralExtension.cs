using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApiWithCore.Extension
{
    public static class GeneralExtension
    {
        public static string GetUserId(this HttpContext httpcontext)
        {
            if (httpcontext.User == null)
            {
                return string.Empty;
            }
            return httpcontext.User.Claims.Single(x => x.Type == "id").Value;
        }
    }
}
