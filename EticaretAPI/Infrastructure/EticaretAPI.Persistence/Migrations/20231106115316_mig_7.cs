using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EticaretAPI.Persistence.Migrations
{
    public partial class mig_7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CompleteOrders_OrderId",
                table: "CompleteOrders",
                column: "OrderId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CompleteOrders_Orders_OrderId",
                table: "CompleteOrders",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompleteOrders_Orders_OrderId",
                table: "CompleteOrders");

            migrationBuilder.DropIndex(
                name: "IX_CompleteOrders_OrderId",
                table: "CompleteOrders");
        }
    }
}
