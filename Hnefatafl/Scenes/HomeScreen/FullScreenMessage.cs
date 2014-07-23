namespace Hnefatafl.Scenes.HomeScreen
{
    public class FullScreenMessage
    {
        public string Message { get; set; }
        public string FontName { get; set; }

        public FullScreenMessage(string message, string fontName)
        {
            Message = message;
            FontName = fontName;
        }
    }
}