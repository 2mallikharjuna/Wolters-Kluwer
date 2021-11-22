using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WkApi.Migrations
{
    public partial class MigrationName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmailId = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MobileNo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    PinCode = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Id", "Address", "DateOfBirth", "EmailId", "FirstName", "Gender", "LastName", "MobileNo", "PinCode" },
                values: new object[] { new Guid("0ba8267c-a786-4e83-ae7f-28e5d2447253"), "11 Collins Street, Melbourne", new DateTime(1991, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test1@yahoo.com", "TestFirstName1", 1, "TestLastName1", "0401645111", "1038" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Id", "Address", "DateOfBirth", "EmailId", "FirstName", "Gender", "LastName", "MobileNo", "PinCode" },
                values: new object[] { new Guid("d999c46b-e706-4fd9-a79b-fe27ef544f8a"), "22 Collins Street, Melbourne", new DateTime(1992, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test2@yahoo.com", "TestFirstName2", 2, "TestLastName2", "0401645222", "2038" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Id", "Address", "DateOfBirth", "EmailId", "FirstName", "Gender", "LastName", "MobileNo", "PinCode" },
                values: new object[] { new Guid("e8b77576-bdec-4c4b-adfa-b7d90e12f675"), "33 Collins Street, Melbourne", new DateTime(1993, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test3@yahoo.com", "TestFirstName3", 0, "TestLastName3", "0401645333", "3038" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customer");
        }
    }
}
