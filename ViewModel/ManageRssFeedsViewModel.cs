using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPFRssFeedReader.ViewModel;

namespace WPFRssFeedReader.ViewModel
{
    public class ManageRssFeedsViewModel : BaseViewModel
    {
        #region Fields
        private ObservableCollection<RssFeedViewModel> feedsList = new ObservableCollection<RssFeedViewModel>();
        private RssFeedViewModel editedRssFeed = new RssFeedViewModel();
        private SyndicationItem selectedFeed = null; 
        #endregion

        #region Properties
        public ObservableCollection<RssFeedViewModel> FeedsList
        {
            get
            {
                return feedsList;
            }
        }
        public RssFeedViewModel EditedRssFeed
        {
            get
            {
                return editedRssFeed;
            }
            private set
            {
                if (value != editedRssFeed)
                {
                    editedRssFeed = value;
                    OnPropertyChanged(this, "EditedRssFeed");
                }
            }
        }
        public SyndicationItem SelectedFeed
        {
            get
            {
                return selectedFeed;
            }
            set
            {
                if (value != selectedFeed)
                {
                    selectedFeed = value;
                    OnPropertyChanged(this, "SelectedFeed");
                }
            }
        }
        #endregion

        #region Constructors
        public ManageRssFeedsViewModel()
        {
            EditedRssFeed.Url = "http://developmentsimplyput.blogspot.com/feeds/posts/default?alt=rss";
            EditedRssFeed.Title = "Test";

            FeedsList.Add(new RssFeedViewModel("Sample 1", "http://developmentsimplyput.blogspot.com/feeds/posts/default?alt=rss"));
            FeedsList.Add(new RssFeedViewModel("Sample 2", "http://www.feedforall.com/sample.xml"));
            FeedsList.Add(new RssFeedViewModel("Sample 3", "http://www.feedforall.com/sample-feed.xml"));
            FeedsList.Add(new RssFeedViewModel("Sample 4", "http://www.feedforall.com/blog-feed.xml"));
            FeedsList.Add(new RssFeedViewModel("Sample 5", "http://www.rss-specifications.com/blog-feed.xml"));
            FeedsList.Add(new RssFeedViewModel("Sample 6", "http://news.google.us/nwshp?hl=en&tab=wn&output=rss"));
            FeedsList.Add(new RssFeedViewModel("Sample 7", "http://feeds.abcnews.com/abcnews/topstories"));
            FeedsList.Add(new RssFeedViewModel("Sample 8", "http://feeds.abcnews.com/abcnews/internationalheadlines"));
            FeedsList.Add(new RssFeedViewModel("Sample 9", "http://feeds.abcnews.com/abcnews/usheadlines"));
            FeedsList.Add(new RssFeedViewModel("Sample 10", "http://feeds.abcnews.com/abcnews/politicsheadlines"));
            FeedsList.Add(new RssFeedViewModel("Sample 11", "http://feeds.abcnews.com/abcnews/blotterheadlines"));
            FeedsList.Add(new RssFeedViewModel("Sample 12", "http://feeds.abcnews.com/abcnews/thelawheadlines"));
            FeedsList.Add(new RssFeedViewModel("Sample 13", "http://feeds.abcnews.com/abcnews/moneyheadlines"));
            FeedsList.Add(new RssFeedViewModel("Sample 14", "http://feeds.abcnews.com/abcnews/technologyheadlines"));
            FeedsList.Add(new RssFeedViewModel("Sample 15", "http://feeds.abcnews.com/abcnews/healthheadlines"));
            FeedsList.Add(new RssFeedViewModel("Sample 16", "http://feeds.abcnews.com/abcnews/entertainmentheadlines"));
            FeedsList.Add(new RssFeedViewModel("Sample 17", "http://feeds.abcnews.com/abcnews/travelheadlines"));
            FeedsList.Add(new RssFeedViewModel("Sample 18", "http://feeds.abcnews.com/abcnews/sportsheadlines"));
            FeedsList.Add(new RssFeedViewModel("Sample 19", "http://feeds.abcnews.com/abcnews/worldnewsheadlines"));
            FeedsList.Add(new RssFeedViewModel("Sample 20", "http://feeds.abcnews.com/abcnews/2020headlines"));
            FeedsList.Add(new RssFeedViewModel("Sample 21", "http://feeds.abcnews.com/abcnews/primetimeheadlines"));
            FeedsList.Add(new RssFeedViewModel("Sample 22", "http://feeds.abcnews.com/abcnews/nightlineheadlines"));
            FeedsList.Add(new RssFeedViewModel("Sample 23", "http://feeds.abcnews.com/abcnews/gmaheadlines"));
            FeedsList.Add(new RssFeedViewModel("Sample 24", "http://feeds.abcnews.com/abcnews/thisweekheadlines"));
            FeedsList.Add(new RssFeedViewModel("Sample 25", "http://abcnews.go.com/Site/page/rss--3520115#top"));
            FeedsList.Add(new RssFeedViewModel("Sample 26", "http://feeds.abcnews.com/abcnews/mostreadstories"));
        }
        public ManageRssFeedsViewModel(ObservableCollection<RssFeedViewModel> _feedsList)
            : this()
        {
            feedsList = _feedsList;

            if(feedsList == null)
            {
                feedsList = new ObservableCollection<RssFeedViewModel>();
            }
        }
        #endregion

        #region Public Methods
        public RssFeedViewModel AddRssFeed(string title, string url)
        {
            return AddRssFeed(new RssFeedViewModel(title, url));
        }
        public RssFeedViewModel AddRssFeed(RssFeedViewModel rssFeed)
        {
            if (rssFeed != null)
            {
                FeedsList.Add(rssFeed);
            }
            
            return rssFeed;
        }
        public void DeleteRssFeed(Guid rssFeedId)
        {
            RssFeedViewModel found = FeedsList.DefaultIfEmpty(null).FirstOrDefault(feed => feed.Id == rssFeedId);

            if(found != null)
            {
                DeleteRssFeed(found);
            }
        }
        public void DeleteRssFeed(RssFeedViewModel rssFeed)
        {
            if (rssFeed != null)
            {
                FeedsList.Remove(rssFeed);
            }
        }
        #endregion

        #region Private Methods
        #endregion

        #region Commands
        public CommandRelay<RssFeedViewModel> AddRssFeedCommand
        {
            get
            {
                return new CommandRelay<RssFeedViewModel>(delegate(RssFeedViewModel feed)
                {
                    return !EditedRssFeed.HasErrors;
                }, delegate(RssFeedViewModel feed)
                {
                    AddRssFeed(feed);
                });
            }
        }
        public CommandRelay<RssFeedViewModel> DeleteRssFeedCommand
        {
            get
            {
                return new CommandRelay<RssFeedViewModel>(delegate(RssFeedViewModel feed)
                {
                    return true;
                }, delegate(RssFeedViewModel feed)
                {
                    DeleteRssFeed(feed);
                });
            }
        }
        #endregion
    }
}
