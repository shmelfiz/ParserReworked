namespace GufoMeParser.Core.BuisinessModels
{
    public class DeWiktionaryDataModel
    {
        public int WordId { get; set; }
        public string CurrentWord { get; set; }
        public string Transcription { get; set; }
        public string PartOfSpeech { get; set; }
        public string PartOfSpeechSeit { get; set; }
        public string Description { get; set; }
        public string Example { get; set; }
        public string WordForms { get; set; }
        public int Condition { get; set; }
    }
}
