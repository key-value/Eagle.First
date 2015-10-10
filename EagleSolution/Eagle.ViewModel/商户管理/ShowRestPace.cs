using System;

namespace Eagle.ViewModel
{
    public class ShowRestPace
    {
        public Guid ID { get; set; }

        public string SqlCommand { get; set; }

        public DateTime SendTime { get; set; }

        public string ReceiptText { get; set; }

        public DateTime ReceiveTime { get; set; }

        public string RestaurantName { get; set; }
    }
}