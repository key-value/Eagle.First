using System.Collections.Generic;

namespace Eagle.Server
{
    public interface IJournalServices
    {
        List<string> GetJournal();
    }
}