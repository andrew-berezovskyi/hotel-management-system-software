using HotelMobileApp.Models;

namespace HotelMobileApp.Services
{
    public static class HotelRepository
    {
        private static readonly List<Hotel> _hotels = new()
        {
            new Hotel
            {
                Id = 1,
                Name = "The Hotel Kyiv",
                City = "Kyiv, Ukraine",
                Country = "Ukraine",
                Image = "hotel_kyiv.png",
                Rating = 4.9,
                IsFavorite = false,
                PricePerPerson = 140m,
                RoomFacilities = "2 Bedrooms   |   1 Bathroom   |   1 Guestroom",
                PublicFacilities = "Wi-Fi, Pool, Food court, Park",
                DefaultRoomType = "Family Room",
                Description =
                    "Modern family hotel in the heart of Kyiv, close to Maidan Nezalezhnosti and the main business district. " +
                    "Perfect for weekend city breaks and business trips.",
                Latitude = 50.4501,
                Longitude = 30.5234,
                MapImage = "map_kyiv.png"
            },
            new Hotel
            {
                Id = 2,
                Name = "The Hotel Kharkiv",
                City = "Kharkiv, Ukraine",
                Country = "Ukraine",
                Image = "hotel_kharkiv.png",
                Rating = 4.7,
                PricePerPerson = 115m,
                RoomFacilities = "1 Bedroom   |   1 Bathroom   |   City view",
                PublicFacilities = "Wi-Fi, Fitness, Cafe",
                DefaultRoomType = "Standard Room",
                Description =
                    "Comfortable hotel near Gorky Central Park with easy access to the university area and city center.",
                Latitude = 49.9935,
                Longitude = 36.2304,
                MapImage = "map_kharkiv.png"
            },
            new Hotel
            {
                Id = 3,
                Name = "The Hotel Dnipro",
                City = "Dnipro, Ukraine",
                Country = "Ukraine",
                Image = "hotel_dnipro.png",
                Rating = 4.6,
                PricePerPerson = 110m,
                RoomFacilities = "2 Bedrooms   |   River view",
                PublicFacilities = "Wi-Fi, River promenade, Restaurant",
                DefaultRoomType = "Deluxe Room",
                Description =
                    "Riverside hotel with panoramic views of the Dnipro embankment. Great for evening walks and relaxing stays.",
                Latitude = 48.4647,
                Longitude = 35.0462,
                MapImage = "map_dnipro.png"
            },
            new Hotel
            {
                Id = 4,
                Name = "The Hotel Lviv",
                City = "Lviv, Ukraine",
                Country = "Ukraine",
                Image = "hotel_lviv.png",
                Rating = 4.9,
                PricePerPerson = 135m,
                RoomFacilities = "2 Bedrooms   |   1 Bathroom   |   Old town view",
                PublicFacilities = "Wi-Fi, Rooftop terrace, Cafe",
                DefaultRoomType = "Family Room",
                Description =
                    "Cozy hotel a few minutes from Rynok Square. Ideal for exploring Lviv’s old streets, coffee culture and museums.",
                Latitude = 49.8397,
                Longitude = 24.0297,
                MapImage = "map_lviv.png"
            },
            new Hotel
            {
                Id = 5,
                Name = "The Hotel Khmelnytskyi",
                City = "Khmelnytskyi, Ukraine",
                Country = "Ukraine",
                Image = "hotel_khmelnytskyi.png",
                Rating = 4.4,
                PricePerPerson = 90m,
                RoomFacilities = "1 Bedroom   |   1 Bathroom",
                PublicFacilities = "Wi-Fi, Parking",
                DefaultRoomType = "Standard Room",
                Description =
                    "Quiet city hotel with convenient access to the central square and local shopping centers.",
                Latitude = 49.422983,
                Longitude = 26.987133,
                MapImage = "map_khmelnytskyi.png"
            },
            new Hotel
            {
                Id = 6,
                Name = "The Hotel Warsaw",
                City = "Warsaw, Poland",
                Country = "Poland",
                Image = "hotel_warsaw.png",
                Rating = 4.8,
                PricePerPerson = 160m,
                RoomFacilities = "2 Bedrooms   |   2 Bathrooms   |   City skyline view",
                PublicFacilities = "Wi-Fi, Pool, Spa, Restaurant",
                DefaultRoomType = "Suite",
                Description =
                    "Modern business & leisure hotel near Warsaw city center. Walking distance to Old Town and main shopping streets.",
                Latitude = 52.2297,
                Longitude = 21.0122,
                MapImage = "map_warsaw.png"
            },
            new Hotel
            {
                Id = 7,
                Name = "The Hotel Krakow",
                City = "Krakow, Poland",
                Country = "Poland",
                Image = "hotel_krakow.png",
                Rating = 4.9,
                PricePerPerson = 155m,
                RoomFacilities = "2 Bedrooms   |   1 Bathroom   |   Castle view",
                PublicFacilities = "Wi-Fi, Restaurant, Lounge bar",
                DefaultRoomType = "Deluxe Room",
                Description =
                    "Elegant hotel near Wawel Castle and the Vistula river. Great choice for weekend escapes and romantic trips.",
                Latitude = 50.0647,
                Longitude = 19.9450,
                MapImage = "map_krakow.png"
            },
            new Hotel
            {
                Id = 8,
                Name = "The Hotel Katowice",
                City = "Katowice, Poland",
                Country = "Poland",
                Image = "hotel_katowice.png",
                Rating = 4.5,
                PricePerPerson = 120m,
                RoomFacilities = "1 Bedroom   |   1 Bathroom",
                PublicFacilities = "Wi-Fi, Parking, Breakfast",
                DefaultRoomType = "Standard Room",
                Description =
                    "Urban hotel close to the business district and Nikiszowiec historical area. Good option for short work trips.",
                Latitude = 50.2649,
                Longitude = 19.0238,
                MapImage = "map_katowice.png"
            }
        };

        public static IReadOnlyList<Hotel> GetAllHotels() => _hotels;

        public static Hotel? GetHotelById(int id) =>
            _hotels.FirstOrDefault(h => h.Id == id);
    }
}
