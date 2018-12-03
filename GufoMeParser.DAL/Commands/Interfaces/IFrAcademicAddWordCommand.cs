namespace GufoMeParser.DAL.Commands.Interfaces
{
    public interface IFrAcademicAddWordCommand : ICommand
    {
        void SendDataAsync(string word);
    }
}
