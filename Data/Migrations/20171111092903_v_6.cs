using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Data.Migrations
{
    public partial class v_6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "Terms",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "Offices",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "NumericCode",
                table: "Countries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "Countries",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "Cities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "Cities",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "ProcessQueuesHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddedToQueue = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ErrorMesage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FinishedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessQueuesHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcessQueuesHistory_AspNetUsers_AddedById",
                        column: x => x.AddedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProcessQueuesHistory_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Terms_TenantId",
                table: "Terms",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Offices_TenantId",
                table: "Offices",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_TenantId",
                table: "Countries",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_TenantId",
                table: "Cities",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessQueuesHistory_AddedById",
                table: "ProcessQueuesHistory",
                column: "AddedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessQueuesHistory_TenantId",
                table: "ProcessQueuesHistory",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Tenants_TenantId",
                table: "Cities",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Countries_Tenants_TenantId",
                table: "Countries",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Offices_Tenants_TenantId",
                table: "Offices",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Terms_Tenants_TenantId",
                table: "Terms",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Tenants_TenantId",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_Countries_Tenants_TenantId",
                table: "Countries");

            migrationBuilder.DropForeignKey(
                name: "FK_Offices_Tenants_TenantId",
                table: "Offices");

            migrationBuilder.DropForeignKey(
                name: "FK_Terms_Tenants_TenantId",
                table: "Terms");

            migrationBuilder.DropTable(
                name: "ProcessQueuesHistory");

            migrationBuilder.DropIndex(
                name: "IX_Terms_TenantId",
                table: "Terms");

            migrationBuilder.DropIndex(
                name: "IX_Offices_TenantId",
                table: "Offices");

            migrationBuilder.DropIndex(
                name: "IX_Countries_TenantId",
                table: "Countries");

            migrationBuilder.DropIndex(
                name: "IX_Cities_TenantId",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Terms");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Offices");

            migrationBuilder.DropColumn(
                name: "NumericCode",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Cities");
        }
    }
}
