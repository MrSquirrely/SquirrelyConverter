using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Image_Converter.Code {
    public class Github {

        public static async void SubmitIssueAsync(string title, string body) {
            GitHubClient client = new GitHubClient(new ProductHeaderValue("imageConverter"));
            Credentials credentials = new Credentials("*Github Token*");
            client.Credentials = credentials;

            NewIssue createIssue = new NewIssue(title) { Body = body };
            Issue issue = await client.Issue.Create("MrSquirrelyNet", "SquirrelyConverter", createIssue);
        }

    }
}
