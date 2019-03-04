using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMSEntity;

namespace HMSRepo
{
    public interface IRoomRepository:IRepository<Room>
    {
        Room Get(string RoomNo);
        Room Get(Room room);
        int InsertRoom(Room room);
        int UpdateRoom(Room room);
        int Delete(string RoomNo);
    }
}
