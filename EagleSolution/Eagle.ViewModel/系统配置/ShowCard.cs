using System;
using System.Collections.Generic;
using System.Linq;
using Eagle.Infrastructrue.Utility;
using Eagle.Model;

namespace Eagle.ViewModel
{
    public class ShowCard
    {
        public int ID { get; set; }

        public string CardName { get; set; }

        public string Action { get; set; }

        public Guid BranchId { get; set; }

        public string BranchName { get; set; }

        public static ShowCard CreateShowCard(SystemCard systemCard, IEnumerable<Branch> branches)
        {
            var showCard = new ShowCard();
            showCard.ID = systemCard.ID;
            showCard.CardName = systemCard.CardName;
            showCard.Action = systemCard.Action;
            showCard.BranchId = systemCard.BranchId;
            var branch = branches.FirstOrDefault(x => x.ID == showCard.BranchId);
            if (!branch.Null())
            {
                showCard.BranchName = branch.Title;
            }
            return showCard;
        }
    }
}