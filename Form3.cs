using FirebirdSql.Data.FirebirdClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace DataBase
{
    public partial class Form3 : Form
    {
        FbConnection fb;

        public Form3()
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

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            /*ID*/textBox16.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            /*MESTO_RABOTU*/textBox17.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            
        }


        //*****************//
        //ПРОСМОТР ЗНАЧЕНИЙ//
        //*****************//
        private void Button1_Click(object sender, System.EventArgs e)
        {
            DataTable dt2 = new DataTable();
            FbDataAdapter da2 = new FbDataAdapter();
            FbCommand cmd2 = new FbCommand("SELECT base_friends.id, base_friends.name, base_friends.surname, data_friends.mesto_rabotu, mesto_rabotu.rabota from data_friends inner join base_friends on (data_friends.id = base_friends.id) inner join mesto_rabotu on (data_friends.id = mesto_rabotu.id);", fb);

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


        //******************//
        //ИЗМЕНЕНИЕ ЗНАЧЕНИЙ//
        //******************//
        private void Button2_Click(object sender, EventArgs e)
        {
            //INSERT INTO mesto_rabotu(ID, RABOTA) VALUES(270951021, NULL);
            FbCommand MESTO_RABOTUSQL = new FbCommand("UPDATE DATA_FRIENDS SET MESTO_RABOTU='" + textBox17.Text + "' WHERE ID = " + textBox16.Text + ";", fb);
            FbCommand RABOTASQL = new FbCommand("UPDATE MESTO_RABOTU SET RABOTA= '" + textBox1.Text + "' WHERE ID = " + textBox16.Text + ";", fb);

            FbTransaction fbt = fb.BeginTransaction();
            MESTO_RABOTUSQL.Transaction = fbt;
            RABOTASQL.Transaction = fbt;

            try
            {
                int res = MESTO_RABOTUSQL.ExecuteNonQuery();
                int res1 = RABOTASQL.ExecuteNonQuery();
                int res0 = res + res1; 
                MessageBox.Show("Выполненно: " + res0.ToString());
                fbt.Commit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            MESTO_RABOTUSQL.Dispose();
            RABOTASQL.Dispose();

            DataTable dt2 = new DataTable();
            FbDataAdapter da2 = new FbDataAdapter();
            FbCommand cmd2 = new FbCommand("SELECT base_friends.id, base_friends.name, base_friends.surname, data_friends.mesto_rabotu, mesto_rabotu.rabota from data_friends inner join base_friends on (data_friends.id = base_friends.id) inner join mesto_rabotu on (data_friends.id = mesto_rabotu.id)", fb);

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
        }

        //*******************//
        //ДОБАВЛЕНИЕ ЗНАЧЕНИЙ//
        //*******************//
        private void Button15_Click(object sender, EventArgs e)
        {
            //INSERT INTO mesto_rabotu(ID, RABOTA) VALUES(270951021, NULL);
            FbCommand Insert1SQL = new FbCommand("INSERT INTO mesto_rabotu(ID, RABOTA) VALUES (" + textBox16.Text + ",'" + textBox1.Text + "');", fb); ;
            FbCommand InsertSQL = new FbCommand("INSERT INTO mesto_rabotu(ID, RABOTA) VALUES (" + textBox16.Text + ",'" + textBox1.Text + "');", fb); ;
            FbTransaction fbt = fb.BeginTransaction();
            InsertSQL.Transaction = fbt;

            try
            {
                int res = InsertSQL.ExecuteNonQuery();
                MessageBox.Show("Выполненно: " + res.ToString());
                fbt.Commit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            InsertSQL.Dispose();
            
            DataTable dt2 = new DataTable();
            FbDataAdapter da2 = new FbDataAdapter();
            FbCommand cmd2 = new FbCommand("SELECT base_friends.id, base_friends.name, base_friends.surname, data_friends.mesto_rabotu, mesto_rabotu.rabota from data_friends inner join base_friends on (data_friends.id = base_friends.id) inner join mesto_rabotu on (data_friends.id = mesto_rabotu.id)", fb);

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
        }
    }
}
