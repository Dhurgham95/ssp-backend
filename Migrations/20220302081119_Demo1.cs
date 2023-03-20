using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class Demo1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Role = table.Column<string>(nullable: true),
                    Branch = table.Column<string>(nullable: true),
                    Department = table.Column<string>(nullable: true),
                    AccountNumber = table.Column<string>(nullable: true),
                    IBAN = table.Column<string>(nullable: true),
                    BussinessName = table.Column<string>(nullable: true),
                    IsOnTmweel = table.Column<bool>(nullable: false),
                    CurrentTmweelEndingTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Goods",
                columns: table => new
                {
                    GoodId = table.Column<string>(nullable: false),
                    GoodSeq = table.Column<int>(nullable: false),
                    GoodStore = table.Column<string>(nullable: true),
                    GoodDimensions = table.Column<string>(nullable: true),
                    GoodName = table.Column<string>(nullable: true),
                    GoodQuentity = table.Column<int>(nullable: false),
                    UnitPrice = table.Column<double>(nullable: false),
                    SumPrice = table.Column<double>(nullable: false),
                    Currency = table.Column<string>(nullable: true),
                    CustomerAcctNum = table.Column<string>(nullable: true),
                    MerchantAcctNum = table.Column<string>(nullable: true),
                    BillId = table.Column<string>(nullable: true),
                    BillSeq = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goods", x => x.GoodId);
                });

            migrationBuilder.CreateTable(
                name: "SmsVerificationCodes",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    VerificationCode = table.Column<string>(nullable: true),
                    AccountNumber = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    InsetedOn = table.Column<DateTime>(nullable: false),
                    VerivicationCodeExpireOn = table.Column<DateTime>(nullable: false),
                    IsExpired = table.Column<bool>(nullable: false),
                    PersonalAccountIdPa = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmsVerificationCodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AskBanks",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerNumberRelatedToAccountNumber = table.Column<string>(nullable: true),
                    AccountNumberRequestInfo = table.Column<string>(nullable: true),
                    PhoneNumberRelatedToAccountNumber = table.Column<string>(nullable: true),
                    RequestInfoDateTime = table.Column<DateTime>(nullable: false),
                    RequestAnswer = table.Column<string>(nullable: true),
                    RequestAnswerDateTime = table.Column<DateTime>(nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AskBanks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AskBanks_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    BillId = table.Column<string>(nullable: false),
                    BillSeq = table.Column<string>(nullable: true),
                    BillDate = table.Column<DateTime>(nullable: false),
                    MerchantNameForBill = table.Column<string>(nullable: true),
                    MerchantAcctNumForBill = table.Column<string>(nullable: true),
                    MerchantPhoneNumber = table.Column<string>(nullable: true),
                    MerchantBussinessName = table.Column<string>(nullable: true),
                    BillAmount = table.Column<string>(nullable: true),
                    CustomerNameForBill = table.Column<string>(nullable: true),
                    CustomerAcctNumForBill = table.Column<string>(nullable: true),
                    CustomerBirth = table.Column<DateTime>(nullable: false),
                    CustomerAddressForBill = table.Column<string>(nullable: true),
                    CustomerPhoneNumberForBill = table.Column<string>(nullable: true),
                    TotalSalaryForBill = table.Column<string>(nullable: true),
                    PayCountForBill = table.Column<string>(nullable: true),
                    MonthlyPayAmt = table.Column<string>(nullable: true),
                    InsurancePay = table.Column<string>(nullable: true),
                    MerchantPay = table.Column<string>(nullable: true),
                    BankProfit = table.Column<string>(nullable: true),
                    StartPayDateForBill = table.Column<DateTime>(nullable: false),
                    EndPayDateForBill = table.Column<DateTime>(nullable: false),
                    BillDoc1 = table.Column<string>(nullable: true),
                    Bill1IncertedOn = table.Column<DateTime>(nullable: false),
                    BillDoc2 = table.Column<string>(nullable: true),
                    Bill2IncertedOn = table.Column<DateTime>(nullable: false),
                    BillDoc3 = table.Column<string>(nullable: true),
                    Bill3IncertedOn = table.Column<DateTime>(nullable: false),
                    BillDoc4 = table.Column<string>(nullable: true),
                    Bill4IncertedOn = table.Column<DateTime>(nullable: false),
                    BillDoc5 = table.Column<string>(nullable: true),
                    Bill5IncertedOn = table.Column<DateTime>(nullable: false),
                    BillDoc6 = table.Column<string>(nullable: true),
                    Bill6IncertedOn = table.Column<DateTime>(nullable: false),
                    billStatus = table.Column<string>(nullable: true),
                    billStatusDate = table.Column<DateTime>(nullable: false),
                    SecondCompanyNotification = table.Column<bool>(nullable: false),
                    DirectiveManagerNotification = table.Column<bool>(nullable: false),
                    OperationsNotification = table.Column<bool>(nullable: false),
                    FirstAuditFromCompanyEmployee = table.Column<bool>(nullable: false),
                    FirstAuditFromCompanyEmployeeDate = table.Column<DateTime>(nullable: false),
                    SecondAuditFromCompanyEmployee = table.Column<bool>(nullable: false),
                    SecondAuditFromCompanyEmployeeDate = table.Column<DateTime>(nullable: false),
                    IsInsured = table.Column<bool>(nullable: false),
                    IsInsuredDate = table.Column<DateTime>(nullable: false),
                    NoteForRejection = table.Column<string>(nullable: true),
                    NoteForRejectionDate = table.Column<DateTime>(nullable: false),
                    NoteForInsuranceRejection = table.Column<string>(nullable: true),
                    NoteForInsuranceRejectionDate = table.Column<DateTime>(nullable: false),
                    NoteForOperationRejection = table.Column<string>(nullable: true),
                    NoteForOperationRejectionDate = table.Column<DateTime>(nullable: false),
                    NoteOne = table.Column<string>(nullable: true),
                    NoteOneDate = table.Column<DateTime>(nullable: false),
                    NoteTwo = table.Column<string>(nullable: true),
                    NoteTwoDate = table.Column<DateTime>(nullable: false),
                    NoteThree = table.Column<string>(nullable: true),
                    NoteThreeDate = table.Column<DateTime>(nullable: false),
                    DirectiveManagerApproval = table.Column<bool>(nullable: false),
                    DirectiveManagerApprovalDate = table.Column<DateTime>(nullable: false),
                    DirectiveManagerApprovalNote = table.Column<string>(nullable: true),
                    DirectiveManagerApprovalNoteDate = table.Column<DateTime>(nullable: false),
                    DirectiveManagerRejectionNote = table.Column<string>(nullable: true),
                    DirectiveManagerRejectionNoteDate = table.Column<DateTime>(nullable: false),
                    OperationNoteForOperations = table.Column<string>(nullable: true),
                    OperationNoteForOperationsDate = table.Column<DateTime>(nullable: false),
                    IsOnTmweelFlag = table.Column<bool>(nullable: false),
                    FCNotification = table.Column<bool>(nullable: false),
                    InsuranceComapnyNotification = table.Column<bool>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.BillId);
                    table.ForeignKey(
                        name: "FK_Bills_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BillTs",
                columns: table => new
                {
                    BillTId = table.Column<string>(nullable: false),
                    MerchantNameForBill = table.Column<string>(nullable: true),
                    MerchantAcctNumForBill = table.Column<string>(nullable: true),
                    BillDoc1 = table.Column<string>(nullable: true),
                    Bill1IncertedOn = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillTs", x => x.BillTId);
                    table.ForeignKey(
                        name: "FK_BillTs_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NotPayAlarms",
                columns: table => new
                {
                    NotPayAlarmId = table.Column<string>(nullable: false),
                    IsNotPay = table.Column<bool>(nullable: false),
                    CustomerPhoneNumber = table.Column<string>(nullable: true),
                    CustomerAccNumber = table.Column<string>(nullable: true),
                    CustomerName = table.Column<string>(nullable: true),
                    MerchantAcctNum = table.Column<string>(nullable: true),
                    MerchantBussinessName = table.Column<string>(nullable: true),
                    MerchantAccoutNumber = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotPayAlarms", x => x.NotPayAlarmId);
                    table.ForeignKey(
                        name: "FK_NotPayAlarms_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonalAccounts",
                columns: table => new
                {
                    ID_PA = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_brn = table.Column<int>(nullable: true),
                    AccountType_PA = table.Column<string>(nullable: true),
                    AccountNamber_PA = table.Column<string>(nullable: true),
                    AccountOpeningDate_PA = table.Column<DateTime>(type: "date", nullable: true),
                    IraqiDinar_PA = table.Column<string>(maxLength: 10, nullable: true),
                    USDollar_PA = table.Column<string>(maxLength: 10, nullable: true),
                    AnotherCurrency_PA = table.Column<string>(maxLength: 10, nullable: true),
                    TypeAnotherCurrency_PA = table.Column<string>(nullable: true),
                    legalStatus_PA = table.Column<string>(nullable: true),
                    FullNameArab_PA = table.Column<string>(nullable: true),
                    FullNameEnglish_PA = table.Column<string>(nullable: true),
                    PassportNumber_PA = table.Column<string>(nullable: true),
                    IssuingThePassport_PA = table.Column<string>(nullable: true),
                    DateOfIssuanceOfPassport_PA = table.Column<DateTime>(type: "date", nullable: true),
                    TheExpiryDateOfThePassport_PA = table.Column<DateTime>(type: "date", nullable: true),
                    DocumentType_PA = table.Column<string>(nullable: true),
                    DocumentNumber_PA = table.Column<string>(nullable: true),
                    IssuerOfTheDocument_PA = table.Column<string>(nullable: true),
                    DateOfTheDocument_PA = table.Column<DateTime>(type: "date", nullable: true),
                    ExpiryDateOfTheDocument_PA = table.Column<DateTime>(type: "date", nullable: true),
                    Nationality_PA = table.Column<string>(nullable: true),
                    PlaceOfBirth_PA = table.Column<string>(nullable: true),
                    DateOfBirth_PA = table.Column<DateTime>(type: "date", nullable: true),
                    OtherNationality_PA = table.Column<string>(nullable: true),
                    TypeOtherNationality_PA = table.Column<string>(nullable: true),
                    PassportNumberForOtherNationality_PA = table.Column<string>(nullable: true),
                    EducationLevel_PA = table.Column<string>(nullable: true),
                    SocialStatus_PA = table.Column<string>(nullable: true),
                    MotherName_PA = table.Column<string>(nullable: true),
                    WifeName_PA = table.Column<string>(nullable: true),
                    NumberOfChildren_PA = table.Column<string>(nullable: true),
                    HomeAdress_PA = table.Column<string>(nullable: true),
                    TheNearestPoint_PA = table.Column<string>(nullable: true),
                    AnotherCountry_PA = table.Column<string>(nullable: true),
                    HomeAdressAnotherCountry_PA = table.Column<string>(nullable: true),
                    AccommodationType_PA = table.Column<string>(nullable: true),
                    NumberPhone1_PA = table.Column<string>(nullable: true),
                    NumberPhone2_PA = table.Column<string>(nullable: true),
                    Email_PA = table.Column<string>(nullable: true),
                    SomeoneWhoCanBeContacted_PA = table.Column<string>(nullable: true),
                    PhoneSomeoneWhoCanBeContacted_PA = table.Column<string>(nullable: true),
                    TheWork_PA = table.Column<string>(nullable: true),
                    CompanyName_PA = table.Column<string>(nullable: true),
                    NameOfTheInstitutionOwner_PA = table.Column<string>(nullable: true),
                    ActivityFoundationWork_PA = table.Column<string>(nullable: true),
                    JobTitle_PA = table.Column<string>(nullable: true),
                    StartDateOfWork_PA = table.Column<DateTime>(type: "date", nullable: true),
                    WorkIDNumber_PA = table.Column<string>(nullable: true),
                    EnterprisePhoneNumber_PA = table.Column<string>(nullable: true),
                    NationalityOfTheInstitution_PA = table.Column<string>(nullable: true),
                    TheAddressOfTheOrganization_PA = table.Column<string>(nullable: true),
                    CopyOfTheDocument_PA = table.Column<string>(nullable: true),
                    CopyOfThePassport_PA = table.Column<string>(nullable: true),
                    CopyOfTheHousingCard_PA = table.Column<string>(nullable: true),
                    CopyOfFinancialReports_PA = table.Column<string>(nullable: true),
                    WhyTheAccountIsNotManagedByTheBeneficiary_PA = table.Column<string>(nullable: true),
                    TheNamesOfBanks_PA = table.Column<string>(nullable: true),
                    DatesOfTransactionsBanks_PA = table.Column<string>(nullable: true),
                    AmountOfCreditFacilities_PA = table.Column<string>(nullable: true),
                    Salary_PA = table.Column<string>(maxLength: 10, nullable: true),
                    CommercialReturns_PA = table.Column<string>(maxLength: 10, nullable: true),
                    PersonalSavings_PA = table.Column<string>(maxLength: 10, nullable: true),
                    Investments_PA = table.Column<string>(maxLength: 10, nullable: true),
                    OtherSources_PA = table.Column<string>(maxLength: 10, nullable: true),
                    NameOtherSources_PA = table.Column<string>(nullable: true),
                    LessThan1Million_PA = table.Column<string>(maxLength: 10, nullable: true),
                    From1To5Million_PA = table.Column<string>(maxLength: 10, nullable: true),
                    From6To10Million_PA = table.Column<string>(maxLength: 10, nullable: true),
                    From10To25Million_PA = table.Column<string>(maxLength: 10, nullable: true),
                    MoreThan25Million_PA = table.Column<string>(maxLength: 10, nullable: true),
                    MonthlyEstimate_PA = table.Column<string>(nullable: true),
                    AnnualEstimate_PA = table.Column<string>(nullable: true),
                    NatureOfExpectedBusiness_PA = table.Column<string>(nullable: true),
                    OppositionPolitician_PA = table.Column<string>(nullable: true),
                    AnotherCustomerName_PA = table.Column<string>(nullable: true),
                    GovernmentTransactions_PA = table.Column<string>(nullable: true),
                    IssuingAcheckBook_PA = table.Column<string>(nullable: true),
                    NumberOfcheckBook_PA = table.Column<string>(nullable: true),
                    TheNamePrintedOnTheMasterCard_PA = table.Column<string>(nullable: true),
                    FatcaRegulations_PA = table.Column<string>(maxLength: 10, nullable: true),
                    NOTFatcaRegulations_PA = table.Column<string>(maxLength: 10, nullable: true),
                    FormOrganizerName_PA = table.Column<string>(nullable: true),
                    DateOfRegulation_PA = table.Column<DateTime>(type: "date", nullable: true),
                    Nots_PA = table.Column<string>(nullable: true),
                    NewInternetBanking_PA = table.Column<string>(maxLength: 10, nullable: true),
                    RenewalInternetBanking_PA = table.Column<string>(maxLength: 10, nullable: true),
                    AML_PA = table.Column<string>(maxLength: 10, nullable: true),
                    id_user = table.Column<int>(nullable: true),
                    OutIN_PA = table.Column<string>(nullable: true),
                    witness_PA = table.Column<string>(nullable: true),
                    FinancialStatements_PA = table.Column<string>(nullable: true),
                    ExtraIncome_PA = table.Column<string>(nullable: true),
                    checkExtraIncome_PA = table.Column<string>(maxLength: 10, nullable: true),
                    FirstAmount_PA = table.Column<string>(nullable: true),
                    CardSsatus_PA = table.Column<string>(nullable: true),
                    DeliveryDate_PA = table.Column<string>(nullable: true),
                    Branches_PA = table.Column<string>(nullable: true),
                    AllowedToTakeTmweel = table.Column<string>(nullable: true),
                    NoteForGivenTmweel = table.Column<string>(nullable: true),
                    IsOnTmweelFlag = table.Column<bool>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalAccounts", x => x.ID_PA);
                    table.ForeignKey(
                        name: "FK_PersonalAccounts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InsuranceDocs",
                columns: table => new
                {
                    InsuranceDocId = table.Column<string>(nullable: false),
                    BillAmount = table.Column<string>(nullable: true),
                    MerchantName = table.Column<string>(nullable: true),
                    CustomerName = table.Column<string>(nullable: true),
                    MonthlyPay = table.Column<string>(nullable: true),
                    PayCount = table.Column<string>(nullable: true),
                    InsuranceDoc1 = table.Column<string>(nullable: true),
                    Insurance1IncertedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    InsuranceDoc2 = table.Column<string>(nullable: true),
                    Insurance2IncertedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    InsuranceDoc3 = table.Column<string>(nullable: true),
                    Insurance3IncertedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    BillId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuranceDocs", x => x.InsuranceDocId);
                    table.ForeignKey(
                        name: "FK_InsuranceDocs_Bills_BillId",
                        column: x => x.BillId,
                        principalTable: "Bills",
                        principalColumn: "BillId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InsuranceTimeToPays",
                columns: table => new
                {
                    InsuranceTimeToPayId = table.Column<string>(nullable: false),
                    YearOne = table.Column<DateTime>(nullable: false),
                    YearTwo = table.Column<DateTime>(nullable: false),
                    YearThree = table.Column<DateTime>(nullable: false),
                    BillId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuranceTimeToPays", x => x.InsuranceTimeToPayId);
                    table.ForeignKey(
                        name: "FK_InsuranceTimeToPays_Bills_BillId",
                        column: x => x.BillId,
                        principalTable: "Bills",
                        principalColumn: "BillId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PartialPays",
                columns: table => new
                {
                    PartialPayId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Num = table.Column<int>(nullable: false),
                    PartialPayDate = table.Column<DateTime>(nullable: false),
                    PartialPayStatus = table.Column<string>(nullable: true),
                    PartialPayAmount = table.Column<double>(nullable: false),
                    BillSeq = table.Column<string>(nullable: true),
                    BillId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartialPays", x => x.PartialPayId);
                    table.ForeignKey(
                        name: "FK_PartialPays_Bills_BillId",
                        column: x => x.BillId,
                        principalTable: "Bills",
                        principalColumn: "BillId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NotPayMonths",
                columns: table => new
                {
                    NotPayMonthId = table.Column<string>(nullable: false),
                    Month = table.Column<DateTime>(nullable: false),
                    NotPayAlarmId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotPayMonths", x => x.NotPayMonthId);
                    table.ForeignKey(
                        name: "FK_NotPayMonths_NotPayAlarms_NotPayAlarmId",
                        column: x => x.NotPayAlarmId,
                        principalTable: "NotPayAlarms",
                        principalColumn: "NotPayAlarmId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AskBanks_UserId",
                table: "AskBanks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_UserId",
                table: "Bills",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BillTs_UserId",
                table: "BillTs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceDocs_BillId",
                table: "InsuranceDocs",
                column: "BillId",
                unique: true,
                filter: "[BillId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceTimeToPays_BillId",
                table: "InsuranceTimeToPays",
                column: "BillId",
                unique: true,
                filter: "[BillId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_NotPayAlarms_UserId",
                table: "NotPayAlarms",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_NotPayMonths_NotPayAlarmId",
                table: "NotPayMonths",
                column: "NotPayAlarmId");

            migrationBuilder.CreateIndex(
                name: "IX_PartialPays_BillId",
                table: "PartialPays",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalAccounts_UserId",
                table: "PersonalAccounts",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AskBanks");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BillTs");

            migrationBuilder.DropTable(
                name: "Goods");

            migrationBuilder.DropTable(
                name: "InsuranceDocs");

            migrationBuilder.DropTable(
                name: "InsuranceTimeToPays");

            migrationBuilder.DropTable(
                name: "NotPayMonths");

            migrationBuilder.DropTable(
                name: "PartialPays");

            migrationBuilder.DropTable(
                name: "PersonalAccounts");

            migrationBuilder.DropTable(
                name: "SmsVerificationCodes");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "NotPayAlarms");

            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
