using HotelManagementSystem.Models;

namespace hotel_management_system.UIHelpers
{
    /// <summary>
    /// Відповідає за картинку, опис та “highlight-и” кімнат по їх типу.
    /// Картинки читаємо з папки Resources поруч з exe.
    /// Є кешування в пам'яті, щоб не вантажити файли кожного разу.
    /// </summary>
    public static class RoomPresentationHelper
    {
        private static readonly string ImageDir =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources");

        // Кеш: ім'я файлу -> Image
        private static readonly Dictionary<string, Image> _imageCache =
            new Dictionary<string, Image>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Ліниве завантаження з кешем.
        /// Читаємо файл один раз, клонуємо картинку і зберігаємо в пам'яті.
        /// </summary>
        private static Image LoadImage(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return null;

            // Якщо картинка вже є в кеші — повертаємо миттєво
            if (_imageCache.TryGetValue(fileName, out var cached))
                return cached;

            try
            {
                var path = Path.Combine(ImageDir, fileName);
                if (!File.Exists(path))
                    return null;

                // Читаємо через FileStream, щоб не тримати файл заблокованим
                using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (var img = Image.FromStream(fs))
                {
                    var clone = (Image)img.Clone();
                    _imageCache[fileName] = clone;
                    return clone;
                }
            }
            catch
            {
                return null;
            }
        }

        public static Image GetImageForRoom(Room room)
        {
            var type = (room?.Type ?? string.Empty).Trim().ToLowerInvariant();

            // назви файлів у тебе:
            // room_standard.jpg
            // room_superior.jpg
            // room_deluxe.jpg
            // room_junior_suite.jpg
            // room_presidential.jpg

            if (type.Contains("standard"))
                return LoadImage("room_standard.jpg");

            if (type.Contains("superior"))
                return LoadImage("room_superior.jpg");

            if (type.Contains("deluxe"))
                return LoadImage("room_deluxe.jpg");

            if (type.Contains("junior"))
                return LoadImage("room_junior_suite.jpg");

            if (type.Contains("president"))
                return LoadImage("room_presidential.jpg");

            // якщо взагалі нічого не впізнали – стандарт
            return LoadImage("room_standard.jpg");
        }

        public static string GetDescription(string roomType)
        {
            var type = (roomType ?? string.Empty).Trim().ToLowerInvariant();

            if (type.Contains("standard"))
            {
                return
@"A cozy, well-designed room for short stays.
Perfect for 1–2 guests, with a comfortable queen bed,
compact work desk and soft ambient lighting.
Ideal choice if you just need a practical place to rest
after a busy day in Kyiv.";
            }

            if (type.Contains("superior"))
            {
                return
@"A more spacious room with extra comfort.
Located on higher floors with a better view,
it features a larger working area and a relaxed seating corner.
Great for longer stays or business trips when you need
both comfort and productivity.";
            }

            if (type.Contains("deluxe"))
            {
                return
@"A stylish room with upgraded design and details.
King-size bed, generous space, large windows and a soft lounge area
create a relaxed premium atmosphere.
Recommended for guests who value comfort and a bit of luxury.";
            }

            if (type.Contains("junior"))
            {
                return
@"A compact suite with separate sleeping and living zones.
You get a comfortable bedroom plus a cozy lounge with sofa,
where you can work, relax or host a guest.
Perfect for couples or small families who need more space.";
            }

            if (type.Contains("president"))
            {
                return
@"The flagship suite of The Hotel Kyiv.
Spacious living room, elegant bedroom, dining area and dedicated workspace
are combined into one private apartment.
Ideal for special occasions, VIP stays and guests who want
a truly luxurious Kyiv experience.";
            }

            return string.Empty;
        }

        public static string GetHighlights(string roomType)
        {
            var type = (roomType ?? string.Empty).Trim().ToLowerInvariant();

            if (type.Contains("standard"))
                return "• 18–22 m²\n• Queen bed\n• Street or courtyard view\n• Compact work desk";

            if (type.Contains("superior"))
                return "• 22–26 m²\n• Queen or King bed\n• Upper floors\n• Larger work desk + armchair";

            if (type.Contains("deluxe"))
                return "• 26–30 m²\n• King bed\n• Panoramic windows\n• Lounge area with sofa";

            if (type.Contains("junior"))
                return "• 30–35 m²\n• Separate bedroom & living area\n• Sofa that can be used as extra bed\n• Large wardrobe";

            if (type.Contains("president"))
                return "• 60+ m²\n• Living room, bedroom & dining area\n• Corner location with city view\n• Separate working desk area";

            return string.Empty;
        }

        public static string GetIncludedServices()
        {
            return
@"• Free high-speed Wi-Fi
• Daily cleaning
• 24/7 reception
• In-room tea & coffee
• Fresh towels and toiletries";
        }
    }
}
