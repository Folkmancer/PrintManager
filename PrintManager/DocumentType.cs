namespace PrintManager
{
    public class DocumentType
    {
        public string Name { get; set; }
        public PaperSize PaperSize { get; set; }
        public int PrintDuration { get; set; }

        public DocumentType(string name, PaperSize paperSize, int printDuration)
        {
            this.Name = name;
            this.PaperSize = paperSize;
            this.PrintDuration = printDuration;
        }
    }
}