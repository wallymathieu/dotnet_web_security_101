using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace Shared.ViewState
{
    public class PersistentSessionPageStatePersister : PageStatePersister
    {
        public PersistentSessionPageStatePersister(Page page) : base(page)
        {
        }

        public override void Load()
        {
            var pair = LoadPageStateFromPersistenceMedium();
            ViewState = pair.First;
            ControlState = pair.Second;
        }

        public override void Save()
        {
            SavePageStateToPersistenceMedium(new Pair(ViewState, ControlState));
        }
        private string File() { return string.Format(@"C:\tmp\viewstate_{0}.txt", this.Page.Session.SessionID); }
        protected Pair LoadPageStateFromPersistenceMedium()
        {
            LosFormatter format = new LosFormatter();
            return System.IO.File.Exists(File()) ? (Pair)format.Deserialize(System.IO.File.ReadAllText(File())) : new Pair();
        }
        protected void SavePageStateToPersistenceMedium(Pair viewState)
        {
            LosFormatter format = new LosFormatter();
            using (var file = new FileStream(File(), FileMode.Truncate,FileAccess.Read))
            using (var writer = new StreamWriter(file))
            {
                format.Serialize(writer, viewState);
                writer.Flush();
            }
        }
    }
}
