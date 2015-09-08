using System;

namespace Eagle.ViewModel
{
    public interface ILoginAccount
    {
        Guid ID { get; set; }

        string LoginID { get; set; }

        string Name { get; set; }
    }
}