using LibraryManageSystem.DataModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManageSystem.StorageService
{
    public class BorrowedSorageService
    {
        List<BorrowedBookOrder> Orders = new List<BorrowedBookOrder>();

        public void Save()
        {
            File.WriteAllText("借书订单列表.json", JsonConvert.SerializeObject(Orders));
        }

        public void Load()
        {
            try
            {
                Orders = JsonConvert.DeserializeObject<List<BorrowedBookOrder>>(File.ReadAllText("借书订单列表.json"));
            }
            catch
            {
                Save();
            }
        }
        /// <summary>
        /// 添加借书订单
        /// </summary>
        /// <param name="order">要添加的借书订单</param>
        public void Add(BorrowedBookOrder order)
        {
            int a = 0;
            if (Orders.Count != 0)
            {
                a = Orders.Max(s => s.Id);
            }
            order.Id = a + 1;
            Orders.Add(order);
            Save();
        }

        /// <summary>
        /// 删除借书订单
        /// </summary>
        /// <param name="order">要删除的借书订单的Id</param>
        public void Delete(BorrowedBookOrder order)
        {
            Orders.Remove(order);
            Save();
        }

        /// <summary>
        /// 获取所有借书订单列表
        /// </summary>
        /// <returns></returns>
        public List<BorrowedBookOrder> GetAllOrders()
        {
            return Orders;
        }
        /// <summary>
        /// 修改借书订单
        /// </summary>
        /// <param name="order">要更新的借书订单实例</param>
        /// <returns></returns>
        public void ChangeOrder(BorrowedBookOrder order)
        {
            //查找借书订单并修改
            foreach (BorrowedBookOrder temp in Orders)
            {
                if (temp.Id == order.Id)
                {
                    temp.StudentId = order.StudentId;
                    temp.BookId = order.BookId;
                    temp.DeadlineTime = order.DeadlineTime;
                    temp.BorrowedTime = order.BorrowedTime;
                    Save();
                    return;
                }
            }

            //查找不到要修改的借书订单
            throw new Exception("查到不到ID为" + order.Id + "的图书");
        }
    }
}
