namespace T.ApexLM.App.Services
{
    public interface IFilterStorageService
    {
        Task SaveFilterState<T>(string key, T value);
        Task<T?> LoadFilterState<T>(string key, T defaultValue);
        Task ClearFilterState(string key);
        Task ClearAllFilterStates();
    }
}
