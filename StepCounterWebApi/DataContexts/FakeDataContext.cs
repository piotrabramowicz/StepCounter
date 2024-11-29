using StepCounterWebApi.Models;

namespace StepCounterWebApi.DataContexts
{
    public sealed class FakeDataContext
    {
        private static FakeDataContext _instance;

        private readonly IList<Team> _teams;
        private readonly IList<TeamMember> _teamMembers;

        public IList<Team> Teams { get { return _teams; } }
        public IList<TeamMember> TeamMembers { get { return _teamMembers; } }

        private FakeDataContext()
        {
            _teams = [];
            _teamMembers = [];
        }

        public static FakeDataContext GetInstance()
        {
            _instance ??= new FakeDataContext();
            return _instance;
        }
    }
}