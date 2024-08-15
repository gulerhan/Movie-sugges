using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieSugges.Migrations
{
    /// <inheritdoc />
    public partial class UserToRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Mevcut Role sütununu Users tablosundan kaldır
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            // Users tablosunda RoleId için bir indeks oluştur
            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            // Users tablosunda RoleId sütunu için bir dış anahtar kısıtlaması ekle
            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            // Roles tablosunu doldurmak için SQL komutları
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT 1 FROM Roles WHERE Id = 1)
                BEGIN
                    INSERT INTO Roles (Id, RoleName) VALUES (1, 'Admin');
                END

                IF NOT EXISTS (SELECT 1 FROM Roles WHERE Id = 2)
                BEGIN
                    INSERT INTO Roles (Id, RoleName) VALUES (2, 'User');
                END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Dış anahtar kısıtlamasını kaldır
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            // RoleId indeksini kaldır
            migrationBuilder.DropIndex(
                name: "IX_Users_RoleId",
                table: "Users");

            // Users tablosuna eski Role sütununu yeniden ekle
            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
