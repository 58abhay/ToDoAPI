using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexesToUserAndToDo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_User_UserEmail",
                table: "Users",
                column: "UserEmail");

            migrationBuilder.CreateIndex(
                name: "IX_ToDo_Task",
                table: "ToDos",
                column: "Task");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User_UserEmail",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_ToDo_Task",
                table: "ToDos");
        }
    }
}
