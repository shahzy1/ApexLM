using T.ApexLM.App.Components;
using MudBlazor;

namespace T.ApexLM.App.Extension
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

        public static async Task ShowImplementationPending(this IDialogService dialogService, string featureName)
        {
            var parameters = new DialogParameters
            {
                { "FeatureName", featureName }
            };

            var options = new DialogOptions()
            {
                MaxWidth = MaxWidth.Small,
                CloseButton = true
            };

            var dialog = await dialogService.ShowAsync<ImplementationPendingDialog>(
                "Coming Soon",
                parameters,
                options
            );

            await dialog.Result;
        }

        // Optional: Specific methods for common features
        public static async Task ShowOneDrivePending(this IDialogService dialogService)
        {
            await ShowImplementationPending(dialogService, "Microsoft OneDrive Integration");
        }

        public static async Task ShowWebsitePending(this IDialogService dialogService)
        {
            await ShowImplementationPending(dialogService, "Website Import");
        }

        public static async Task ShowYouTubePending(this IDialogService dialogService)
        {
            await ShowImplementationPending(dialogService, "YouTube Integration");
        }

        public static async Task ShowPasteTextPending(this IDialogService dialogService)
        {
            await ShowImplementationPending(dialogService, "Text Paste Feature");
        }
    }
}
