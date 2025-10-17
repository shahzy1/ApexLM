namespace ApexLM.Services
{
    public interface ILayoutService
    {
        event Action<string> OnTitleChanged;
        void SetTitle(string title);
    }
}
