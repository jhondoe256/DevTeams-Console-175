namespace DevTeams.Data.Entities
{
    public class DeveloperTeam
    {
        public DeveloperTeam()
        {
            
        }

        public DeveloperTeam(string name)
        {
            Name = name;
        }

        public DeveloperTeam(string name, List<Developer> developers)
        {
            Name = name;
            DevsOnTeam = developers;
        }
        
        //* Primary Key
        public int Id { get; set; }
        public string Name { get; set; }

        //* One developer team can have "many" developers
        public List<Developer> DevsOnTeam { get; set; } = new List<Developer>();
    }
}