using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarrierFeedDownload.CrossCutting.Adapter.Shared.Enums;

public enum ReportType
{
    [Display(Name = "End of Day Report Job")]
    EODReportJob = 1,

    [Display(Name = "Policy Update Job")]
    PolicyUpdateJob = 2,

    [Display(Name = "Commission Report Job")]
    CommissionReportJob = 3,

    [Display(Name = "Cancelled Report Job")]
    CancelledReportJob = 4,

    [Display(Name = "Upcoming Renewal Report Job")]
    UpcomingRenewalReportJob = 5,

    [Display(Name = "Suspense Report Job")]
    SuspenseReportJob = 6,

    [Display(Name = "ID Card Job")]
    IDCardJob = 9,

    [Display(Name = "Due Payment Update Job")]
    NextPaymentUpdateJob = 11,

    [Display(Name = "Payment Upload Job")]
    PaymentUploadJob = 12,

    [Display(Name = "SH Web Policy Update Job")]
    SHWebPolicyUpdateJob = 13
}
