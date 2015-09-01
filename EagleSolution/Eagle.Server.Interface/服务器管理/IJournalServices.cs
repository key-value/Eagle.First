using System;
using System.Collections.Generic;
using Eagle.ViewModel;

namespace Eagle.Server
{
    public interface IJournalServices : IAppServices
    {
        List<ShowJournal> GetJournals(int pageNum);
        UpdateJournal Get(Guid journalId);
        void Update(UpdateJournal updateJournal);
        void Delete(List<Guid> journalList);
        List<string> GetFiles(Guid journalId, string path);
        string GetFile(Guid journalId, string fileName);
    }
}