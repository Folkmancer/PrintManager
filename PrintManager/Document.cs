using System;

namespace PrintManager
{
    public class Document
    {
        public string Name { get; set; }
        public DocumentType Type { get; set; }

        public Document(string name, DocumentType type)
        {
            this.Name = name;
            this.Type = type;
        }

        public static int CompareByDocumentType(Document doc1, Document doc2)
        {
            return string.Compare(doc1.Type.Name, doc2.Type.Name);
        }

        public static int CompareByPaperSize(Document doc1, Document doc2)
        {
            return doc1.Type.PaperSize.CompareTo(doc2.Type.PaperSize);
        }
    }
}
