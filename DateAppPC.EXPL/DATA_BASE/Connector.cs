using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;
using DateAppPC.EXPL.OBJECTS;
using Npgsql;

namespace DateAppPC.EXPL.DATA_BASE {
    public class Connector {
        private NpgsqlConnection NpgsqlConnection { get; set; }
        
        private const string Server = "localhost";
        private const string Port = "5432";
        private const string Database = "postgres";
        private const string UserId = "postgres";
        private const string Password = "1234";
        private void Connect() {
            try {
                var connectionCommand = 
                    $"Server={Server};Port={Port};Database={Database};User Id={UserId};Password={Password}";
                NpgsqlConnection = new NpgsqlConnection(connectionCommand);
                NpgsqlConnection.Open();
            }
            catch (Exception e) {
                MessageBox.Show($"{e}");
            }
        }
        public void ChangeUserData(User user) {
            try {
                if (NpgsqlConnection == null) Connect();

                var sqlSetCommand = 
                                     "UPDATE users SET " +
                                    $"user_name = '{user.Name}', " +
                                    $"user_surname = '{user.Surname}', " +
                                    $"user_patronymic = '{user.Patronymic}', " +
                                    $"user_age = '{user.Age}', " +
                                    $"user_sex = '{user.Sex.ToString()}', " +
                                    $"user_temperament = '{user.Temperament}', " +
                                    $"user_character = '{user.Character}', " +
                                    $"user_login = '{user.Login}', " +
                                    $"user_password = '{user.Password}', " +
                                    $"user_nick = '{user.Nick}', " +
                                    $"user_favorite = '{string.Join(" ",user.Favorite)}', " +
                                    $"user_picture = '{user.ProfileImage}'," +
                                    $"user_role = '{user.Role}'," +
                                    $"user_interests = '{user.Interests}'," +
                                    $"user_info = '{user.Info}'," +
                                    $"user_type = '{user.Type}' " +
                                    $"WHERE  user_id = '{user.UserId}'";
                
                SendCommand(sqlSetCommand);
                Disconnect();
            }
            catch (Exception e) {
                MessageBox.Show($"{e}");
            }
        }
        public void SaveUserData(User user) {
            try {
                if (NpgsqlConnection == null) Connect();
                
                var sqlSaveCommand = $"INSERT INTO users values ('{user.UserId}','{user.Name}'," +
                                             $"'{user.Surname}','{user.Patronymic}','{user.Age}'," +
                                             $"'{user.Sex.ToString()}','{user.Temperament}','{user.Character}'," +
                                             $"'{user.Login}','{user.Password}','{user.Nick}'," +
                                             $"'{string.Join(" ",user.Favorite)}'," +
                                             $"'{user.ProfileImage}','{user.Role}','{user.Interests}','{user.Info}'," +
                                             $"'{user.Type}')";
                
                SendCommand(sqlSaveCommand);
                Disconnect();
            }
            catch (Exception e) {
                MessageBox.Show($"{e}");
            }
        }
        public List<User> GetUsersData() {
            try {
                var userList = new List<User>();
                if (NpgsqlConnection == null) Connect();
                
                const string sqlGetCommand = "SELECT * FROM users";
                
                var command         = new NpgsqlCommand(sqlGetCommand);
                command.Connection  = NpgsqlConnection;
                command.CommandType = CommandType.Text;
                
                var dataAdapter = new NpgsqlDataAdapter(command);
                var dataSet     = new DataSet();
                dataAdapter.Fill(dataSet);

                command.ExecuteNonQuery();
                command.Dispose();
                
                Disconnect();

                const int usersTable = 0;
                
                var sqlData = dataSet.Tables[usersTable].Rows;
                
                if (sqlData.Count <= 0) return userList;
                for (var i = 0; i < sqlData.Count; i++) {
                    userList.Add(ToUserData(sqlData[i]));
                }

                return userList;
            }
            catch (Exception e) {
                MessageBox.Show($"{e}");
            }

            return null;
        }
        private static User ToUserData(DataRow row) {
            return new User {
                UserId       = (int)row["user_id"],
                Name         = row["user_name"].ToString(),
                Nick         = row["user_nick"].ToString(),                
                Surname      = row["user_surname"].ToString(),
                Patronymic   = row["user_patronymic"].ToString(),
                Age          = (int)row["user_age"],
                Login        = row["user_login"].ToString(),
                Password     = row["user_password"].ToString(),    
                
                Info         = row["user_info"].ToString(),
                Interests    = row["user_interests"].ToString(),
                ProfileImage = row["user_picture"].ToString(),
                Favorite     = row["user_favorite"].ToString()!.Split(" ").ToList(),          
                
                Sex = row["user_sex"].ToString() switch {
                    "Man"   => Sex.Man,
                    "Woman" => Sex.Woman,
                    _       => Sex.Man
                },
                
                Temperament  = row["user_temperament"].ToString(),
                Character    = row["user_character"].ToString(),
                Type         = row["user_type"].ToString(),
                Role         = row["user_role"].ToString()
            };
        }
        private void SendCommand(string sqlCommand) {
            var command         = new NpgsqlCommand(sqlCommand);
            command.Connection  = NpgsqlConnection;
            command.CommandType = CommandType.Text;

            command.ExecuteNonQuery();
            command.Dispose();
        }
        private void Disconnect() {
            try {
                NpgsqlConnection.Close();
            }
            catch (Exception e) {
                MessageBox.Show($"{e}");
            }
        }
    }
}