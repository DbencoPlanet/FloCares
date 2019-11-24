using Microsoft.EntityFrameworkCore.Migrations;

namespace FloCares.Data.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Departments_DepartmentId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_BedAllotments_BedCategory_BedCategoryId",
                table: "BedAllotments");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Doctor_DoctorId",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "MedCatId",
                table: "PrescriptionMedicine");

            migrationBuilder.DropColumn(
                name: "PresId",
                table: "PrescriptionMedicine");

            migrationBuilder.DropColumn(
                name: "AcctId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "MedCatId",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "AcctId",
                table: "InvoiceLines");

            migrationBuilder.DropColumn(
                name: "DeptId",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "PresId",
                table: "Diagnosis");

            migrationBuilder.DropColumn(
                name: "BloodGpId",
                table: "BloodBank");

            migrationBuilder.DropColumn(
                name: "BedCatId",
                table: "Beds");

            migrationBuilder.DropColumn(
                name: "BedCatId",
                table: "BedAllotments");

            migrationBuilder.DropColumn(
                name: "DeptId",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "invoiceNumber",
                table: "Invoices",
                newName: "InvoiceNumber");

            migrationBuilder.AlterColumn<int>(
                name: "DoctorId",
                table: "Reports",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "SenderId",
                table: "Messages",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ReceiverId",
                table: "Messages",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<decimal>(
                name: "Vat",
                table: "Invoices",
                nullable: true,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "Total",
                table: "Invoices",
                nullable: true,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "Paid",
                table: "Invoices",
                nullable: true,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "GrandTotal",
                table: "Invoices",
                nullable: true,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "Due",
                table: "Invoices",
                nullable: true,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "Discount",
                table: "Invoices",
                nullable: true,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "SubTotal",
                table: "InvoiceLines",
                nullable: true,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "InvoiceLines",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "InvoiceLines",
                nullable: true,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<int>(
                name: "BedCategoryId",
                table: "BedAllotments",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "Appointments",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Departments_DepartmentId",
                table: "Appointments",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BedAllotments_BedCategory_BedCategoryId",
                table: "BedAllotments",
                column: "BedCategoryId",
                principalTable: "BedCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Doctor_DoctorId",
                table: "Reports",
                column: "DoctorId",
                principalTable: "Doctor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Departments_DepartmentId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_BedAllotments_BedCategory_BedCategoryId",
                table: "BedAllotments");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Doctor_DoctorId",
                table: "Reports");

            migrationBuilder.RenameColumn(
                name: "InvoiceNumber",
                table: "Invoices",
                newName: "invoiceNumber");

            migrationBuilder.AlterColumn<int>(
                name: "DoctorId",
                table: "Reports",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MedCatId",
                table: "PrescriptionMedicine",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PresId",
                table: "PrescriptionMedicine",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AcctId",
                table: "Payments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "SenderId",
                table: "Messages",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ReceiverId",
                table: "Messages",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MedCatId",
                table: "Medicines",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "Vat",
                table: "Invoices",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Total",
                table: "Invoices",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Paid",
                table: "Invoices",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "GrandTotal",
                table: "Invoices",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Due",
                table: "Invoices",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Discount",
                table: "Invoices",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "SubTotal",
                table: "InvoiceLines",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "InvoiceLines",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "InvoiceLines",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AcctId",
                table: "InvoiceLines",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeptId",
                table: "Doctor",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PresId",
                table: "Diagnosis",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BloodGpId",
                table: "BloodBank",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BedCatId",
                table: "Beds",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BedCategoryId",
                table: "BedAllotments",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "BedCatId",
                table: "BedAllotments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "Appointments",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "DeptId",
                table: "Appointments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Departments_DepartmentId",
                table: "Appointments",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BedAllotments_BedCategory_BedCategoryId",
                table: "BedAllotments",
                column: "BedCategoryId",
                principalTable: "BedCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Doctor_DoctorId",
                table: "Reports",
                column: "DoctorId",
                principalTable: "Doctor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
