using System;
// This is local
namespace cinema
{
    class Program
    {
        public static void Main()
        {
            bool pw = true;
            string pw_strenght = "[Missing]                     ";
            while (pw == true)
            {
                // let's you put in a password and checks if it has an digit and a uppercase letter in it.
                Console.Write(pw_strenght);
                int digit = 0, letter = 0;
                Console.Write("Give an 8 or longer password with a uppercase letter and a Number: ");
                string password = Console.ReadLine();
                for (int c = 0; c < password.Length; c++)
                {
                    // check uppercase letter
                    if (char.IsUpper(password, c))
                    {
                        letter = 1;
                        pw_strenght = "[Missing a Digit]               ";
                    }
                    // check digit 
                    if (char.IsDigit(password, c))
                    {
                        digit = 1;
                        pw_strenght = "[Missing a Uppercase Letter]  ";
                    }
                }
                // checks if the password is 8 or longer
                if (password.Length < 8)
                {
                    pw = true;
                    pw_strenght = "[your password is to short]    ";

                }
                // check if you are sure you want to use this as your password 
                if (password.Length >= 8 && (digit == 1 && letter == 1))
                {
                    Console.WriteLine("The Password is strong.");
                    Console.Write("Are you sure you want to use this password?     (y) or (n): ");
                    string check = Console.ReadLine();
                    if (check == "y")
                    {
                        pw = false;
                    }
                    else
                    {
                        pw = true;
                        pw_strenght = "[Missing Digit and Uppercase] ";

                    }
                }
            }
            // Give an account a role, later on we can put varriables which are only true for a specific account role
            int Account_Number = 0;
            string Account_Role = "";
            switch (Account_Number)
            {
                case 1:
                    Account_Role = "Guest";
                    break;

                case 2:
                    Account_Role = "User";
                    break;

                case 3:
                    Account_Role = "Employe";
                    break;

                case 4:
                    Account_Role = "Moderator";
                    break;
            }

        }
    }
}