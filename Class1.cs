using System;
using Microsoft.Data.SqlClient;

namespace AdoNetHomework
{
    class Program
    {
        // Строка подключения. Замените "Server=localhost" на имя вашего сервера.
        // Если используете LocalDB: "Server=(localdb)\\mssqllocaldb;Database=CollegeDB;Trusted_Connection=True;"
        // Если используете полный SQL Server: "Server=YourServerName;Database=CollegeDB;Trusted_Connection=True;TrustServerCertificate=True;"
        static string connectionString = "Server=localhost;Database=CollegeDB;Trusted_Connection=True;TrustServerCertificate=True;";

        // Задание 5: Функция Main с меню ввода-вывода
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Управление колледжем ===");
                Console.WriteLine("1. Показать всех студентов (ShowAllStudents)");
                Console.WriteLine("2. Найти студента по имени (FindStudentByName)");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowAllStudents();
                        break;
                    case "2":
                        Console.Write("Введите имя для поиска: ");
                        string name = Console.ReadLine();
                        FindStudentByName(name);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный ввод.");
                        break;
                }

                Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                Console.ReadKey();
            }
        }

        // Задание 6: Функция ShowAllStudents()
        static void ShowAllStudents()
        {
            Console.WriteLine("\n--- Список всех студентов ---");

            // Используем конструкцию using для автоматического закрытия соединения
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string sql = "SELECT Id, FirstName, LastName, GroupId FROM Students";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (!reader.HasRows)
                            {
                                Console.WriteLine("Студентов нет в базе.");
                                return;
                            }

                            // Форматированный вывод
                            Console.WriteLine($"{"ID",-5} {"Имя",-15} {"Фамилия",-15} {"Группа ID",-10}");
                            Console.WriteLine(new string('-', 50));

                            while (reader.Read())
                            {
                                int id = (int)reader["Id"];
                                string firstName = reader["FirstName"].ToString();
                                string lastName = reader["LastName"].ToString();
                                int groupId = (int)reader["GroupId"];

                                Console.WriteLine($"{id,-5} {firstName,-15} {lastName,-15} {groupId,-10}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка подключения: {ex.Message}");
                }
            }
        }

        // Задание 6: Функция FindStudentByName(string name)
        static void FindStudentByName(string name)
        {
            Console.WriteLine($"\n--- Поиск студента по имени: {name} ---");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    // Используем параметризованный запрос для защиты от SQL-инъекций
                    string sql = "SELECT Id, FirstName, LastName FROM Students WHERE FirstName = @Name";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        // Добавляем параметр
                        command.Parameters.AddWithValue("@Name", name);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    Console.WriteLine($"Найден: ID={reader["Id"]}, Имя={reader["FirstName"]}, Фамилия={reader["LastName"]}");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Студент с таким именем не найден.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
        }
    }
}