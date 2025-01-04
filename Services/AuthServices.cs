using Expenses_tracker.Models;
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
        public const string FilePath = "C:\\Users\\sthar\\OneDrive\\Desktop\\Expenses-tracker\\Database\\users.json";  // Path to store user data as JSON
        
        // Load users from JSON file
        public List<User> FetchAllUsers()
        {
            if (!File.Exists(FilePath))
                return new List<User>();  // Return an empty list if no users exist

            var json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
        }

        // Save users to JSON file
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

            // Validate password
            return ValidatePassword(password, user.Password);
        }
    }
}
