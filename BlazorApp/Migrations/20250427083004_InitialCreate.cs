using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BlazorApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ContactName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    City = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Region = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Address", "City", "CompanyName", "ContactName", "Country", "Phone", "PostalCode", "Region" },
                values: new object[,]
                {
                    { "5A6AD17C-A342-4DD6-9A5D-959732DEEA5E", "Στέφανου Σαράφη 12", "Χαλάνδρι", "Singular Logic", "Μαρία Παπαδοπούλου", "Ελλάδα", "6945899878", "12345", "Αττική" },
                    { "6e9e0727-62ee-4b48-8cf7-76c1f24bee4c", "Νάουσα", "Πάρος", "Epsilon NET", "Γιάννης Καλογερόπουλος", "Ελλάδα", "6947093404", "84401", "Κυκλάδες" },
                    { "D270A06D-6E5D-4845-9F8E-85FABEAA46C5", "Green Hill 2312", "California", "Data Communications", "Antonia Damascus", "USA", "(5) 555-3932", "05023", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
