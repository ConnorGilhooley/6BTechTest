using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SixBeeHealthTech.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialUserWithAdminRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1", 0, "6694e43c-a5db-4a46-ab24-262f6186bb32", "joebloggs@example.com", true, false, null, "JOEBLOGGS@EXAMPLE.COM", "JOE BLOGGS", "AQAAAAIAAYagAAAAEMGece/H56p/Ju+NUl43MuonPTWU4iNNNRgFpxT4guYRfPftOdXH0j+FSWIXWzgA5Q==", null, false, "", false, "Joe Bloggs" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1");
        }
    }
}
