using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrlShortener.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AboutSection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutSection", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(512)", maxLength: 512, nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(64)", maxLength: 64, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    UserRoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_UserRoles_UserRoleId",
                        column: x => x.UserRoleId,
                        principalTable: "UserRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UrlsData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LongValue = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    ShortValue = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResourceType = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrlsData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UrlsData_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AboutSection",
                columns: new[] { "Id", "Text", "Title" },
                values: new object[] { 1, "This project was created using Angular 21, ASP.NET Core 6, EF Core 6 and MSSQL 2022.URL Checker works based on base62 encoding. Every url entry has own unique id number, so base62 will return unique short url every time for every new and current long urls.\n\nHome page is done with Angular. You can navigate through urls that are present in the table.If you are authenticated, you can create own url matches, also delete them if they are yours.Admin can delete every entry in the table.\n\nLogin page is done with Razor View. Under the hood, when user logs in, cookie is used to authenticate user. About page is done with Razor View as well, and if you are admin, you can click on text to modify it and save.", "About" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "Role" },
                values: new object[] { 1, "Admin" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "Role" },
                values: new object[] { 2, "User" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "PasswordHash", "PasswordSalt", "UserRoleId" },
                values: new object[] { 1, "admin@email.com", "John", "Doe", new byte[] { 211, 33, 120, 101, 169, 107, 8, 26, 184, 165, 13, 127, 150, 82, 223, 60, 165, 177, 201, 75, 13, 248, 12, 10, 88, 93, 159, 33, 114, 200, 193, 214, 234, 189, 105, 82, 142, 5, 75, 246, 209, 234, 156, 233, 186, 7, 1, 122, 196, 206, 92, 193, 147, 37, 189, 156, 233, 230, 246, 119, 157, 74, 2, 240 }, new byte[] { 76, 0, 211, 160, 48, 54, 164, 0, 185, 223, 228, 224, 44, 1, 236, 87, 56, 239, 158, 33, 246, 36, 114, 110, 19, 38, 198, 11, 141, 237, 145, 225, 182, 3, 31, 87, 63, 191, 7, 114, 7, 178, 161, 147, 110, 171, 219, 202, 173, 121, 128, 223, 13, 26, 32, 11, 101, 117, 197, 194, 67, 205, 255, 126 }, 1 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "PasswordHash", "PasswordSalt", "UserRoleId" },
                values: new object[] { 2, "user@email.com", "William", "Erlon", new byte[] { 129, 48, 6, 38, 41, 8, 24, 208, 92, 59, 14, 79, 230, 38, 113, 71, 190, 246, 111, 207, 213, 240, 225, 163, 105, 190, 134, 147, 102, 80, 18, 21, 13, 237, 197, 227, 9, 201, 120, 251, 110, 43, 57, 252, 182, 52, 230, 236, 43, 101, 13, 89, 69, 196, 123, 156, 255, 144, 153, 109, 187, 79, 29, 57 }, new byte[] { 98, 69, 116, 93, 112, 237, 59, 37, 83, 183, 58, 28, 59, 195, 12, 214, 75, 101, 164, 194, 95, 170, 108, 77, 214, 249, 36, 94, 3, 210, 90, 181, 16, 200, 170, 116, 243, 133, 247, 87, 193, 116, 172, 234, 84, 61, 45, 45, 221, 18, 44, 44, 107, 84, 59, 78, 83, 156, 82, 4, 109, 132, 144, 22 }, 2 });

            migrationBuilder.CreateIndex(
                name: "IX_UrlsData_LongValue_ShortValue",
                table: "UrlsData",
                columns: new[] { "LongValue", "ShortValue" },
                unique: true,
                filter: "[ShortValue] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UrlsData_OwnerId",
                table: "UrlsData",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserRoleId",
                table: "Users",
                column: "UserRoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AboutSection");

            migrationBuilder.DropTable(
                name: "UrlsData");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "UserRoles");
        }
    }
}
