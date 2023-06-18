using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace books
{
    class Userservice
    {
        public bool checkUsernameAndPwd(string username, string pwd, int keys)
        {
            pwd = GetMD5Hash.MD5(pwd);
            bool flag = false;
            DB.GrtCn();
            string str = "select * from buser where username=@username and pwd=@pwd and keys=@keys";
            SqlCommand cmd = new SqlCommand(str, DB.cn);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@pwd", pwd);
            cmd.Parameters.AddWithValue("@keys", keys);
            try
            {
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    flag = true;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            finally
            {
                DB.cn.Close();
            }
            return flag;
        }
    }
}
