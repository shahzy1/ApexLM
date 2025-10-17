using ApexLM.Shared;
using Microsoft.AspNetCore.Components;

namespace ApexLM.Pages
{
    public partial class NotebookDetail
    {
        [Parameter] public string NotebookId { get; set; } = string.Empty;

        private Notebook? currentNotebook;

        protected override async Task OnParametersSetAsync()
        {
            await LoadNotebook();
        }

        private async Task LoadNotebook()
        {
            try
            {
                var allNotebooks = GetSampleNotebooks();
                currentNotebook = allNotebooks.FirstOrDefault(n => n.Id == NotebookId);

                if (currentNotebook == null)
                {
                    // Notebook not found, redirect to home
                    Navigation.NavigateTo("/");
                    return;
                }

                // Update title using service
                LayoutService.SetTitle(currentNotebook.Title);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading notebook: {ex.Message}");
                Navigation.NavigateTo("/");
            }
        }

        private void GoBack()
        {
            LayoutService.SetTitle("Notebooks");
            Navigation.NavigateTo("/");
        }

        private List<Notebook> GetSampleNotebooks()
        {
            return new List<Notebook>
        {
            new Notebook { Id = "1", Title = "The Two Sequences", Description = "Parenting Advice for the Digital Age", Date = "May 4, 2025", SourceCount = 21 },
            new Notebook { Id = "2", Title = "On World in Duff", Description = "Trends in Health, Wealth and...", Date = "Apr 15, 2025", SourceCount = 24 },
            new Notebook { Id = "3", Title = "The Encrowded", Description = "The World Ahead 2025", Date = "Jul 7, 2025", SourceCount = 10 },
            new Notebook { Id = "4", Title = "The Atlantic", Description = "How To Build A Life, from The Atlantic", Date = "Apr 22, 2025", SourceCount = 44 },
            new Notebook { Id = "5", Title = "New notebook", Description = "Antl and Culture\nWilliam Shakespeare: The...", Date = "Apr 25, 2025", SourceCount = 45 },
            new Notebook { Id = "6", Title = "Untitled notebook", Description = "", Date = "Oct 14, 2025", SourceCount = 0 },
            new Notebook { Id = "7", Title = "Azure AI Text Analytics for...", Description = "", Date = "Oct 15, 2025", SourceCount = 3 }
        };
        }

        public class Notebook
        {
            public string Id { get; set; } = string.Empty;
            public string Title { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public string Date { get; set; } = string.Empty;
            public int SourceCount { get; set; }
        }
    }
}
