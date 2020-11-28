namespace KNU.IT.DbServices.Models.SettingModels
{
    public class MongoDatabaseSettings : IMongoDatabaseSettings
    {
        public string DatabasesCollectionName { get; set; }
        public string TablesCollectionName { get; set; }
        public string RowsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IMongoDatabaseSettings
    {
        string DatabasesCollectionName { get; set; }
        string TablesCollectionName { get; set; }
        string RowsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
