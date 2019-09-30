using System;
using System.Collections.Generic;

namespace PrintManager
{
    class Program
    {
        static bool work = true;
        static void Main(string[] args)
        {
            var DocumentTypes = new[]
            {
                new DocumentType("txt", PaperSize.A3, 20),
                new DocumentType("pdf", PaperSize.A1, 150),
                new DocumentType("doc", PaperSize.A2, 70),
                new DocumentType("docx", PaperSize.A4, 50),
            };
            while (work)
            {
                Menu();
                string line = Console.ReadLine();
                int.TryParse(line, out int answer);
                switch (answer)
                {
                    case 1:
                        AddDocument();
                        break;
                    case 2:
                        CancelDocument();
                        break;
                    case 3:
                        StopPrint();
                        break;
                    case 4:
                        GetPrinted();
                        break;
                    case 5:
                        GetAVG();
                        break;
                    case 6:
                        work = false;
                        break;
                    default:
                        Console.WriteLine("Ошибка выбора пункта меню");
                        break;
                };
            }
        }

        public static void Menu()
        {
            Console.WriteLine("1. Принять документ на печать\n" +
                "2. Отменить печать документа\n" +
                "3. Остановить печать\n" +
                "4. Получить отсортированный список напечатанных документов\n" +
                "5. Рассчитать среднюю продолжительность печати напечатанных документов\n" +
                "6. Выход\n"
                );
        }

        public static void AddDocument()
        {
            Console.WriteLine("Укажите файл:");
            string fileAccept = Console.ReadLine();
            Dispatcher.AcceptPrintDocument(fileAccept);
        }

        public static void CancelDocument()
        {
            Console.WriteLine("Укажите файл:");
            string fileCancel = Console.ReadLine();
            Dispatcher.CancelPrintDocument(fileCancel);
        }

        public static void StopPrint()
        {
            Console.WriteLine("Печать остановлена:");
            var list = Dispatcher.StopPrinting();
            PrintList(list);
        }

        private static void PrintList(List<Document> list)
        {
            if (list != null)
            {
                foreach (var element in list)
                {
                    Console.WriteLine(element.Name + " "
                        + element.Type.Name + " "
                        + element.Type.PaperSize + " "
                        + element.Type.PrintDuration
                    );
                }
            }
            else
                Console.WriteLine("Список документов пуст");
        }

        public static void GetPrinted()
        {
            Console.WriteLine("1. Отсортировать по возрастанию по порядку печати.\n" +
                "2. Отсортировать по убыванию по порядку печати.\n" +
                "3. Отсортировать по возрастанию по типу документа.\n" +
                "4. Отсортировать по убыванию по типу документа.\n" +
                "5. Отсортировать по возрастанию по размеру бумаги.\n" +
                "6. Отсортировать по убыванию по размеру бумаги.\n"
                );
            var list = new List<Document>();
            string line = Console.ReadLine();
            int.TryParse(line, out int answer);
            switch (answer)
            {
                case 1:
                    list = Dispatcher.GetListPrintedDocuments(OrderType.ByOrder, false);
                    break;
                case 2:
                    list = Dispatcher.GetListPrintedDocuments(OrderType.ByOrder, true);
                    break;
                case 3:
                    list = Dispatcher.GetListPrintedDocuments(OrderType.ByDocumentType, false);
                    break;
                case 4:
                    list = Dispatcher.GetListPrintedDocuments(OrderType.ByDocumentType, true);
                    break;
                case 5:
                    list = Dispatcher.GetListPrintedDocuments(OrderType.PaperSize, false);
                    break;
                case 6:
                    list = Dispatcher.GetListPrintedDocuments(OrderType.PaperSize, true);
                    break;
                default:
                    Console.WriteLine("Ошибка выбора пункта меню");
                    break;
            };
            PrintList(list);
        }

        public static void GetAVG()
        {
            Console.WriteLine($"среднюю продолжительность печати: {Dispatcher.GetAVGTimePrinting()}");
        }
    }
}
