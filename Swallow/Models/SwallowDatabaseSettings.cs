using System;

namespace Swallow.Models
{
    public class SwallowDatabaseSettings
    {
        public string UsersCollectionName { get; set; } = null;
        public string ConnectionString { get; set; } = null;
        public string DatabaseName { get; set; } = null;

        
    }


    public interface ISwallowDatabaseSettings
    {
        string UsersCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }

}
