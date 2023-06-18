using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.IO;

namespace books
{
    class ExcelService
    {
        public void DataGirdViewExportToExcel(DataSet ds,string strTitle)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel files (*.xls)|*.xls";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.CreatePrompt = false;
            saveFileDialog.FileName = strTitle + ".xls";
            if (saveFileDialog.ShowDialog()==DialogResult.Cancel)
            {
                return;
            }
            Stream myStream = saveFileDialog.OpenFile();
            StreamWriter sw = new StreamWriter(myStream,System.Text.Encoding.GetEncoding(-0));
            string strHeaderText = "";
            try
            {
                for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                {
                    if (i>0)
                    {
                        strHeaderText += "\t";
                    }
                    strHeaderText += ds.Tables[0].Columns[i].ToString();
                }
                sw.WriteLine(strHeaderText);

                string strItemValue = "";
                for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                {
                    strItemValue = "";
                    for (int k = 0; k < ds.Tables[0].Columns.Count; k++)
                    {
                        if (k > 0)
                        {
                            strItemValue += "\t";
                        }
                        strItemValue += ds.Tables[0].Rows[j][k].ToString();
                    }
                    sw.WriteLine(strItemValue);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "软件提示");
                throw ex;
            }
            finally
            {
                sw.Close();
                myStream.Close();
            }
        }
    }
}
