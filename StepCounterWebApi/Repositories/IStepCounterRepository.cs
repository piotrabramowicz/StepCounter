using StepCounterWebApi.Models;

namespace StepCounterWebApi.Services
{
    public interface IStepCounterRepository
    {
        Task CreateTeamWithMembers(string teamName, string[] memberNames);
        Task AddNewMemberToTeam(Guid teamid, string memberName);
        Task IncreaseCounter(Guid memberId);  
        Task<int> GetTotalStepsForTeam(Guid teamId);
        Task<List<Tuple<string, int>>> GetTotalStepsForAllTeams();
        Task<List<TeamMember>> GetMembersForTeam(Guid teamId);
        Task DeleteMember(Guid memberId);
        Task DeleteTeam(Guid teamId);
    }
}