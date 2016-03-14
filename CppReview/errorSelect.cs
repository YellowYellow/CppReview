using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing; 
using System.Text; 
using System.Windows.Forms;
using System.Xml;
using CCWin;
//using System.Resources;
//using System.Reflection;

namespace CheckReview
{
    public partial class errorSelect : CCSkinMain
    {
        public XmlDocument xmlDoc;
        public Form1 ui;

        public errorSelect(Form1 ui)
        {
            InitializeComponent();

            this.ui = ui; 
            xmlDoc =new XmlDocument();
            xmlDoc.Load("./res/ErrorForCpp.xml"); //加载xml文件 
             
            XmlNodeList nodeList = xmlDoc.SelectSingleNode("ErrorType").ChildNodes;

            //遍历所有子节点 
            foreach (XmlNode xn in nodeList)
            {
                XmlElement xe = (XmlElement)xn; //将子节点类型转换为XmlElement类型 
                if (xe.Name == "pointer")
                {  
                    if(xe.InnerText == "1"){checkBox1.CheckState = CheckState.Checked;}
                    else{ checkBox1.CheckState = CheckState.Unchecked;}
                } 
                if (xe.Name == "num")
                {
                    if (xe.InnerText == "1") { checkBox4.CheckState = CheckState.Checked; }
                    else { checkBox4.CheckState = CheckState.Unchecked; }
                }
                if (xe.Name == "array")
                {
                    if (xe.InnerText == "1") { checkBox5.CheckState = CheckState.Checked; }
                    else { checkBox5.CheckState = CheckState.Unchecked; } 
                }
                if (xe.Name == "iter")
                {
                    if (xe.InnerText == "1") { checkBox6.CheckState = CheckState.Checked; }
                    else { checkBox6.CheckState = CheckState.Unchecked; }
                }
                if (xe.Name == "format")
                {
                    if (xe.InnerText == "1") { checkBox7.CheckState = CheckState.Checked; }
                    else { checkBox7.CheckState = CheckState.Unchecked; }
                }
                if (xe.Name == "vastart")
                {
                    if (xe.InnerText == "1") { checkBox8.CheckState = CheckState.Checked; }
                    else { checkBox8.CheckState = CheckState.Unchecked; }
                }
                if (xe.Name == "struct")
                {
                    if (xe.InnerText == "1") { checkBox9.CheckState = CheckState.Checked; }
                    else { checkBox9.CheckState = CheckState.Unchecked; }
                }
                if (xe.Name == "map")
                {
                    if (xe.InnerText == "1") { checkBox10.CheckState = CheckState.Checked; }
                    else { checkBox10.CheckState = CheckState.Unchecked; }
                }
                if (xe.Name == "constnull")
                {
                    if (xe.InnerText == "1") { checkBox11.CheckState = CheckState.Checked; }
                    else { checkBox11.CheckState = CheckState.Unchecked; }
                } 
                if (xe.Name == "earse")
                {
                    if (xe.InnerText == "1") { checkBox13.CheckState = CheckState.Checked; }
                    else { checkBox13.CheckState = CheckState.Unchecked; }
                } 
            } 
             
        }
 
        //向xml配置文件改写某个节点
        public void writeNode(XmlNodeList nodeList,string name,string value)
        {
             foreach (XmlNode xn in nodeList)
            {
                XmlElement xe = (XmlElement)xn; //将子节点类型转换为XmlElement类型 
                if (xe.Name == name)
                {  
                    xe.InnerText = value;
                    break;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            xmlDoc = new XmlDocument();
            xmlDoc.Load("./res/ErrorForCpp.xml"); //加载xml文件 

            XmlNodeList nodeList = xmlDoc.SelectSingleNode("ErrorType").ChildNodes;


            if (checkBox1.CheckState == CheckState.Checked)
            {
                writeNode(nodeList, "pointer", "1");
            }
            else { writeNode(nodeList, "pointer", "0"); } 

            if (checkBox4.CheckState == CheckState.Checked)
            {
                writeNode(nodeList, "num", "1");
            }
            else { writeNode(nodeList, "num", "0"); }

            if (checkBox5.CheckState == CheckState.Checked)
            {
                writeNode(nodeList, "array", "1");
            }
            else { writeNode(nodeList, "array", "0"); }

            if (checkBox6.CheckState == CheckState.Checked)
            {
                writeNode(nodeList, "iter", "1");
            }
            else { writeNode(nodeList, "iter", "0"); }

            if (checkBox7.CheckState == CheckState.Checked)
            {
                writeNode(nodeList, "format", "1");
            }
            else { writeNode(nodeList, "format", "0"); }

            if (checkBox8.CheckState == CheckState.Checked)
            {
                writeNode(nodeList, "vastart", "1");
            }
            else { writeNode(nodeList, "vastart", "0"); }

            if (checkBox9.CheckState == CheckState.Checked)
            {
                writeNode(nodeList, "struct", "1");
            }
            else { writeNode(nodeList, "struct", "0"); }

            if (checkBox10.CheckState == CheckState.Checked)
            {
                writeNode(nodeList, "map", "1");
            }
            else { writeNode(nodeList, "map", "0"); }

            if (checkBox11.CheckState == CheckState.Checked)
            {
                writeNode(nodeList, "constnull", "1");
            }
            else { writeNode(nodeList, "constnull", "0"); } 

            if (checkBox13.CheckState == CheckState.Checked)
            {
                writeNode(nodeList, "earse", "1");
            }
            else { writeNode(nodeList, "earse", "0"); } 

            xmlDoc.Save("./res/ErrorForCpp.xml"); 

            this.Close();
            //ResXResourceWriter rw = new ResXResourceWriter(@"../../errorSelect.resx");
            //ResXDataNode node = new ResXDataNode("aa", "0");

            //if (checkBox1.CheckState == CheckState.Checked) rw.AddResource(node);
            //else rw.AddResource(node);
            //if (checkBox2.CheckState == CheckState.Checked) rw.AddResource("loop", "1");
            //else rw.AddResource("loop","0");
            //if (checkBox3.CheckState == CheckState.Checked) rw.AddResource("objectleek", "1");
            //else rw.AddResource("objectleek", "0");
            //if (checkBox4.CheckState == CheckState.Checked) rw.AddResource("num", "1");
            //else rw.AddResource("num","0");
            //if (checkBox5.CheckState == CheckState.Checked) rw.AddResource("array", "1");
            //else rw.AddResource("array","0");
            //if (checkBox6.CheckState == CheckState.Checked) rw.AddResource("iter", "1");
            //else rw.AddResource("iter","0");

            //rw.Generate();
            //rw.Close();
        } 
 
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
          
        private void errorSelect_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            ui.loadXml();
            ui.tabInit();
        }

    }
}
