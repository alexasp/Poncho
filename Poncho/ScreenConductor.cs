using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Caliburn.Micro;

namespace Poncho
{
    class ScreenConductor : IConductor
    {
        public IEnumerable GetChildren()
        {
            throw new NotImplementedException();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyOfPropertyChange(string propertyName)
        {
            throw new NotImplementedException();
        }

        public void Refresh()
        {
            throw new NotImplementedException();
        }

        public bool IsNotifying
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public void ActivateItem(object item)
        {
            throw new NotImplementedException();
        }

        public void DeactivateItem(object item, bool close)
        {
            throw new NotImplementedException();
        }

        public event EventHandler<ActivationProcessedEventArgs> ActivationProcessed;
    }
}
