using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class PersonalAccount
    {
        [Key]
        [Column("ID_PA")]
        public int IdPa { get; set; }
        [Column("ID_brn")]
        public int? IdBrn { get; set; }
        [Column("AccountType_PA")]
        public string AccountTypePa { get; set; }
        [Column("AccountNamber_PA")]
        public string AccountNamberPa { get; set; }
        [Column("AccountOpeningDate_PA", TypeName = "date")]
        public DateTime? AccountOpeningDatePa { get; set; }
        [Column("IraqiDinar_PA")]
        [StringLength(10)]
        public string IraqiDinarPa { get; set; }
        [Column("USDollar_PA")]
        [StringLength(10)]
        public string UsdollarPa { get; set; }
        [Column("AnotherCurrency_PA")]
        [StringLength(10)]
        public string AnotherCurrencyPa { get; set; }
        [Column("TypeAnotherCurrency_PA")]
        public string TypeAnotherCurrencyPa { get; set; }
        [Column("legalStatus_PA")]
        public string LegalStatusPa { get; set; }
        [Column("FullNameArab_PA")]
        public string FullNameArabPa { get; set; }
        [Column("FullNameEnglish_PA")]
        public string FullNameEnglishPa { get; set; }
        [Column("PassportNumber_PA")]
        public string PassportNumberPa { get; set; }
        [Column("IssuingThePassport_PA")]
        public string IssuingThePassportPa { get; set; }
        [Column("DateOfIssuanceOfPassport_PA", TypeName = "date")]
        public DateTime? DateOfIssuanceOfPassportPa { get; set; }
        [Column("TheExpiryDateOfThePassport_PA", TypeName = "date")]
        public DateTime? TheExpiryDateOfThePassportPa { get; set; }
        [Column("DocumentType_PA")]
        public string DocumentTypePa { get; set; }
        [Column("DocumentNumber_PA")]
        public string DocumentNumberPa { get; set; }
        [Column("IssuerOfTheDocument_PA")]
        public string IssuerOfTheDocumentPa { get; set; }
        [Column("DateOfTheDocument_PA", TypeName = "date")]
        public DateTime? DateOfTheDocumentPa { get; set; }
        [Column("ExpiryDateOfTheDocument_PA", TypeName = "date")]
        public DateTime? ExpiryDateOfTheDocumentPa { get; set; }
        [Column("Nationality_PA")]
        public string NationalityPa { get; set; }
        [Column("PlaceOfBirth_PA")]
        public string PlaceOfBirthPa { get; set; }
        [Column("DateOfBirth_PA", TypeName = "date")]
        public DateTime? DateOfBirthPa { get; set; }
        [Column("OtherNationality_PA")]
        public string OtherNationalityPa { get; set; }
        [Column("TypeOtherNationality_PA")]
        public string TypeOtherNationalityPa { get; set; }
        [Column("PassportNumberForOtherNationality_PA")]
        public string PassportNumberForOtherNationalityPa { get; set; }
        [Column("EducationLevel_PA")]
        public string EducationLevelPa { get; set; }
        [Column("SocialStatus_PA")]
        public string SocialStatusPa { get; set; }
        [Column("MotherName_PA")]
        public string MotherNamePa { get; set; }
        [Column("WifeName_PA")]
        public string WifeNamePa { get; set; }
        [Column("NumberOfChildren_PA")]
        public string NumberOfChildrenPa { get; set; }
        [Column("HomeAdress_PA")]
        public string HomeAdressPa { get; set; }
        [Column("TheNearestPoint_PA")]
        public string TheNearestPointPa { get; set; }
        [Column("AnotherCountry_PA")]
        public string AnotherCountryPa { get; set; }
        [Column("HomeAdressAnotherCountry_PA")]
        public string HomeAdressAnotherCountryPa { get; set; }
        [Column("AccommodationType_PA")]
        public string AccommodationTypePa { get; set; }
        [Column("NumberPhone1_PA")]
        public string NumberPhone1Pa { get; set; }
        [Column("NumberPhone2_PA")]
        public string NumberPhone2Pa { get; set; }
        [Column("Email_PA")]
        public string EmailPa { get; set; }
        [Column("SomeoneWhoCanBeContacted_PA")]
        public string SomeoneWhoCanBeContactedPa { get; set; }
        [Column("PhoneSomeoneWhoCanBeContacted_PA")]
        public string PhoneSomeoneWhoCanBeContactedPa { get; set; }
        [Column("TheWork_PA")]
        public string TheWorkPa { get; set; }
        [Column("CompanyName_PA")]
        public string CompanyNamePa { get; set; }
        [Column("NameOfTheInstitutionOwner_PA")]
        public string NameOfTheInstitutionOwnerPa { get; set; }
        [Column("ActivityFoundationWork_PA")]
        public string ActivityFoundationWorkPa { get; set; }
        [Column("JobTitle_PA")]
        public string JobTitlePa { get; set; }
        [Column("StartDateOfWork_PA", TypeName = "date")]
        public DateTime? StartDateOfWorkPa { get; set; }
        [Column("WorkIDNumber_PA")]
        public string WorkIdnumberPa { get; set; }
        [Column("EnterprisePhoneNumber_PA")]
        public string EnterprisePhoneNumberPa { get; set; }
        [Column("NationalityOfTheInstitution_PA")]
        public string NationalityOfTheInstitutionPa { get; set; }
        [Column("TheAddressOfTheOrganization_PA")]
        public string TheAddressOfTheOrganizationPa { get; set; }
        [Column("CopyOfTheDocument_PA")]
        public string CopyOfTheDocumentPa { get; set; }
        [Column("CopyOfThePassport_PA")]
        public string CopyOfThePassportPa { get; set; }
        [Column("CopyOfTheHousingCard_PA")]
        public string CopyOfTheHousingCardPa { get; set; }
        [Column("CopyOfFinancialReports_PA")]
        public string CopyOfFinancialReportsPa { get; set; }
        [Column("WhyTheAccountIsNotManagedByTheBeneficiary_PA")]
        public string WhyTheAccountIsNotManagedByTheBeneficiaryPa { get; set; }
        [Column("TheNamesOfBanks_PA")]
        public string TheNamesOfBanksPa { get; set; }
        [Column("DatesOfTransactionsBanks_PA")]
        public string DatesOfTransactionsBanksPa { get; set; }
        [Column("AmountOfCreditFacilities_PA")]
        public string AmountOfCreditFacilitiesPa { get; set; }
        [Column("Salary_PA")]
        [StringLength(10)]
        public string SalaryPa { get; set; }
        [Column("CommercialReturns_PA")]
        [StringLength(10)]
        public string CommercialReturnsPa { get; set; }
        [Column("PersonalSavings_PA")]
        [StringLength(10)]
        public string PersonalSavingsPa { get; set; }
        [Column("Investments_PA")]
        [StringLength(10)]
        public string InvestmentsPa { get; set; }
        [Column("OtherSources_PA")]
        [StringLength(10)]
        public string OtherSourcesPa { get; set; }
        [Column("NameOtherSources_PA")]
        public string NameOtherSourcesPa { get; set; }
        [Column("LessThan1Million_PA")]
        [StringLength(10)]
        public string LessThan1MillionPa { get; set; }
        [Column("From1To5Million_PA")]
        [StringLength(10)]
        public string From1To5MillionPa { get; set; }
        [Column("From6To10Million_PA")]
        [StringLength(10)]
        public string From6To10MillionPa { get; set; }
        [Column("From10To25Million_PA")]
        [StringLength(10)]
        public string From10To25MillionPa { get; set; }
        [Column("MoreThan25Million_PA")]
        [StringLength(10)]
        public string MoreThan25MillionPa { get; set; }
        [Column("MonthlyEstimate_PA")]
        public string MonthlyEstimatePa { get; set; }
        [Column("AnnualEstimate_PA")]
        public string AnnualEstimatePa { get; set; }
        [Column("NatureOfExpectedBusiness_PA")]
        public string NatureOfExpectedBusinessPa { get; set; }
        [Column("OppositionPolitician_PA")]
        public string OppositionPoliticianPa { get; set; }
        [Column("AnotherCustomerName_PA")]
        public string AnotherCustomerNamePa { get; set; }
        [Column("GovernmentTransactions_PA")]
        public string GovernmentTransactionsPa { get; set; }
        [Column("IssuingAcheckBook_PA")]
        public string IssuingAcheckBookPa { get; set; }
        [Column("NumberOfcheckBook_PA")]
        public string NumberOfcheckBookPa { get; set; }
        [Column("TheNamePrintedOnTheMasterCard_PA")]
        public string TheNamePrintedOnTheMasterCardPa { get; set; }
        [Column("FatcaRegulations_PA")]
        [StringLength(10)]
        public string FatcaRegulationsPa { get; set; }
        [Column("NOTFatcaRegulations_PA")]
        [StringLength(10)]
        public string NotfatcaRegulationsPa { get; set; }
        [Column("FormOrganizerName_PA")]
        public string FormOrganizerNamePa { get; set; }
        [Column("DateOfRegulation_PA", TypeName = "date")]
        public DateTime? DateOfRegulationPa { get; set; }
        [Column("Nots_PA")]
        public string NotsPa { get; set; }
        [Column("NewInternetBanking_PA")]
        [StringLength(10)]
        public string NewInternetBankingPa { get; set; }
        [Column("RenewalInternetBanking_PA")]
        [StringLength(10)]
        public string RenewalInternetBankingPa { get; set; }
        [Column("AML_PA")]
        [StringLength(10)]
        public string AmlPa { get; set; }
        [Column("id_user")]
        public int? IdUser { get; set; }
        [Column("OutIN_PA")]
        public string OutInPa { get; set; }
        [Column("witness_PA")]
        public string WitnessPa { get; set; }
        [Column("FinancialStatements_PA")]
        public string FinancialStatementsPa { get; set; }
        [Column("ExtraIncome_PA")]
        public string ExtraIncomePa { get; set; }
        [Column("checkExtraIncome_PA")]
        [StringLength(10)]
        public string CheckExtraIncomePa { get; set; }
        [Column("FirstAmount_PA")]
        public string FirstAmountPa { get; set; }
        [Column("CardSsatus_PA")]
        public string CardSsatusPa { get; set; }
        [Column("DeliveryDate_PA")]
        public string DeliveryDatePa { get; set; }
        [Column("Branches_PA")]
        public string BranchesPa { get; set; } 
        public string AllowedToTakeTmweel { get; set; } 
        public string NoteForGivenTmweel { get; set; }  

        public bool IsOnTmweelFlag {get;set;}

        public string UserId { get; set; } 

        public User User { get; set; }
    
}
}
