namespace CryptographicCreator.Models
{
    public interface IStatusBarMessages
    {
        string this[StatusBarMessage message] { get; }
    }
}