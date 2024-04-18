using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgendamentoBackend.Migrations
{
    public partial class newmig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Servico",
                newName: "NomeService");

            migrationBuilder.AddColumn<DateTime>(
                name: "HorarioAgendado",
                table: "Agendamento",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HorarioAgendado",
                table: "Agendamento");

            migrationBuilder.RenameColumn(
                name: "NomeService",
                table: "Servico",
                newName: "Nome");
        }
    }
}
