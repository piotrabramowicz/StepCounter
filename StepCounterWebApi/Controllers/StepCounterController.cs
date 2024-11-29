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
        private readonly IStepCounterRepository stepCounterFasade = new StepCounterRepository(FakeDataContext.GetInstance());

        //1. As a User
        //I want to be able to create a new counter
        //So that steps can be accumulated for a team of one or multiple employees
        [HttpPost()]
        public async Task<IActionResult> CreateNewTeamWithMembers(string teamName, string[] memberNames)
        {
            await stepCounterFasade.CreateTeamWithMembers(teamName, memberNames);
            return Ok(teamName);
        }

        //2. As a User
        //I want to be able to increment the value of a stored counter
        //So that I can get steps counted towards my team's score
        [HttpPut()]
        public async Task<IActionResult> IncrementCounter(string counterId)
        {
            await stepCounterFasade.IncreaseCounter(ParseGuid(counterId));
            return Ok();
        }

        //3. As a User
        //I want to get the current total steps taken by a team
        //So that I can see how much that team have walked in total
        [HttpGet()]
        public async Task<int> GetTotalStepsForTeam(string teamId)
        {
            return await stepCounterFasade.GetTotalStepsForTeam(ParseGuid(teamId));
        }

        //4. As a User
        //I want to list all teams and see their step counts
        //So that I can compare my team with the others
        [HttpGet()]
        public async Task<List<Tuple<string, int>>> GetTotalStepsByAllTeams()
        {
            return await stepCounterFasade.GetTotalStepsForAllTeams();
        }

        //5. As a User
        //I want to list all counters in a team
        //So that I can see how much each team member have walked
        [HttpGet()]
        public async Task<List<TeamMember>> GetMembersForTeam(string teamId)
        {
            return await stepCounterFasade.GetMembersForTeam(ParseGuid(teamId));
        }

        //6. As a User
        //I want to be able to add/delete teams
        //So that I can manage teams
        [HttpPost()]
        public async Task<IActionResult> DeleteTeam(string teamId)
        {
            await stepCounterFasade.DeleteTeam(ParseGuid(teamId));
            return Ok();
        }

        //As a User
        //I want to be able to add/delete counters
        //So that I can manage team member's counters
        [HttpPost()]
        public async Task<IActionResult> AddNewMemberToTeam(string teamId, string memberName)
        {
            await stepCounterFasade.AddNewMemberToTeam(ParseGuid(teamId), memberName);
            return Ok(memberName);
        }

        [HttpPost()]
        public async Task<IActionResult> DeleteCounter(string counterId)
        {
            await stepCounterFasade.DeleteMember(ParseGuid(counterId));
            return Ok();
        }

        private static Guid ParseGuid(string id) => Guid.Parse(id);
    }
}