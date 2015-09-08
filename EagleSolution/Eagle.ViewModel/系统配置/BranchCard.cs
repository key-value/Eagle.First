using System;
using System.Collections.Generic;
using Eagle.Model;

namespace Eagle.ViewModel
{
    public class BranchCard
    {
        public int ID { get; set; }

        public string CardName { get; set; }

        public string Action { get; set; }

        public static BranchCard CreateBranchCard(SystemCard systemCard)
        {
            var branchCard = new BranchCard();
            branchCard.ID = systemCard.ID;
            branchCard.Action = systemCard.Action;
            branchCard.CardName = systemCard.CardName;
            return branchCard;
        }
    }
}