namespace QuorumTest.Model.Dtos
{
    public class BillsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int SupporterCount { get; set; }
        public int OpposerCount { get; set; }
        public string PrimarySponsor { get; set; }
    }
}
