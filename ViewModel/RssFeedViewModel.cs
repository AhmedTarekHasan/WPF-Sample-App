using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPFRssFeedReader.Model;

namespace WPFRssFeedReader.ViewModel
{
    public class RssFeedViewModel : RssFeed, IDisposable
    {
        #region Fields
        private bool isExpanded = false;
        private bool isCollapsed = true;
        private bool isExpandedAtleastOnce = false;
        private bool isInViewMode = true;
        private bool isInEditMode = false;
        private bool shouldEnableEditButton = true;
        private bool shouldShowSaveButton = false;
        private bool shouldShowDiscardButton = false;
        private RssFeedPresentationMode presentationMode = RssFeedPresentationMode.View;
        private RssFeed shadow = new RssFeed();
        #endregion

        #region Properties
        public bool IsExpanded
        {
            get
            {
                return isExpanded;
            }
            set
            {
                if (value != isExpanded)
                {
                    isExpanded = value;
                    OnPropertyChanged(this, "IsExpanded");
                }

                if (!isExpandedAtleastOnce && value)
                {
                    if (base.LoadFeed())
                    {
                        isExpandedAtleastOnce = true;
                    }
                    else
                    {
                        isExpandedAtleastOnce = false;
                        IsCollapsed = true;
                        isExpanded = false;
                        IsExpanded = false;
                        OnPropertyChanged(this, "IsExpanded");
                    }
                }
            }
        }
        public bool IsCollapsed
        {
            get
            {
                return isCollapsed;
            }
            set
            {
                if (value != isCollapsed)
                {
                    isCollapsed = value;
                    OnPropertyChanged(this, "IsCollapsed");
                }
            }
        }
        public bool IsInViewMode
        {
            get
            {
                return isInViewMode;
            }
            private set
            {
                if (value != isInViewMode)
                {
                    isInViewMode = value;
                    OnPropertyChanged(this, "IsInViewMode");
                }
            }
        }
        public bool IsInEditMode
        {
            get
            {
                return isInEditMode;
            }
            private set
            {
                if (value != isInEditMode)
                {
                    isInEditMode = value;
                    OnPropertyChanged(this, "isInEditMode");
                }
            }
        }
        public bool ShouldEnableEditButton
        {
            get
            {
                return shouldEnableEditButton;
            }
            private set
            {
                if (value != shouldEnableEditButton)
                {
                    shouldEnableEditButton = value;
                    OnPropertyChanged(this, "ShouldEnableEditButton");
                }
            }
        }
        public bool ShouldShowSaveButton
        {
            get
            {
                return shouldShowSaveButton;
            }
            private set
            {
                if (value != shouldShowSaveButton)
                {
                    shouldShowSaveButton = value;
                    OnPropertyChanged(this, "ShouldShowSaveButton");
                }
            }
        }
        public bool ShouldShowDiscardButton
        {
            get
            {
                return shouldShowDiscardButton;
            }
            private set
            {
                if (value != shouldShowDiscardButton)
                {
                    shouldShowDiscardButton = value;
                    OnPropertyChanged(this, "ShouldShowDiscardButton");
                }
            }
        }
        public RssFeedPresentationMode PresentationMode
        {
            get
            {
                return presentationMode;
            }
            private set
            {
                if (value != presentationMode)
                {
                    presentationMode = value;
                    OnPropertyChanged(this, "PresentationMode");
                }
            }
        }
        public RssFeed Shadow
        {
            get
            {
                return shadow;
            }
            private set
            {
                if (value != shadow)
                {
                    shadow = value;
                    OnPropertyChanged(this, "Shadow");
                }
            }
        }
        #endregion

        #region Constructors
        public RssFeedViewModel() { }
        public RssFeedViewModel(string _title, string _url) : base(_title, _url)
        {

        }
        #endregion

        #region Public Methods
        public bool Expand()
        {
            bool result = true;

            IsExpanded = true;

            if (IsExpanded)
            {
                IsCollapsed = false;
            }

            if (!IsExpanded)
            {
                result = false;
            }

            return result;
        }
        public void Collapse()
        {
            IsCollapsed = true;
            IsExpanded = false;
        }
        public bool ExpandIfCollapsed()
        {
            bool result = true;

            if (!IsExpanded)
            {
                result = Expand();
            }

            return result;
        }
        public void CollapseIfExpanded()
        {
            if (IsExpanded)
            {
                Collapse();
            }
        }
        public void SetInViewMode()
        {
            SwitchPresentationMode(RssFeedPresentationMode.View);
        }
        public void SetInEditMode()
        {
            SwitchPresentationMode(RssFeedPresentationMode.Edit);
        }
        public void SaveChanges()
        {
            bool urlIsChanged = (this.Url != Shadow.Url);
            this.Title = Shadow.Title;
            this.Url = Shadow.Url;

            if (urlIsChanged)
            {
                isExpandedAtleastOnce = false;
                IsCollapsed = true;
                IsExpanded = false;
                ResetFeed();
            }

            SetInViewMode();
            ResetShadow();
        }
        public void DiscardChanges()
        {
            SetInViewMode();
            ResetShadow();
        }
        #endregion

        #region Private Methods
        private void ResetShadow()
        {
            Shadow = this.CreateShadow();
        }
        private void SwitchPresentationMode(RssFeedPresentationMode mode)
        {
            if (mode == RssFeedPresentationMode.Edit)
            {
                ShouldEnableEditButton = false;
                ShouldShowSaveButton = true;
                ShouldShowDiscardButton = true;
                ResetShadow();
                IsInEditMode = true;
                IsInViewMode = false;
            }
            else
            {
                ShouldEnableEditButton = true;
                ShouldShowSaveButton = false;
                ShouldShowDiscardButton = false;
                IsInEditMode = false;
                IsInViewMode = true;
            }

            PresentationMode = mode;
        }
        #endregion

        #region Commands
        public CommandRelay<RssFeedViewModel> ExpandCommand
        {
            get
            {
                return new CommandRelay<RssFeedViewModel>(delegate(RssFeedViewModel feed)
                {
                    return true;
                }, delegate(RssFeedViewModel feed)
                {
                    bool foundFeeds = Expand();

                    if (!foundFeeds)
                    {
                        MessageBox.Show("No feeds are found");
                    }
                });
            }
        }
        public CommandRelay<RssFeedViewModel> ExpandIfCollapsedCommand
        {
            get
            {
                return new CommandRelay<RssFeedViewModel>(delegate(RssFeedViewModel feed)
                {
                    return true;
                }, delegate(RssFeedViewModel feed)
                {
                    bool foundFeeds = ExpandIfCollapsed();

                    if (!foundFeeds)
                    {
                        MessageBox.Show("No feeds are found");
                    }
                });
            }
        }
        public CommandRelay<RssFeedViewModel> CollapseCommand
        {
            get
            {
                return new CommandRelay<RssFeedViewModel>(delegate(RssFeedViewModel feed)
                {
                    return true;
                }, delegate(RssFeedViewModel feed)
                {
                    Collapse();
                });
            }
        }
        public CommandRelay<RssFeedViewModel> CollapseIfExpandedCommand
        {
            get
            {
                return new CommandRelay<RssFeedViewModel>(delegate(RssFeedViewModel feed)
                {
                    return true;
                }, delegate(RssFeedViewModel feed)
                {
                    CollapseIfExpanded();
                });
            }
        }
        public CommandRelay<RssFeedViewModel> EditRssFeedCommand
        {
            get
            {
                return new CommandRelay<RssFeedViewModel>(delegate(RssFeedViewModel feed)
                {
                    return true;
                }, delegate(RssFeedViewModel feed)
                {
                    SetInEditMode();
                });
            }
        }
        public CommandRelay<RssFeedViewModel> SaveRssFeedCommand
        {
            get
            {
                return new CommandRelay<RssFeedViewModel>(delegate(RssFeedViewModel feed)
                {
                    bool valid = true;

                    if (isInEditMode && Shadow != null && Shadow.HasErrors)
                    {
                        valid = false;
                    }

                    return valid;
                }, delegate(RssFeedViewModel feed)
                {
                    SaveChanges();
                });
            }
        }
        public CommandRelay<RssFeedViewModel> DiscardRssFeedCommand
        {
            get
            {
                return new CommandRelay<RssFeedViewModel>(delegate(RssFeedViewModel feed)
                {
                    return true;
                }, delegate(RssFeedViewModel feed)
                {
                    DiscardChanges();
                });
            }
        }
        #endregion

        #region IDisposable Members
        public override void Dispose()
        {
            base.Dispose();
            Shadow.Dispose();
        }
        #endregion
    }

    public enum RssFeedPresentationMode
    {
        View = 0,
        Edit = 1
    }
}
