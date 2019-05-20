using System;
using Converter_Utilities.API;
using Octokit;

namespace Converter_Utilities.Github {
    public class Github {
        public static Github Instance(string headerValue) => new Github(headerValue);
        private readonly GitHubClient _client;
        private readonly Credentials _credentials = new Credentials(Token.GithubToken);

        private Github(string headerValue) {
            try {
                _client = new GitHubClient(new ProductHeaderValue(headerValue)) { Credentials = _credentials };
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
                default:
                    throw new ArgumentOutOfRangeException(nameof(label), label, null);
            }

            _ = await _client.Issue.Create(repoOwner, repo, issue);
        }

        public static NewIssue Issue(string title, string body) => new NewIssue(title) { Body = body };

        public enum Label {
            Bug,
            Enhancement
        }
    }


}
