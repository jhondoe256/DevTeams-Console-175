namespace DevTeams.Data.Entities
{
    public class Developer
    {
        //* Empty Constructor
        public Developer(){}
        
        //* Full Constructor
        public Developer(string firstName, string lastName, bool hasPluralsight)
        {
            FirstName = firstName;
            LastName = lastName;
            HasPluralsight = hasPluralsight;
        }

        //* Primary Key **UNIQUE IDENTIFIER**
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName 
        {
             get 
             {
                return $"{FirstName} {LastName}";
             }
        }
        public bool HasPluralsight { get; set; }

        //* foreign key
        public int DevTeamId { get; set; }
    }
}