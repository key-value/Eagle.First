using System;
using Eagle.Model;

namespace Eagle.ViewModel
{
    public class UpdateCard
    {

        public int ID { get; set; }

        public string CardName { get; set; }

        public string Action { get; set; }

        public Guid BranchId { get; set; }

        public SystemCard CreateSystemCard()
        {
            var systemCard = new SystemCard();
            systemCard.ID = ID;
            systemCard.CardName = CardName;
            systemCard.Action = Action;
            systemCard.BranchId = BranchId;
            return systemCard;
        }

        public static UpdateCard CreateUpdateCard(SystemCard systemCard)
        {
            var updateCard = new UpdateCard();
            updateCard.ID = systemCard.ID;
            updateCard.CardName = systemCard.CardName;
            updateCard.Action = systemCard.Action;
            updateCard.BranchId = systemCard.BranchId;
            return updateCard;
        }

    }
}