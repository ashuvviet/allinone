namespace OnBoarding.api.Options
{
    public class DbOptions
    {
        public string DefaultConnection {  get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }

    public class EnvOptions
    {
        public string ASPNETCORE_ENVIRONMENT { get; set; }
    }
}
