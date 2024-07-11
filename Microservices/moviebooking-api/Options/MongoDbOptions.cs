namespace moviebooking_api.Options
{
    public class MongoDbOptions
    {
        /// <summary>
        /// Position in app setting.
        /// </summary>
        public const string Position = "Mongo";

        /// <summary>
        /// Connection string
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// UserName of MySql Connection
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Password of Mysql Connection
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// CollectionName
        /// </summary>
        public string CityCollectionName { get; set; }
        public string CinemaCollectionName { get; set; }
        public string MovieCollectionName { get; set; }

    }
}
