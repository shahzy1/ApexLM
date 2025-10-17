using Microsoft.AspNetCore.Components;

namespace ApexLM.Pages
{
    public partial class NotebookDetail
    {
        [Parameter] public string NotebookId { get; set; } = string.Empty;

        private Notebook? currentNotebook;
        private int mobileActiveTab = 1; // Default to Chat on mobile

        protected override void OnParametersSet()
        {
            LoadNotebook();
        }

        private void LoadNotebook()
        {
            var allNotebooks = GetSampleNotebooks();
            currentNotebook = allNotebooks.FirstOrDefault(n => n.Id == NotebookId);

            if (currentNotebook == null)
            {
                Navigation.NavigateTo("/");
                return;
            }

            LayoutService.SetTitle(currentNotebook.Title);
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
            new Notebook { Id = "5", Title = "New notebook", Description = "Antl and Culture\nWilliam Shakespeare: The...", Date = "Apr 25, 2025", SourceCount = 45 }
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
