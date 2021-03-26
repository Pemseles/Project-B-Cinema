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
                Console.Write(pw_strenght);
                int digit = 0, letter = 0;
                Console.Write("Give an 8 or longer password with a uppercase letter and a Number: ");
                string password = Console.ReadLine();
                for (int c = 0; c < password.Length; c++)
                {
                    if (char.IsUpper(password, c))
                    {
                        letter = 1;
                        pw_strenght = "[Missing a Digit]               ";
                    }
                    if (char.IsDigit(password, c))
                    {
                        digit = 1;
                        pw_strenght = "[Missing a Uppercase Letter]  ";
                    }
                }
                if (password.Length < 8)
                {
                    pw = true;
                    pw_strenght = "[your password is to short]    ";

                }
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
            
        }
    }
}