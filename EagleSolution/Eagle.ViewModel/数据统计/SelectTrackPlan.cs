using System;

namespace Eagle.ViewModel
{
    public interface ISelectTrackPlan
    {
        Guid ID
        {
            get; set;
        }

        string Name
        {
            get; set;
        }
    }
}