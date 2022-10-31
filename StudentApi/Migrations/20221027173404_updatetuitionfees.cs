using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentApi.Migrations
{
    public partial class updatetuitionfees : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentDetails_Hostels_HostelId",
                table: "StudentDetails");

            migrationBuilder.AlterColumn<double>(
                name: "TuitionFees",
                table: "StudentDetails",
                type: "double",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.AlterColumn<int>(
                name: "HostelId",
                table: "StudentDetails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentDetails_Hostels_HostelId",
                table: "StudentDetails",
                column: "HostelId",
                principalTable: "Hostels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentDetails_Hostels_HostelId",
                table: "StudentDetails");

            migrationBuilder.AlterColumn<decimal>(
                name: "TuitionFees",
                table: "StudentDetails",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AlterColumn<int>(
                name: "HostelId",
                table: "StudentDetails",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentDetails_Hostels_HostelId",
                table: "StudentDetails",
                column: "HostelId",
                principalTable: "Hostels",
                principalColumn: "Id");
        }
    }
}
