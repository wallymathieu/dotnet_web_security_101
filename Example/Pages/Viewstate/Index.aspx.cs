﻿using Shared;
using Shared.ViewState;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;

namespace Example.Viewstate
{
    public partial class Index : System.Web.UI.Page
    {
        public string[] TextsInViewstate
        {
            get { return (string[])ViewState["TextsInViewstate"]; }
            set { ViewState["TextsInViewstate"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (TextsInViewstate == null)
            {
                // texts isn't in view state, so we need to load it from scratch.
                TextsInViewstate = new List<string>() { "item 1", "item 2", "item 3", "item 4" }.ToArray();
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            Texts.DataSource = TextsInViewstate;
            Texts.DataBind();
            base.OnPreRender(e);
        }

        protected void AddText_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Text.Text))
                return;

            TextsInViewstate = TextsInViewstate
                .ToList()
                .Tap(l => l.Add(Text.Text))
                .ToArray();
        }
#if LOG_VIEWSTATE
        private string File() { return string.Format(@"C:\tmp\viewstate_{0}.txt", this.Page.GetType().FullName); }
        protected override void SavePageStateToPersistenceMedium(object state)
        {
            LosFormatter format = new LosFormatter();
            using (var file = new FileStream(File(), FileMode.Create, FileAccess.Write, FileShare.Read))
            using (var writer = new StreamWriter(file))
            {
                format.Serialize(writer, state);
                writer.Flush();
            }
            base.SavePageStateToPersistenceMedium(state);
        }
#endif
    }
}