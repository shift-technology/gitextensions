using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GitCommands.Utils;
using Microsoft.VisualStudio.Threading;
using ResourceManager;

namespace ShiftFlow
{
    public partial class PullRequestBodyPrompt : Form
    {
        public PullRequestBodyPrompt(string defaultContent)
        {
            InitializeComponent();

            PullRequestContent.Body = defaultContent;
            description.Text = defaultContent;
        }

        private void description_TextChanged(object sender, EventArgs e)
        {
            UpdateState();
        }

        private void UpdateState()
        {
            PullRequestContent.Body = description.Text;
            btn_OK.Enabled = !string.IsNullOrWhiteSpace(description.Text);
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;

            PullRequestContent.Body = description.Text;

            Close();
        }
    }

    public static class PullRequestContent
    {
        public static string Body { get; set; }
    }
}
