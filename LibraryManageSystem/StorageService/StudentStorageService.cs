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
    public class StudentStorageService
    {
        List<Student> Students = new List<Student>();
        public void Save()
        {
            File.WriteAllText("学生列表.json", JsonConvert.SerializeObject(Students));
        }

        public void Load()
        {
            try
            {
                Students = JsonConvert.DeserializeObject<List<Student>>(File.ReadAllText("学生列表.json"));
            }
            catch
            {
                Save();
            }
        }
        /// <summary>
        /// 添加学生
        /// </summary>
        /// <param name="student">要添加的学生</param>
        public void Add(Student student)
        {
            int a = 0;
            if (Students.Count != 0)
            {
                a = Students.Max(s => s.Id);
            }

            student.Id = a + 1;
            Students.Add(student);
            Save();
        }

        /// <summary>
        /// 删除学生
        /// </summary>
        /// <param name="id">要删除的学生的Id</param>
        public void Delete(Student student)
        {
            Students.Remove(student);
            Save();
        }
        /// <summary>
        /// 获取所有学生列表
        /// </summary>
        /// <returns></returns>
        public List<Student> GetAllStudents()
        {
            return Students;
        }
        /// <summary>
        /// 修改学生
        /// </summary>
        /// <param name="student">要更新的学生实例</param>
        /// <returns></returns>
        public void ChangeStudent(Student student)
        {
            //查找学生并修改
            foreach (Student temp in Students)
            {
                if (temp.Id == student.Id)
                {
                    temp.ClassName = student.ClassName;
                    temp.Name = student.Name;
                    Save();
                    return;
                }
            }

            //查找不到要修改的学生
            throw new Exception("查到不到ID为" + student.Id + "的学生");
        }
    }
}
