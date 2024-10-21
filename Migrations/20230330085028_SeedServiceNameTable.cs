using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WindowsServicesCheck.Migrations
{
    /// <inheritdoc />
    public partial class SeedServiceNameTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("ServiceNames",
                new[] { "Id", "Name", "Type", "IsActive" },
                new object[] { Guid.NewGuid(), "KeyIso", "P", true });

            migrationBuilder.InsertData("ServiceNames",
                new[] { "Id", "Name", "Type", "IsActive" },
                new object[] { Guid.NewGuid(), "DcomLaunch", "P", true });

            migrationBuilder.InsertData("ServiceNames",
                new[] { "Id", "Name", "Type", "IsActive" },
                new object[] { Guid.NewGuid(), "DoSvc","S", true });

            migrationBuilder.InsertData("ServiceNames",
                new[] { "Id", "Name", "Type", "IsActive" },
                new object[] { Guid.NewGuid(), "Dhcp", "H", true });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
