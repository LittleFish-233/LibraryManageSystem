using LibraryManageSystem.DataModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LibraryManageSystem.StorageService
{
    public class BookStorageService
    {
        List<Book> Books = new List<Book>();

        public void Save()
        {
            File.WriteAllText("书籍列表.json", JsonConvert.SerializeObject(Books));
        }

        public void Load()
        {
            try
            {
                Books = JsonConvert.DeserializeObject<List<Book>>(File.ReadAllText("书籍列表.json"));
            }
            catch
            {
                Save();
            }
        }
        /// <summary>
        /// 添加图书
        /// </summary>
        /// <param name="book">要添加的图书</param>
        public void Add(Book book)
        {
            //对书籍ID赋值

            int a = 0;
            if(Books.Count!=0)
            {
                a= Books.Max(s => s.Id);
            }
            book.Id = a + 1;

            Books.Add(book);
            Save();
        }

        /// <summary>
        /// 删除图书
        /// </summary>
        /// <param name="book">要删除的图书的Id</param>
        public void Delete(Book book)
        {
            Books.Remove(book);
            Save();
        }

        /// <summary>
        /// 获取所有图书列表
        /// </summary>
        /// <returns></returns>
        public List<Book> GetAllBooks()
        {
            return Books;
        }
        /// <summary>
        /// 修改图书
        /// </summary>
        /// <param name="student">要更新的图书实例</param>
        /// <returns></returns>
        public void ChangeBook(Book book)
        {
            //查找图书并修改
            foreach (Book temp in Books)
            {
                if (temp.ISBN == book.ISBN)
                {
                    temp.Name = book.Name;
                    temp.Price = book.Price;
                    Save();
                    return;
                }
            }

            //查找不到要修改的图书
            throw new Exception("查到不到ID为" + book.ISBN + "的图书");
        }
    }
}
