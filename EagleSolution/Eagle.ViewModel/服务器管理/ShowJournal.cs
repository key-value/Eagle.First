using System;
using Eagle.Model;

namespace Eagle.ViewModel
{
    public class ShowJournal
    {
        public Guid ID { get; set; }

        public string ProjectName { get; set; }

        public string Path { get; set; }

        public DateTime CreateTime { get; set; }

        public static ShowJournal CreateShowJournal(Journal journal)
        {
            var showJournal = new ShowJournal();
            showJournal.ID = Guid.NewGuid();
            showJournal.Path = journal.Path;
            showJournal.ProjectName = journal.ProjectName;
            showJournal.CreateTime = journal.CreateTime;
            return showJournal;
        }
    }
}