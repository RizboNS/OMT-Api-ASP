using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OMT_Api.Data.Migrations
{
    public partial class refactor1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Address", "City", "Created", "Email", "FirstName", "LastName", "Phone", "State", "Zip" },
                values: new object[] { new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"), "Flash Street 1", "Gotham", new DateTime(2022, 11, 15, 9, 2, 54, 42, DateTimeKind.Local).AddTicks(9613), "ballan@company.com", "Berry", "Allan", "+381601400222", "DC", "10034" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"));
        }
    }
}
