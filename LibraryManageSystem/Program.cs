using LibraryManageSystem.Controller;
using LibraryManageSystem.DataModel;
using LibraryManageSystem.StorageService;
using LibraryManageSystem.ToolHelper;
using LibraryManageSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace LibraryManageSystem
{
    class Program
    {
        static string chars = "=";
        static int screenLength = 100;
        static LibraryService _libraryService = new LibraryService();

        static void Main(string[] args)
        {
            _libraryService.Load();
            while (true)
            {
                Console.Clear();
                Console.WriteLine();
                Tools.RepeatOutput(chars, screenLength, true);
                Tools.CenterOutput("图书馆管理系统", screenLength, 2, true);
                Console.WriteLine();
                Tools.CenterOutput("Powered By LittleFish", screenLength, 1, true);
                Tools.RepeatOutput(chars, screenLength, true);
                Console.WriteLine();
                Tools.OutputOption(new string[] { "管理图书", "管理学生", "借阅管理", "退出" }, screenLength, 3, new double[] { 2, 2, 2, 2 }, true);
                int[] input = Tools.InputInt(1, new int[] { 1 }, new int[] { 4 });
                switch (input[0])
                {
                    case 1:
                        ManageBook();
                        break;
                    case 2:
                        ManageStudent();
                        break;
                    case 3:
                        ManageLend();
                        break;
                    case 4:
                        About();
                        return;
                }
            }
        }
        static void About()
        {
            Console.Clear();
            for (int i = 0; i < 8; i++)
            {
                Console.WriteLine();
            }
            Tools.CenterOutput("图书馆管理系统", screenLength, 2, true);
            Console.WriteLine();
            Tools.CenterOutput("Powered By LittleFish", screenLength, 1, true);
            Console.WriteLine();
            Tools.CenterOutput("2021/6/26 10:57", screenLength, 1, true);
            Thread.Sleep(5000);
        }
        static void ManageLend()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine();
                Tools.RepeatOutput(chars, screenLength, true);
                Tools.CenterOutput("图书馆管理系统", screenLength, 2, true);
                Console.WriteLine();
                Tools.CenterOutput("借阅管理", screenLength, 1, true);
                Tools.RepeatOutput(chars, screenLength, true);
                Console.WriteLine();
                Tools.OutputOption(new string[] { "借书", "还书", "查看所有借书记录", "查看逾期未还名单", "返回" }, screenLength, 2, new double[] { 2, 2, 2, 2, 2 }, true);
                int[] input = Tools.InputInt(1, new int[] { 1 }, new int[] { 5 });
                switch (input[0])
                {
                    case 1:
                        LendBook();
                        break;
                    case 2:
                        ReturnBook();
                        break;
                    case 3:
                        LookAllLendBooks();
                        break;
                    case 4:
                        LookAllOverdueLendBooks();
                        break;
                    case 5:
                        return;
                }
            }
        }
      
        static void LookAllLendBooks()
        {
            Console.Clear();

            Console.WriteLine();
            Tools.RepeatOutput(chars, screenLength, true);
            Tools.CenterOutput("图书馆管理系统", screenLength, 2, true);
            Console.WriteLine();
            Tools.CenterOutput("查看所有借书记录", screenLength, 2, true);
            Tools.RepeatOutput(chars, screenLength, true);
            Console.WriteLine();
            LookLendBook_table(_libraryService.GetAllBorrowedRecords());
            Console.WriteLine();
            Console.WriteLine("按下回车返回");
            Console.ReadLine();
        }
        static void LookAllOverdueLendBooks()
        {
            Console.Clear();

            Console.WriteLine();
            Tools.RepeatOutput(chars, screenLength, true);
            Tools.CenterOutput("图书馆管理系统", screenLength, 2, true);
            Console.WriteLine();
            Tools.CenterOutput("查看逾期未还名单", screenLength, 2, true);
            Tools.RepeatOutput(chars, screenLength, true);
            Console.WriteLine();
            LookLendBook_table(_libraryService.GetOverTimeRecords());
            Console.WriteLine();
            Console.WriteLine("按下回车返回");
            Console.ReadLine();
        }
        static void ReturnBook()
        {
            Console.Clear();

            Console.WriteLine();
            Tools.RepeatOutput(chars, screenLength, true);
            Tools.CenterOutput("图书馆管理系统", screenLength, 2, true);
            Console.WriteLine();
            Tools.CenterOutput("还书", screenLength, 2, true);
            Tools.RepeatOutput(chars, screenLength, true);
            while (true)
            {
                Console.Write("请输入还书学生的学号：");
                List<Student> students = new List<Student>();
                while (students.Count != 1)
                {
                    string temp = Console.ReadLine();
                    if (temp == "")
                    {
                        return;
                    }
                    Student student = _libraryService.GetStudents().Find(s => s.Id.ToString() == temp);
                    if (student != null)
                    {
                        students.Clear();
                        students.Add(student);
                        break;
                    }
                    students = _libraryService.GetStudents().Where(s => s.Name.Contains(temp) || s.ClassName.Contains(temp) || s.StudentId.Contains(temp)).ToList();
                    if (students.Count == 0)
                    {
                        Tools.RepeatOutput(chars, screenLength, true);
                        Console.Write("学生不存在，请重新输入：");
                    }
                    if (students.Count > 1)
                    {
                        Tools.RepeatOutput(chars, screenLength, true);
                        Console.WriteLine();
                        LookStudent_table(students);
                        Console.Write("无法确定目标学生，请重新输入：");
                    }
                }
                Console.WriteLine("选中学生：" + students[0].Name);

                Console.WriteLine();
                LookLendBook_table(_libraryService.GetStudentBorrowedRecords(students[0]));

                Console.WriteLine();
                Console.Write("请输入所借书籍的名称、ISBN、价格.....");

                List<Book> books = new List<Book>();
                while (books.Count != 1)
                {
                    string temp = Console.ReadLine();
                    if (temp == "")
                    {
                        return;
                    }
                    Book book = _libraryService.GetBooks().Find(s => s.Id.ToString() == temp);
                    if (book != null)
                    {
                        books.Clear();
                        books.Add(book);
                        break;
                    }
                    books = _libraryService.GetBooks().Where(s => s.Name.Contains(temp) || s.ISBN.Contains(temp) || s.Price.ToString().Contains(temp)).ToList();

                    if (books.Count == 0)
                    {
                        Tools.RepeatOutput(chars, screenLength, true);

                        Console.Write("书籍不存在，请重新输入：");
                    }
                    if (books.Count > 1)
                    {
                        Tools.RepeatOutput(chars, screenLength, true);

                        Console.WriteLine();
                        LookBook_table(books);
                        Console.Write("无法确定目标书籍，请重新输入：");
                    }
                }

                Console.WriteLine("选中图书《" + books[0].Name + "》");
                try
                {
                    _libraryService.ReturnBook(students[0],books[0]);
                    Console.WriteLine();
                    Tools.RepeatOutput(chars, screenLength, true);
                    Console.WriteLine("成功为" + students[0].Name + "归还《" + books[0].Name+"》");
                    Tools.RepeatOutput(chars, screenLength, true);
                }
                catch (Exception exc)
                {
                    Tools.RepeatOutput(chars, screenLength, true);
                    Console.WriteLine("为" + students[0].Name + "归还《" + books[0].Name + "》图书失败");
                    Console.WriteLine("详细信息：" + exc.Message);
                    Tools.RepeatOutput(chars, screenLength, true);
                }
            }
        }
        static void LendBook()
        {
            Console.Clear();

            Console.WriteLine();
            Tools.RepeatOutput(chars, screenLength, true);
            Tools.CenterOutput("图书馆管理系统", screenLength, 2, true);
            Console.WriteLine();
            Tools.CenterOutput("借书", screenLength, 2, true);
            Tools.RepeatOutput(chars, screenLength, true);
            while (true)
            {
                Console.WriteLine();
                Console.Write("请输入所借书籍的名称、ISBN、价格.....");

                List<Book> books = new List<Book>();
                while (books.Count != 1)
                {
                    string temp = Console.ReadLine();
                    if (temp == "")
                    {
                        return;
                    }
                    Book book = _libraryService.GetBooks().Find(s => s.Id.ToString() == temp);
                    if(book!=null)
                    {
                        books.Clear();
                        books.Add(book);
                        break;
                    }
                    books = _libraryService.GetBooks().Where(s => s.Name.Contains(temp) || s.ISBN.Contains(temp) || s.Price.ToString().Contains(temp)).ToList();
                    
                    if (books.Count == 0)
                    {
                        Tools.RepeatOutput(chars, screenLength, true);

                        Console.Write("书籍不存在，请重新输入：");
                    }
                    if (books.Count > 1)
                    {
                        Tools.RepeatOutput(chars, screenLength, true);

                        Console.WriteLine();
                        LookBook_table(books);
                        Console.Write("无法确定目标书籍，请重新输入：");
                    }
                }
                Console.WriteLine("选中图书《" + books[0].Name + "》");
                Console.Write("请输入借书学生的学号：");
                List<Student> students = new List<Student>();
                while (students.Count != 1)
                {
                    string temp = Console.ReadLine();
                    if (temp == "")
                    {
                        return;
                    }
                    Student student = _libraryService.GetStudents().Find(s => s.Id.ToString() == temp);
                    if (student != null)
                    {
                        students.Clear();
                        students.Add(student);
                        break;
                    }
                    students = _libraryService.GetStudents().Where(s => s.Name.Contains(temp) || s.ClassName.Contains(temp) || s.StudentId.Contains(temp)).ToList();
                    if (students.Count == 0)
                    {
                        Tools.RepeatOutput(chars, screenLength, true);
                        Console.Write("学生不存在，请重新输入：");
                    }
                    if (students.Count > 1)
                    {
                        Tools.RepeatOutput(chars, screenLength, true);
                        Console.WriteLine();
                        LookStudent_table(students);
                        Console.Write("无法确定目标学生，请重新输入：");
                    }
                }
                Console.WriteLine("选中学生：" + students[0].Name );
                try
                {
                    _libraryService.BorrowBook(students[0], books[0]);
                    Console.WriteLine();
                    Tools.RepeatOutput(chars, screenLength, true);
                    Console.WriteLine("成功为" + students[0].Name + "借阅《" + books[0].Name+"》");
                    Tools.RepeatOutput(chars, screenLength, true);
                }
                catch (Exception exc)
                {
                    Tools.RepeatOutput(chars, screenLength, true);
                    Console.WriteLine("为" + students[0].Name + "借阅《" + books[0].Name + "》 图书失败");
                    Console.WriteLine("详细信息：" + exc.Message);
                    Tools.RepeatOutput(chars, screenLength, true);
                }
            }
        }
        static void ManageStudent()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine();
                Tools.RepeatOutput(chars, screenLength, true);
                Tools.CenterOutput("图书馆管理系统", screenLength, 2, true);
                Console.WriteLine();
                Tools.CenterOutput("管理学生", screenLength, 2, true);
                Tools.RepeatOutput(chars, screenLength, true);
                Console.WriteLine();
                Tools.OutputOption(new string[] { "查看学生列表", "删除学生", "添加学生", "查找学生", "返回" }, screenLength, 3, new double[] { 2, 2, 2, 2, 2 }, true);
                int[] input = Tools.InputInt(1, new int[] { 1 }, new int[] { 5 });
                switch (input[0])
                {
                    case 1:
                        LookStudent();
                        break;
                    case 2:
                        DeleteStudent();
                        break;
                    case 3:
                        AddStudent();
                        break;
                    case 4:
                        SearchStudent();
                        break;
                    case 5:
                        return;
                }
            }

        }
        static void SearchStudent()
        {
            Console.Clear();

            Console.WriteLine();
            Tools.RepeatOutput(chars, screenLength, true);
            Tools.CenterOutput("图书馆管理系统", screenLength, 2, true);
            Console.WriteLine();
            Tools.CenterOutput("查找学生", screenLength, 2, true);
            Tools.RepeatOutput(chars, screenLength, true);
            Console.WriteLine();
            while (true)
            {
                Console.WriteLine("请输入你想要查找的学生的学号（可以输入不完全）");
                Console.WriteLine("直接按下回车返回");
                string temp = Console.ReadLine();
                if (temp == "")
                {
                    return;
                }
                Tools.RepeatOutput(chars, screenLength, true);
                LookStudent_table(_libraryService.GetStudents().Where(s => s.Name.Contains(temp) || s.ClassName.Contains(temp) || s.StudentId.Contains(temp)).ToList());

                Tools.RepeatOutput(chars, screenLength, true);
                Console.WriteLine();
            }
        }
        static void DeleteStudent()
        {
            Console.Clear();

            Console.WriteLine();
            Tools.RepeatOutput(chars, screenLength, true);
            Tools.CenterOutput("图书馆管理系统", screenLength, 2, true);
            Console.WriteLine();
            Tools.CenterOutput("删除学生", screenLength, 2, true);
            Tools.RepeatOutput(chars, screenLength, true);
            Console.WriteLine();
            LookStudent_table(_libraryService.GetStudents());
            Console.WriteLine();
            Console.WriteLine("请输入你想要删除的学生序号（用空格分开多个序号或用-连接前后序号，可以批量删除）");
            Console.WriteLine("例如：4 5 8-11 12-14");
            Console.WriteLine("将会删除第4、5、8、9、10、11、12、13、14本书");
            string result = Console.ReadLine();
            if (result == "")
            {
                return;
            }
            bool if_success = true;
            List<int> result_ = Tools.InputIntBatch(result);//1-4 5 7     1 2 3 4 5 7
            for (int i = 0; i < result_.Count; i++)
            {
                try
                {
                    Student student = _libraryService.GetStudents().Find(s => s.Id == result_[i]);
                    if (student == null)
                    {
                        if_success = false;
                        Console.WriteLine("无法找到Id为：" + result_[i] + " 的学生");
                        continue;
                    }
                    _libraryService.DeleteStudent(student);
                }
                catch
                {
                    if_success = false;
                    Console.WriteLine("无法找到Id为：" + result_[i] + " 的学生");
                }

            }

            Tools.RepeatOutput(chars, screenLength, true);
            if (if_success == false)
            {
                Console.WriteLine("在删除过程中发生一个或多个错误，有可能部分学生删除成功，请以学生列表显示结果为准");
            }
            else
            {
                Console.WriteLine("成功删除所选学生");
            }
            Tools.RepeatOutput(chars, screenLength, true);
            Console.WriteLine();
            Console.WriteLine("按下回车返回");
            Console.ReadLine();

        }
        static void AddStudent()
        {
            Console.Clear();

            Console.WriteLine();
            Tools.RepeatOutput(chars, screenLength, true);
            Tools.CenterOutput("图书馆管理系统", screenLength, 2, true);
            Console.WriteLine();
            Tools.CenterOutput("添加学生 直接按下回车返回上级菜单", screenLength, 2, true);
            Tools.RepeatOutput(chars, screenLength, true);
            Console.WriteLine();
            while (true)
            {
                Console.WriteLine("正在添加第" + (_libraryService.GetStudents().Count + 1) + "位学生：");
                Console.Write("学生的学号：");
                string studentId = Console.ReadLine();
                if (studentId == "")
                {
                    return;
                }
                Console.Write("学生的姓名：");
                string studentName = Console.ReadLine();
                if (studentName == "")
                {
                    return;
                }
                Console.Write("学生的班级：");
                string studentClass = Console.ReadLine();
                if (studentClass == "")
                {
                    return;
                }
                Student student = new Student
                {
                    StudentId = studentId,
                    ClassName = studentClass,
                    Name = studentName
                };

                _libraryService.AddStudent(student);

                Console.WriteLine("成功添加第" + _libraryService.GetStudents().Count + "位学生");
                Tools.RepeatOutput(chars, screenLength, true);
                Console.WriteLine();
            }
        }
        static void LookStudent_table(List<Student> students)
        {
            string[,] chart = new string[students.Count + 1, 4];
            double[,] parameters = new double[students.Count + 1, 4];
            parameters[0, 0] = 1;
            parameters[0, 1] = 2;
            parameters[0, 2] = 2;
            parameters[0, 3] = 2;
            chart[0, 0] = "Id";
            chart[0, 1] = "姓名";
            chart[0, 2] = "学号";
            chart[0, 3] = "班级";
            for (int i = 0; i < students.Count; i++)
            {
                chart[i + 1, 0] = students[i].Id.ToString();
                chart[i + 1, 1] = students[i].Name;
                chart[i + 1, 2] = students[i].StudentId;
                chart[i + 1, 3] = students[i].ClassName;
                parameters[i + 1, 0] = 1;
                parameters[i + 1, 1] = 2;
                parameters[i + 1, 2] = 1;
                parameters[i + 1, 3] = 2;
            }

            Tools.OutputTable(chart, students.Count + 1, 4, parameters, true);
        }
        static void LookStudent()
        {
            Console.Clear();

            Console.WriteLine();
            Tools.RepeatOutput(chars, screenLength, true);
            Tools.CenterOutput("图书馆管理系统", screenLength, 2, true);
            Console.WriteLine();
            Tools.CenterOutput("查看学生列表", screenLength, 2, true);
            Tools.RepeatOutput(chars, screenLength, true);
            Console.WriteLine();

            LookStudent_table(_libraryService.GetStudents());

            Console.WriteLine();
            Tools.RepeatOutput(chars, screenLength, true);
            Console.WriteLine("输入要查看详细信息的学生Id，或按下回车返回");
            while (true)
            {
                string result = Console.ReadLine();
                if (result == "")
                {
                    return;
                }
                int index = 0;
                try
                {
                    index = int.Parse(result);
                    Student student = _libraryService.GetStudents().Find(s => s.Id == index);
                    if (student == null)
                    {
                        Console.WriteLine("无法找到Id为：" + index + " 的学生");
                    }
                    StudentDetail(student);
                    break;
                }
                catch
                {
                    Console.WriteLine("请输入数字");
                }
            }
            
        }
        static void StudentDetail(Student student)
        {
            Console.Clear();

            Console.WriteLine();
            Tools.RepeatOutput(chars, screenLength, true);
            Tools.CenterOutput("图书馆管理系统", screenLength, 2, true);
            Console.WriteLine();
            Tools.CenterOutput("查看学生详细信息", screenLength, 2, true);
            Tools.RepeatOutput(chars, screenLength, true);
            Console.WriteLine();
            Console.WriteLine("学号：" + student.StudentId);
            Console.WriteLine("姓名：" + student.Name);
            Console.WriteLine("班级：" + student.ClassName);
            Console.WriteLine();

            Console.WriteLine("借书清单：");
            Console.WriteLine();

            LookLendBook_table(_libraryService.GetStudentBorrowedRecords(student));

            Console.WriteLine();
            Console.WriteLine("按下回车返回");
            Console.ReadLine();
        }
        static void LookLendBook_table(List<BookBorrowedRecordModel> bookBorroweds)
        {
            string[,] chart = new string[bookBorroweds.Count + 1, 8];
            double[,] parameters = new double[bookBorroweds.Count + 1, 8];
            parameters[0, 0] = 1;
            parameters[0, 1] = 2;
            parameters[0, 2] = 2;
            parameters[0, 3] = 2;
            parameters[0, 4] = 2;
            parameters[0, 5] = 1;
            parameters[0, 6] = 2;
            parameters[0, 7] = 2;
            chart[0, 0] = "Id";
            chart[0, 1] = "学生姓名";
            chart[0, 2] = "学号";
            chart[0, 3] = "班级";
            chart[0, 4] = "书籍名称";
            chart[0, 5] = "ISBN";
            chart[0, 6] = "借书日期";
            chart[0, 7] = "最迟还书日期";
            for (int i = 0; i < bookBorroweds.Count; i++)
            {
                chart[i + 1, 0] = bookBorroweds[i].OrderId.ToString();
                chart[i + 1, 1] = bookBorroweds[i].StudentName;
                chart[i + 1, 2] = bookBorroweds[i].StudentId;
                chart[i + 1, 3] = bookBorroweds[i].StudentClass;
                chart[i + 1, 4] = bookBorroweds[i].BookName;
                chart[i + 1, 5] = bookBorroweds[i].BookISBN;
                chart[i + 1, 6] = bookBorroweds[i].BorrowedTime.ToString("g");
                chart[i + 1, 7] = bookBorroweds[i].DeadlineTime.ToString("g");
                parameters[i + 1, 0] = 1;
                parameters[i + 1, 1] = 2;
                parameters[i + 1, 2] = 1;
                parameters[i + 1, 3] = 2;
                parameters[i + 1, 4] = 1;
                parameters[i + 1, 5] = 1;
                parameters[i + 1, 6] = 1;
                parameters[i + 1, 7] = 1;
            }

            Tools.OutputTable(chart, bookBorroweds.Count + 1, 8, parameters, true);
        }
        static void ManageBook()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine();
                Tools.RepeatOutput(chars, screenLength, true);
                Tools.CenterOutput("图书馆管理系统", screenLength, 2, true);
                Console.WriteLine();
                Tools.CenterOutput("管理图书", screenLength, 2, true);
                Tools.RepeatOutput(chars, screenLength, true);
                Console.WriteLine();
                Tools.OutputOption(new string[] { "查看书籍列表", "删除图书", "添加图书", "查找图书", "返回" }, screenLength, 3, new double[] { 2, 2, 2, 2, 2 }, true);
                int[] input = Tools.InputInt(1, new int[] { 1 }, new int[] { 5 });
                switch (input[0])
                {
                    case 1:
                        LookBooks();
                        break;
                    case 2:
                        DeleteBooks();
                        break;
                    case 3:
                        AddBook();
                        break;
                    case 4:
                        SearchBook();
                        break;
                    case 5:
                        return;
                }
            }

        }
        static void SearchBook()
        {
            Console.Clear();

            Console.WriteLine();
            Tools.RepeatOutput(chars, screenLength, true);
            Tools.CenterOutput("图书馆管理系统", screenLength, 2, true);
            Console.WriteLine();
            Tools.CenterOutput("查找图书", screenLength, 2, true);
            Tools.RepeatOutput(chars, screenLength, true);
            Console.WriteLine();
            while (true)
            {
                Console.WriteLine("请输入你想要查找的图书名称、ISBN、价格......");
                Console.WriteLine("直接按下回车返回");
                string temp = Console.ReadLine();
                if (temp == "")
                {
                    return;
                }
                Tools.RepeatOutput(chars, screenLength, true);//"1234".contains("567")==false
                LookBook_table(_libraryService.GetBooks().Where(s => s.Name.Contains(temp) || s.ISBN.Contains(temp) || s.Price.ToString().Contains(temp)).ToList());
                Tools.RepeatOutput(chars, screenLength, true);
                Console.WriteLine();
            }
        }
        static void DeleteBooks()
        {
            Console.Clear();

            Console.WriteLine();
            Tools.RepeatOutput(chars, screenLength, true);
            Tools.CenterOutput("图书馆管理系统", screenLength, 2, true);
            Console.WriteLine();
            Tools.CenterOutput("删除图书", screenLength, 2, true);
            Tools.RepeatOutput(chars, screenLength, true);
            Console.WriteLine();
            LookBook_table(_libraryService.GetBooks());
            Console.WriteLine();
            Console.WriteLine("请输入你想要删除的图书Id（用空格分开多个序号或用-连接前后序号，可以批量删除）");
            Console.WriteLine("例如：4 5 8-11 12-14");
            Console.WriteLine("将会删除第4、5、8、9、10、11、12、13、14本书");
            string result = Console.ReadLine();
            if (result == "")
            {
                return;
            }

            bool if_success = true;
            List<int> result_ = Tools.InputIntBatch(result);//1-4 5 7     1 2 3 4 5 7
            for(int i=0;i<result_.Count;i++)
            {
                try
                {
                    Book book = _libraryService.GetBooks().Find(s => s.Id == result_[i]);
                    if (book == null)
                    {
                        if_success = false;
                        Console.WriteLine("无法找到Id为：" + result_[i] + " 的图书");
                        continue;
                    }
                    _libraryService.DeleteBook(book);
                }
                catch
                {
                    if_success = false;
                    Console.WriteLine("无法找到Id为：" + result_[i] + " 的图书");
                }
                
            }

            Tools.RepeatOutput(chars, screenLength, true);
            if (if_success == false)
            {
                Console.WriteLine("在删除过程中发生一个或多个错误，有可能部分图书删除成功，请以图书列表显示结果为准");
            }
            else
            {
                Console.WriteLine("成功删除所选图书");
            }
            Tools.RepeatOutput(chars, screenLength, true);
            Console.WriteLine();
            Console.WriteLine("按下回车返回");
            Console.ReadLine();

        }
        static void LookBook_table(List<Book> books)
        {
            //行列式
            string[,] chart = new string[books.Count + 1, 4];
            double[,] parameters = new double[books.Count + 1, 4];
            parameters[0, 0] = 1;
            parameters[0, 1] = 2;
            parameters[0, 2] = 2;
            parameters[0, 3] = 1;
            chart[0, 0] = "Id";
            chart[0, 1] = "名称";
            chart[0, 2] = "价格";
            chart[0, 3] = "ISBN";
            for (int i = 0; i < books.Count; i++)
            {
                chart[i + 1, 0] = books[i].Id.ToString();
                chart[i + 1, 1] = books[i].Name;
                chart[i + 1, 2] = books[i].Price.ToString();
                chart[i + 1, 3] = books[i].ISBN.ToString();
                parameters[i + 1, 0] = 1;
                parameters[i + 1, 1] = 1;
                parameters[i + 1, 2] = 1;
                parameters[i + 1, 3] = 1;
            }

            Tools.OutputTable(chart, books.Count + 1, 4, parameters, true);
        }
        static void LookBooks()
        {
            Console.Clear();

            Console.WriteLine();
            Tools.RepeatOutput(chars, screenLength, true);
            Tools.CenterOutput("图书馆管理系统", screenLength, 2, true);
            Console.WriteLine();
            Tools.CenterOutput("查看书籍列表", screenLength, 2, true);
            Tools.RepeatOutput(chars, screenLength, true);
            Console.WriteLine();

            LookBook_table(_libraryService.GetBooks());

            Console.WriteLine();
            Tools.RepeatOutput(chars, screenLength, true);
            Console.WriteLine("按下回车返回");
            Console.ReadLine();
        }
        static void AddBook()
        {
            Console.Clear();

            Console.WriteLine();
            Tools.RepeatOutput(chars, screenLength, true);
            Tools.CenterOutput("图书馆管理系统", screenLength, 2, true);
            Console.WriteLine();
            Tools.CenterOutput("添加图书 直接按下回车返回上级菜单", screenLength, 2, true);
            Tools.RepeatOutput(chars, screenLength, true);
            Console.WriteLine();
            while (true)
            {
                Console.WriteLine("正在添加第" + (_libraryService.GetBooks().Count + 1) + "本书：");
                Console.Write("书籍的ISBN码：");
                string isbn = Console.ReadLine();
                if (isbn == "")
                {
                    return;
                }
                Console.Write("书籍的名称：");
                string name = Console.ReadLine();
                if (name == "")
                {
                    return;
                }
                double price_ = 0;
                Console.Write("书籍的价格：");
                while (true)
                {
                    string price = Console.ReadLine();
                    if (price == "")
                    {
                        return;
                    }
                    try
                    {
                        price_ = double.Parse(price);
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("价格必须是数字");
                    }
                }
                
                Book book = new Book
                {
                    ISBN = isbn,
                    Name = name,
                    Price =price_
                };
                _libraryService.AddBook(book);
                Console.WriteLine("成功添加第" + _libraryService.GetBooks().Count + "本书");
                Tools.RepeatOutput(chars, screenLength, true);
                Console.WriteLine();
            }
        }
    }

    
   
}
