namespace Sarafi.Domain.Constants;

public static class AuthorizationRoles
{
    private const string root = "r";
    public static class Accounts
    {
        public const string Get = $"{root}, 00";
        public const string Post = $"{root}, 01";
        public const string Put = $"{root}, 02";
        public const string Delete = $"{root}, 03";
    }
    public static class Company
    {
        public const string Get = $"{root}, 10";
        public const string Post = $"{root}, 11";
        public const string Put = $"{root}, 12";
        public const string Delete = $"{root}, 13";
    }
    public static class MasterAccount
    {
        public const string Get = $"{root}, 20";
        public const string Post = $"{root}, 21";
        public const string Put = $"{root}, 22";
        public const string Delete = $"{root}, 23";
    }
    public static class Transation
    {
        public const string Get = $"{root}, 30";
        public const string Post = $"{root}, 31";
        public const string Put = $"{root}, 32";
        public const string Delete = $"{root}, 33";
    }
    public static class User
    {
        public const string Get = $"{root}, 40";
        public const string Post = $"{root}, 41";
        public const string Put = $"{root}, 42";
        public const string Delete = $"{root}, 43";
    }
}
