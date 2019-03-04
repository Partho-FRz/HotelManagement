using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMSEntity;

namespace HMSRepo
{
    public class RoomRepository:Repository<Room>,IRoomRepository
    {
        private DataContext context;
        public RoomRepository()
        {
            this.context = new DataContext();
        }
        public Room Get(string RoomNo)
        {
            return this.context.Rooms.SingleOrDefault(e => e.RoomNo == RoomNo);
        }
        public Room Get(Room room)
        {
            return this.context.Rooms.SingleOrDefault(e => e.RoomNo==room.RoomNo);
        }
        public int InsertRoom(Room room)
        {
            this.context.Rooms.Add(room);
            return this.context.SaveChanges();
        }
        public int UpdateRoom(Room room)
        {
            Room roomToUp = this.context.Rooms.SingleOrDefault(e => e.RoomNo == room.RoomNo);
            roomToUp.RoomNo = room.RoomNo;
            roomToUp.NoOfBed = room.NoOfBed;
            roomToUp.Price = room.Price;
            roomToUp.RoomType = room.RoomType;
            roomToUp.Picture = room.Picture;
            roomToUp.Status = room.Status;
            roomToUp.BedType = room.BedType;
            roomToUp.MaxPerson = room.MaxPerson;
            roomToUp.BookedDate = room.BookedDate;
            roomToUp.CheckInDate = room.CheckInDate;
            roomToUp.CheckOutDate = room.CheckOutDate;
            roomToUp.ClientName = room.ClientName;
            roomToUp.ClientPhone = room.ClientPhone;
            roomToUp.ClientEmail = room.ClientEmail;
            roomToUp.ClientNIDPicture = room.ClientNIDPicture;
            roomToUp.TotalPerson = room.TotalPerson;
            roomToUp.Payment = room.Payment;
            roomToUp.PaymentStatus = room.PaymentStatus;
           
            return this.context.SaveChanges();
        }
        public int Delete(string RoomNo)
        {
            Room roomToDelete = this.context.Rooms.SingleOrDefault(e => e.RoomNo == RoomNo);
            this.context.Rooms.Remove(roomToDelete);

            return this.context.SaveChanges();
        }
    }
}
