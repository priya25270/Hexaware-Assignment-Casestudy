using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualArtGallery.Models
{
    public class Artist
    {
        public int ArtistID { get; set; }
        public string Name { get; set; }
        public string Biography { get; set; }
        public DateTime BirthDate { get; set; }
        public string Nationality { get; set; }
        public string Website { get; set; }
        public string ContactInfo { get; set; }

        public Artist() { }

        public Artist(int id, string name, string bio, DateTime birth, string nationality, string site, string contact)
        {
            ArtistID = id;
            Name = name;
            Biography = bio;
            BirthDate = birth;
            Nationality = nationality;
            Website = site;
            ContactInfo = contact;
        }
    }
}
