using QuorumTest.Helpers;
using QuorumTest.Model.Dtos;
using QuorumTest.Model.Enums;
using QuorumTest.Model;

namespace QuorumTest.Controller
{
    public class DataHandlingController
    {
        private List<Bill> _bills = [];
        private List<Person> _people = [];
        private List<Vote> _votes = [];
        private List<VoteResult> _voteResults = [];

        public DataHandlingController() 
        {
            LoadData();
        }

        private void LoadData()
        {
            _bills = CsvReaderHelper.ReadCsv<Bill>(@"..\..\..\Assets\bills.csv");
            _people = CsvReaderHelper.ReadCsv<Person>(@"..\..\..\Assets\legislators.csv");
            _votes = CsvReaderHelper.ReadCsv<Vote>(@"..\..\..\Assets\votes.csv");
            _voteResults = CsvReaderHelper.ReadCsv<VoteResult>(@"..\..\..\Assets\vote_results.csv");
        }

        public void CreateCountCsv()
        {
            if (_people.Count == 0)
            {
                Console.WriteLine("The Legislators CSV file have not been loaded yet");
                return;
            }

            var countCsvDto = new List<CountDto>();

            foreach (var person in _people)
            {
                var voteResultsForPerson = _voteResults.Where(x => x.LegislatorId == person.Id);

                var suportedBills = voteResultsForPerson.Count(x => x.VoteType == VoteType.Yea);
                var opposedBills = voteResultsForPerson.Count(x => x.VoteType == VoteType.Nay);

                var countDto = CreateCountDto(person, suportedBills, opposedBills);

                countCsvDto.Add(countDto);
            }

            var filePath = @"CSV files\legislators-support-oppose-count.csv";

            CsvWriterHelper.WriteCsv(countCsvDto, filePath);
        }

        private static CountDto CreateCountDto(Person person, int suportedBills, int opposedBills)
        {
            return new CountDto
            {
                Id = person.Id,
                Name = person.Name,
                OpposedBills = opposedBills,
                SupportedBills = suportedBills
            };
        }

        public void CreateBillsCsv()
        {
            if (_bills.Count == 0)
            {
                Console.WriteLine("The Bills CSV file have not been loaded yet");
                return;
            }

            var billsCsvDto = new List<BillsDto>();

            foreach (var bill in _bills)
            {
                var billVotesIds = _votes
                    .Where(x => x.BillId == bill.Id)
                    .Select(x => x.Id)
                    .ToList();

                var supportersCount = _voteResults.Count(x => billVotesIds.Contains(x.VoteId) && x.VoteType == VoteType.Yea);
                var opposerCount = _voteResults.Count(x => billVotesIds.Contains(x.VoteId) && x.VoteType == VoteType.Nay);

                var primarySponsor = _people
                    .Where(x => x.Id == bill.PrimarySponsor)
                    .Select(x => x.Name)
                    .FirstOrDefault() ??
                    "Unknown";

                var billsDto = CreateBillsDto(bill, supportersCount, opposerCount, primarySponsor);

                billsCsvDto.Add(billsDto);
            }

            var filePath = @"CSV Files\bills.csv";

            CsvWriterHelper.WriteCsv(billsCsvDto, filePath);
        }

        private static BillsDto CreateBillsDto(Bill bill, int supportersCount, int opposerCount, string primarySponsor)
        {
            return new BillsDto
            {
                Id = bill.Id,
                Title = bill.Title,
                OpposerCount = opposerCount,
                SupporterCount = supportersCount,
                PrimarySponsor = primarySponsor
            };
        }
    }
}
