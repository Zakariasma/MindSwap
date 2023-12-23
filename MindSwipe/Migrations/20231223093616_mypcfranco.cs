using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MindSwipe.Migrations
{
    public partial class mypcfranco : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CardUploadModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FrontImgName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    BackImgName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CardID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardUploadModel", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CardUploadModel");
        }
    }
}
