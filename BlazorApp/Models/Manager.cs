namespace BlazorApp.Models
{
    public class Manager
    {
        public Manager(string name)
        {
            Name = name;
        }

        public string Name { get; set; } = string.Empty;
    }
}
