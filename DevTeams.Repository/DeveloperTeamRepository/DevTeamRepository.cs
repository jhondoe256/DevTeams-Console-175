using DevTeams.Data.Entities;

namespace DevTeams.Repository.DeveloperTeamRepository
{
    public class DevTeamRepository
    {
        private readonly List<DeveloperTeam> _devTeamDbContext = new List<DeveloperTeam>();

        private int _count = 0;

        public bool AddDeveloperTeam(DeveloperTeam team)
        {
            if (team is null)
            {
                return false;
            }
            else
            {
                _count++;
                team.Id = _count;
                _devTeamDbContext.Add(team);
                return true;
            }
        }

        public bool AddDeveloperToTeam(int teamId, Developer developer)
        {
            if (teamId > 0 && developer != null)
            {
                DeveloperTeam team = GetDeveloperTeamById(teamId);
                if (team != null)
                {
                    developer.DevTeamId = team.Id;
                    team.DevsOnTeam.Add(developer);
                    return true;
                }
            }
            return false;
        }

        public bool RemoveDeveloperFromTeam(int teamId, Developer developer)
        {
            if (teamId > 0 && developer != null)
            {
                DeveloperTeam team = GetDeveloperTeamById(teamId);
                if (team != null)
                    return team.DevsOnTeam.Remove(developer);

            }
            return false;
        }

        public List<DeveloperTeam> GetDeveloperTeams()
        {
            return _devTeamDbContext;
        }

        public DeveloperTeam GetDeveloperTeamById(int id)
        {
            return _devTeamDbContext.SingleOrDefault(x => x.Id == id)!;
        }

        public bool UpdateDevTeam(int teamId, DeveloperTeam newDeveloperTeamData)
        {
            DeveloperTeam teamInDb = GetDeveloperTeamById(teamId);
            if (teamInDb is not null)
            {
                teamInDb!.Name = newDeveloperTeamData.Name;
                teamInDb.DevsOnTeam = (newDeveloperTeamData.DevsOnTeam.Count() > 0) ?
                                            newDeveloperTeamData.DevsOnTeam :
                                            teamInDb.DevsOnTeam;

                return true;
            }

            return false;
        }

        public bool DeleteDevTeam(DeveloperTeam team)
        {
            return _devTeamDbContext.Remove(team);
        }

        //* a method that gives back all the developers that aren't on a team?
    }
}