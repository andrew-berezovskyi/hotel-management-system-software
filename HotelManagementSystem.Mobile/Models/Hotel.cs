namespace HotelMobileApp.Models
{
    public class Hotel
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;

        /// <summary>Картинка для карток (Home / Search)</summary>
        public string Image { get; set; } = string.Empty;

        public double Rating { get; set; }

        public bool IsFavorite { get; set; }

        // ---------- ДЕТАЛІ Готелю ----------

        /// <summary>Ціна за людину / за ніч (як тобі зручніше).</summary>
        public decimal PricePerPerson { get; set; }

        /// <summary>
        /// Alias для сумісності з кодом, який очікує PricePerNight.
        /// Повертає те саме, що і PricePerPerson.
        /// </summary>
        public decimal PricePerNight
        {
            get => PricePerPerson;
            set => PricePerPerson = value;
        }

        /// <summary>Опис типу кімнат, як на макеті (2 Bedrooms | 1 Bathroom ...).</summary>
        public string RoomFacilities { get; set; } = string.Empty;

        /// <summary>Публічні зручності (Wi-Fi, Pool, Park ...).</summary>
        public string PublicFacilities { get; set; } = string.Empty;

        /// <summary>Тип кімнати за замовчуванням (Family Room / Suite ...).</summary>
        public string DefaultRoomType { get; set; } = string.Empty;

        /// <summary>Текстовий опис готелю.</summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>Картинка мапи для екрану деталізації.</summary>
        public string MapImage { get; set; } = string.Empty;

        /// <summary>Головне фото на детальному екрані (може співпадати з Image).</summary>
        public string MainImage { get; set; } = string.Empty;

        /// <summary>Текстова адреса (для опису/карти).</summary>
        public string Location { get; set; } = string.Empty;

        /// <summary>Координати для відкриття в Google Maps.</summary>
        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
