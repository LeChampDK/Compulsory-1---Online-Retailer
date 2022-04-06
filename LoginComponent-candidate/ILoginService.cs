using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginComponent
{
    public interface ILoginService {

        //will return true if the passwords hashed value is the same that is
        //stored in the database with the email 
        //false if not 
        //parameters must be validated and check that any value isn't null if so throw an Exception
        bool Login(string email, string password);

        //should generate a unique value and generate a salted password with the hashing method. 
        //then store these 3 values in the database
        //parameters must be validated and check that any value isn't null if so throw an Exception
        bool CreateLogin (string email, string password);

        //should be able to check if the oldPasswords hashed value matches the currently stored hash 
        //if so should update the old value with the newPassword hashed value and return true 
        //else return false 
        //parameters must be validated and check that any value isn't null if so throw an Exception
        bool UpdateLogin(string email, string newPassword, string oldPassword);

        //should generate a random salt and hash the password with it. then return the hash and seed 
        //parameters must be validated and check that any value isn't null if so throw an Exception
        HashAndSalt Hashing(string password);

        //should generate a salted hash value from the paramaters provided then return the hashed value as a string 
        //parameters must be validated and check that any value isn't null if so throw an Exception
        string ReHashing (string password, string salt);
    }
}
