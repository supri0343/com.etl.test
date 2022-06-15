namespace Com.Danliris.ETL.Service.Models
{
    public class Area
    {
        public Area(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
        public int id { get; set;}
        public string name { get; set;}
    }
}