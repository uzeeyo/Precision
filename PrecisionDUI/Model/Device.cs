using System;
using System.ComponentModel;

namespace Precision
{
    public class Device : ObservableObject
    {
        #region Private Fields
        private int _deviceID;
        private int _customerID;
        private string _make;
        private string _model;
        private string _osType;
        private string _notes;
        #endregion

        #region Public Properties
        public int DeviceID
        {
            get { return _deviceID; }
            set
            {
                if (value != _deviceID)
                {
                    _deviceID = value;
                    OnPropertyChanged(nameof(DeviceID));
                }
            }
        }

        public int CustomerID
        {
            get { return _customerID; }
            set
            {
                if (value != _customerID)
                {
                    _customerID = value;
                    OnPropertyChanged(nameof(CustomerID));
                }
            }
        }

        public string Make
        {
            get { return _make; }
            set
            {
                if (value != _make)
                {
                    _make = value;
                    OnPropertyChanged(nameof(Make));
                }
            }   
        }

        public string Model
        {
            get { return _model; }
            set
            {
                if (value != _model)
                {
                    _model = value;
                    OnPropertyChanged(nameof(Model));
                }
            }
        }

        public string OSType
        {
            get { return _osType; }
            set
            {
                if (value != _osType)
                {
                    _osType = value;
                    OnPropertyChanged(nameof(OSType));
                }
            }
        }
        #endregion

        //#region INotifyPropertyChanged Implementation
        //public event PropertyChangedEventHandler PropertyChanged;
        //private void OnPropertyChanged(string propertyName)
        //{
        //    if (PropertyChanged != null)
        //    {
        //        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        //    }
        //}
        //#endregion
    }
}
