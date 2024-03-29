﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using MySql.Data.MySqlClient;
using System.Data;


namespace WpfApp1
{
    public partial class Table1 : Page
    {
        MySqlConnection con = new MySqlConnection("server=localhost;port=3306;user=root;password=;database=store");
        private MySqlCommand cmd;
        private MySqlDataAdapter da;
        private DataTable table;
        public Table1()
        {
            //при відкритті вікна відбувається ініціалізація компонентів, та вивід записів в таблицю
            InitializeComponent();
            select_data();
        }

        public void select_data()
        {
            //команда для виводу записів з таблиці
            string select_sql = "select * from item";
           
            cmd = new MySqlCommand(select_sql, con);
            cmd.Connection = con;
            cmd.CommandText = select_sql;
            da = new MySqlDataAdapter(cmd);
            table = new DataTable();
            da.Fill(table);
            Data_Grid_clients.ItemsSource = table.DefaultView;
           con.Close();
            
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //insert click відкриває вікно в якому ми додаємо запис в таблицю
            insert_item insert_wind = new insert_item();
            insert_wind.Show();
        }
        private void DELETE_Click(object sender, RoutedEventArgs e)
        {
            Delete_Item delete = new Delete_Item();
            delete.Show();
        }
        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            //подія , яка оновляє таблицю
            select_data();
        }

        private void Update_Click_1(object sender, RoutedEventArgs e)
        {
            //update click, відкриває вікно в якому ми можемо оновити запис в таблиці 
            Update_item qwind = new Update_item();
            qwind.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //повернення до вибору таблиці
            NavigationService.GoBack();
        }
        public static void Display_and_search()
        {

        }

        private void search_Click(object sender, RoutedEventArgs e)
        {
           
            try
            {
                string select_sql = "select * from item where ID ='" + searchBOX.Text + "' or Name like '%" + searchBOX.Text + "%' or License like '%" + searchBOX.Text + "%';";
                cmd = new MySqlCommand(select_sql, con);
                cmd.Connection = con;
                cmd.CommandText = select_sql;
                da = new MySqlDataAdapter(cmd);
                table = new DataTable();
                da.Fill(table);
                Data_Grid_clients.ItemsSource = table.DefaultView;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

       
    }
}
