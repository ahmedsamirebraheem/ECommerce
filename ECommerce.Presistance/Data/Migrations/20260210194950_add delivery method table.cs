using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Presistance.Data.Migrations
{
    /// <inheritdoc />
    public partial class adddeliverymethodtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_DeliveryMethod_DeliveryMethodId",
                table: "Order");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeliveryMethod",
                table: "DeliveryMethod");

            migrationBuilder.RenameTable(
                name: "DeliveryMethod",
                newName: "DeliveryMethods");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeliveryMethods",
                table: "DeliveryMethods",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_DeliveryMethods_DeliveryMethodId",
                table: "Order",
                column: "DeliveryMethodId",
                principalTable: "DeliveryMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_DeliveryMethods_DeliveryMethodId",
                table: "Order");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeliveryMethods",
                table: "DeliveryMethods");

            migrationBuilder.RenameTable(
                name: "DeliveryMethods",
                newName: "DeliveryMethod");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeliveryMethod",
                table: "DeliveryMethod",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_DeliveryMethod_DeliveryMethodId",
                table: "Order",
                column: "DeliveryMethodId",
                principalTable: "DeliveryMethod",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
