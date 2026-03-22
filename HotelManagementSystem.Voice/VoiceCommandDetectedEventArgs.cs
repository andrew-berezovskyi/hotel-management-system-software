
namespace HotelManagementSystem.Voice
{
    public class VoiceCommandDetectedEventArgs : EventArgs
    {
        public VoiceCommandType Command { get; }

        public string RawText { get; }

        public VoiceCommandDetectedEventArgs(VoiceCommandType command, string rawText)
        {
            Command = command;
            RawText = rawText;
        }
    }
}

