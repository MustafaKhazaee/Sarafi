namespace Sarafi.Domain.Constants
{
    public static class AuthorizationRoles
    {
        private const string root = "r";
        public static class Accounts
        {
            public static string Get = $"{root}, 00";
            public static string Post = $"{root}, 01";
            public static string Put = $"{root}, 02";
            public static string Delete = $"{root}, 03";
        }
        public static class Company
        {
            public static string Get = $"{root}, 10";
            public static string Post = $"{root}, 11";
            public static string Put = $"{root}, 12";
            public static string Delete = $"{root}, 13";
        }
        public static class MasterAccount
        {
            public static string Get = $"{root}, 20";
            public static string Post = $"{root}, 21";
            public static string Put = $"{root}, 22";
            public static string Delete = $"{root}, 23";
        }
        public static class Transation
        {
            public static string Get = $"{root}, 30";
            public static string Post = $"{root}, 31";
            public static string Put = $"{root}, 32";
            public static string Delete = $"{root}, 33";
        }
        public static class User
        {
            public static string Get = $"{root}, 40";
            public static string Post = $"{root}, 41";
            public static string Put = $"{root}, 42";
            public static string Delete = $"{root}, 43";
        }
    }
}
