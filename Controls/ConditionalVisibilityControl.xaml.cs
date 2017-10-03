using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFRssFeedReader.Controls
{
    [ContentProperty("Items")]
    public partial class ConditionalVisibilityControl : UserControl
    {
        #region Dependency Properties
        public static readonly DependencyProperty ItemsSourceProperty = ItemsControl.ItemsSourceProperty.AddOwner(typeof(ConditionalVisibilityControl));
        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }
        #endregion

        #region Attached Properties
        // Using a DependencyProperty as the backing store for IsVisible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsVisibleProperty = DependencyProperty.RegisterAttached("IsVisible", typeof(bool), typeof(ConditionalVisibilityControl), new PropertyMetadata(true, IsVisibleHasChanged));
        public static bool GetIsVisible(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsVisibleProperty);
        }
        public static void SetIsVisible(DependencyObject obj, bool value)
        {
            obj.SetValue(IsVisibleProperty, value);
        }
        private static void IsVisibleHasChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)  
        {
            if (d is UIElement)
            {
                if((bool)e.NewValue)
                {
                    ((UIElement)d).Visibility = Visibility.Visible;
                }
                else
                {
                    ((UIElement)d).Visibility = Visibility.Collapsed;
                }
            }
        }
        #endregion

        #region Properties
        public ItemCollection Items
        {
            get { return _itemsControl.Items; }
        }
        #endregion

        #region Constructors
        public ConditionalVisibilityControl()
        {
            InitializeComponent();
        }
        #endregion
    }
}
