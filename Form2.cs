using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DataBase
{
    public partial class Form2 : Form
    {
        FbConnection fb;

        public Form2()
        {
                FbConnectionStringBuilder fb_con = new FbConnectionStringBuilder();
                fb_con.Charset = "WIN1251";
                fb_con.UserID = "SYSDBA"; //SYSDBA
                fb_con.Password = "masterkey"; //masterkey
                fb_con.Database = "C:\\Users\\developer\\Desktop\\PROEKT\\BASE.FDB";
                fb_con.ServerType = 0;
                fb = new FbConnection(fb_con.ToString());
                fb.Open();

            InitializeComponent();
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            /*ID*/textBox16.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            /*NAME*/textBox17.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            /*SURNAME*/textBox18.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            /*BIRTHDAY*/textBox22.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }

        private void Button15_Click(object sender, EventArgs e)
        {
            FbCommand InsertSQL = new FbCommand("INSERT INTO BASE_FRIENDS(ID, NAME, SURNAME) VALUES (" + textBox16.Text + ",'" + textBox17.Text + "','" + textBox18.Text + "');", fb); ;
            FbCommand Insert1SQL = new FbCommand("INSERT INTO DATA_FRIENDS(ID, BIRTHDAY, MESTO_RABOTU, WEB_SAYT, INSTAGRAM) VALUES (" + textBox16.Text + ",'" + textBox22.Text + "','NULL'" + ",'NULL'" + ",'NULL'" + ");", fb);
            FbTransaction fbt = fb.BeginTransaction();
            InsertSQL.Transaction = fbt;
            Insert1SQL.Transaction = fbt;


            try
            {
                int res = InsertSQL.ExecuteNonQuery();
                int res1 = Insert1SQL.ExecuteNonQuery();
                int res0 = res + res1;
                MessageBox.Show("Выполненно: " + res0.ToString());
                fbt.Commit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            InsertSQL.Dispose();
            Insert1SQL.Dispose();


            DataTable dt2 = new DataTable();
            FbDataAdapter da2 = new FbDataAdapter();
            FbCommand cmd2 = new FbCommand("SELECT base_friends.id, base_friends.name, base_friends.surname, data_friends.birthday FROM base_friends inner join data_friends on(base_friends.id = data_friends.id);", fb);

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
            textBox18.Clear();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            DataTable dt2 = new DataTable();
            FbDataAdapter da2 = new FbDataAdapter();
            FbCommand cmd2 = new FbCommand("SELECT base_friends.id, base_friends.name, base_friends.surname, data_friends.birthday FROM base_friends inner join data_friends on(base_friends.id = data_friends.id);", fb);
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
            textBox18.Clear();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            FbCommand IDSQL = new FbCommand("UPDATE BASE_FRIENDS SET ID= '" + textBox16.Text + "' WHERE ID = " + textBox16.Text + ";", fb);
            FbCommand NAMESQL = new FbCommand("UPDATE BASE_FRIENDS SET NAME= '" + textBox17.Text + "' WHERE ID = " + textBox16.Text + ";", fb);
            FbCommand SURNAMESQL = new FbCommand("UPDATE BASE_FRIENDS SET SURNAME= '" + textBox18.Text + "' WHERE ID = " + textBox16.Text + ";", fb);
            FbCommand BIRTHDAYSQL = new FbCommand("UPDATE DATA_FRIENDS SET BIRTHDAY= '" + textBox22.Text + "' WHERE ID = " + textBox16.Text + ";", fb);
            FbTransaction fbt = fb.BeginTransaction();

            IDSQL.Transaction = fbt;
            NAMESQL.Transaction = fbt;
            SURNAMESQL.Transaction = fbt;
            BIRTHDAYSQL.Transaction = fbt;
                        
            try
            {
                int res = IDSQL.ExecuteNonQuery();
                int res2 = NAMESQL.ExecuteNonQuery();
                int res3 = SURNAMESQL.ExecuteNonQuery();
                int res4 = BIRTHDAYSQL.ExecuteNonQuery();

                int res0 = res + res2 + res3 + res4;

                MessageBox.Show("Выполненно: " + res0.ToString(), "Завершенно");

                fbt.Commit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            IDSQL.Dispose();
            NAMESQL.Dispose();
            SURNAMESQL.Dispose();
            BIRTHDAYSQL.Dispose();

            DataTable dt2 = new DataTable();
            FbDataAdapter da2 = new FbDataAdapter();
            FbCommand cmd2 = new FbCommand("SELECT base_friends.id, base_friends.name, base_friends.surname, data_friends.birthday FROM base_friends inner join data_friends on(base_friends.id = data_friends.id); ", fb);

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
            textBox18.Clear();
            textBox22.Clear();
        }

        
    }
}
