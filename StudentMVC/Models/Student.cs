using System;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Text;

namespace StudentMVC.Models
{
    public class Student
    {

        // Used by Db
        public long Id { get; set; }


        private string _key;

        public string key
        {
            get
            {
                if (_key == null)
                {
                    _key = Regex.Replace(Email.ToLower(), "[^a-z0-9]", "-");
                }

                return _key;
            }
            set
            {
                _key = value;
            }
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public bool Enrolled { get; set; }
        public DateTime CreatedAt { get; set; }

        public Student()
        {

        }

        public string Avatar()
        {

            // Create a new instance of the MD5CryptoServiceProvider object.  
            MD5 md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.  
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(Email));

            // Create a new Stringbuilder to collect the bytes  
            // and create a string.  
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data  
            // and format each one as a hexadecimal string.  
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            string HashedEmail = sBuilder.ToString();  // Return the hexadecimal string. 

            return $"https://secure.gravatar.com/avatar/{HashedEmail}?s=140&r=g&d=mm";
        }
    }
}
