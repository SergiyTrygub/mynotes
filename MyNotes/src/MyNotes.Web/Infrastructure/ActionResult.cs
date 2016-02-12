using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNotes.Web.Infrastructure
{
    public class ActionResult
    {
        public bool Succeeded { get; internal set; }

        public object Item { get; protected set; }

        public IEnumerable<string> Errors { get; internal set; }

        public static ActionResult Success(object item = null)
        {
            return new ActionResult
            {
                Succeeded = true,
                Item = item
            };
        }

        public static ActionResult Failed(params string[] errors)
        {
            return new ActionResult
            {
                Succeeded = false,
                Errors = errors
            };
        }

        public static ActionResult Failed(Exception ex)
        {
            try
            {
                //NotificationManager.SendErrorMessage(ex);
            }
            catch
            { }

            var errorString = ex.Message;
            while (ex.InnerException != null)
            {
                errorString += Environment.NewLine + ex.InnerException.Message;
                ex = ex.InnerException;
            }
            return new ActionResult
            {
                Succeeded = false,
                Errors = new[] { errorString }
            };
        }
    }
}
