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

        /// <summary>
        ///     Creates new team with members
        /// </summary>
        /// <param name="teamName">team name</param>
        /// <param name="memberNames"> names of members</param>
        /// <remarks>
        ///     Sample request:
        ///
        ///        POST /StepCounter/CreateNewTeamWithMembers
        ///         {
        ///            "teamName": "Best Team",
        ///            "memberNames": { "member 1", "member 2", "member 3",}
        ///         }
        ///
        /// </remarks>
        /// <response code="200">Counter created</response>
        /// <response code="500">Oops! Not today.</response>
        [HttpPost()]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public void CreateNewTeamWithMembers(string teamName, string[] memberNames)
        {
            stepCounterRepository.CreateTeamWithMembers(teamName, memberNames);
        }

        /// <summary>
        /// Report new step of employee
        /// </summary>
        /// <param name="memberId">Member ID</param>
        /// <response code="200">Counter inreased</response>
        /// <response code="500">Oops! Not today.</response>
        [HttpPut()]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public void IncrementCounter(string memberId)
        {
            stepCounterRepository.IncreaseCounter(ParseGuid(memberId));
        }

        /// <summary>
        /// Get list of steps of all members for a team
        /// </summary>
        /// <param name="teamId">Team ID</param>
        /// <returns></returns>
        /// <response code="200">ok</response>
        /// <response code="500">Oops! Not today.</response>
        [HttpGet()]
        [ProducesResponseType(typeof(Team), 200)]
        [ProducesResponseType(500)]
        public Team GetTotalStepsForTeam(string teamId)
        {
            return stepCounterRepository.GetTotalStepsForTeam(ParseGuid(teamId));
        }

        /// <summary>
        /// Get list of steps of all members of all teams
        /// </summary>
        /// <returns></returns>
        /// <response code="200">ok</response>
        /// <response code="500">Oops! Not today.</response>
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<Team>), 200)]
        [ProducesResponseType(500)]
        public IEnumerable<Team> GetTotalStepsByAllTeams()
        {
            return stepCounterRepository.GetTotalStepsForAllTeams();
        }

        /// <summary>
        /// Get list of members for a team
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns></returns>
        /// <response code="200">ok</response>
        /// <response code="500">Oops! Not today.</response>
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<TeamMember>), 200)]
        [ProducesResponseType(500)]
        public IEnumerable<TeamMember> GetMembersForTeam(string teamId)
        {
            return stepCounterRepository.GetMembersForTeam(ParseGuid(teamId));
        }

        /// <summary>
        /// Add team
        /// </summary>
        /// <param name="teamId">team ID</param>
        /// <response code="200">ok</response>
        /// <response code="500">Oops! Not today.</response>
        [HttpPost()]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public void AddTeam(string teamId)
        {
            stepCounterRepository.DeleteTeam(ParseGuid(teamId));
        }

        /// <summary>
        /// Delete team 
        /// </summary>
        /// <param name="teamId">Team ID</param>
        /// <response code="200">ok</response>
        /// <response code="500">Oops! Not today.</response>
        [HttpDelete()]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public void DeleteTeam(string teamId)
        {
            stepCounterRepository.DeleteTeam(ParseGuid(teamId));
        }

        /// <summary>
        /// Add new member
        /// </summary>
        /// <param name="teamId">Team ID</param>
        /// <param name="memberName">Name of member</param>
        /// <response code="200">ok</response>
        /// <response code="500">Oops! Not today.</response>
        [HttpPost()]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public void AddNewMember(string teamId, string memberName)
        {
            stepCounterRepository.AddNewMember(ParseGuid(teamId), memberName);
        }

        /// <summary>
        /// Delete team member
        /// </summary>
        /// <param name="memberId">Member ID</param>
        /// <response code="200">ok</response>
        /// <response code="500">Oops! Not today.</response>
        [HttpDelete()]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public void DeleteTeamMember(string memberId)
        {
            stepCounterRepository.DeleteMember(ParseGuid(memberId));
        }

        private static Guid ParseGuid(string id) => Guid.Parse(id);
    }
}