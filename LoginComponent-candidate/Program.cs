using LoginComponent;
using LoginComponent.util;
using System.Security.Cryptography;

DAO db = new DAO();
LoginService ls = new LoginService(db, new EmailAndPasswordValidator());

/*
// Testing hashing functionality
HashAndSalt hashCakeWithSalt = ls.Hashing("ultra_safe_password");
Console.WriteLine("Testing hash password with random salt:"); 
Console.WriteLine($"salt for password is: " + hashCakeWithSalt.Salt);
Console.WriteLine($"hash for password is: " + hashCakeWithSalt.Hash + "\n");

string moreHash = ls.ReHashing("ultra_safe_password", hashCakeWithSalt.Salt);
Console.WriteLine("Testing if rehash generates same password using same salt:");
Console.WriteLine($"hash for password is: " + moreHash);
*/


// Testing login functionality
string createLoginStatus = ls.CreateLogin("hey@email.dk", "123abcrff") ? "Success!" : "Failed!";
Console.WriteLine($"Testing create login --> {createLoginStatus}");

string loginStatus = ls.Login("hey@email.dk", "123abcABC") ? "Success!" : "Failed!";
Console.WriteLine($"Testing login --> {loginStatus}");

string updateLoginStatus = ls.UpdateLogin("hey@email.dk", "Esbjerg2022", "123abcABC") ? "Success!" : "Failed!";
Console.WriteLine($"Testing update login --> {updateLoginStatus}");

string newLoginStatus = ls.Login("hey@email.dk", "Esbjerg2022") ? "Success!" : "Failed!";
Console.WriteLine($"Testing updated login --> {newLoginStatus}");
