using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EticaretAPI.Persistence.Migrations
{
    public partial class mig_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompleteOrder_Orders_OrderId",
                table: "CompleteOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompleteOrder",
                table: "CompleteOrder");

            migrationBuilder.DropColumn(
                name: "BasketId",
                table: "Orders");

            migrationBuilder.RenameTable(
                name: "CompleteOrder",
                newName: "CompleteOrders");

            migrationBuilder.RenameIndex(
                name: "IX_CompleteOrder_OrderId",
                table: "CompleteOrders",
                newName: "IX_CompleteOrders_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompleteOrders",
                table: "CompleteOrders",
                column: "ID");

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

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompleteOrders",
                table: "CompleteOrders");

            migrationBuilder.RenameTable(
                name: "CompleteOrders",
                newName: "CompleteOrder");

            migrationBuilder.RenameIndex(
                name: "IX_CompleteOrders_OrderId",
                table: "CompleteOrder",
                newName: "IX_CompleteOrder_OrderId");

            migrationBuilder.AddColumn<Guid>(
                name: "BasketId",
                table: "Orders",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompleteOrder",
                table: "CompleteOrder",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_CompleteOrder_Orders_OrderId",
                table: "CompleteOrder",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
