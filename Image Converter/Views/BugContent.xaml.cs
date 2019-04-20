using Image_Converter.Code;
using MaterialDesignThemes.Wpf;
using Octokit;
using System.Windows;
using System.Windows.Controls;

namespace Image_Converter.Views {
    /// <summary>
    /// Interaction logic for BugContent.xaml
    /// </summary>
    public partial class BugContent : UserControl {
        private string QuoteToUse { get; set; }

        public BugContent() {
            InitializeComponent();
            QuoteToUse = Utilities.GetQuote();
            BodyContent.Text = QuoteToUse;
        }

        //! The token is located in a class called Github located in the folder "Code"
        private async void SubmitButton_Click(object sender, RoutedEventArgs e) {
            if (BodyContent.Text != QuoteToUse && TitleContent.Text != "Example Bug Title") {
                GitHubClient client = new GitHubClient(new ProductHeaderValue("imageConverter"));
                Credentials credentials = new Credentials(Github.token);
                client.Credentials = credentials;

                NewIssue createIssue = new NewIssue(TitleContent.Text) { Body = $"{BodyContent.Text}\n\n-Submitted in the Image Converter application" };
                ComboBoxItem selectedItem = (ComboBoxItem)IssueTypeBox.SelectedItem;
                switch (selectedItem.Content) {
                    case "Bug":
                        createIssue.Labels.Add("bug");
                        break;
                    default:
                        createIssue.Labels.Add("enhancement");
                        break;
                }
                Issue issue = await client.Issue.Create("MrSquirrelyNet", "SquirrelyConverter", createIssue);
            }else if (BodyContent.Text == QuoteToUse) {
                ShowError("You must change the title text.");
            }else if (TitleContent.Text == "Example Bug Title") {
                ShowError("You must change the body content.");
            }
            Utilities.flyout.IsOpen = false;
        }

        private void ShowError(string message) {
            DialogButton.Content = "CLOSE";
            DialogTitle.Text = "ERROR";
            DialogMessage.Text = message;
            DialogHost.IsOpen = true;
        }

        private void DialogButton_Click(object sender, RoutedEventArgs e) {
            DialogHost.IsOpen = false;
        }
    }
}
