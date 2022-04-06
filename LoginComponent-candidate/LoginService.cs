using LoginComponent.util;
using System.Security.Cryptography;

namespace LoginComponent
{
    public class LoginService : ILoginService
    {
        private DAO db;
        private EmailAndPasswordValidator validator;

        public LoginService(DAO db, EmailAndPasswordValidator validator)
        {
            this.db = db;
            this.validator = validator;
        }

        public bool Login(string email, string password)
        {
            validator.IsEmailValid(email);
            validator.IsPasswordValid(password);

            string salt = db.GetLoginPasswordSalt(email);
            string hashedPassword = ReHashing(password, salt);

            return db.Login(email, hashedPassword);
        }

        public bool CreateLogin(string email, string password)
        {
            validator.IsEmailValid(email);
            validator.IsPasswordValid(password);
            HashAndSalt hashAndSalt = Hashing(password);

            return db.CreateLogin(email, hashAndSalt);
        }

        public bool UpdateLogin(string email, string newPassword, string oldPassword)
        {
            validator.IsEmailValid(email);
            validator.IsPasswordValid(newPassword);
            bool isLoginValid = Login(email, oldPassword);
            HashAndSalt hashAndSalt = new HashAndSalt();
            hashAndSalt.Salt = db.GetLoginPasswordSalt(email);
            hashAndSalt.Hash = ReHashing(newPassword, hashAndSalt.Salt);

            return db.UpdateLogin(email, hashAndSalt.Hash);
        }

        public HashAndSalt Hashing(string password)
        {
            PasswordWithSaltHasher pwHasher = new PasswordWithSaltHasher();
            HashAndSalt hashAndSalt = pwHasher.HashWithRandomSalt(password, 64, SHA256.Create());

            return hashAndSalt;
        }

        public string ReHashing(string password, string salt)
        {
            PasswordWithSaltHasher pwHasher = new PasswordWithSaltHasher();
            HashAndSalt hashAndSalt = pwHasher.HashWithSalt(password, salt, SHA256.Create());

            return hashAndSalt.Hash;
        }
    }
}
