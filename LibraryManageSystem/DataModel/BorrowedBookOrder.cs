using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManageSystem.DataModel
{
    public class BorrowedBookOrder
    {
        public int Id;

        /// <summary>
        /// 数据库中的ID
        /// </summary>
        public int BookId;

        /// <summary>
        /// 数据库中的ID
        /// </summary>
        public int StudentId;

        public DateTime BorrowedTime;

        public DateTime DeadlineTime;
    }
}
