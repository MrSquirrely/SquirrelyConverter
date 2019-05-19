using System;
using Converter_Utilities.API;
using Converter_Utilities.Interface;
using Octokit;

namespace Converter_Utilities.Github {
    public class Github {
        internal static Github Instance(string headerValue) => new Github(headerValue);
        private GitHubClient client;
        private Credentials credentials = new Credentials(Token.token);

        private Github(string headerValue) {
            try {
                client = new GitHubClient(new ProductHeaderValue(headerValue)) { Credentials = credentials };
            }
            catch (Exception ex) {
                Logger logger = Logger.Instance("Github");
                logger.LogError(ex);
            }
        }

        private async void CreateIssue(string repoOwner, string repo, NewIssue issue, Label label) {
            switch (label) {
                case Label.Bug:
                    issue.Labels.Add("bug");
                    break;
                case Label.Enhancement:
                    issue.Labels.Add("enhancement");
                    break;
            }
            Issue issueCreator = await client.Issue.Create(repoOwner, repo, issue);
        }

        public static NewIssue issue(string title, string body) => new NewIssue(title) { Body = body };

        public enum Label {
            Bug,
            Enhancement
        }
    }


}
