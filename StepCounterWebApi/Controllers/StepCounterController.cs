using Microsoft.AspNetCore.Mvc;
using StepCounterWebApi.DataContexts;
using StepCounterWebApi.Models;
using StepCounterWebApi.Services;

namespace StepCounterWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StepCounterController : ControllerBase
    {
        private readonly IStepCounterRepository stepCounterRepository = new StepCounterRepository(FakeDataContext.GetInstance());

        //1. As a User
        //I want to be able to create a new counter
        //So that steps can be accumulated for a team of one or multiple employees
        [HttpPost()]
        public void CreateNewTeamWithMembers(string teamName, string[] memberNames)
        {
            stepCounterRepository.CreateTeamWithMembers(teamName, memberNames);
        }

        //2. As a User
        //I want to be able to increment the value of a stored counter
        //So that I can get steps counted towards my team's score
        [HttpPut()]
        public void IncrementCounter(string counterId)
        {
            stepCounterRepository.IncreaseCounter(ParseGuid(counterId));
        }

        //3. As a User
        //I want to get the current total steps taken by a team
        //So that I can see how much that team have walked in total
        [HttpGet()]
        public Team GetTotalStepsForTeam(string teamId)
        {
            return stepCounterRepository.GetTotalStepsForTeam(ParseGuid(teamId));
        }

        //4. As a User
        //I want to list all teams and see their step counts
        //So that I can compare my team with the others
        [HttpGet()]
        public IEnumerable<Team> GetTotalStepsByAllTeams()
        {
            return stepCounterRepository.GetTotalStepsForAllTeams();
        }

        //5. As a User
        //I want to list all counters in a team
        //So that I can see how much each team member have walked
        [HttpGet()]
        public IEnumerable<TeamMember> GetMembersForTeam(string teamId)
        {
            return stepCounterRepository.GetMembersForTeam(ParseGuid(teamId));
        }

        //6. As a User
        //I want to be able to add/delete teams
        //So that I can manage teams
        [HttpPost()]
        public void AddTeam(string teamId)
        {
            stepCounterRepository.DeleteTeam(ParseGuid(teamId));
        }

        [HttpDelete()]
        public void DeleteTeam(string teamId)
        {
            stepCounterRepository.DeleteTeam(ParseGuid(teamId));
        }

        //7. As a User
        //I want to be able to add/delete counters
        //So that I can manage team member's counters
        [HttpPost()]
        public void AddNewMemberToTeam(string teamId, string memberName)
        {
            stepCounterRepository.AddNewMember(ParseGuid(teamId), memberName);
        }

        [HttpDelete()]
        public void DeleteTeamMember(string counterId)
        {
            stepCounterRepository.DeleteMember(ParseGuid(counterId));
        }

        private static Guid ParseGuid(string id) => Guid.Parse(id);
    }
}