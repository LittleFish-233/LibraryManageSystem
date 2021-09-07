using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManageSystem.ToolHelper
{
    static public class Tools
    {
        /// <summary>
        /// 重复输出字符串
        /// </summary>
        /// <param name="chars">要输出的字符串</param>
        /// <param name="frequency">重复次数</param>
        /// <param name="isLineFeed">是否换行</param>
        public static void RepeatOutput(string chars, int frequency, bool isLineFeed)
        {
            for(int i=0;i<frequency;i++)
            {
                Console.Write(chars);
            }
            if(isLineFeed)
            {
                Console.WriteLine();
            }
        }

        /// <summary>
        /// 居中显示
        /// </summary>
        /// <param name="chars">输出的字符串</param>
        /// <param name="length">屏幕长度</param>
        /// <param name="parameter">偏移值</param>
        /// <param name="if_LineFeed">是否换行</param>
        public static void CenterOutput(string chars, int length, double parameter, bool if_LineFeed)
        {
            int length_front = (int)((length - chars.Length * parameter) / 2);

            RepeatOutput(" ", length_front, false);

            Console.Write(chars);

            RepeatOutput(" ", length_front, false);

            if (if_LineFeed == true)
            {
                Console.WriteLine();
            }
        }

        /// <summary>
        /// 输出选项
        /// </summary>
        /// <param name="chars">选项</param>
        /// <param name="length">屏幕长度</param>
        /// <param name="Columns">列数</param>
        /// <param name="parameters">偏移量</param>
        /// <param name="if_LineFeed">是否换行</param>//int a[89][90];int[,] a=new int[89,90];
        public static void OutputOption(string[] chars, int length, int Columns, double[] parameters, bool if_LineFeed)
        {
            //一列的宽度
            int dan_lie = length / Columns;
            //当前输出到第几列
            int danqian_lie = 0;
            //遍历选项
            for (int i = 0; i < chars.Length; i++)
            {
                //当前选项占用了几列
                int lie_zhanyong = (int)(chars[i].Length * parameters[i] / dan_lie) + 1;
                //判断占用列数是否大于最大列数
                if (lie_zhanyong > Columns)
                {
                    lie_zhanyong = Columns;
                }
                //更新当前输出到的列数
                danqian_lie += lie_zhanyong;
                //如果当前输出的列数大于最大列数 换行 重置
                if (danqian_lie > Columns)
                {
                    Console.WriteLine();
                    danqian_lie = lie_zhanyong;
                }
                //输出选项
                Console.Write((i + 1).ToString() + ") " + chars[i]);
                //计算剩余的长度
                double kong = dan_lie * lie_zhanyong - chars[i].Length * parameters[i];
                //输出后置空格
                for (int k = 0; k < kong; k++)
                {
                    Console.Write(" ");
                }
            }
            //判断是否换行
            if (if_LineFeed)
            {
                Console.WriteLine();
            }
        }
        /// <summary>
        /// 获取批量 整数 输入
        /// </summary>
        /// <param name="number">要获取的整数数量</param>
        /// <param name="Range_l">左边界</param>
        /// <param name="Range_r">右边界</param>
        /// <returns></returns>
        public static int[] InputInt(int number, int[] Range_l, int[] Range_r)
        {
            int[] jieguo = new int[number];
            for (int i = 0; i < number; i++)
            {
                int shuju = 0;
                while (true)
                {
                    string linshi = Console.ReadLine();
                    try
                    {
                        shuju = int.Parse(linshi);
                        if (shuju < Range_l[i] || shuju > Range_r[i])
                        {
                            Console.WriteLine("请输入一个在" + Range_l[i] + " - " + Range_r[i] + "之间的数字");
                        }
                        else
                        {
                            break;
                        }
                    }
                    catch
                    {
                        Console.WriteLine("请输入一个数字");
                    }
                }
                jieguo[i] = shuju;

            }
            return jieguo;
        }
        /// <summary>
        /// 输出表格
        /// </summary>
        /// <param name="chart">表格中的数据</param>
        /// <param name="hang">屏幕宽度</param>
        /// <param name="lie">列数</param>
        /// <param name="parameters">表格中的数据的偏移量</param>
        /// <param name="if_LineFeed">是否换行</param>
        public static void OutputTable(string[,] chart, int hang, int lie, double[,] parameters, bool if_LineFeed)
        {
            //遍历表格 确定列宽
            int[] lie_kuan = new int[lie];
            for (int i = 0; i < lie; i++)
            {
                double kuan_zui = 0;
                for (int k = 0; k < hang; k++)
                {
                    if (chart[k, i].Length * parameters[k, i] > kuan_zui)
                    {
                        kuan_zui = chart[k, i].Length * parameters[k, i];
                    }
                }
                lie_kuan[i] = (int)kuan_zui;
            }
            //遍历输出

            for (int k = 0; k < hang; k++)
            {
                for (int i = 0; i < lie; i++)
                {
                    double kong = lie_kuan[i] - chart[k, i].Length * parameters[k, i];
                    Console.Write(chart[k, i]);
                    for (int m = 0; m < kong; m++)
                    {
                        Console.Write(" ");
                    }
                    Console.Write(" | ");
                }
                if (k == 0)
                {
                    Console.WriteLine();
                    for (int i = 0; i < lie; i++)
                    {
                        double kong = lie_kuan[i];
                        for (int m = 0; m < kong; m++)
                        {
                            Console.Write("-");
                        }
                        Console.Write(" | ");
                    }
                }
                Console.WriteLine();
            }
            if (if_LineFeed == true)
            {
                Console.WriteLine();
            }
        }
        public static List<int> InputIntBatch(string str)
        {
            List<int> jieguo = new List<int>();
            string linshi = "";
            bool shifoukaishi = false;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] != ' ' && i != str.Length - 1)
                {
                    if (shifoukaishi == false)
                    {
                        shifoukaishi = true;
                        linshi = "";
                    }
                    linshi += str[i];
                }
                else
                {
                    if (i == str.Length - 1)
                    {
                        if (shifoukaishi == false)
                        {
                            shifoukaishi = true;
                            linshi = "";
                        }
                        linshi += str[i];
                    }
                    try
                    {
                        if (shifoukaishi == true)
                        {
                            shifoukaishi = false;
                            string qian = "";
                            string hou = "";
                            bool shifou_qian = true;
                            for (int k = 0; k < linshi.Length; k++)
                            {
                                if (linshi[k] != '-')
                                {
                                    if (shifou_qian == true)
                                    {
                                        qian += linshi[k];
                                    }
                                    else
                                    {
                                        hou += linshi[k];
                                    }
                                }
                                else
                                {
                                    shifou_qian = false;
                                }
                            }
                            int qian_ = int.Parse(qian);
                            int hou_ = qian_;
                            if (hou != "")
                            {
                                hou_ = int.Parse(hou);
                            }
                            for (int m = qian_; m <= hou_; m++)
                            {
                                jieguo.Add(m);
                            }
                        }
                    }
                    catch
                    {

                    }
                }
            }
            return jieguo;
        }
    }
}
