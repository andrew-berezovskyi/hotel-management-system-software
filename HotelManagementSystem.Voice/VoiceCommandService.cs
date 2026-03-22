using System.Speech.Recognition;

namespace HotelManagementSystem.Voice
{
    public enum VoiceCommandType
    {
        None,
        OpenRooms,
        OpenUsers,
        OpenBookings,
        OpenPayments,
        OpenStaff,
        OpenHotelInfo,
        OpenReports,
        OpenSettings,
        Logout
    }

    public class VoiceCommandEventArgs : EventArgs
    {
        public VoiceCommandType Command { get; }
        public string RawText { get; }

        public VoiceCommandEventArgs(VoiceCommandType command, string rawText)
        {
            Command = command;
            RawText = rawText;
        }
    }

    public class VoiceCommandService : IDisposable
    {
        private readonly SpeechRecognitionEngine _engine;
        private readonly Dictionary<string, VoiceCommandType> _commandMap;

        public bool IsRunning { get; private set; }

        // Події, на які підпишеться форма
        public event EventHandler<VoiceCommandEventArgs>? CommandRecognized;
        public event EventHandler<string>? StatusChanged;
        public event EventHandler<string>? ErrorOccurred;

        public VoiceCommandService()
        {
            try
            {
                // Використовуємо системну мову за замовчуванням
                _engine = new SpeechRecognitionEngine();

                _engine.SetInputToDefaultAudioDevice();
                _engine.SpeechRecognized += OnSpeechRecognized;
                _engine.AudioStateChanged += (s, e) =>
                {
                    StatusChanged?.Invoke(this, $"Audio state: {e.AudioState}");
                };
                _engine.RecognizeCompleted += (s, e) =>
                {
                    if (e.Error != null)
                        ErrorOccurred?.Invoke(this, e.Error.Message);
                };

                _commandMap = BuildCommandMap();
                LoadGrammar();
            }
            catch (Exception ex)
            {
                ErrorOccurred?.Invoke(this, $"Voice init error: {ex.Message}");
                throw;
            }
        }

        private Dictionary<string, VoiceCommandType> BuildCommandMap()
        {
            // Сюди додаємо всі фрази, які мають відкривати певні форми
            // Можеш додати українські: "відкрити бронювання", "бронювання" тощо
            return new Dictionary<string, VoiceCommandType>(StringComparer.OrdinalIgnoreCase)
            {
                // Rooms
                { "manage rooms", VoiceCommandType.OpenRooms },
                { "rooms", VoiceCommandType.OpenRooms },
                { "open rooms", VoiceCommandType.OpenRooms },

                // Users
                { "manage users", VoiceCommandType.OpenUsers },
                { "users", VoiceCommandType.OpenUsers },

                // Bookings
                { "manage bookings", VoiceCommandType.OpenBookings },
                { "bookings", VoiceCommandType.OpenBookings },
                { "open bookings", VoiceCommandType.OpenBookings },

                // Payments
                { "payments", VoiceCommandType.OpenPayments },
                { "payments and transactions", VoiceCommandType.OpenPayments },
                { "transactions", VoiceCommandType.OpenPayments },

                // Staff
                { "manage staff", VoiceCommandType.OpenStaff },
                { "staff", VoiceCommandType.OpenStaff },

                // Hotel info
                { "hotel info", VoiceCommandType.OpenHotelInfo },
                { "hotel information", VoiceCommandType.OpenHotelInfo },

                // Reports
                { "reports", VoiceCommandType.OpenReports },
                { "analytics", VoiceCommandType.OpenReports },
                { "reports and analytics", VoiceCommandType.OpenReports },

                // Settings
                { "system settings", VoiceCommandType.OpenSettings },
                { "settings", VoiceCommandType.OpenSettings },

                // Logout
                { "logout", VoiceCommandType.Logout },
                { "log out", VoiceCommandType.Logout },
                { "sign out", VoiceCommandType.Logout },
            };
        }

        private void LoadGrammar()
        {
            var choices = new Choices();

            foreach (var phrase in _commandMap.Keys)
            {
                choices.Add(phrase);
            }

            var builder = new GrammarBuilder(choices);

            // Можеш тут примусово вказати мову, якщо треба:
            // builder.Culture = new System.Globalization.CultureInfo("en-US");

            var grammar = new Grammar(builder)
            {
                Name = "HotelAdminCommands"
            };

            _engine.UnloadAllGrammars();
            _engine.LoadGrammar(grammar);
        }

        public void Start()
        {
            if (IsRunning) return;

            try
            {
                _engine.RecognizeAsync(RecognizeMode.Multiple);
                IsRunning = true;
                StatusChanged?.Invoke(this, "Voice recognition started");
            }
            catch (Exception ex)
            {
                ErrorOccurred?.Invoke(this, ex.Message);
            }
        }

        public void Stop()
        {
            if (!IsRunning) return;

            try
            {
                _engine.RecognizeAsyncStop();
                IsRunning = false;
                StatusChanged?.Invoke(this, "Voice recognition stopped");
            }
            catch (Exception ex)
            {
                ErrorOccurred?.Invoke(this, ex.Message);
            }
        }

        private void OnSpeechRecognized(object? sender, SpeechRecognizedEventArgs e)
        {
            var text = e.Result.Text.Trim();
            if (e.Result.Confidence < 0.70)
            {
                StatusChanged?.Invoke(this, $"Low confidence ({e.Result.Confidence:F2}) for '{text}'");
                return;
            }

            if (_commandMap.TryGetValue(text, out var command))
            {
                StatusChanged?.Invoke(this, $"Recognized: {text} → {command}");
                CommandRecognized?.Invoke(this, new VoiceCommandEventArgs(command, text));
            }
            else
            {
                StatusChanged?.Invoke(this, $"Unknown command: {text}");
            }
        }

        public void Dispose()
        {
            try
            {
                Stop();
                _engine.Dispose();
            }
            catch { }
        }
    }
}
