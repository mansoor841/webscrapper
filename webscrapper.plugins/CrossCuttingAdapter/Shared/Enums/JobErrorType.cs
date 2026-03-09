using System.ComponentModel.DataAnnotations;

namespace CarrierFeedDownload.CrossCutting.Adapter.Shared.Enums;

public enum JobErrorType
{
    [Display(Name = "No Error")]
    NoError = 0,

    [Display(Name = "No Data For Date")]
    NoDataForDate = 1,

    [Display(Name = "Can't Reach Site")]
    CantReachSite = 2,

    [Display(Name = "Login Failed")]
    LoginFailed = 3,

    [Display(Name = "Reset Password")]
    ResetPassword = 4,

    [Display(Name = "Report Page Not Working")]
    ReportPageNotWorking = 5,

    [Display(Name = "Input Validation Error")]
    InputValidationError = 6,

    [Display(Name = "Generic Exception")]
    GenericException = 7,

    [Display(Name = "Response Input Validation Error")]
    ResponseInputValidationError = 8,

    [Display(Name = "Call Failed or Timeout Error")]
    CallFailedOrTimeoutError = 9,

    [Display(Name = "File Unavailable")]
    FileUnavailable = 10,

    [Display(Name = "Session Expired")]
    SessionExpired = 12,

    [Display(Name = "MFA Failed")]
    MFAFailed = 13,

    [Display(Name = "Admin Login Failed")]
    AdminLoginFailed = 14,

    [Display(Name = "Record not Found")]
    RecordNotFound = 15
}
