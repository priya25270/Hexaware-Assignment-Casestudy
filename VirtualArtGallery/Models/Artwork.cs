using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualArtGallery.Models
{
    public class Artwork
    {
        public int ArtworkID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public string Medium { get; set; }
        public string ImageURL { get; set; }
        public int ArtistID { get; set; }

        public Artwork() { }

        public Artwork(int id, string title, string desc, DateTime date, string medium, string url, int artistId)
        {
            ArtworkID = id;
            Title = title;
            Description = desc;
            CreationDate = date;
            Medium = medium;
            ImageURL = url;
            ArtistID = artistId;
        }
    }
}
