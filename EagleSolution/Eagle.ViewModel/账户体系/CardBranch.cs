using System;
using System.Collections.Generic;
using Eagle.Model;

namespace Eagle.ViewModel
{
    public class CardBranch
    {

        public Guid ID { get; set; }

        public string Title { get; set; }

        public string Logo { get; set; }

        public string AreaName { get; set; }

        public string ActionName { get; set; }

        public List<BranchCard> BranchCards { get; set; }

        public static CardBranch CreateShowBranch(Branch branch, IEnumerable<SystemCard> systemCards)
        {
            var cardBranch = new CardBranch();
            cardBranch.ID = branch.ID;
            cardBranch.Title = branch.Title;
            cardBranch.Logo = branch.Logo;
            cardBranch.BranchCards = new List<BranchCard>();
            cardBranch.ActionName = branch.ActionName;
            cardBranch.AreaName = branch.AreaName;
            foreach (var systemCard in systemCards)
            {
                cardBranch.BranchCards.Add(BranchCard.CreateBranchCard(systemCard));
            }
            return cardBranch;
        }
    }
}