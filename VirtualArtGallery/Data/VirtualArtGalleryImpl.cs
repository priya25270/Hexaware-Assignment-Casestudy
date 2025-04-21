using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using VirtualArtGallery.Data;
using VirtualArtGallery.Models;
using exception;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace dao
{
    public class VirtualArtGalleryServiceImpl : IVirtualArtGallery
    {
        SqlConnection con = null;
        SqlCommand command = null;

        public bool AddArtwork(Artwork artwork)
        {
            string query = @"
                insert into Artwork ( Title, Description, CreationDate, Medium, ImageURL, ArtistID)
                values ( @title, @desc, @date, @medium, @url, @artistId)";
            try
            {
                using (con = DBUtility.GetConnection())
                {
                    command = new SqlCommand(query, con);
                    command.Parameters.AddWithValue("@title", artwork.Title);
                    command.Parameters.AddWithValue("@desc", artwork.Description);
                    command.Parameters.AddWithValue("@date", artwork.CreationDate);
                    command.Parameters.AddWithValue("@medium", artwork.Medium);
                    command.Parameters.AddWithValue("@url", artwork.ImageURL);
                    command.Parameters.AddWithValue("@artistId", artwork.ArtistID);
                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("SQL Error while adding artwork: " + ex.Message);
            }
        }

        public bool UpdateArtwork(Artwork artwork)
        {
            string query = @"
                UPDATE Artwork
                SET Title = @title,
                    Description = @desc,
                    CreationDate = @date,
                    Medium = @medium,
                    ImageURL = @url,
                    ArtistID = @artistId
                WHERE ArtworkID = @id";
            try
            {
                using (con = DBUtility.GetConnection())
                {
                    command = new SqlCommand(query, con);
                    command.Parameters.AddWithValue("@id", artwork.ArtworkID);
                    command.Parameters.AddWithValue("@title", artwork.Title);
                    command.Parameters.AddWithValue("@desc", artwork.Description);
                    command.Parameters.AddWithValue("@date", artwork.CreationDate);
                    command.Parameters.AddWithValue("@medium", artwork.Medium);
                    command.Parameters.AddWithValue("@url", artwork.ImageURL);
                    command.Parameters.AddWithValue("@artistId", artwork.ArtistID);
                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("SQL Error while updating artwork: " + ex.Message);
            }
        }

        public bool RemoveArtwork(int artworkID)
        {
            string query = "delete from Artwork where ArtworkID = @id";
            try
            {
                using (con = DBUtility.GetConnection())
                {
                    command = new SqlCommand(query, con);
                    command.Parameters.AddWithValue("@id", artworkID);
                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("SQL Error while removing artwork: " + ex.Message);
            }
        }

        public Artwork GetArtworkById(int artworkID)
        {
            string query = "select * from Artwork WHERE ArtworkID = @id";
            try
            {
                using (con = DBUtility.GetConnection())
                {
                    command = new SqlCommand(query, con);
                    command.Parameters.AddWithValue("@id", artworkID);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        return new Artwork(
                            reader.GetInt32(0),
                            reader.GetString(1),
                            reader.GetString(2),
                            reader.GetDateTime(3),
                            reader.GetString(4),
                            reader.GetString(5),
                            reader.GetInt32(6)
                        );
                    }
                }
                throw new ArtworkNotFoundException($"Artwork with ID {artworkID} not found.");
            }
            catch (SqlException ex)
            {
                throw new Exception("SQL Error while fetching artwork: " + ex.Message);
            }
        }

        public List<Artwork> SearchArtworks(string keyword)
        {
            string query = @"
                select * from Artwork
                where Title like @kw or Description like @kw";
            var results = new List<Artwork>();
            try
            {
                using (con = DBUtility.GetConnection())
                {
                    command = new SqlCommand(query, con);
                    command.Parameters.AddWithValue("@kw", $"%{keyword}%");
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        results.Add(new Artwork(
                            reader.GetInt32(0),
                            reader.GetString(1),
                            reader.GetString(2),
                            reader.GetDateTime(3),
                            reader.GetString(4),
                            reader.GetString(5),
                            reader.GetInt32(6)
                        ));
                    }
                }
                return results;
            }
            catch (SqlException ex)
            {
                throw new Exception("SQL Error while searching artworks: " + ex.Message);
            }
        }



        public bool AddArtworkToFavorite(int userId, int artworkId)
        {
            string query = @"
                insert into User_Favorite_Artwork (UserID, ArtworkID)
                values (@u, @a)";
            try
            {
                using (con = DBUtility.GetConnection())
                {
                    command = new SqlCommand(query, con);
                    command.Parameters.AddWithValue("@u", userId);
                    command.Parameters.AddWithValue("@a", artworkId);
                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("SQL Error while adding to favorites: " + ex.Message);
            }
        }

        public bool RemoveArtworkFromFavorite(int userId, int artworkId)
        {
            string query = @"
                delete from User_Favorite_Artwork
                where UserID = @u and ArtworkID = @a";
            try
            {
                using (con = DBUtility.GetConnection())
                {
                    command = new SqlCommand(query, con);
                    command.Parameters.AddWithValue("@u", userId);
                    command.Parameters.AddWithValue("@a", artworkId);
                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("SQL Error while removing from favorites: " + ex.Message);
            }
        }
        public bool AddUser(User user)
        {
            bool isAdded = false;

            string query = @"insert into UserAccount 
                    (Username, Password, Email, FirstName, LastName, DateOfBirth, ProfilePicture)
                     values (@Username, @Password, @Email, @FirstName, @LastName, @DateOfBirth, @ProfilePicture)";

            try
            {
                using (SqlConnection con = DBUtility.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Username", user.Username);
                        cmd.Parameters.AddWithValue("@Password", user.Password);
                        cmd.Parameters.AddWithValue("@Email", user.Email);
                        cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", user.LastName);
                        cmd.Parameters.AddWithValue("@DateOfBirth", user.DOB);
                        cmd.Parameters.AddWithValue("@ProfilePicture", string.IsNullOrWhiteSpace(user.ProfilePicture) ? (object)DBNull.Value : user.ProfilePicture);


                        int rowsAffected = cmd.ExecuteNonQuery();
                        isAdded = rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: SQL Error while adding user: " + ex.Message);
            }

            return isAdded;
        }

        public bool AddArtist(Artist artist)
        {
            string query = @"
        insert into Artist 
        ( Name, Biography, BirthDate, Nationality, Website, ContactInfo)
        values 
        ( @name, @bio, @dob, @nation, @web, @contact)";

            try
            {
                using (con = DBUtility.GetConnection())
                {
                    command = new SqlCommand(query, con);
                    command.Parameters.AddWithValue("@name", artist.Name);
                    command.Parameters.AddWithValue("@bio", artist.Biography);
                    command.Parameters.AddWithValue("@dob", artist.BirthDate);
                    command.Parameters.AddWithValue("@nation", artist.Nationality);
                    command.Parameters.AddWithValue("@web", artist.Website);
                    command.Parameters.AddWithValue("@contact", artist.ContactInfo);

                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("SQL Error while adding artist: " + ex.Message);
            }
        }

        public bool AddGallery(Gallery gallery)
        {
            bool isAdded = false;

            string query = @"insert into Gallery (Name, Description, Location, CuratorArtistID, OpeningHours)
                     values (@Name, @Description, @Location, @Curator, @OpeningHours)";

            try
            {
                using (SqlConnection con = DBUtility.GetConnection())
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Name", gallery.Name);
                    cmd.Parameters.AddWithValue("@Description", gallery.Description);
                    cmd.Parameters.AddWithValue("@Location", gallery.Location);
                    cmd.Parameters.AddWithValue("@Curator", gallery.CuratorArtistID);
                    cmd.Parameters.AddWithValue("@OpeningHours", gallery.OpeningHours);


                    int rows = cmd.ExecuteNonQuery();
                    isAdded = rows > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: SQL Error while adding gallery: " + ex.Message);
            }

            return isAdded;
        }

        public bool LinkArtworkToGallery(int artworkId, int galleryId)
        {
            bool linked = false;

            string query = @"insert into Artwork_Gallery (ArtworkID, GalleryID)
                     values (@ArtworkID, @GalleryID)";

            try
            {
                using (SqlConnection con = DBUtility.GetConnection())
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ArtworkID", artworkId);
                    cmd.Parameters.AddWithValue("@GalleryID", galleryId);


                    int rows = cmd.ExecuteNonQuery();
                    linked = rows > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: SQL Error while linking artwork to gallery: " + ex.Message);
            }

            return linked;
        }

        public List<Artwork> GetUserFavoriteArtworks(int userId)
        {
            string query = @"
                select A.* 
                from Artwork A
                join User_Favorite_Artwork UFA
                  on A.ArtworkID = UFA.ArtworkID
                where UFA.UserID = @u";
            var favs = new List<Artwork>();
            try
            {
                using (con = DBUtility.GetConnection())
                {
                    command = new SqlCommand(query, con);
                    command.Parameters.AddWithValue("@u", userId);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        favs.Add(new Artwork(
                            reader.GetInt32(0),
                            reader.GetString(1),
                            reader.GetString(2),
                            reader.GetDateTime(3),
                            reader.GetString(4),
                            reader.GetString(5),
                            reader.GetInt32(6)
                        ));
                    }
                }
                if (favs.Count == 0)
                    throw new UserNotFoundException($"No favorites found for user ID {userId}.");
                return favs;
            }
            catch (SqlException ex)
            {
                throw new Exception("SQL Error while fetching favorites: " + ex.Message);
            }
        }
    }
}
