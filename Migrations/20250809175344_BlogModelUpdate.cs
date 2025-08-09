using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Migrations
{
    /// <inheritdoc />
    public partial class BlogModelUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "INSCRICOES_BLOG");

            migrationBuilder.AddColumn<int>(
                name: "Avaliacao",
                table: "INSCRICOES_BLOG",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "BLOGS",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avaliacao",
                table: "INSCRICOES_BLOG");

            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "BLOGS");

            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "INSCRICOES_BLOG",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
