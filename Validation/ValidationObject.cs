using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFRssFeedReader.Validation
{
    public class ValidationObject
    {
        #region Properties
        public string Message { get; set; }
        public ValidationObjectSeverity Severity { get; set; }
        #endregion

        #region Constructors
        public ValidationObject() : this(null, ValidationObjectSeverity.Default)
        {

        }

        public ValidationObject(string message, ValidationObjectSeverity severity)
        {
            Message = message;
            Severity = severity;
        }
        #endregion
    }
}
