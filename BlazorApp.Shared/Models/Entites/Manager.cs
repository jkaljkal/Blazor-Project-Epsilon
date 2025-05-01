using BlazorApp.Models.Interfaces;

namespace BlazorApp.Shared.Models.Entites
{
    public class Manager: IPerson
    {
        public Manager(string name)
        {
            Name = name;
        }

        public string Name { get; set; } = string.Empty;
    }
}
