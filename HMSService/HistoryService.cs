using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMSEntity;

namespace HMSService
{
    public class HistoryService:Service<History>,IHistoryService
    {
        private DataContext context;
        public HistoryService()
        {
            this.context = new DataContext();
        }
        public History Get(string RoomNo)
        {
            return this.context.Histories.SingleOrDefault(e => e.RoomNo == RoomNo);
        }
        public History Get(History history)
        {
            return this.context.Histories.SingleOrDefault(e => e.RoomNo == history.RoomNo);
        }
        public int InsertHistory(History history)
        {
            this.context.Histories.Add(history);
            return this.context.SaveChanges();
        }
        public int UpdateHistory(History history)
        {
            History HistoryToUp = this.context.Histories.SingleOrDefault(e => e.RoomNo == history.RoomNo);
            HistoryToUp.RoomNo = history.RoomNo;
            HistoryToUp.ClientName = history.ClientName;
            HistoryToUp.ClientEmail = history.ClientEmail;
            HistoryToUp.ClientNumber = history.ClientNumber;
            HistoryToUp.ClientNidPicture = history.ClientNidPicture;
            HistoryToUp.BookedDate = history.BookedDate;
            HistoryToUp.CheckInDate = history.CheckInDate;
            HistoryToUp.CheckOutDate = history.CheckOutDate;
            HistoryToUp.RoomType = history.RoomType;
            HistoryToUp.Person = history.Person;


            return this.context.SaveChanges();
        }
        public int Delete(string RoomNo)
        {
            History historyToDelete = this.context.Histories.SingleOrDefault(e => e.RoomNo == RoomNo);
            this.context.Histories.Remove(historyToDelete);

            return this.context.SaveChanges();
        }
    }
}
