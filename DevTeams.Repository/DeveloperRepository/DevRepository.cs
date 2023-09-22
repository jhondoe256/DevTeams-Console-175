using DevTeams.Data.Entities;

namespace DevTeams.Repository.DeveloperRepository
{
    public class DevRepository
    {
        //* Create our fake database
        private readonly List<Developer> _devDbContext = new List<Developer>();

        //* Database Id base value;
        private int _count = 0;

        public bool AddDeveloper(Developer developer)
        {
            if (developer is null)
            {
                return false;
            }
            else
            {
                _count++;
                developer.Id = _count;
                _devDbContext.Add(developer);
                return true;
            }
        }

        public List<Developer> GetDevelopers()
        {
            return _devDbContext;
        }

        public Developer GetDeveloperById(int id)
        {
            return _devDbContext.FirstOrDefault(x => x.Id == id)!;
        }

        public bool UpdateDeveloper(int id, Developer newDevData)
        {
            Developer devInDb = GetDeveloperById(id);
            if (devInDb != null)
            {
                devInDb.FirstName = newDevData.FirstName;
                devInDb.LastName = newDevData.LastName;
                devInDb.HasPluralsight = newDevData.HasPluralsight;

                return true;
            }

            return false;
        }

        public bool DeleteDeveloper(Developer dev)
        {
            return _devDbContext.Remove(dev);
        }

        public List<Developer> GetDevelopersWithoutPluralsight()
        {
            return _devDbContext.Where(dev => dev.HasPluralsight == false).ToList();

           //*  List<Developer> devsWithoutPS = new List<Developer>();
           //*  foreach (var dev in _devDbContext)
           //*  {
           //*      if(dev.HasPluralsight == false)
           //*      {
           //*          devsWithoutPS.Add(dev);
           //*      }
           //*  }
           //*  return devsWithoutPS;
        }

         public List<Developer> GetDevelopersNotOnATeam()
        {
            return _devDbContext.Where(dev => dev.DevTeamId == 0).ToList();

           //*  List<Developer> devsNotOnTeam = new List<Developer>();
           //*  foreach (var dev in _devDbContext)
           //*  {
           //*      if(dev.DevTeamId == 0)
           //*      {
           //*          devsNotOnTeam.Add(dev);
           //*      }
           //*  }
           //*  return devsNotOnTeam;
        }
    }
}