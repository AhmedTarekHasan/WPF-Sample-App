using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPFRssFeedReader
{
    public class CommandRelay<T> : ICommand
    {
        #region Fields
        private Func<T, bool> shouldExecute;
        private Action<T> run;
        private bool lastCanExecute;
        #endregion

        #region Constructors
        public CommandRelay() : this(null, null)
        {

        }
        public CommandRelay(Func<T, bool> _shouldExecute, Action<T> _run)
        {
            lastCanExecute = false;
            shouldExecute = _shouldExecute;
            run = _run;
        }
        #endregion

        #region ICommand Members
        public bool CanExecute(object parameter)
        {
            bool result = lastCanExecute;
            bool oldVal = lastCanExecute;
            bool newVal = lastCanExecute;

            if(null != shouldExecute)
            {
                result = shouldExecute((T)parameter);
                newVal = result;
            }

            if (oldVal != newVal)
            {
                CanExecuteChangedEventArgs args = new CanExecuteChangedEventArgs(oldVal, newVal);
                OnCanExecuteChanged(this, args);
            }

            lastCanExecute = result;

            return result;
        }
        public void Execute(object parameter)
        {
            if(null != run)
            {
                run((T)parameter);
            }
        }
        #endregion

        #region Events
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        #endregion

        #region Events Handlers
        protected virtual void OnCanExecuteChanged(object sender, EventArgs e)
        {
            
        }
        #endregion
    }

    public class CommandRelay : CommandRelay<Object>
    {
        #region Constructors
        public CommandRelay(): base(null, null)
        {

        }
        public CommandRelay(Func<object, bool> _shouldExecute, Action<object> _run)
            : base(_shouldExecute, _run)
        {
            
        }
        #endregion
    }

    public class CanExecuteChangedEventArgs : EventArgs
    {
        #region Fields And Properties
        public bool OldVal { get; set; }
        public bool NewVal { get; set; }
        #endregion

        #region Constructors
        public CanExecuteChangedEventArgs()
        {

        }
        public CanExecuteChangedEventArgs(bool _oldVal, bool _newVal)
        {
            OldVal = _oldVal;
            NewVal = _newVal;
        }
        #endregion
    }
}
