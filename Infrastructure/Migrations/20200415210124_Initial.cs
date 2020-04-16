using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovimientoFinanciero");

            migrationBuilder.DropTable(
                name: "CuentaBancaria");

            migrationBuilder.CreateTable(
                name: "Empleado",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cedula = table.Column<string>(nullable: true),
                    Nombre = table.Column<string>(nullable: true),
                    Salario = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleado", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Credito",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoCredito = table.Column<string>(nullable: true),
                    Valor = table.Column<double>(nullable: false),
                    PlazoMeses = table.Column<int>(nullable: false),
                    TasaInteres = table.Column<double>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    EmpleadoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Credito", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Credito_Empleado_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Empleado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cuota",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Valor = table.Column<double>(nullable: false),
                    Estado = table.Column<int>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    ValorPagado = table.Column<double>(nullable: false),
                    CreditoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuota", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cuota_Credito_CreditoId",
                        column: x => x.CreditoId,
                        principalTable: "Credito",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pago",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Valor = table.Column<double>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    CreditoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pago", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pago_Credito_CreditoId",
                        column: x => x.CreditoId,
                        principalTable: "Credito",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Credito_EmpleadoId",
                table: "Credito",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Cuota_CreditoId",
                table: "Cuota",
                column: "CreditoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pago_CreditoId",
                table: "Pago",
                column: "CreditoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cuota");

            migrationBuilder.DropTable(
                name: "Pago");

            migrationBuilder.DropTable(
                name: "Credito");

            migrationBuilder.DropTable(
                name: "Empleado");

            migrationBuilder.CreateTable(
                name: "CuentaBancaria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Numero = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Saldo = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CuentaBancaria", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MovimientoFinanciero",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CuentaBancariaId = table.Column<int>(type: "int", nullable: true),
                    FechaMovimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValorConsignacion = table.Column<double>(type: "float", nullable: false),
                    ValorRetiro = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimientoFinanciero", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovimientoFinanciero_CuentaBancaria_CuentaBancariaId",
                        column: x => x.CuentaBancariaId,
                        principalTable: "CuentaBancaria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovimientoFinanciero_CuentaBancariaId",
                table: "MovimientoFinanciero",
                column: "CuentaBancariaId");
        }
    }
}
