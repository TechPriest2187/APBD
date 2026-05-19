using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APBD_TASK_7.Migrations
{
    /// <inheritdoc />
    public partial class AddedStock3Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Stock3",
                table: "Products",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stock3",
                table: "Products");
        }
    }
}
