using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using System.Text.RegularExpressions;

namespace CheckReview.StateForCpp
{
    //打印状态
    public class PrintStateForCpp : AbstractStateForCpp
    {
        private int type;

        public PrintStateForCpp(AbstractStateForCpp state, int errorType)
        {
            this.so = state.so;
            this.type = errorType;
        }

        //打印  将错误信息添加至列表  
        public void print(string filename)
        {
            if (type == 1)
            {
                int row = TempRow + 1;
 
                lvi = new ListViewItem();
                lvi.Text = row.ToString() + "行";
                lvi.SubItems.Add("指针" + TempPointer + "没有检查");
                lvi.SubItems.Add(filename);
                lvi.SubItems.Add(strRead[TempRow]);
                ui.Invoke(new VoidDelegate(delegate()
                {
                    ui.listview1.Items.Add(lvi);
                }));
                Back();
            }  
            //else if (type == 4)
            //{
            //    int row = now - 1;


            //    test.CreatCon("20151127", 5, row.ToString() + "行 对象可能没使用场景ID来创建(不确定)", 1, GetRoute(filename), row, GetCodeAround(row), 0, 0); 

            //    lvi = new ListViewItem();
            //    lvi.Text = now.ToString() + "行 使用数字控制逻辑，可能存在硬编码";
            //    lvi.SubItems.Add(filename);
            //    lvi.SubItems.Add(strRead[row]);
            //    ui.Invoke(new VoidDelegate(delegate()
            //    {
            //        ui.listview5.Items.Add(lvi);
            //    }));
            //}
            else if (type == 5)
            {
                int row = TempRow + 1;
 
                lvi = new ListViewItem();
                lvi.Text = row.ToString() + "行";
                lvi.SubItems.Add("数组" + TempArray + "可能越界，需要保护");
                lvi.SubItems.Add(filename);
                lvi.SubItems.Add(strRead[TempRow]);
                ui.Invoke(new VoidDelegate(delegate()
                {
                    ui.listview4.Items.Add(lvi);
                }));
                TempArray = "";
                Back();
            }
            else if (type == 6)
            {
                int row = TempRow + 1;
 
                lvi = new ListViewItem();
                lvi.Text = row.ToString() + "行";
                lvi.SubItems.Add("迭代器" + TempIter + "需要保护");
                lvi.SubItems.Add(filename);
                lvi.SubItems.Add(strRead[TempRow]);
                ui.Invoke(new VoidDelegate(delegate()
                {
                    ui.listview5.Items.Add(lvi);
                }));
                TempIter = "";
                Back();
            }
            else if (type == 7)
            {
                int row = TempRow;
 
                lvi = new ListViewItem();
                lvi.Text = row.ToString() + "行";
                lvi.SubItems.Add("格式和个数需要再检查");
                lvi.SubItems.Add(filename);
                lvi.SubItems.Add(strRead[TempRow]);
                ui.Invoke(new VoidDelegate(delegate()
                {
                    ui.listview7.Items.Add(lvi);
                }));
            }
            //else if (type == 8)
            //{
            //    int row = now - 1;
            //    lvi = new ListViewItem();
            //    lvi.Text = row.ToString() + "JustForTest";
            //    lvi.SubItems.Add(filename);
            //    lvi.SubItems.Add(strRead[row]);
            //    ui.Invoke(new VoidDelegate(delegate()
            //    {
            //        ui.listview7.Items.Add(lvi);
            //    }));
            //}
            //else if (type == 9)
            //{
            //    int row = TempRow + 1;

            //    //if (GetRoute(filename) != "" && ui.flags.dbFlag == 1)
            //    //    test.CreatCon(test.GetGameID(so.GameName), so.GameVersion, 6, row.ToString() + "行 Varlist没有检查", 1, GetRoute(filename), row, GetCodeAround(row), 0, 0); 

            //    lvi = new ListViewItem();
            //    lvi.Text = row.ToString() + "行 Varlist  " + TempStringValvar + "没有检查";
            //    lvi.SubItems.Add(filename);
            //    lvi.SubItems.Add(strRead[TempRow]);
            //    ui.Invoke(new VoidDelegate(delegate()
            //    {
            //        ui.listview7.Items.Add(lvi);
            //    }));
            //    Back();
            //}
            else if (type == 10)
            {
                int row = TempRow + 1;
 
                lvi = new ListViewItem();
                lvi.Text = row.ToString() + "行";
                lvi.SubItems.Add("Map  " + TempMapvar + "使用[]规则");
                lvi.SubItems.Add(filename);
                lvi.SubItems.Add(strRead[TempRow]);
                ui.Invoke(new VoidDelegate(delegate()
                {
                    ui.listview7.Items.Add(lvi);
                }));
                Back();
            } 
            else if (type == 12)
            {
                int row = TempRow + 1;
 
                lvi = new ListViewItem();
                lvi.Text = row.ToString() + "行";
                lvi.SubItems.Add("va_start和va_end要成对出现");
                lvi.SubItems.Add(filename);
                lvi.SubItems.Add(strRead[TempRow]);
                ui.Invoke(new VoidDelegate(delegate()
                {
                    ui.listview7.Items.Add(lvi);
                }));
                Back();
            }
            else if (type == 13)
            {
                int row = now - 1;
 
                lvi = new ListViewItem();
                lvi.Text = (row+1).ToString() + "行";
                lvi.SubItems.Add("可能返回了null或0");
                lvi.SubItems.Add(filename);
                lvi.SubItems.Add(strRead[row]);
                ui.Invoke(new VoidDelegate(delegate()
                {
                    ui.listview7.Items.Add(lvi);
                }));
            }
            else if (type == 14)
            {
                int row = now - 1;
 
                lvi = new ListViewItem();
                lvi.Text = row.ToString() + "行";
                lvi.SubItems.Add("memcpy memmove memset统一保护不能越界");
                lvi.SubItems.Add(filename);
                lvi.SubItems.Add(strRead[row]);
                ui.Invoke(new VoidDelegate(delegate()
                {
                    ui.listview7.Items.Add(lvi);
                }));
            }
            else if (type == 15)
            {
                int row = now;
 
                lvi = new ListViewItem();
                lvi.Text = row.ToString() + "行";
                lvi.SubItems.Add("结构体没有构造器");
                lvi.SubItems.Add(filename);
                lvi.SubItems.Add(strRead[now - 1]);
                ui.Invoke(new VoidDelegate(delegate()
                {
                    ui.listview7.Items.Add(lvi);
                }));
            }
            else if (type == 16)
            {
                int row = now - 1;
 
                lvi = new ListViewItem();
                lvi.Text = row.ToString() + "行";
                lvi.SubItems.Add("对vector使用了earse");
                lvi.SubItems.Add(filename);
                lvi.SubItems.Add(strRead[row]);
                ui.Invoke(new VoidDelegate(delegate()
                {
                    ui.listview7.Items.Add(lvi);
                }));
            }
            else if (type == 17)
            {
                int row = now - 1;
 
                lvi = new ListViewItem();
                lvi.Text = row.ToString() + "行";
                lvi.SubItems.Add("switch需要适时的break");
                lvi.SubItems.Add(filename);
                lvi.SubItems.Add(strRead[row]);
                ui.Invoke(new VoidDelegate(delegate()
                {
                    ui.listview7.Items.Add(lvi);
                }));
            }
            else if (type == 18)    
            {
                int row = TempRow + 1;
 
                lvi = new ListViewItem();
                lvi.Text = row.ToString() + "行";
                lvi.SubItems.Add("除法和取模运算要保护");
                lvi.SubItems.Add(filename);
                lvi.SubItems.Add(strRead[TempRow]);
                ui.Invoke(new VoidDelegate(delegate()
                {
                    ui.listview7.Items.Add(lvi);
                }));
            }
            else if (type == 19)
            {
                int row = TempRow + 1;
 
                lvi = new ListViewItem();
                lvi.Text = row.ToString() + "行";
                lvi.SubItems.Add("if中进行赋值");
                lvi.SubItems.Add(filename);
                lvi.SubItems.Add(strRead[TempRow]);
                ui.Invoke(new VoidDelegate(delegate()
                {
                    ui.listview7.Items.Add(lvi);
                }));
            } 
            else if (type == 21)
            {
                int row = TempRow + 1;
 
                lvi = new ListViewItem();
                lvi.Text = row.ToString() + "行";
                lvi.SubItems.Add("stl的.at方法可能需要保护界限");
                lvi.SubItems.Add(filename);
                lvi.SubItems.Add(strRead[TempRow]);
                ui.Invoke(new VoidDelegate(delegate()
                {
                    ui.listview7.Items.Add(lvi);
                }));
            }
        }

        public override void Process()
        {
            print(so.filename);
            so.State = new FreeStateForCpp(this);
        }
    }

}
