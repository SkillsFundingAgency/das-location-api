using System;

namespace SFA.DAS.Location.Domain.Entities
{
    public class ImportAudit
    {
        public ImportAudit(DateTime timeStarted, int rowsImported, string name, ImportType importType = 0)
        {
            TimeStarted = timeStarted;
            RowsImported = rowsImported;
            TimeFinished = DateTime.UtcNow;
            ImportType = importType;
            Name = name;
        }

        public int Id { get; set; }
        public DateTime TimeStarted { get;  set; }
        public DateTime TimeFinished { get;  set; }
        public int RowsImported { get;  set; }
        public string Name { get; set; }
        public ImportType ImportType { get ;  set; }
    }

    public enum ImportType : byte
    {
        OnsLocation = 0
    }
}