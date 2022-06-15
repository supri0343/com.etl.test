namespace Com.Danliris.ETL.Service.Models
{
    public class Person
    {
        public Person(string name, string gender)
        {
            Name = name;
            Gender = gender;
        }

        public string Name { get; private set; }
        public string Gender { get; private set; }
    }
}