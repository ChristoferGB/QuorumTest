using CsvHelper;
using QuorumTest.Mappers;
using System.Globalization;
using System.Reflection;

namespace QuorumTest.Helpers
{
    public class CsvWriterHelper
    {
        public static void WriteCsv<T>(List<T> records, string filePath)
        {
            using var writer = new StreamWriter(filePath);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

            var mapType = Assembly
                .GetAssembly(typeof(BaseMap<T>))?
                .GetTypes()
                .Where(map => map.IsClass && map.IsSubclassOf(typeof(BaseMap<T>)))
                .FirstOrDefault();

            csv.Context.RegisterClassMap(mapType);

            csv.WriteRecords(records);
        }
    }
}
