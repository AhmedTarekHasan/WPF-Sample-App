using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using WPFRssFeedReader.Validation;
using WPFRssFeedReader.ViewModel;

namespace WPFRssFeedReader.Model
{
    public class RssFeed : BaseViewModel, IDisposable
    {
        #region Fields
        private string title;
        private string url;
        private Guid id = new Guid();
        private SyndicationFeed feed = null;
        #endregion

        #region Properties
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                IEnumerable errors = ValidateTitle(value);
                
                if(null != errors)
                {
                    PropertyIsInValid("Title", errors);
                }
                else
                {
                    PropertyIsValid("Title");
                }

                if (value != title)
                {
                    title = value;
                    OnPropertyChanged(this, "Title");
                }
            }
        }
        public string Url
        {
            get
            {
                return url;
            }
            set
            {
                IEnumerable errors = ValidateUrl(value);

                if (null != errors)
                {
                    PropertyIsInValid("Url", errors);
                }
                else
                {
                    PropertyIsValid("Url");
                }

                if (value != url)
                {
                    url = value;
                    OnPropertyChanged(this, "Url");
                }
            }
        }
        public Guid Id
        {
            get
            {
                return id;
            }
            private set
            {
                id = value;
            }
        }
        public SyndicationFeed Feed
        {
            get
            {
                return feed;
            }
            private set
            {
                if (value != feed)
                {
                    feed = value;
                    OnPropertyChanged(this, "Feed");
                }
            }
        }
        #endregion

        #region Constructors
        public RssFeed() : this(null, null)
        {

        }
        public RssFeed(string _title, string _url)
        {
            Title = _title;
            Url = _url;
        }
        #endregion

        #region Methods
        public bool LoadFeed()
        {
            bool done = true;

            try
            {
                using (XmlReader reader = XmlReader.Create(Url))
                {
                    Feed = SyndicationFeed.Load(reader);
                }
            }
            catch(Exception)
            {
                Feed = null;
                done = false;
            }

            return done;
        }
        public RssFeed CreateShadow()
        {
            RssFeed result = new RssFeed(this.title, this.Url);
            result.Id = this.Id;
            return result;
        }
        public void ResetFeed()
        {
            Feed = null;
        }
        #endregion

        #region Validation Methods
        protected virtual IEnumerable ValidateTitle(string title)
        {
            List<ValidationObject> result = null;

            if (string.IsNullOrEmpty(title) || string.IsNullOrWhiteSpace(title))
            {
                result = new List<ValidationObject>();
                ValidationObject obj = new ValidationObject("Title is required field.", ValidationObjectSeverity.Fatal);
                result.Add(obj);
            }
            else if(title.Length > 30)
            {
                result = new List<ValidationObject>();
                ValidationObject obj = new ValidationObject("Title should not exceed 30 characters.", ValidationObjectSeverity.Minor);
                result.Add(obj);
            }

            if (result != null && result.Count > 1)
            {
                result = result.OrderBy(i => (int)i.Severity).ToList();
            }

            return result;
        }
        protected virtual IEnumerable ValidateUrl(string url)
        {
            List<ValidationObject> result = null;

            if (string.IsNullOrEmpty(url) || string.IsNullOrWhiteSpace(url))
            {
                result = new List<ValidationObject>();
                ValidationObject obj = new ValidationObject("Url is required field.", ValidationObjectSeverity.Fatal);
                result.Add(obj);
            }
            else
            {
                Uri uriResult;
                bool isValid = Uri.TryCreate(url, UriKind.Absolute, out uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

                if (!isValid)
                {
                    if (result == null)
                    {
                        result = new List<ValidationObject>();
                    }

                    ValidationObject obj = new ValidationObject("Url is invalid.", ValidationObjectSeverity.Major);
                    result.Add(obj);
                }

                if (url.ToLower().Contains("adult"))
                {
                    if (result == null)
                    {
                        result = new List<ValidationObject>();
                    }

                    ValidationObject obj = new ValidationObject("No adult content is allowed.", ValidationObjectSeverity.Minor);
                    result.Add(obj);
                }

            }

            if(result != null && result.Count > 1)
            {
                result = result.OrderByDescending(i => (int)i.Severity).ToList();
            }

            return result;
        }
        #endregion

        #region IDisposable Members
        public virtual void Dispose()
        {
            feed = null;
        }
        #endregion
    }
}
