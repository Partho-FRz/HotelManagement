using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSEntity
{
    public class History:Entity
    {
        public string RoomNo { get; set; }
        public string ClientName  { get; set; }
        public string ClientEmail { get; set; }
        public string ClientNumber { get; set; }
        public string BookedDate { get; set; }
        public byte[] ClientNidPicture { get; set; }
        public string CheckInDate { get; set; }
        public string CheckOutDate { get; set; }
        public string Person { get; set; }
        public string RoomType { get; set; }
    }
}
