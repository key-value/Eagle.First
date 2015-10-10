using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Eagle.Domain.EF;
using Eagle.Domain.EF.DataContext;
using Eagle.Infrastructrue.Aop.Attribute;
using Eagle.Infrastructrue.Utility;
using Eagle.ViewModel;

namespace Eagle.Server.Services
{
    [Injection(typeof(IRestPaceServices))]
    public class RestPaceServices : ApplicationServices, IRestPaceServices
    {
        public List<ShowRestPace> Get(int pageNum)
        {
            List<ShowRestPace> showRestPace;
            using (var restContext = new DefaultContext())
            {
                var restPaces = from restPace in restContext.RestPaces
                                let rest = restContext.MonitorRestaurants.FirstOrDefault(x => x.ID == restPace.RestaurantId)
                                orderby rest.Name
                                select new
                                {
                                    restPace,
                                    rest
                                };
                showRestPace =
                    restPaces.AsNoTracking().Pageing(pageNum, PageSize, ref _pageCount).ToList().Select(x => new ShowRestPace()
                    {
                        ID = x.restPace.ID,
                        RestaurantName = x.rest.Name,
                        SqlCommand = x.restPace.SqlCommand,
                        SendTime = x.restPace.SendTime,
                        ReceiptText = x.restPace.ReceiptText,
                        ReceiveTime = x.restPace.ReceiveTime,
                    }).ToList();
            }

            return showRestPace;
        }
    }
}