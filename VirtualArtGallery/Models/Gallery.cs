using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualArtGallery.Models
{
    public class Gallery
    {
        public int GalleryID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int CuratorArtistID { get; set; }
        public string OpeningHours { get; set; }

        public Gallery() { }

        public Gallery(int id, string name, string desc, string loc, int curatorId, string hours)
        {
            GalleryID = id;
            Name = name;
            Description = desc;
            Location = loc;
            CuratorArtistID = curatorId;
            OpeningHours = hours;
        }
    }
}
