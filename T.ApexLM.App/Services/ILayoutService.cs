namespace T.ApexLM.App.Services
{
    public interface ILayoutService
    {
        event Action<string> OnTitleChanged;
        void SetDefaultTitle();
        void SetTitle(string title);
    }
}
