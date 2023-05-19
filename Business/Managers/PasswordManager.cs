using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public static class PasswordManager
    {
        // The class generates and evaluates passwords


        static char[] numbers = {'0','1','2','3','4','5','6','7','8','9'};
        static char[] symbols = { '!', '.', ',', '?', '@', '#', '$', '%', '^', '&','*','(',')','_','-','/','\\','|','+',':',';','"','\'','№','=','`','~'};
        static string letters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

        // Returns a numeric evaluation (0-100) of the strength of the given password
        public static int Evaluate(string password)
        {
            int score = 100;
            int letterCount = 0;
            int symbolCount = 0;
            int numberCount = 0;
            int prevNum = 0;
            int highestSequence = 0;
            int sequence = 0;
            
            if (password.Length <= 5) score = score - 80;
            if (password.Length >= 10) score = score + 15;
            if (password.Length >= 14) score = score + 20;
            if (password.ToLower() == password) score = score - 10;
            if (password.ToUpper() == password) score = score - 10;
            foreach (char symbol in password)
            {
                if (numbers.Contains(symbol) == true)
                {
                    numberCount = numberCount + 1;
                    if (prevNum == int.Parse(symbol.ToString()) + 1 || prevNum == int.Parse(symbol.ToString()) - 1)
                    {
                        sequence++;
                    }
                    else
                    {
                        sequence = 0;
                    }
                    if (sequence > highestSequence) highestSequence = sequence;
                    prevNum = int.Parse(symbol.ToString());
                }
                else
                {
                    if (symbols.Contains(symbol) == true) symbolCount = symbol + 1;
                    else
                    {
                        letterCount = letterCount + 1;
                    }

                }
            }
            if (highestSequence >= 4) score = score - 70;
            if (letterCount == 0) score = score - 40;
            if (numberCount == 0) score = score - 40;
            if (symbolCount == 0) score = score - 40;
            if (score > 100) score = 100;
            if (score < 0) score = 0;
            return score;
            
        }

        // Returns a strong (100/100) password
        public static string GeneratePassword(string forbidden,int length)
        {
            int maixmumSequrity = 100;
            if (length <= 5) maixmumSequrity = 0;

            char[] nums = numbers.Where(x => forbidden.Contains(x) == false).ToArray();
            char[] symbs = symbols.Where(x => forbidden.Contains(x) == false).ToArray();
            string lett = letters.Where(x => forbidden.Contains(x) == false).ToString();

            string password = "";
            Random random = new Random();
            do
            {
                password = "";
                for (int i = 0; i < length; i++)
                {
                    int arr = random.Next(1, 4);
                    switch (arr)
                    {
                        case 1:
                            password = password + nums[random.Next(nums.Length-1)];
                            break;
                        case 2:
                            password = password + symbs[random.Next(symbs.Length - 1)];
                            break;
                        case 3:
                            if (random.Next(1) == 0) password = password + lett[random.Next(lett.Length - 1)];
                            else password = password + lett[random.Next(lett.Length - 1)].ToString().ToUpper();
                            break;
                    }
                }
            }
            while (Evaluate(password) != maixmumSequrity);
            return password;
        }
    }
}
