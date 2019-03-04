using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMSEntity;

namespace HMSRepo
{
    public interface IHistoryRepository:IRepository<History>
    {
        History Get(string RoomNo);
        History Get(History history);
        int InsertHistory(History history);
        int UpdateHistory(History history);
        int Delete(string RoomNo);
    }
}
