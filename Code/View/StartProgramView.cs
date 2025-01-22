using QuorumTest.Controller;

namespace QuorumTest.View
{
    public class StartProgramView
    {
        private readonly DataHandlingController controller;

        public StartProgramView() 
        {
            controller = new DataHandlingController();
        }

        public void StartConsole()
        {
            Console.WriteLine("Welcome to the bills voting CSV generator!\n\n");

            var executing = true;

            while (executing)
            {
                MenuSelection();

                var option = Console.ReadLine();
                Console.Write("\n\n");

                switch (option)
                {
                    case "1":
                        controller.CreateCountCsv();
                        break;
                    case "2":
                        controller.CreateBillsCsv();
                        break;
                    default:
                        executing = false;
                        Console.WriteLine("See ya!");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private static void MenuSelection()
        {
            Console.WriteLine("Choose one of the following options to proceed:\n");
            Console.WriteLine("Type 1 to download the report for legislators votes");
            Console.WriteLine("Type 2 to download the report for bills");
            Console.WriteLine("Type any other key to exit\n");
        }
    }
}
