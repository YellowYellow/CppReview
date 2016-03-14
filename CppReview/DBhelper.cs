using System;
using System.Collections.Generic;
using System.Text; 
using MySql.Data.MySqlClient;
using System.Data;  

namespace Check2005
{ 
        public  class DBHelper
        {
            public string GameAndVersion = "";
           // public string ConnectionString = "Server=localhost;Database=project_bug;Uid=root;Charset=utf8";
            public string ConnectionString = "Server=172.19.11.93;Database=project_bug;Uid=hzw;Charset=utf8";
            public MySqlConnection con;

            public void CreatConnect()
            {
                con = new MySqlConnection(ConnectionString); 
                con.Open();
            }

            public void CreatCon(string game_id, string version, int bug_id, string notes, int level_id, string route, int rows, string code, int isSubmit, int flag)
            {
                int isMachineChecked = 1; 
                MySqlCommand cmd;
                try
                {
                    cmd = con.CreateCommand();
                    cmd.CommandText = "insert into bug_record(game_id,version,bug_id,notes,level_id,route,rows,code,isSubmit,flag,isMachineChecked) values(@game_id,@version,@bug_id,@notes,@level_id,@route,@rows,@code,@isSubmit,@flag,@isMachineChecked);";

                    cmd.Parameters.AddWithValue("@game_id", game_id);
                    cmd.Parameters.AddWithValue("@version", version);
                    cmd.Parameters.AddWithValue("@bug_id", bug_id);
                    cmd.Parameters.AddWithValue("@notes", notes);
                    cmd.Parameters.AddWithValue("@level_id", level_id);
                    cmd.Parameters.AddWithValue("@route", route);
                    cmd.Parameters.AddWithValue("@rows", rows);
                    cmd.Parameters.AddWithValue("@code",code);
                    cmd.Parameters.AddWithValue("@isSubmit", isSubmit);
                    cmd.Parameters.AddWithValue("@flag", flag);
                    cmd.Parameters.AddWithValue("@isMachineChecked", isMachineChecked); 
                    cmd.ExecuteNonQuery();
                }
                catch { }
                finally
                { 

                }
            }

            public string GetGameID(string game_name)
            {
                string temp = ""; 
                MySqlCommand mySqlCommand; 
                mySqlCommand = con.CreateCommand();
                mySqlCommand.CommandText = "select * from game where game_name = '"+ game_name +"';";
                MySqlDataReader reader = mySqlCommand.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        if (reader.HasRows)
                        {
                            temp = reader.GetString(0);
                        }
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("查询失败了！");
                }
                finally
                {
                }
                return temp; 
            }

            public List<Dictionary<string, string>> GetWaitedCheck()
            {
                List<Dictionary<string, string>> temp = new List<Dictionary<string, string>>(); 
 
                MySqlCommand mySqlCommand; 
                mySqlCommand = con.CreateCommand();
                mySqlCommand.CommandText = "select * from checkstate where IsCheckedByTool = 0;";
                MySqlDataReader reader = mySqlCommand.ExecuteReader();
                try
                {
                    while (reader.Read())
                    { 
                        if (reader.HasRows)
                        {
                            Dictionary<string, string> item = new Dictionary<string, string>();
                            item.Add("game_id",reader.GetString(1));
                            item.Add("game_name",reader.GetString(2));
                            item.Add("version",reader.GetString(3));
                            temp.Add(item);
                        } 
                    }
                }
                catch (Exception)
                {  
                    Console.WriteLine("查询失败了！");
                }
                finally
                { 

                }
                return temp; 
            }

            public void ChangeCheckFlag(string game_id,string version)
            {  
                MySqlCommand mySqlCommand; 
                mySqlCommand = con.CreateCommand();
                mySqlCommand.CommandText = "update game_version set IsCheckedByTool = 1 ,state = 1 where game_id = '"+game_id+"' and version = '" + version +"';";
                try
                {
                    mySqlCommand.ExecuteNonQuery(); 
                }
                catch (Exception)
                {
                    Console.WriteLine("修改失败了！");
                }
                finally
                { 

                } 
            }

            public void shut()
            {
                con.Close();
            }
        } 
}
