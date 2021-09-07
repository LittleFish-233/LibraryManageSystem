using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManageSystem.DataModel;
using LibraryManageSystem.StorageService;
using LibraryManageSystem.ViewModel;

namespace LibraryManageSystem.Controller
{
    public class LibraryService
    {
        StudentStorageService _studentStorageService = new StudentStorageService();
        BookStorageService _bookStorageService = new BookStorageService();
        BorrowedSorageService _borrowedSorageService = new BorrowedSorageService();

        public void Load()
        {
            _studentStorageService.Load();
            _bookStorageService.Load();
            _borrowedSorageService.Load();
        }
        public void AddStudent(Student student)
        {
            _studentStorageService.Add(student);
        }

        public void DeleteStudent(Student student)
        {
            _studentStorageService.Delete(student);
        }

        public List<Student> GetStudents()
        {
            return _studentStorageService.GetAllStudents();
        }

        public void AddBook(Book book)
        {
            _bookStorageService.Add(book);
        }

        public void DeleteBook(Book book)
        {
            _bookStorageService.Delete(book);
        }

        public List<Book> GetBooks()
        {
            return _bookStorageService.GetAllBooks();
        }

        public void BorrowBook(Student student,Book book)
        {
            //一个学生最多借多少书
            int maxNum = 5;
            //最多能借多少天
            int deadlineDays = 30;

            //检查学生借书是否超过上限
            List<BorrowedBookOrder> orders= _borrowedSorageService.GetAllOrders();
            int num = 0;
            for(int i=0;i<orders.Count;i++)
            {
                if(orders[i].StudentId == student.Id)
                {
                    num++;
                }
            }
            if(num>maxNum)
            {
                //学生借书超过上限
                throw new Exception("学生借书超过上限");
            }

            //添加借书订单
            BorrowedBookOrder order = new BorrowedBookOrder();
            order.StudentId = student.Id;
            order.BookId = book.Id;
            order.BorrowedTime = DateTime.Now;
            DateTime date = DateTime.Now;
            order.DeadlineTime = date.AddDays(deadlineDays);

            //把借书订单添加到列表中
            _borrowedSorageService.Add(order);
        }

        public void ReturnBook(Student student, Book book)
        {
            //检查是否存在
            List<BorrowedBookOrder> orders = _borrowedSorageService.GetAllOrders();
            for (int i = 0; i < orders.Count; i++)
            {
                if (orders[i].StudentId == student.Id&& orders[i].BookId == book.Id)
                {
                    _borrowedSorageService.Delete(orders[i]);
                    return;
                }
            }

            throw new Exception("未找到匹配的借书订单，学生ID："+student.Id+"图书Id："+book.Id);
        }


        /// <summary>
        /// 获取学生的借书订单
        /// </summary>
        /// <returns></returns>
        public List<BookBorrowedRecordModel> GetStudentBorrowedRecords(Student student)
        {
            return CombinationOrder(_borrowedSorageService.GetAllOrders().Where(s => s.StudentId == student.Id).ToList());
        }
        /// <summary>
        /// 获取所有借书订单
        /// </summary>
        /// <returns></returns>
        public List<BookBorrowedRecordModel> GetAllBorrowedRecords()
        {
            return CombinationOrder(_borrowedSorageService.GetAllOrders());
        }
        /// <summary>
        /// 获取超过期限的借书订单
        /// </summary>
        /// <returns></returns>
        public List<BookBorrowedRecordModel> GetOverTimeRecords()
        {
            return CombinationOrder(_borrowedSorageService.GetAllOrders().Where(s => s.DeadlineTime < DateTime.Now).ToList());
        }
        /// <summary>
        /// 使用借书订单列表 组合 新的借书记录视图
        /// </summary>
        /// <param name="orders">借书订单</param>
        /// <returns></returns>
        public List<BookBorrowedRecordModel> CombinationOrder(List<BorrowedBookOrder> orders)
        {
            //创建返回的结果列表
            List<BookBorrowedRecordModel> recordModels = new List<BookBorrowedRecordModel>();

            //遍历借书订单
            for (int i = 0; i < orders.Count; i++)
            {
                //根据学生Id拿到学生
                Student student = _studentStorageService.GetAllStudents().Find(s => s.Id == orders[i].StudentId);

                Book book = _bookStorageService.GetAllBooks().Find(s => s.Id == orders[i].BookId);

                //拿到原始数据之后 组合数据
                BookBorrowedRecordModel model = new BookBorrowedRecordModel
                {
                    OrderId=orders[i].Id,
                    BookISBN = book.ISBN,
                    BookName = book.Name,
                    StudentClass = student.ClassName,
                    StudentId = student.StudentId,
                    StudentName = student.Name,
                    DeadlineTime = orders[i].DeadlineTime,
                    BorrowedTime = orders[i].BorrowedTime
                };
                //把数据添加到列表中
                recordModels.Add(model);
            }

            return recordModels;
        }

      
    }
}
