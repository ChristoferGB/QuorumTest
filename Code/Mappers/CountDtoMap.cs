using QuorumTest.Model.Dtos;

namespace QuorumTest.Mappers
{
    public sealed class CountDtoMap : BaseMap<CountDto>
    {
        public CountDtoMap() 
        {
            Map(x => x.Id).Index(0).Name("id");
            Map(x => x.Name).Index(1).Name("name");
            Map(x => x.SupportedBills).Index(1).Name("num_supported_bills");
            Map(x => x.OpposedBills).Index(0).Name("num_opposed_bills");
        }
    }
}
