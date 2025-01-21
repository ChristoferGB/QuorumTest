using QuorumTest.Model;

namespace QuorumTest.Mappers
{
    public sealed class BillMap : BaseMap<Bill>
    {
        public BillMap() 
        {
            Map(x => x.Id).Index(0).Name("id");
            Map(x => x.Title).Index(1).Name("title");
            Map(x => x.PrimarySponsor).Index(2).Name("sponsor_id");
        }
    }
}
