using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TodoApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tables",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Priority = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDone = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tables", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Tables",
                columns: new[] { "Id", "Description", "DueDate", "Priority", "Title" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Süt,ekmek,yumurta", new DateTime(2026, 10, 6, 0, 0, 0, 0, DateTimeKind.Local), 1, "Alışveriş yap" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "Pazartesi toplantısı için slaytlar", new DateTime(2026, 10, 8, 0, 0, 0, 0, DateTimeKind.Local), 2, "Sunum Hazırla" }
                });

            migrationBuilder.InsertData(
                table: "Tables",
                columns: new[] { "Id", "Description", "DueDate", "IsDone", "Title" },
                values: new object[] { new Guid("33333333-3333-3333-3333-333333333333"), "30 dk koşu", new DateTime(2026, 10, 7, 0, 0, 0, 0, DateTimeKind.Local), true, "Spor" });

            migrationBuilder.InsertData(
                table: "Tables",
                columns: new[] { "Id", "Description", "DueDate", "Priority", "Title" },
                values: new object[] { new Guid("44444444-4444-4444-4444-444444444444"), "Yağ değişimi ve filtreler", new DateTime(2026, 10, 12, 0, 0, 0, 0, DateTimeKind.Local), 1, "Araba bakımı" });

            migrationBuilder.CreateIndex(
                name: "IX_Tables_DueDate",
                table: "Tables",
                column: "DueDate");

            migrationBuilder.CreateIndex(
                name: "IX_Tables_IsDone",
                table: "Tables",
                column: "IsDone");

            migrationBuilder.CreateIndex(
                name: "IX_Tables_Priority",
                table: "Tables",
                column: "Priority");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tables");
        }
    }
}
