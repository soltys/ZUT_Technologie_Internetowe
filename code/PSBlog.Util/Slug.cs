using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
namespace PSBlog.Util
{
    public class Slug
    {
        public static string GenerateSlug(string phrase)
        {
            string str = phrase;
            // invalid chars           
            str = Regex.Replace(str, @"[^A-Za-z0-9\s-]", "");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim 
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            str = Regex.Replace(str, @"\s", "_"); // hyphens   
            return str;
        }

       
    }
}
