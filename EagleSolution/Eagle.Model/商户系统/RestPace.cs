using System;

namespace Eagle.Model
{
    public class RestPace
    {
        public Guid ID { get; set; }

        public string SqlCommand { get; set; }

        public DateTime SendTime { get; set; }

        public string ReceiptText { get; set; }

        public DateTime ReceiveTime { get; set; }

        public Guid RestaurantId { get; set; }
    }
}