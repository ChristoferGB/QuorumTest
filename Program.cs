using QuorumTest.Helpers;
using QuorumTest.Model;
using QuorumTest.Model.Dtos;
using QuorumTest.Model.Enums;

internal class Program
{
    private static void Main(string[] args)
    {
        List<Bill> bills = CsvReaderHelper.ReadCsv<Bill>(@"..\..\..\Assets\bills.csv");
        List<Person> people = CsvReaderHelper.ReadCsv<Person>(@"..\..\..\Assets\legislators.csv");
        List<Vote> votes = CsvReaderHelper.ReadCsv<Vote>(@"..\..\..\Assets\votes.csv");
        List<VoteResult> voteResults = CsvReaderHelper.ReadCsv<VoteResult>(@"..\..\..\Assets\vote_results.csv");
        
        StartProgramView(bills, people, votes, voteResults);
    }

    private static void StartProgramView(List<Bill> bills, List<Person> people, List<Vote> votes, List<VoteResult> voteResults)
    {
        Console.WriteLine("Welcome to the bills voting CSV generator!\n\n");

        var executing = true;

        while (executing)
        {
            Console.WriteLine("Choose one of the following options to proceed:\n");
            Console.WriteLine("Type 1 to download the report for legislators votes");
            Console.WriteLine("Type 2 to download the report for bills");
            Console.WriteLine("Type any other key to exit\n");

            var option = Console.ReadLine();
            Console.Write("\n\n");

            switch (option)
            {
                case "1":
                    CreateCountCsv(people, voteResults);
                    break;
                case "2":
                    CreateBillsCsv(bills, people, votes, voteResults);
                    break;
                default:
                    executing = false;
                    Console.WriteLine("See ya!");
                    break;
            }
        }
    }

    private static void CreateCountCsv(List<Person> people, List<VoteResult> voteResults)
    {
        var countCsvDto = new List<CountDto>();

        foreach (var person in people)
        {
            var voteResultsForPerson = voteResults.Where(x => x.LegislatorId == person.Id);

            var suportedBills = voteResultsForPerson.Count(x => x.VoteType == VoteType.Yea);
            var opposedBills = voteResultsForPerson.Count(x => x.VoteType == VoteType.Nay);

            var countDto = CreateCountDto(person, suportedBills, opposedBills);

            countCsvDto.Add(countDto);
        }

        var filePath = "legislators-support-oppose-count.csv";

        WriteCsv(countCsvDto, filePath);
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

    private static void CreateBillsCsv(List<Bill> bills, List<Person> people, List<Vote> votes, List<VoteResult> voteResults)
    {
        var billsCsvDto = new List<BillsDto>();

        foreach (var bill in bills)
        {
            var billVotesIds = votes.Where(x => x.BillId == bill.Id).Select(x => x.Id).ToList();

            var supportersCount = voteResults.Count(x => billVotesIds.Contains(x.VoteId) && x.VoteType == VoteType.Yea);
            var opposerCount = voteResults.Count(x => billVotesIds.Contains(x.VoteId) && x.VoteType == VoteType.Nay);

            var primarySponsor = people.Where(x => x.Id == bill.PrimarySponsor).Select(x => x.Name).FirstOrDefault() ?? "Unknown";

            var billsDto = CreateBillsDto(bill, supportersCount, opposerCount, primarySponsor);

            billsCsvDto.Add(billsDto);
        }

        var filePath = "bills.csv";

        WriteCsv(billsCsvDto, filePath);
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

    private static void WriteCsv<T>(List<T> dto, string filePath)
    {
        CsvWriterHelper.WriteCsv(dto, filePath);

        Console.WriteLine($"CSV file created at: {Path.GetFullPath(filePath)}\n");
    }
}