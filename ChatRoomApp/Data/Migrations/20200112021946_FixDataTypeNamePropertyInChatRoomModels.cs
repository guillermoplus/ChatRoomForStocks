using Microsoft.EntityFrameworkCore.Migrations;

namespace ChatRoomApp.Data.Migrations
{
    public partial class FixDataTypeNamePropertyInChatRoomModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ChatRooms",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "ChatRooms",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
