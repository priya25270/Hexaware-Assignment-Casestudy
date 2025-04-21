using NUnit.Framework;
using dao;
using VirtualArtGallery.Models;
using System;
using System.Collections.Generic;

namespace VirtualArtGallery.Tests
{
    [TestFixture]
    public class VirtualArtGalleryServiceImplTests
    {
        private VirtualArtGalleryServiceImpl service;

        [SetUp]
        public void Setup()
        {
            service = new VirtualArtGalleryServiceImpl();
        }

        [Test]
        public void Test_AddUser_ReturnsTrue()
        {
            var user = new User
            {
                Username = "ram" + Guid.NewGuid(),
                Password = "Pass@123",
                Email = "ram@gmail.com",
                FirstName = "ram",
                LastName = "S",
                DOB = new DateTime(1999, 2, 1),
                ProfilePicture = null
            };

            bool result = service.AddUser(user);

            Assert.IsTrue(result);
        }

        [Test]
        public void Test_AddArtist_ReturnsTrue()
        {
            var artist = new Artist
            {
                Name = "Thread Artist ",
                Biography = "Famous for thread artworks",
                BirthDate = new DateTime(1998, 5, 15),
                Nationality = "Nepal",
                Website = "http://testartist.com",
                ContactInfo = "artist@thread.com"
            };

            bool result = service.AddArtist(artist);

            Assert.IsTrue(result);
        }

        [Test]
        public void Test_AddArtwork_ReturnsTrue()
        {
          
            int existingArtistId = 4;

            var artwork = new Artwork
            {
                Title = "thread Artwork " + Guid.NewGuid(),
                Description = "excellent thread artwork",
                CreationDate = DateTime.Now,
                Medium = "Digital",
                ImageURL = "http://example.com/image.jpg",
                ArtistID = existingArtistId
            };

            bool result = service.AddArtwork(artwork);

            Assert.IsTrue(result);
        }

        [Test]
        public void Test_SearchArtworks_ReturnsList()
        {
          
            string keyword = "test";

            List<Artwork> results = service.SearchArtworks(keyword);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Count >= 0); 
        }

        [Test]
        public void Test_RemoveArtwork_ReturnsTrue()
        {
          
            int existingArtworkId = 2;

            bool result = service.RemoveArtwork(existingArtworkId);

            Assert.IsTrue(result);
        }
        [Test]
        public void TestCreateGallery()
        {
            var newGallery = new Gallery
            {
                Name = "Modern Art Gallery",
                Description = "A place for modern art enthusiasts.",
                Location = "New York, USA",
                CuratorArtistID = 3,  
                OpeningHours = "9 AM - 6 PM"
            };

            bool result = service.AddGallery(newGallery);
            Assert.IsTrue(result, "Gallery should be created successfully");
        }

    }
}
