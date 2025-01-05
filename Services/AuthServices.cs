using Expenses_tracker.Models;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;



namespace Expenses_tracker.Services
{
    public class AuthServices
    {
        public const string FilePath = "C:\\Users\\sthar\\OneDrive\\Desktop\\Expenses-tracker\\Database\\users.json";
        //public const string FilePath = "C:\\Users\\CHME\\Desktop\\Expenses tracker\\Database\\users.json";

        // Load users from JSON file
        public List<User> FetchAllUsers()
        {
            if (!File.Exists(FilePath))
                return new List<User>();

            var json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
        }

        public void SaveUsers(List<User> users)
        {
            var json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FilePath, json);
        }

        // Hash the password using SHA-256
        public string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);  // Return hashed password
        }

        // Validate the password by comparing hashed versions
        public bool ValidatePassword(string inputPassword, string storedPassword)
        {
            var hashedInputPassword = HashPassword(inputPassword);
            return hashedInputPassword == storedPassword;
        }

        // Register a new user
        public async Task<bool> RegisterUser(string username, string password, string type)
        {
            try
            {
                var users = FetchAllUsers();

                if (users.Any(u => u.Username == username))
                {
                    return false; // User already exists
                }

                var hashedPassword = HashPassword(password);
                var newUser = new User
                {
                    Username = username,
                    Password = hashedPassword,
                    type = type
                };

                users.Add(newUser);
                SaveUsers(users);

                return true;
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                Console.WriteLine($"Error during user registration: {ex.Message}");
                return false;
            }
        }


        // Authenticate a user
        public async Task<bool> AuthenticateUser(string username, string password)
        {
            var users = FetchAllUsers();

            // Simulate database query delay
            await Task.Delay(500);

            var user = users.FirstOrDefault(u => u.Username == username);

            if (user == null)
            {
                return false; // User not found
            }
            StaticValue.UserId = user.Id;
            //StaticValue.TotalBalance = user.TotalBalance;

            // Validate password
            return ValidatePassword(password, user.Password);
        }
        public async Task<bool> AddTransaction(string userId, string title, decimal amount, string notes, string tags, bool isIncome)
        {
            // Read existing data
            var users = await ReadUsersFromFile();

            var user = users.Find(u => u.Id == userId);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            Transactions transaction = new Transactions
            {
                Title = title,
                Amount = amount,
                Notes = notes,
                Tags = tags,
                Date = DateTime.Now.ToString("yyyy-MM-dd"),
                Type = isIncome ? "Income" : "Expense"
            };

            user.Transactions.Add(transaction);

            user.TotalBalance += isIncome ? amount : -amount;

            await WriteUsersToFile(users);

            return true;
        }
        public async Task<bool> AddDebt(string userId, decimal amount, string notes, string Source, string tags, DateOnly dueDate, bool IsPaid)
        {

            var users = await ReadUsersFromFile();

            var user = users.Find(u => u.Id == userId);

            if (user == null)
            {
                throw new Exception("User not found.");
            }

            DebtModels debtModels = new DebtModels
            {
                Amount = amount,
                Notes = notes,
                Tags = tags,
                DueDate = dueDate,
                Source = Source,
                IsPaid = IsPaid
            };

            user.Debts.Add(debtModels);

            await WriteUsersToFile(users);

            return true;
        }
        public static async Task WriteUsersToFile(List<User> users)
        {
            var json =JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(FilePath, json);
        }
        public static async Task<List<User>> ReadUsersFromFile()
        {
            if (!File.Exists(FilePath))
            {
                return new List<User>();
            }

            var json = await File.ReadAllTextAsync(FilePath);
            return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
        }
        public async Task UpdateUserDebt(string userId, DebtModels updatedDebt)
        {
            var users = await ReadUsersFromFile();
            var user = users.FirstOrDefault(u => u.Id == userId);

            if (user != null)
            {
                var debt = user.Debts.FirstOrDefault(d => d.Id == updatedDebt.Id);
                if (debt != null)
                {
                    if (!debt.IsPaid && updatedDebt.IsPaid)
                    {
                        // Deduct the debt amount from the user's total balance
                        user.TotalBalance -= debt.Amount;
                    }

                    // Update the debt
                    debt.IsPaid = updatedDebt.IsPaid;

                    // Save changes to file or database
                    await WriteUsersToFile(users);
                }
            }
        }


    }
}
