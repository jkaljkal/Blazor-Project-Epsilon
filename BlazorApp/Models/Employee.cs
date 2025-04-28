using BlazorApp.Models.Interfaces;

namespace BlazorApp.Models
{
    public class Employee : IPerson
    {
        public Employee(string name) 
        {
            Name = name;
        }

        public string Name { get; set; } = string.Empty;
    }
}
