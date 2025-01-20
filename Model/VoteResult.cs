using QuorumTest.Model.Enums;

namespace QuorumTest.Model
{
    public class VoteResult
    {
        public int Id { get; set; }
        public int LegislatorId { get; set; }
        public int VoteId { get; set; }
        public VoteType VoteType { get; set; }
    }
}