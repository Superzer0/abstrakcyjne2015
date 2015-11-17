namespace Abstrakcyjne1.Store
{
    public class TextData : Data
    {
        private readonly string _textToBeImported;

        public TextData(string textToBeImported)
        {
            _textToBeImported = textToBeImported;
        }

        public string Text { get { return _textToBeImported; } }
    }
}
