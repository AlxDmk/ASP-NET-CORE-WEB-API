using System;
using System.Text;
using System.Text.RegularExpressions;

namespace ASP_NET_CORE_WEB_API.Lesson2
{
    public class Lesson2
    {
        static void Main(string[] args)
        {
            string str = "    Первое   предложние   Второе предложение   Третье   предложение  ";
            Regex regex = new Regex(@"[ ]{2,}", RegexOptions.None);
            string result = regex.Replace(str, " ");
            
            Regex regex1 = new Regex(@"([ ])([А-Я])");
            result = regex1.Replace(new StringBuilder(result.Trim()).Append(".").ToString(), ". $2");

            Console.WriteLine(result);

        }
    }
}