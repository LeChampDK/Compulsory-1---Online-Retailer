using System.Globalization;
using System.Text.RegularExpressions;

namespace LoginComponent.util
{
    public class EmailAndPasswordValidator
    {
        public bool IsEmailValid(string email)
        {
            if(email == null)
            {
                throw new ArgumentNullException("Email invalid");
            }

            if(email.Length < 5)
            {
                throw new ArgumentException("Email invalid");
            }

            // the following is modified from Microsoft code examples
            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                throw new InvalidOperationException("Something went wrong");
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException("Email invalid");
            }

            try
            {
                if (!Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
                {
                    throw new ArgumentException("Email invalid");
                }
                return true;
            }
            catch (RegexMatchTimeoutException)
            {
                throw new InvalidOperationException("Something went wrong");
            }


        }

        public bool IsPasswordValid(string password)
        {
            if (password == null)
            {
                throw new ArgumentNullException("Password invalid");
            }

            if (password.Length < 8 
                || !password.Any(char.IsDigit) 
                || !password.Any(char.IsLower) 
                || !password.Any(char.IsUpper))
            {
                throw new ArgumentException("Password invalid");
            }

            return true;
        }
    }
}
