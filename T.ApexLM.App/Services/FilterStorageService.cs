using Microsoft.JSInterop;

namespace T.ApexLM.App.Services
{
    public class FilterStorageService : IFilterStorageService
    {
        private readonly IJSRuntime _jsRuntime;

        public FilterStorageService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task SaveFilterState<T>(string key, T value)
        {
            try
            {
                await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, value?.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving filter state: {ex.Message}");
            }
        }

        public async Task<T?> LoadFilterState<T>(string key, T defaultValue)
        {
            try
            {
                var storedValue = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", key);
                if (!string.IsNullOrEmpty(storedValue))
                {
                    return ConvertValue<T>(storedValue);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading filter state: {ex.Message}");
            }

            return defaultValue;
        }

        public async Task ClearFilterState(string key)
        {
            try
            {
                await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error clearing filter state: {ex.Message}");
            }
        }

        public async Task ClearAllFilterStates()
        {
            try
            {
                var keys = new[] { "notebookFilter", "notebookView", "showAllFeatured", "showAllRecent" };
                foreach (var key in keys)
                {
                    await ClearFilterState(key);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error clearing all filter states: {ex.Message}");
            }
        }

        private static T ConvertValue<T>(string value)
        {
            var targetType = typeof(T);

            if (targetType.IsEnum)
            {
                return (T)Enum.Parse(targetType, value);
            }

            if (targetType == typeof(bool))
            {
                return (T)(object)bool.Parse(value);
            }

            if (targetType == typeof(int))
            {
                return (T)(object)int.Parse(value);
            }

            if (targetType == typeof(string))
            {
                return (T)(object)value;
            }

            throw new NotSupportedException($"Type {targetType} is not supported for filter storage");
        }
    }
}
