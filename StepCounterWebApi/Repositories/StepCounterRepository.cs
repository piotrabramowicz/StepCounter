using StepCounterWebApi.DataContexts;
using StepCounterWebApi.Models;

namespace StepCounterWebApi.Services
{
    public class StepCounterRepository(FakeDataContext dataContext) : IStepCounterRepository
    {
        private readonly IList<TeamMember> TeamMembers = dataContext.TeamMembers;
        private readonly IList<Team> Teams = dataContext.Teams;

        public Task CreateTeamWithMembers(string teamName, string[] memberNames)
        {
            var teamId = Guid.NewGuid();
            Teams.Add(new Team() { Id = teamId, Name = teamName });

            foreach (var memberName in memberNames)
            {
                AddNewMemberToTeam(teamId, memberName);
            }
            return Task.CompletedTask;
        }

        public Task AddNewMemberToTeam(Guid teamId, string memberName)
        {
            TeamMembers.Add(new TeamMember() { Id = Guid.NewGuid(), Name = memberName, TeamId = teamId });
            return Task.CompletedTask;
        }

        public Task IncreaseCounter(Guid counterId)
        {
            (from member in TeamMembers
             where member.Id == counterId
             select member).First().NumberOfSteps++;

            return Task.CompletedTask;
        }

        public Task<int> GetTotalStepsForTeam(Guid teamId)
        {
            var result = 0;
            foreach (var member in TeamMembers)
            {
                if (member.TeamId == teamId)
                {
                    result += member.NumberOfSteps;
                }
            }
            return Task.FromResult(result);
        }

        public Task<List<Tuple<string, int>>> GetTotalStepsForAllTeams()
        {
            var result = new List<Tuple<string, int>>();
            var query = from team in Teams
                        select new { TeamName = team.Name, TeamTotalSteps = GetTotalStepsForTeam(team.Id) };

            foreach (var item in query)  
            {
                result.Add(new Tuple<string, int>(item.TeamName, item.TeamTotalSteps.Result));
            }

            return Task.FromResult(result);
        }

        public Task DeleteTeam(Guid teamId)
        {
            var teamToRemove = (from team in Teams
                                where team.Id == teamId
                                select team).First();

            var teamMembersToRemove = (from member in TeamMembers
                                       where member.TeamId == teamId
                                       select member).ToList();

            Teams.Remove(teamToRemove);
            foreach (var member in teamMembersToRemove)
            {
                TeamMembers.Remove(member);
            }

            return Task.CompletedTask;
        }

        public async Task<List<TeamMember>> GetMembersForTeam(Guid teamId)
        {
            var result = (from member in TeamMembers
                          where member.TeamId == teamId
                          select member).ToList();
            return await Task.FromResult(result);
        }

        public Task DeleteMember(Guid memberId)
        {
            var teamMemberToDelete = (from member in TeamMembers
                                      where member.Id == memberId
                                      select member).First();
            TeamMembers.Remove(teamMemberToDelete);
            return Task.CompletedTask;
        }
    }
}