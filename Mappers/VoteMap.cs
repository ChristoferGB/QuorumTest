using QuorumTest.Model;

namespace QuorumTest.Mappers
{
    public sealed class VoteMap : BaseMap<Vote>
    {
        public VoteMap() 
        {
            Map(x => x.Id).Index(0).Name("id");
            Map(x => x.BillId).Index(1).Name("bill_id");
        }
    }
}
