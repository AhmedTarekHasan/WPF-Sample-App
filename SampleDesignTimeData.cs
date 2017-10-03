using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFRssFeedReader.ViewModel;

namespace WPFRssFeedReader
{
    public class SampleDesignTimeData
    {
        public static ManageRssFeedsViewModel ViewModel
        {
            get
            {
                ManageRssFeedsViewModel vm = new ManageRssFeedsViewModel();

                vm.EditedRssFeed.Url = "http://developmentsimplyput.blogspot.com/feeds/posts/default?alt=rss";
                vm.EditedRssFeed.Title = "Test";

                vm.FeedsList.Add(new RssFeedViewModel("Sample 25", "http://abcnews.go.com/Site/page/rss--3520115#top"));
                vm.FeedsList.Add(new RssFeedViewModel("Sample 26", "http://feeds.abcnews.com/abcnews/mostreadstories"));

                vm.FeedsList[0].Expand();

                return vm;
            }
        }
    }
}
