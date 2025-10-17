namespace ApexLM.Services
{
    public class LayoutService : ILayoutService
    {
        public event Action<string>? OnTitleChanged;

        public void SetTitle(string title)
        {
            OnTitleChanged?.Invoke(title);
        }
    }
}
