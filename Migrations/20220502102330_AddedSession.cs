using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace elearning_platform.Migrations
{
    public partial class AddedSession : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentOrders",
                columns: table => new
                {
                    PaymentOrderId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentOrders", x => x.PaymentOrderId);
                });

            migrationBuilder.CreateTable(
                name: "TutorRequests",
                columns: table => new
                {
                    TutorRequestId = table.Column<Guid>(type: "uuid", nullable: false),
                    StudentId = table.Column<Guid>(type: "uuid", nullable: false),
                    TaughtSubjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    OnlineSession = table.Column<bool>(type: "boolean", nullable: false),
                    SessionLength = table.Column<int>(type: "integer", nullable: false),
                    PreferredDates = table.Column<DateTime[]>(type: "timestamp with time zone[]", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TutorRequests", x => x.TutorRequestId);
                    table.ForeignKey(
                        name: "FK_TutorRequests_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TutorRequests_TaughtSubjects_TaughtSubjectId",
                        column: x => x.TaughtSubjectId,
                        principalTable: "TaughtSubjects",
                        principalColumn: "TaughtSubjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    SessionId = table.Column<Guid>(type: "uuid", nullable: false),
                    TutorRequestId = table.Column<Guid>(type: "uuid", nullable: false),
                    PaymentStatus = table.Column<string>(type: "text", nullable: false),
                    BookingStatus = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.SessionId);
                    table.ForeignKey(
                        name: "FK_Sessions_TutorRequests_TutorRequestId",
                        column: x => x.TutorRequestId,
                        principalTable: "TutorRequests",
                        principalColumn: "TutorRequestId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OnlineSession",
                columns: table => new
                {
                    OnlineSessionId = table.Column<Guid>(type: "uuid", nullable: false),
                    PaymentOrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    SessionId = table.Column<Guid>(type: "uuid", nullable: false),
                    VideoChatLink = table.Column<string>(type: "text", nullable: false),
                    TutorRequestId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnlineSession", x => x.OnlineSessionId);
                    table.ForeignKey(
                        name: "FK_OnlineSession_PaymentOrders_PaymentOrderId",
                        column: x => x.PaymentOrderId,
                        principalTable: "PaymentOrders",
                        principalColumn: "PaymentOrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OnlineSession_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "SessionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OnlineSession_TutorRequests_TutorRequestId",
                        column: x => x.TutorRequestId,
                        principalTable: "TutorRequests",
                        principalColumn: "TutorRequestId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OnlineSession_PaymentOrderId",
                table: "OnlineSession",
                column: "PaymentOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OnlineSession_SessionId",
                table: "OnlineSession",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_OnlineSession_TutorRequestId",
                table: "OnlineSession",
                column: "TutorRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_TutorRequestId",
                table: "Sessions",
                column: "TutorRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_TutorRequests_StudentId",
                table: "TutorRequests",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_TutorRequests_TaughtSubjectId",
                table: "TutorRequests",
                column: "TaughtSubjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OnlineSession");

            migrationBuilder.DropTable(
                name: "PaymentOrders");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "TutorRequests");
        }
    }
}
