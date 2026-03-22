
namespace hotel_management_system.Helpers
{
    public static class RoomOptions
    {
        // 5 типів для 5★ готелю
        public static readonly string[] RoomTypes =
        {
            "Standard Room",
            "Superior Room",
            "Deluxe Room",
            "Junior Suite",
            "Presidential Suite"
        };

        // Статуси – ті, що вже використовуються в проєкті
        public static readonly string[] RoomStatuses =
        {
            "Available",    // вільний
            "Booked",       // заброньований (майбутнє поселення)
            "Occupied",     // заселений
            "Maintenance"   // на ремонті / недоступний
        };
    }
}

