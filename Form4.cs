using FirebirdSql.Data.FirebirdClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace DataBase
{
    public partial class Form4 : Form
    {
        FbConnection fb;
        public Form4()
        {
            InitializeComponent();
            FbConnectionStringBuilder fb_con = new FbConnectionStringBuilder();
            fb_con.Charset = "WIN1251";
            fb_con.UserID = "SYSDBA"; //SYSDBA
            fb_con.Password = "masterkey"; //masterkey
            fb_con.Database = "C:\\Users\\developer\\Desktop\\PROEKT\\BASE.FDB";
            fb_con.ServerType = 0;
            fb = new FbConnection(fb_con.ToString());
            fb.Open();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            DataTable dt2 = new DataTable();
            FbDataAdapter da2 = new FbDataAdapter();
            FbCommand cmd2 = new FbCommand("SELECT base_friends.id, base_friends.name, base_friends.surname, data_friends.web_sayt, data_friends.instagram from data_friends inner join base_friends on (data_friends.id = base_friends.id);", fb);

            try
            {
                cmd2.CommandType = CommandType.Text;

                FbDataReader dr = cmd2.ExecuteReader();
                dt2.Load(dr);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            dataGridView1.DataSource = dt2;
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            /*ID*/textBox16.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            /*WEB-SAYT*/textBox17.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            /*INSTAGRAM*/textBox1.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            FbCommand WEB_SAYTSQL = new FbCommand("UPDATE DATA_FRIENDS SET WEB_SAYT= '" + textBox17.Text + "' WHERE ID = " + textBox16.Text + ";", fb);
            FbCommand INSTAGRAMSQL = new FbCommand("UPDATE DATA_FRIENDS SET INSTAGRAM= '" + textBox1.Text + "' WHERE ID = " + textBox16.Text + ";", fb);

            FbTransaction fbt = fb.BeginTransaction();
            WEB_SAYTSQL.Transaction = fbt;
            INSTAGRAMSQL.Transaction = fbt;

            try
            {
                int res = WEB_SAYTSQL.ExecuteNonQuery();
                int res1 = INSTAGRAMSQL.ExecuteNonQuery();
                int res0 = res + res1;
                MessageBox.Show("Выполненно: " + res0.ToString());
                fbt.Commit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            WEB_SAYTSQL.Dispose();
            INSTAGRAMSQL.Dispose();

            DataTable dt2 = new DataTable();
            FbDataAdapter da2 = new FbDataAdapter();
            FbCommand cmd2 = new FbCommand("SELECT base_friends.id, base_friends.name, base_friends.surname, data_friends.web_sayt, data_friends.instagram from data_friends inner join base_friends on (data_friends.id = base_friends.id);", fb);

            try
            {
                cmd2.CommandType = CommandType.Text;

                FbDataReader dr = cmd2.ExecuteReader();
                dt2.Load(dr);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            dataGridView1.DataSource = dt2;


            textBox16.Clear();
            textBox17.Clear();
            textBox1.Clear();
        }
    }
}
