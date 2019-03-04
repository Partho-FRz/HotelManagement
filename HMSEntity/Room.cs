using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace HMSEntity
{
    public class Room:Entity
    {
        [Required]
        public string RoomNo { get; set; }
        [Required]
        public string RoomType { get; set; }
        [Required]
        public string NoOfBed { get; set; }
        [Required]
        public string BedType { get; set; }
        [Required]
        public string MaxPerson { get; set; }
        [Required]
        public double Price { get; set; }
        public byte[] Picture { get; set; }
        public string Status { get; set; }
        public string BookedDate { get; set; }
        public string CheckInDate { get; set; }
        public string CheckOutDate { get; set; }
        public string ClientName { get; set; }
        public string ClientPhone { get; set; }
        public string ClientEmail { get; set; }
        public byte[] ClientNIDPicture { get; set; }
        public string TotalPerson { get; set; }
        public string Payment { get; set; }
        public string PaymentStatus { get; set; }
    }
}
