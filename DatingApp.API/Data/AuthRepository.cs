using System;
using System.Threading.Tasks;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _DataContext;
        public AuthRepository(DataContext DataContext)
        {
            _DataContext = DataContext;
        }

        public async Task<User> Login(string username, string password)
        {
            User user = await _DataContext.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
                return null;

            if (!VerifyPassword(password, user.PasswordHash, user.PasswordSalt))
                return null;
            else
                return user;
        }

        public bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            // Using the saved salt, create a new hmac object and compute the user's hash. Should match what is in database, return true.
            using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                byte[] submittedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < submittedHash.Length; i++)
                {
                    if (submittedHash[i] != passwordHash[i])
                        return false;
                }
            }

            return true;
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash;
            byte[] passwordSalt;

            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _DataContext.Users.AddAsync(user);
            await _DataContext.SaveChangesAsync();

            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key; // store the random key as our Salt.
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)); // hash our password but first convert it to a byte array.

            }
        }

        public async Task<bool> UserExists(string username)
        {
            // Return true if we find matching username. Else return false.
            return (await _DataContext.Users.AnyAsync(u => u.Username == username));
        }
    }
}