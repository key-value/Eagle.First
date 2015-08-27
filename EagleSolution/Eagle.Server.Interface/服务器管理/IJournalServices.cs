using System.Collections.Generic;
using Eagle.ViewModel;

namespace Eagle.Server
{
    public interface IJournalServices : IAppServices
    {
        List<ShowJournal> GetJournals(int pageNum);
    }
}