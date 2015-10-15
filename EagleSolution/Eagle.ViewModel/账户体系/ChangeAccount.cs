using System;

namespace Eagle.ViewModel
{
    public class ChangeAccount
    {
        public Guid ID
        {
            get; set;
        }

        public string OldPassword
        {
            get; set;
        }

        public string NewPassword
        {
            get; set;
        }
    }
}