﻿using CsvHelper;
using QuorumTest.Mappers;
using System.Globalization;
using System.Reflection;

namespace QuorumTest.Helpers
{
    public class CsvReaderHelper
    {
        public static List<T> ReadCsv<T>(string filePath)
        {
            try
            {
                using var reader = new StreamReader(filePath);
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

                var mapType = Assembly
                    .GetAssembly(typeof(BaseMap<T>))?
                    .GetTypes()
                    .Where(map => map.IsClass && map.IsSubclassOf(typeof(BaseMap<T>)))
                    .FirstOrDefault();

                csv.Context.RegisterClassMap(mapType);

                var records = csv.GetRecords<T>().ToList();
                return records;
            }
            catch (Exception e)
            {
                Console.WriteLine($"It was not possible to read the {typeof(T).Name} CSV file at this time. See the error stack trace below: ");
                Console.WriteLine(e);

                return [];
            }
        }
    }
}
