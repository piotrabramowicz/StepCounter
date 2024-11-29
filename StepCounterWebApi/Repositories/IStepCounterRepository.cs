using StepCounterWebApi.Models;

namespace StepCounterWebApi.Services
{
    public interface IStepCounterRepository
    {
        void CreateTeamWithMembers(string teamName, string[] memberNames);
        void IncreaseCounter(Guid memberId);  
        Team GetTotalStepsForTeam(Guid teamId);
        IEnumerable<Team> GetTotalStepsForAllTeams();
        IEnumerable<TeamMember> GetMembersForTeam(Guid teamId);
        Team AddNewTeam(string teamName);
        void DeleteTeam(Guid teamId);
        void AddNewMember(Guid teamid, string memberName);
        void DeleteMember(Guid memberId);
    }
}