using BlazorApp.Models.Interfaces;

namespace BlazorApp.Shared.Models.Entites
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
