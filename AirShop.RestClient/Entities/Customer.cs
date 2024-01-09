using System;

namespace AirShop.ExternalServices.Entities
{
    public class Customer
    {
        private string _name; 
        private string _firstName; 
        private string _lastName;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                if (string.IsNullOrWhiteSpace(_name))
                {
                    _name = _firstName + _lastName;
                }
            }
        }

        public string FirstName
        {
            get => _firstName;
            set
            {
                if (string.IsNullOrWhiteSpace(Name))
                {
                    _firstName = value;
                }
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                if (string.IsNullOrWhiteSpace(Name))
                {
                    _lastName = value;
                }
            }
        }

        public Customer()
        {
            _name = string.Empty;
            _firstName = string.Empty;
            _lastName = string.Empty;
        }

        public Customer(string name, string firstName, string lastName)
        {
            _name = name;
            _firstName = firstName;
            _lastName = lastName;
        }
    }
}
