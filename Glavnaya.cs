using System;
using System.Data;
using System.Windows.Forms;
using FirebirdSql.Data.FirebirdClient;


namespace DataBase
{
    public partial class Glavnaya : Form
    {

        FbConnection fb;
       

        public Glavnaya()
        {
            InitializeComponent();
        }

        //*****************//
        //ПОДКЛЮЧЕНИЕ К БД//
        //*****************//
        public void Button7_Click(object sender, EventArgs e)
        {
            try
            {
                FbConnectionStringBuilder fb_con = new FbConnectionStringBuilder();
                fb_con.Charset = "WIN1251";
                fb_con.UserID = "" + textBox12.Text + ""; //SYSDBA
                fb_con.Password = "" + textBox13.Text + ""; //masterkey
                fb_con.Database = "" + textBox14.Text + ""; //C:\\Users\\developer\\Desktop\\PROEKT\\BASE.FDB
                fb_con.ServerType = 0;
                fb = new FbConnection(fb_con.ToString());
                fb.Open();

                FbDatabaseInfo fb_inf = new FbDatabaseInfo(fb);
                MessageBox.Show("Подключение успешно выполненно.\n" + "Информация: " + fb_inf.ServerClass + "; " + fb_inf.ServerVersion, "Успешно.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }

            //ДАННЫЕ ТАБЛИЦЫ DATA_FRIENDS//
            DataTable dt2 = new DataTable();
            FbDataAdapter da2 = new FbDataAdapter();
            FbCommand cmd2 = new FbCommand("SELECT base_friends.id, base_friends.name, base_friends.surname, data_friends.birthday,data_friends.mesto_rabotu, data_friends.web_sayt,data_friends.instagram FROM base_friends inner join data_friends on(base_friends.id = data_friends.id); ", fb);

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

            dataGridView3.DataSource = dt2;
            dataGridView4.DataSource = dt2;
        }

        //****************//
        //ОТКЛЮЧЕНИЕ ОТ БД//
        //****************//
        private void Button14_Click(object sender, EventArgs e)
        {
            try
            {
                fb.Close();
                MessageBox.Show("Соединение отключено", "Внимание");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        //*******************//
        //Добавление значений//
        //*******************//
        private void Button4_Click(object sender, EventArgs e)
        {
            FbCommand InsertSQL = new FbCommand("INSERT INTO BASE_FRIENDS(ID, NAME, SURNAME) VALUES (" + textBox1.Text + ",'" + textBox2.Text + "','" + textBox3.Text + "')", fb); ;
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

            //ДАННЫЕ ТАБЛИЦЫ DATA_FRIENDS//
            DataTable dt2 = new DataTable();
            FbDataAdapter da2 = new FbDataAdapter();
            FbCommand cmd2 = new FbCommand("SELECT * FROM DATA_FRIENDS", fb);
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
            dataGridView3.DataSource = dt2;
            dataGridView4.DataSource = dt2;

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }


        //****************//
        //Поиск по записям//
        //****************//
        private void Button3_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < dataGridView4.RowCount; i++)
            {
                dataGridView4.Rows[i].Selected = false;
                for (int j = 0; j < dataGridView4.ColumnCount; j++)
                {
                    if (dataGridView4.Rows[i].Cells[j].Value != null)
                    {
                        if (dataGridView4.Rows[i].Cells[j].Value.ToString().Contains(textBox4.Text))
                        {
                            dataGridView4.Rows[i].Selected = true;
                            break;
                        }
                    }
                }
            }
        }

        //*****************//
        //Удаление значений//
        //*****************//
        private void Button1_Click(object sender, EventArgs e)
        {
            FbCommand InsertSQL = new FbCommand("DELETE FROM BASE_FRIENDS WHERE ID = " + textBox10.Text + " ", fb);
            FbCommand DeleteSQL = new FbCommand("DELETE FROM DATA_FRIENDS WHERE ID = " + textBox10.Text + " ", fb);
            FbCommand Delete2SQL = new FbCommand("DELETE FROM MESTO_RABOTU WHERE ID = " + textBox10.Text + " ", fb);

            FbTransaction fbt = fb.BeginTransaction();
            InsertSQL.Transaction = fbt;
            DeleteSQL.Transaction = fbt;
            Delete2SQL.Transaction = fbt;
            try
            {
                int res = InsertSQL.ExecuteNonQuery();
                int res2 = DeleteSQL.ExecuteNonQuery();
                int res3 = Delete2SQL.ExecuteNonQuery();
                int res0 = res + res2 + res3;
                MessageBox.Show("Запись успешно удалена.", "Удалено:" + res0.ToString());
                fbt.Commit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
                        
            DeleteSQL.Dispose();
            Delete2SQL.Dispose();
            InsertSQL.Dispose();

            DataTable dt2 = new DataTable();
            FbDataAdapter da2 = new FbDataAdapter();
            FbCommand cmd2 = new FbCommand("SELECT base_friends.id, base_friends.name, base_friends.surname, data_friends.birthday,data_friends.mesto_rabotu, data_friends.web_sayt,data_friends.instagram FROM base_friends inner join data_friends on(base_friends.id = data_friends.id); ", fb);
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
            dataGridView3.DataSource = dt2;
            textBox10.Clear();
        }

        //***************************//
        //Обновление значений поля ID//
        //***************************//
        private void Button5_Click(object sender, EventArgs e)
        {
            FbCommand InsertSQL = new FbCommand("UPDATE base_friends SET ID = " + textBox9.Text + " WHERE ID = " + textBox8.Text + ";", fb);
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

            //ДАННЫЕ ТАБЛИЦЫ DATA_FRIENDS//
            DataTable dt2 = new DataTable();
            FbDataAdapter da2 = new FbDataAdapter();
            FbCommand cmd2 = new FbCommand("SELECT * FROM DATA_FRIENDS", fb);
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
            dataGridView3.DataSource = dt2;
            dataGridView4.DataSource = dt2;

            textBox8.Clear();
            textBox9.Clear();
        }

        //****************************************//
        //Обновление значений полей NAME и SURNAME//
        //****************************************//
        private void Button2_Click(object sender, EventArgs e)
        {
            FbCommand InsertSQL = new FbCommand("UPDATE base_friends SET NAME = '" + textBox6.Text + "', SURNAME = '" + textBox5.Text + "' WHERE ID = " + textBox7.Text + ";", fb);
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

            //ДАННЫЕ ТАБЛИЦЫ DATA_FRIENDS//
            DataTable dt2 = new DataTable();
            FbDataAdapter da2 = new FbDataAdapter();
            FbCommand cmd2 = new FbCommand("SELECT * FROM DATA_FRIENDS", fb);
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
            dataGridView3.DataSource = dt2;
            dataGridView4.DataSource = dt2;

            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
        }

        //****************************//
        //Вывод записей в DataGridView//
        //****************************//
        private void Button6_Click(object sender, EventArgs e)
        {
            DataTable dt2 = new DataTable();
            FbDataAdapter da2 = new FbDataAdapter();
            FbCommand cmd2 = new FbCommand("SELECT * FROM DATA_FRIENDS", fb);
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
            dataGridView4.DataSource = dt2;
        }

        //****************//
        //ПОИСК ПО ЗАПИСЯМ//
        //****************//
        private void Button9_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView3.RowCount; i++)
            {
                dataGridView3.Rows[i].Selected = false;
                for (int j = 0; j < dataGridView3.ColumnCount; j++)
                {
                    if (dataGridView3.Rows[i].Cells[j].Value != null)
                    {
                        if (dataGridView3.Rows[i].Cells[j].Value.ToString().Contains(textBox15.Text))
                        {
                            dataGridView3.Rows[i].Selected = true;
                            break;
                        }
                    }
                }
            }
        }


        //********************//
        //ТАБЛИЦА DATA_FRIENDS//
        //********************//
        private void Button6_Click_1(object sender, EventArgs e)
        {
            DataTable dt2 = new DataTable();
            FbDataAdapter da2 = new FbDataAdapter();
            FbCommand cmd2 = new FbCommand("SELECT * FROM DATA_FRIENDS", fb);

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
            dataGridView3.DataSource = dt2;
            dataGridView4.DataSource = dt2;
        }

        //**************//
        //ПОИСК ПО ИМЕНИ//
        //**************//
        private void Button6_Click_2(object sender, EventArgs e)
        {
            for (int i = 1; i < dataGridView4.RowCount; i++)
            {
                dataGridView4.Rows[i].Selected = false;
                for (int j = 1; j < dataGridView4.ColumnCount; j++)
                {
                    if (dataGridView4.Rows[i].Cells[j].Value != null)
                    {
                        if (dataGridView4.Rows[i].Cells[j].Value.ToString().Contains(textBox11.Text))
                        {
                            dataGridView4.Rows[i].Selected = true;
                            break;
                        }
                    }
                }
            }
            textBox11.Clear();
        }
               

        //********************************//
        //ДОБАВЛЕНИЕ МЕСТА РАБОТЫ ЧЕЛОВЕКА//
        //********************************//
        private void Button8_Click(object sender, EventArgs e)
        {
            Form3 f = new Form3();
            f.ShowDialog();
        }

        //***********//
        //ФОРМА СВЯЗИ//
        //***********//
        private void Button10_Click_1(object sender, EventArgs e)
        {
            Form4 f = new Form4();
            f.ShowDialog();
        }

        //******************************//
        //ДОБАВЛЕНИЕ СВЕДЕНИЙ О ЧЕЛОВЕКЕ//
        //******************************//
        private void Button15_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.ShowDialog();
        }
    }
}