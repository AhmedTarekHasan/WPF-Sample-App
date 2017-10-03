using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFRssFeedReader.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(object sender, string propertyName)
        {
            if(null != PropertyChanged)
            {
                PropertyChanged(sender, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region INotifyDataErrorInfo Members
        private Dictionary<string, IEnumerable> PropertyErrors = new Dictionary<string, IEnumerable>();
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        protected virtual void OnErrorsChanged(object sender, string propertyName)
        {
            if (null != ErrorsChanged)
            {
                ErrorsChanged(sender, new DataErrorsChangedEventArgs(propertyName));
            }
        }
        public System.Collections.IEnumerable GetErrors(string propertyName)
        {
            IEnumerable propertyErrors = null;

            if(!string.IsNullOrEmpty(propertyName) && !string.IsNullOrWhiteSpace(propertyName))
            {
                lock (PropertyErrors)
                {
                    if (PropertyErrors.ContainsKey(propertyName))
                    {
                        propertyErrors = PropertyErrors[propertyName];
                    }
                }
            }

            if (null == propertyErrors)
            {
                propertyErrors = new List<object>();
            }

            return propertyErrors;
        }
        public bool HasErrors
        {
            get
            {
                bool found = false;

                lock (PropertyErrors)
                {
                    found = (PropertyErrors.Count > 0);
                }

                return found;
            }
        }
        protected virtual void PropertyIsValid(string propertyName)
        {
            if (!string.IsNullOrEmpty(propertyName) && !string.IsNullOrWhiteSpace(propertyName))
            {
                lock (PropertyErrors)
                {
                    if (PropertyErrors.ContainsKey(propertyName))
                    {
                        PropertyErrors.Remove(propertyName);
                        OnErrorsChanged(this, propertyName);
                    }
                }
            }
        }
        protected virtual void PropertyIsInValid(string propertyName, IEnumerable errors)
        {
            if (!string.IsNullOrEmpty(propertyName) && !string.IsNullOrWhiteSpace(propertyName))
            {
                lock (PropertyErrors)
                {
                    if (PropertyErrors.ContainsKey(propertyName))
                    {
                        PropertyErrors.Remove(propertyName);
                    }

                    PropertyErrors.Add(propertyName, errors);
                    OnErrorsChanged(this, propertyName);
                }
            }
        }
        #endregion
    }
}
