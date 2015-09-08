using System.Collections.Generic;
using Eagle.ViewModel;

namespace Eagle.Server
{
    public interface ICardServices : IAppServices
    {
        List<ShowCard> Get(int pageNum);
        UpdateCard GetCard(int cardId);
        void Update(UpdateCard updateCard);
        void Delete(List<int> cardIdList);
    }
}