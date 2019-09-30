using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Linq.Expressions;

namespace PrintManager
{
    public static class Dispatcher
    {
        public static List<DocumentType> DocumentTypes = new List<DocumentType>();
        private static List<Document> DocumentsQueue { get; set; } = new List<Document>();
        private static List<Document> PrintedDocuments { get; set; } = new List<Document>();
        private static CancellationTokenSource source = new CancellationTokenSource();
        private static bool stop = true;

        public static List<Document> StopPrinting()
        {
            source.Cancel();
            stop = true;
            var list = DocumentsQueue.ToList();
            return list;
        }

        public static async Task Print()
        {
            while (DocumentsQueue.Count != 0)
            {
                var document = DocumentsQueue.First();
                if (document != null)
                {
                    await Task.Delay(document.Type.PrintDuration * 1000, source.Token);
                    Console.WriteLine(document.Name);
                    PrintedDocuments.Add(document);
                    DocumentsQueue.Remove(document);
                }
            }
        }

        public static bool AcceptPrintDocument(string document)
        {
            string name = document.Split('.')[0];
            string type = document.Split('.')[1];
            var typeDoc = DocumentTypes.Where(x => x.Name == type.ToLower()).First();
            if (document != null)
            {
                DocumentsQueue.Add(new Document(name, typeDoc));
                if (stop == true)
                {
                    stop = false;
                    Task.Run(() => Print());
                }
                return true;
            }
            else
                return false;
        }

        public static bool CancelPrintDocument(string document)
        {
            string name = document.Split('.')[0];
            string type = document.Split('.')[1];
            var doc = DocumentsQueue.Where(x => x.Name == name && x.Type.Name == type).First();
            if (doc != null)
            {
                DocumentsQueue.Remove(doc);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static List<Document> GetListPrintedDocuments(OrderType type, bool descending = false)
        {
            switch (type)
            {
                case OrderType.ByOrder: 
                    return descending ? PrintedDocuments.OrderByDescending(x => x).ToList() : PrintedDocuments.OrderBy(x => x).ToList();
                case OrderType.ByDocumentType: 
                    return descending ? PrintedDocuments.OrderByDescending(x => x.Type).ToList() : PrintedDocuments.OrderBy(x => x.Type).ToList();
                case OrderType.PaperSize: 
                    return descending ? PrintedDocuments.OrderByDescending(x => x.Type.PaperSize).ToList() : PrintedDocuments.OrderBy(x => x.Type.PaperSize).ToList();
                default: 
                    return new List<Document>();
            }
        }

        public static double GetAVGTimePrinting()
        {
            return (PrintedDocuments.Count != 0) ? (double)PrintedDocuments
                .Select(x => x.Type.PrintDuration)
                .Sum() / PrintedDocuments.Count() : 0;
        }
    }
}