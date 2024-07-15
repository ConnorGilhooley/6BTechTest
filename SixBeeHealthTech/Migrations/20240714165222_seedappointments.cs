using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SixBeeHealthTech.Migrations
{
    /// <inheritdoc />
    public partial class seedappointments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "AppointmentDate", "AppointmentTime", "ContactNumber", "EmailAddress", "IsApproved", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 7, 16, 0, 0, 0, 0, DateTimeKind.Local), new TimeOnly(23, 0, 0), "123-456-7890", "johndoe@example.com", false, "John Doe" },
                    { 2, new DateTime(2024, 7, 17, 0, 0, 0, 0, DateTimeKind.Local), new TimeOnly(11, 0, 0), "987-654-3210", "janesmith@example.com", false, "Jane Smith" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1ce40c76-a8ca-4c0f-9a3a-b7a55076ad79", "AQAAAAIAAYagAAAAEBSsl1tHrKnCFWYQCQP0herjdhYUYAmyw017izwFI7UxIK3QxLdPxQRCRDW5fYGDBg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "6694e43c-a5db-4a46-ab24-262f6186bb32", "AQAAAAIAAYagAAAAEMGece/H56p/Ju+NUl43MuonPTWU4iNNNRgFpxT4guYRfPftOdXH0j+FSWIXWzgA5Q==" });
        }
    }
}
