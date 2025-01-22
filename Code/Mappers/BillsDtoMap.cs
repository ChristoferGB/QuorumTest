using QuorumTest.Model.Dtos;

namespace QuorumTest.Mappers
{
    public sealed class BillsDtoMap : BaseMap<BillsDto>
    {
        BillsDtoMap()
        {
            Map(x => x.Id).Index(0).Name("id");
            Map(x => x.Title).Index(1).Name("title");
            Map(x => x.SupporterCount).Index(2).Name("supporte_count");
            Map(x => x.OpposerCount).Index(3).Name("opposer_count");
            Map(x => x.PrimarySponsor).Index(4).Name("primary_sponsor");
        }
    }
}
