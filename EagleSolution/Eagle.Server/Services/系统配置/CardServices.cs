using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.ServiceModel.Channels;
using Eagle.Domain.EF;
using Eagle.Domain.EF.DataContext;
using Eagle.Infrastructrue;
using Eagle.Infrastructrue.Aop.Attribute;
using Eagle.Infrastructrue.Utility;
using Eagle.Model;
using Eagle.ViewModel;

namespace Eagle.Server.Services
{
    [Injection(typeof(ICardServices))]
    public class CardServices : ApplicationServices, ICardServices
    {
        public List<ShowCard> Get(int pageNum)
        {
            Flag = true;
            using (var defaultContext = new AccountContext())
            {
                var systemCards = defaultContext.SystemCards.OrderByDescending(x => x.ID).AsNoTracking()
                    .Pageing(pageNum, PageSize, ref _pageCount).ToList();

                var showCards = new List<ShowCard>();
                var branch = defaultContext.Branches.Where(x => x.Level == 1).AsNoTracking().ToList();
                foreach (var systemCard in systemCards)
                {
                    showCards.Add(ShowCard.CreateShowCard(systemCard, branch));
                }
                return showCards;
            }
        }

        public UpdateCard GetCard(int cardId)
        {
            var updateCard = new UpdateCard();
            using (var defaultContext = new DefaultContext())
            {
                var systemCard = defaultContext.SystemCards.FirstOrDefault(x => x.ID == cardId);
                if (systemCard.Null())
                {
                    Message = "未找到要修改的配置";
                    return updateCard;
                }
                updateCard = UpdateCard.CreateUpdateCard(systemCard);
                Flag = true;
                return updateCard;
            }
        }

        private void Edit(UpdateCard updateCard)
        {
            using (var defaultContext = new DefaultContext())
            {
                var systemCard = defaultContext.SystemCards.FirstOrDefault(x => x.ID == updateCard.ID);
                if (systemCard.Null())
                {
                    Message = "请选择要修改的服务器";
                    return;
                }
                systemCard.Action = updateCard.Action;
                systemCard.CardName = updateCard.CardName;
                systemCard.BranchId = updateCard.BranchId;
                defaultContext.ModifiedModel(systemCard);
                defaultContext.SaveChanges();
            }
            Flag = true;
        }

        private void Add(UpdateCard updateCard)
        {
            var systemCard = updateCard.CreateSystemCard();
            using (var defaultContext = new DefaultContext())
            {
                defaultContext.SystemCards.Add(systemCard);
                defaultContext.SaveChanges();
            }
            Flag = true;
        }

        public void Update(UpdateCard updateCard)
        {
            if (updateCard.ID == 0)
            {
                Add(updateCard);
            }
            else
            {
                Edit(updateCard);
            }
        }

        public void Delete(List<int> cardIdList)
        {
            using (var defalutContent = new DefaultContext())
            {
                var systemCards = defalutContent.SystemCards.Where(x => cardIdList.Contains(x.ID));
                if (!systemCards.Any())
                {
                    Message = "请选择要删除的数据";
                    return;
                }
                defalutContent.SystemCards.RemoveRange(systemCards);
                defalutContent.SaveChanges();
                Flag = true;
            }
        }
    }
}