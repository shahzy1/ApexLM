namespace ApexLM.Services
{
    public interface ILayoutService
    {
        event Action<string> OnTitleChanged;
        void SetDefaultTitle();
        void SetTitle(string title);
    }
}
