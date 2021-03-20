using System;

namespace CoffeeShop
{
    [Serializable]
    public class EmployeeLogin
    {
        public string Email { get; set; }
        public string EmployeeName { get; set; }
    }
}