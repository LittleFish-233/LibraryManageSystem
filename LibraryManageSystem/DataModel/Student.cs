using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManageSystem.DataModel
{
    public class Student
    {
        /// <summary>
        /// 学生实例的唯一标识
        /// </summary>
        public int Id;

        /// <summary>
        /// 学号
        /// </summary>
        public string StudentId;

        /// <summary>
        /// 名字
        /// </summary>
        public string Name;

        /// <summary>
        /// 班级名
        /// </summary>
        public string ClassName;
    }
}
