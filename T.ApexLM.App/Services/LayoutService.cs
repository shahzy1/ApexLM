namespace T.ApexLM.App.Services
{
    public class LayoutService : ILayoutService
    {
        public event Action<string>? OnTitleChanged;

        public void SetDefaultTitle()
        {
            OnTitleChanged?.Invoke("ApexLM");
        }

        public void SetTitle(string title)
        {
            OnTitleChanged?.Invoke(title);
        }
    }
}
