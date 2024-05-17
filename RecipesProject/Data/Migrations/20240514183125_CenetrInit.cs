using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipesProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class CenetrInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RecyclingCenters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    Paper = table.Column<bool>(type: "bit", nullable: false),
                    Plastic = table.Column<bool>(type: "bit", nullable: false),
                    Metal = table.Column<bool>(type: "bit", nullable: false),
                    Glass = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecyclingCenters", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecyclingCenters");
        }
    }
}
