namespace HotelMobileApp.Models;

public class NotificationItem
{
    public string Title { get; set; }           // "Your payment succeeded"
    public string Message { get; set; }         // "Your payment for hotel booking..."
    public string TimeText { get; set; }        // "Today, 08:54 AM"
    public bool IsSuccess { get; set; }       // true = зелена галочка, false = червоний хрестик
    public bool IsHighlighted { get; set; }   // для синьої підсвітки (як перший елемент у Фігмі)
}

