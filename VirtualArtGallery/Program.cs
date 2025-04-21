using System;
using System.Collections.Generic;
using dao;                              
using VirtualArtGallery.Models; 
using VirtualArtGallery.Data;
using exception;
using System.Text.RegularExpressions;

namespace VirtualArtGallery.mainmod
{
    class Program
    {
        static IVirtualArtGallery service = new VirtualArtGalleryServiceImpl();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Virtual Art Gallery");
                Console.WriteLine("1. Add Artist");
                Console.WriteLine("2. Add Artwork");
                Console.WriteLine("3. Update Artwork");
                Console.WriteLine("4. Remove Artwork");
                Console.WriteLine("5. Get Artwork by ID");
                Console.WriteLine("6. Search Artworks");
                Console.WriteLine("7. Add Artwork to Favorites");
                Console.WriteLine("8. Remove Artwork from Favorites");
                Console.WriteLine("9. View User Favorites");
                Console.WriteLine("10. Add User");
                Console.WriteLine("11. Add Gallery");
                Console.WriteLine("12. Assign Artwork to Gallery");
                Console.WriteLine("13. Exit");
                Console.Write("Choose an option: ");

                int choice = Convert.ToInt32(Console.ReadLine());

                try
                {
                    switch (choice)
                    {
                        case 1:
                            {
                                var artist = new Artist();
                                Console.Write("Name: ");
                                artist.Name = Console.ReadLine();
                                Console.Write("Biography: ");
                                artist.Biography = Console.ReadLine();
                                Console.Write("Birth Date (yyyy-MM-dd): ");
                                artist.BirthDate = DateTime.Parse(Console.ReadLine());
                                Console.Write("Nationality: ");
                                artist.Nationality = Console.ReadLine();
                                Console.Write("Website: ");
                                artist.Website = Console.ReadLine();
                                Console.Write("Contact Information: ");
                                artist.ContactInfo = Console.ReadLine();

                                bool addedArtist = service.AddArtist(artist);
                                Console.WriteLine(addedArtist
                                    ? "Artist added successfully."
                                    : "Failed to add artist.");
                                break;
                            }

                        case 2:
                            {
                                var art = new Artwork();
                                Console.Write("Title: ");
                                art.Title = Console.ReadLine();
                                Console.Write("Description: ");
                                art.Description = Console.ReadLine();
                                Console.Write("Creation Date (yyyy-MM-dd): ");
                                art.CreationDate = DateTime.Parse(Console.ReadLine());
                                Console.Write("Medium: ");
                                art.Medium = Console.ReadLine();
                                Console.Write("Image URL: ");
                                art.ImageURL = Console.ReadLine();
                                Console.Write("Artist ID: ");
                                art.ArtistID = Convert.ToInt32(Console.ReadLine());

                                bool added = service.AddArtwork(art);
                                Console.WriteLine(added
                                    ? "Artwork added successfully."
                                    : "Failed to add artwork.");
                                break;
                            }
                        

                        case 3:
                            {
                                Console.Write("Artwork ID to update: ");
                                int updId = Convert.ToInt32(Console.ReadLine());
                                var existing = service.GetArtworkById(updId);

                                Console.Write("New Title: ");
                                existing.Title = Console.ReadLine();
                                Console.Write("New Description: ");
                                existing.Description = Console.ReadLine();
                                Console.Write("New Creation Date (yyyy-MM-dd): ");
                                existing.CreationDate = DateTime.Parse(Console.ReadLine());
                                Console.Write("New Medium: ");
                                existing.Medium = Console.ReadLine();
                                Console.Write("New Image URL: ");
                                existing.ImageURL = Console.ReadLine();
                                Console.Write("New Artist ID: ");
                                existing.ArtistID = Convert.ToInt32(Console.ReadLine());

                                bool updated = service.UpdateArtwork(existing);
                                Console.WriteLine(updated
                                    ? "Artwork updated successfully."
                                    : "Failed to update artwork.");
                                break;
                            }

                        case 4:
                            {
                                Console.Write("Artwork ID to remove: ");
                                int remId = Convert.ToInt32(Console.ReadLine());
                                bool removed = service.RemoveArtwork(remId);
                                Console.WriteLine(removed
                                    ? "Artwork removed successfully."
                                    : "Failed to remove artwork.");
                                break;
                            }

                        case 5:
                            {
                                Console.Write("Enter Artwork ID: ");
                                int getId = Convert.ToInt32(Console.ReadLine());
                                var art = service.GetArtworkById(getId);
                                Console.WriteLine(
                                    $"ID: {art.ArtworkID}\n" +
                                    $"Title: {art.Title}\n" +
                                    $"Description: {art.Description}\n" +
                                    $"Date: {art.CreationDate:yyyy-MM-dd}\n" +
                                    $"Medium: {art.Medium}\n" +
                                    $"Image URL: {art.ImageURL}\n" +
                                    $"Artist ID: {art.ArtistID}");
                                break;
                            }

                        case 6:
                            {
                                Console.Write("Keyword: ");
                                string kw = Console.ReadLine();
                                List<Artwork> found = service.SearchArtworks(kw);
                                Console.WriteLine($"\nFound {found.Count} artwork(s):");
                                foreach (var a in found)
                                    Console.WriteLine($"- ID {a.ArtworkID}: {a.Title}");
                                break;
                            }

                        case 7:
                            {
                                Console.Write("User ID: ");
                                int uid = Convert.ToInt32(Console.ReadLine());
                                Console.Write("Artwork ID: ");
                                int aid = Convert.ToInt32(Console.ReadLine());
                                bool favAdded = service.AddArtworkToFavorite(uid, aid);
                                Console.WriteLine(favAdded
                                    ? "Added to favorites."
                                    : "Failed to add to favorites.");
                                break;
                            }

                        case 8:
                            {
                                Console.Write("User ID: ");
                                int uid = Convert.ToInt32(Console.ReadLine());
                                Console.Write("Artwork ID: ");
                                int aid = Convert.ToInt32(Console.ReadLine());
                                bool favRemoved = service.RemoveArtworkFromFavorite(uid, aid);
                                Console.WriteLine(favRemoved
                                    ? "Removed from favorites."
                                    : "Failed to remove from favorites.");
                                break;
                            }

                        case 9:
                            {
                                Console.Write("User ID: ");
                                int uid = Convert.ToInt32(Console.ReadLine());
                                List<Artwork> favs = service.GetUserFavoriteArtworks(uid);
                                Console.WriteLine($"\nUser {uid} favorites:");
                                foreach (var f in favs)
                                    Console.WriteLine($"- ID {f.ArtworkID}: {f.Title}");
                                break;
                            }


                        case 10:
                            {
                                var user = new User();

                                Console.Write("Username: ");
                                user.Username = Console.ReadLine();

                                string password = "";
                                bool isValidPassword = false;

                                // Password validation loop
                                while (!isValidPassword)
                                {
                                    Console.Write("Password: ");
                                    password = Console.ReadLine();

                                    bool isLongEnough = password.Length >= 8;
                                    bool hasUppercase = Regex.IsMatch(password, "[A-Z]");
                                    bool hasDigit = Regex.IsMatch(password, @"\d");

                                    if (isLongEnough && hasUppercase && hasDigit)
                                    {
                                        isValidPassword = true;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Password is invalid. Requirements:");
                                        if (!isLongEnough) Console.WriteLine("- At least 8 characters.");
                                        if (!hasUppercase) Console.WriteLine("- At least one uppercase letter.");
                                        if (!hasDigit) Console.WriteLine("- At least one digit.");
                                    }
                                }

                                user.Password = password;

                                Console.Write("Email: ");
                                user.Email = Console.ReadLine();

                                Console.Write("First Name: ");
                                user.FirstName = Console.ReadLine();

                                Console.Write("Last Name: ");
                                user.LastName = Console.ReadLine();

                                Console.Write("Date of Birth (yyyy-MM-dd): ");
                                user.DOB = DateTime.Parse(Console.ReadLine());

                                Console.Write("Profile Picture URL (optional): ");
                                user.ProfilePicture = Console.ReadLine();

                                bool added = service.AddUser(user);
                                Console.WriteLine(added
                                    ? "User added successfully."
                                    : "Failed to add user.");
                                break;
                            }

                        case 11:
                            {
                                Gallery gallery = new Gallery();
                                Console.Write("Name: ");
                                gallery.Name = Console.ReadLine();
                                Console.Write("Description: ");
                                gallery.Description = Console.ReadLine();
                                Console.Write("Location: ");
                                gallery.Location = Console.ReadLine();
                                Console.Write("Curator (ArtistID): ");
                                gallery.CuratorArtistID = Convert.ToInt32(Console.ReadLine());
                                Console.Write("Opening Hours: ");
                                gallery.OpeningHours = Console.ReadLine();

                                bool added = service.AddGallery(gallery);
                                Console.WriteLine(added ? "Gallery added successfully." : "Failed to add gallery.");
                                break;
                            }

                        case 12:
                            {
                                Console.Write("Artwork ID: ");
                                int artworkId = Convert.ToInt32(Console.ReadLine());
                                Console.Write("Gallery ID: ");
                                int galleryId = Convert.ToInt32(Console.ReadLine());

                                bool linked = service.LinkArtworkToGallery(artworkId, galleryId);
                                Console.WriteLine(linked ? "Artwork assigned to gallery successfully." : "Failed to assign artwork to gallery.");
                                break;
                            }



                        case 13:
                            Console.WriteLine("Exiting...");
                            return;

                        default:
                            Console.WriteLine("Invalid option.");
                            break;
                    }
                }
                catch (ArtworkNotFoundException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                catch (UserNotFoundException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
    }
}
