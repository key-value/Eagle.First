using System.Collections.Generic;
using System.Linq;
using Eagle.Domain.EF.DataContext;
using Eagle.Infrastructrue.Aop.Attribute;
using Eagle.Infrastructrue.Utility;
using Eagle.ViewModel;

namespace Eagle.Server.Services
{
    [Injection(typeof(IJournalServices))]
    public class JournalServices : ApplicationServices, IJournalServices
    {
        public List<ShowJournal> GetJournals(int pageNum)
        {
            var showJournals = new List<ShowJournal>();
            using (var content = new DefaultContext())
            {
                var journals = content.Journals.AsNoTracking().OrderByDescending(x => x.CreateTime).Pageing(pageNum, PageSize, ref _pageCount).ToList();

                foreach (var journal in journals)
                {
                    showJournals.Add(ShowJournal.CreateShowJournal(journal));
                }
            }
            return showJournals;
        }
    }
}