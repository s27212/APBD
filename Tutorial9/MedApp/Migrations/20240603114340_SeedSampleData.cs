using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedApp.Migrations
{
    /// <inheritdoc />
    public partial class SeedSampleData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
                        migrationBuilder.InsertData(
                table: "Doctor",
                columns: new[] { "IdDoctor", "FirstName", "LastName", "Email" },
                values: new object[,]
                {
                    { 1, "John", "Doe", "john.doe@example.com" },
                    { 2, "Jane", "Smith", "jane.smith@example.com" },
                    { 3, "Emily", "Johnson", "emily.johnson@example.com" },
                    { 4, "Michael", "Williams", "michael.williams@example.com" },
                    { 5, "Jessica", "Brown", "jessica.brown@example.com" },
                    { 6, "Daniel", "Jones", "daniel.jones@example.com" },
                    { 7, "Laura", "Garcia", "laura.garcia@example.com" },
                    { 8, "David", "Martinez", "david.martinez@example.com" },
                    { 9, "Sarah", "Rodriguez", "sarah.rodriguez@example.com" },
                    { 10, "Paul", "Miller", "paul.miller@example.com" }
                });
                        
            migrationBuilder.InsertData(
                table: "Patient",
                columns: new[] { "IdPatient", "FirstName", "LastName", "Birthdate" },
                values: new object[,]
                {
                    { 1, "Alice", "Williams", new DateTime(1990, 1, 1) },
                    { 2, "Bob", "Brown", new DateTime(1985, 2, 2) },
                    { 3, "Charlie", "Davis", new DateTime(1970, 3, 3) },
                    { 4, "Diana", "Martinez", new DateTime(2000, 4, 4) },
                    { 5, "Ethan", "Garcia", new DateTime(1995, 5, 5) },
                    { 6, "Fiona", "Rodriguez", new DateTime(1988, 6, 6) },
                    { 7, "George", "Miller", new DateTime(1975, 7, 7) },
                    { 8, "Hannah", "Wilson", new DateTime(1992, 8, 8) },
                    { 9, "Isaac", "Moore", new DateTime(1982, 9, 9) },
                    { 10, "Jasmine", "Taylor", new DateTime(2001, 10, 10) }
                });
            
            migrationBuilder.InsertData(
                table: "Medicament",
                columns: new[] { "IdMedicament", "Name", "Description", "Type" },
                values: new object[,]
                {
                    { 1, "Aspirin", "Pain reliever", "Analgesic" },
                    { 2, "Tylenol", "Pain reliever", "Analgesic" },
                    { 3, "Lipitor", "Cholesterol medication", "Statin" },
                    { 4, "Metformin", "Diabetes medication", "Antidiabetic" },
                    { 5, "Amoxicillin", "Antibiotic", "Penicillin" },
                    { 6, "Zoloft", "Antidepressant", "SSRI" },
                    { 7, "Plavix", "Blood thinner", "Antiplatelet" },
                    { 8, "Nexium", "Acid reducer", "Proton Pump Inhibitor" },
                    { 9, "Advair", "Asthma medication", "Corticosteroid" },
                    { 10, "Crestor", "Cholesterol medication", "Statin" }
                });
            
            migrationBuilder.InsertData(
                table: "Prescription",
                columns: new[] { "IdPrescription", "Date", "DueDate", "IdPatient", "IdDoctor" },
                values: new object[,]
                {
                    { 1, DateTime.Now.AddMonths(-1), DateTime.Now.AddMonths(1), 1, 1 },
                    { 2, DateTime.Now.AddMonths(-1), DateTime.Now.AddMonths(1), 2, 2 },
                    { 3, DateTime.Now.AddMonths(-1), DateTime.Now.AddMonths(1), 3, 3 },
                    { 4, DateTime.Now.AddMonths(-1), DateTime.Now.AddMonths(1), 4, 4 },
                    { 5, DateTime.Now.AddMonths(-1), DateTime.Now.AddMonths(1), 5, 5 },
                    { 6, DateTime.Now.AddMonths(-1), DateTime.Now.AddMonths(1), 6, 6 },
                    { 7, DateTime.Now.AddMonths(-1), DateTime.Now.AddMonths(1), 7, 7 },
                    { 8, DateTime.Now.AddMonths(-1), DateTime.Now.AddMonths(1), 8, 8 },
                    { 9, DateTime.Now.AddMonths(-1), DateTime.Now.AddMonths(1), 9, 9 },
                    { 10, DateTime.Now.AddMonths(-1), DateTime.Now.AddMonths(1), 10, 10 }
                });
            
            migrationBuilder.InsertData(
                table: "PrescriptionMedicament",
                columns: new[] { "IdMedicament", "IdPrescription", "Dose", "Details" },
                values: new object[,]
                {
                    { 1, 1, 1, "Take one daily" },
                    { 2, 2, 2, "Take two daily" },
                    { 3, 3, 1, "Take one daily" },
                    { 4, 4, 1, "Take one daily" },
                    { 5, 5, 3, "Take three daily" },
                    { 6, 6, 2, "Take two daily" },
                    { 7, 7, 1, "Take one daily" },
                    { 8, 8, 2, "Take two daily" },
                    { 9, 9, 1, "Take one daily" },
                    { 10, 10, 2, "Take two daily" }
                });
        }
        }
    }
