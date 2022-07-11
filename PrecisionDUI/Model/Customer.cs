using System;
using System.ComponentModel;

namespace Precision.Model
{
    public class Customer : BaseModel
    {
        public Customer()
        {
            Address = new Address();
        }

        #region Private Fields

        private int _customerID;
        private string _firstName;
        private string _lastName;
        private string _phoneNumber;
        private string _emailAddress;
        private Address _address;

        #endregion

        #region Public Properties
        public int CustomerID 
        { 
            get { return _customerID; }
            set {
                if (value != _customerID) { _customerID = value; }
                OnPropertyChanged(nameof(CustomerID));
                }

        }
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (value != _firstName) { _firstName = value; }
                OnPropertyChanged(nameof(FirstName));
                OnPropertyChanged(nameof(FullName));
            }
        }
        public string LastName 
        {
            get { return _lastName; }
            set
            {
                if (value != _lastName) { _lastName = value; }
                OnPropertyChanged(nameof(LastName));
                OnPropertyChanged(nameof(FullName));

            }
        }
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                if (value != _phoneNumber) { _phoneNumber = value; }
                OnPropertyChanged(nameof(PhoneNumber));
                OnPropertyChanged(nameof(PhoneNumberFormatted));
            }
        }
        public string EmailAddress
        {
            get { return _emailAddress; }
            set
            {
                if (value != _emailAddress) { _emailAddress = value; }
                OnPropertyChanged(nameof(EmailAddress));
            }
        }
        public Address Address
        {
            get { return _address; }
            set
            {
                if (value != _address) { _address = value; }
                OnPropertyChanged(nameof(Address));
                OnPropertyChanged(nameof(AddressFormatted));
            }
        }

        public string FullName { get { return $"{_firstName} {_lastName}"; } }
        public string PhoneNumberFormatted { get { return FormatNumber(_phoneNumber); } }
        public string AddressFormatted 
        { 
            get 
            {
                if (Address.AddressID != null)
                {
                    return $"{Address.Street}, {Address.City} {Address.State} {Address.ZipCode}";
                }
                else { return "N/A"; }

            } 
        }


        #endregion

        private string FormatNumber(string n)
        {
            string num = $"({n[0]}{n[1]}{n[2]})-{n[3]}{n[4]}{n[5]}-{n[6]}{n[7]}{n[8]}{n[9]}";
            return num;
        }


    }
}
