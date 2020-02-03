using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApiWithCore.Contracts
{
 
    public static class ApiRoutes
    {
        public const string root = "api";
        public const string Version = "v1";
        public const string Base = root+"/"+Version;
        public static class Posts
        {

            public const string GetAllPost = Base+"/posts";
            public const string Create = Base+"/create";
            public const string Update = Base+"/update/{postId}";
            public const string Get = Base+"/posts/{postId}";
            public const string Delete = Base+"/delete/{postId}";

        }

        public static class Identity
        {
            public const string Login = Base + "Identity/login";
            public const string Register = Base + "Identity/register";
        }


    }
}
