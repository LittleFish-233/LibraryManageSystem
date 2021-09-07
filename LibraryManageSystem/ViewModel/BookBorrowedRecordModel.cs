using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManageSystem.ViewModel
{
    public class BookBorrowedRecordModel
    {
        public int OrderId;

        public string StudentName;

        public string StudentClass;

        public string StudentId;

        public string BookName;

        public string BookISBN;

        public DateTime BorrowedTime;

        public DateTime DeadlineTime;
    }
}
