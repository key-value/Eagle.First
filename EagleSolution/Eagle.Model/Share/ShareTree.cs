using System;

namespace Eagle.Model.Share
{
    public class ShareTree
    {
        public Guid ID
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public string Description
        {
            get; set;
        }

        public DateTime CreateTime
        {
            get; set;
        }

        public string IpAddr
        {
            get; set;
        }
    }
}