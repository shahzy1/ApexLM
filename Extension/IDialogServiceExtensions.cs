using ApexLM.Components;
using MudBlazor;

namespace ApexLM.Extension
{
    public static class DialogServiceExtensions
    {
        public static async Task<bool> ShowAddSourcesDialog(this IDialogService dialogService)
        {
            var parameters = new DialogParameters();

            var options = new DialogOptions()
            {
                MaxWidth = MaxWidth.Medium,
                FullWidth = true,
                CloseButton = true
            };

            var dialog = await dialogService.ShowAsync<AddSourcesDialog>("Add Sources", parameters, options);
            var result = await dialog.Result;

            return !result.Canceled;
        }

        // You can add more dialog extension methods here later
        public static async Task<bool> ShowConfirmationDialog(this IDialogService dialogService, string title, string message)
        {
            var parameters = new DialogParameters
            {
                { "ContentText", message }
            };

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.Small };

            var dialog = await dialogService.ShowAsync<MudDialog>("Confirm", parameters, options);
            var result = await dialog.Result;

            return !result.Canceled;
        }
    }
}
