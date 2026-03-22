using System.Drawing;


namespace hotel_management_system.Helpers
{
    public static class RoomHelper
    {
        /// <summary>
        /// Завантажує зображення кімнати з файлу. Якщо не знайдено — повертає передане стандартне зображення.
        /// </summary>
        public static Image LoadRoomImage(string path, Image defaultImage)
        {
            try
            {
                if (!string.IsNullOrEmpty(path) && File.Exists(path))
                {
                    using var temp = Image.FromFile(path);
                    return new Bitmap(temp, new Size(140, 80));
                }
            }
            catch
            {
                // Ігноруємо помилки читання зображення
            }

            return defaultImage;
        }

        /// <summary>
        /// Відображає просте вікно введення тексту (InputBox).
        /// </summary>
        public static string AskInput(string caption, string defaultValue = "")
        {
            return Microsoft.VisualBasic.Interaction.InputBox(caption, "Input", defaultValue);
        }
    }
}

