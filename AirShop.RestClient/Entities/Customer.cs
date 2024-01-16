using System;

namespace AirShop.ExternalServices.Entities
{
    public class Customer
    {
        private string _name; 
        private string _firstName; 
        private string _lastName;
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

        public string? Street { get; set; }
        public string City { get; set; }
        public string HouseNumber { get; set; }
        public string? ApartmentNumber { get; set; }
        public string PostalCode { get; set; }
        public string? Nip { get; set; }
    }
}
