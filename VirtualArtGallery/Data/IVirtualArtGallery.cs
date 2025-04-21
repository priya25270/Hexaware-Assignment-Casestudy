using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualArtGallery.Models;

namespace VirtualArtGallery.Data
{
    public interface IVirtualArtGallery
    {
        bool AddArtwork(Artwork artwork);
        bool UpdateArtwork(Artwork artwork);
        bool RemoveArtwork(int artworkID);
        Artwork GetArtworkById(int artworkID);
        List<Artwork> SearchArtworks(string keyword);
        bool AddArtworkToFavorite(int userId, int artworkId);
        bool RemoveArtworkFromFavorite(int userId, int artworkId);
        List<Artwork> GetUserFavoriteArtworks(int userId);
        bool AddArtist(Artist artist);
        bool AddUser(User user);
        bool AddGallery(Gallery gallery);
        bool LinkArtworkToGallery(int artworkId, int galleryId);
        
        
    }
}
