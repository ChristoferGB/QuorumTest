using QuorumTest.Model;

namespace QuorumTest.Mappers
{
    public sealed class VoteResultMap : BaseMap<VoteResult>
    {
        public VoteResultMap() 
        {
            Map(x => x.Id).Index(0).Name("id");
            Map(x => x.LegislatorId).Index(1).Name("legislator_id");
            Map(x => x.VoteId).Index(2).Name("vote_id");
            Map(x => x.VoteType).Index(3).Name("vote_type");
        }
    }
}
