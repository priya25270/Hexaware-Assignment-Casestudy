using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualArtGallery.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public string ProfilePicture { get; set; }
        public List<int> FavoriteArtworks { get; set; }

        public User()
        {
            FavoriteArtworks = new List<int>();
        }

        public User(int id, string uname, string pwd, string email, string fname, string lname, DateTime dob, string pic)
        {
            UserID = id;
            Username = uname;
            Password = pwd;
            Email = email;
            FirstName = fname;
            LastName = lname;
            DOB = dob;
            ProfilePicture = pic;
            FavoriteArtworks = new List<int>();
        }
    }
}
