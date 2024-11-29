using StepCounterWebApi.DataContexts;
using StepCounterWebApi.Models;

namespace StepCounterWebApi.Services
{
    public class StepCounterRepository(FakeDataContext dataContext) : IStepCounterRepository
    {
        private readonly IList<TeamMember> TeamMembers = dataContext.TeamMembers;
        private readonly IList<Team> Teams = dataContext.Teams;

        public void CreateTeamWithMembers(string teamName, string[] memberNames)
        {
            var newTeam = AddNewTeam(teamName);

            foreach (var memberName in memberNames)
            {
                AddNewMember(newTeam.Id, memberName);
            }
        }

        public void IncreaseCounter(Guid memberId)
        {
            (from member in TeamMembers
             where member.Id == memberId
             select member).First().NumberOfSteps++;
        }

        public Team GetTotalStepsForTeam(Guid teamId)
        {
            var result = (from team in Teams
                          where team.Id == teamId
                          select team).First();

            var members = (from member in TeamMembers
                           where member.TeamId == teamId
                           select member).ToList();

            foreach (var member in members)
            {
                result.SumOfSteps += member.NumberOfSteps;
            }

            return result;
        }

        public IEnumerable<Team> GetTotalStepsForAllTeams()
        {
            var result = from team in Teams
                         select new Team { Id = team.Id, Name = team.Name, SumOfSteps = GetTotalStepsForTeam(team.Id).SumOfSteps };

            return result;
        }

        public IEnumerable<TeamMember> GetMembersForTeam(Guid teamId)
        {
            var result = (from member in TeamMembers
                          where member.TeamId == teamId
                          select member).ToList();

            return result;
        }

        public Team AddNewTeam(string teamName)
        {
            var result = new Team() { Id = Guid.NewGuid(), Name = teamName };
            Teams.Add(result);

            return result;
        }

        public void DeleteTeam(Guid teamId)
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
        }

        public void AddNewMember(Guid teamId, string memberName)
        {
            TeamMembers.Add(new TeamMember() { Id = Guid.NewGuid(), Name = memberName, TeamId = teamId });
        }

        public void DeleteMember(Guid memberId)
        {
            var teamMemberToDelete = (from member in TeamMembers
                                      where member.Id == memberId
                                      select member).First();
            TeamMembers.Remove(teamMemberToDelete);
        }      
    }
}