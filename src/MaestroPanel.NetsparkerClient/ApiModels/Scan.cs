using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaestroPanel.NetsparkerClient.Model
{
    public class NewScanTaskApiModel
    {
        /// <summary>
        /// Required
        /// </summary>
        public string TargetUri { get; set; }
        public Guid? PolicyId { get; set; }
        public ScanTaskScope Scope { get; set; }
        public bool ExcludeLinks { get; set; }
        public List<ExcludedLinkModel> ExcludedLinks { get; set; }
        public UrlRewriteMode UrlRewriteMode { get; set; }
        public List<UrlRewriteRuleModel> UrlRewriteRules { get; set; }
        public string Cookies { get; set; }
        public string FormAuthUsername { get; set; }
    }

    public enum ScanTaskScope
    {
        EnteredPathAndBelow,
        OnlyEnteredUrl,
        WholeDomain
    }

    public class ExcludedLinkModel
    {
        public string RegexPattern { get; set; }
    }

    public enum UrlRewriteMode
    {
        None,
        Heuristic,
        Custom
    }

    public class UrlRewriteRuleModel
    {
        public string PlaceholderPattern { get; set; }
        public string RegexPattern { get; set; }
    }

    public enum ScanTaskState
    {
        Queued,
        Scanning,
        Archiving,
        Complete,
        Failed,
        Cancelled
    }

    public enum ScanPhase
    {
        Pending,
        Crawling,
        CrawlingAndAttacking,
        Attacking,
        ReCrawling,
        Complete
    }

    public enum FailureReason
    {
        NameResolutionFailure,
        HostUnavailable,
        ProxyFailure,
        UnableToLoadScanSession
    }

    public enum ScanType
    {
        Full,
        Retest,
        Incremental
    }

    public enum ThreatLevel
    {
        Unknown,
        Good,
        Advise,
        Insecure,
        Defcon
    }

    public class ScanTaskModel
    {
        public Guid Id { get; set; }
        public string WebsiteUrl { get; set; }
        public string TargetPath { get; set; }
        public string TargetUrl { get; set; }
        public string TargetUrlRoot { get; set; }
        public string Initiated { get; set; }
        public string InitiatedDate { get; set; }
        public Guid? PolicyId { get; set; }
        public ScanTaskScope? Scope { get; set; }
        public bool? ExcludeLinks { get; set; }
        public string ExcludedLinks { get; set; }
        public string ImportedLinks { get; set; }
        public int? TotalVulnerabilityCount { get; set; }
        public string WebsiteName { get; set; }
        public Guid? WebsiteId { get; set; }
        public string PolicyName { get; set; }
        public Guid? UserId { get; set; }
        public UrlRewriteMode? UrlRewriteMode { get; set; }
        public List<UrlRewriteRuleModel> UrlRewriteRules { get; set; }
        public string Cookies { get; set; }
        public string FormAuthUsername { get; set; }
        public ScanTaskState? State { get; set; }
        public ScanPhase? Phase { get; set; }
        public int? CompletedSteps { get; set; }
        public int? EstimatedSteps { get; set; }
        public int? Percentage { get; set; }
        public int? VulnerabilityInfoCount { get; set; }
        public int? VulnerabilityLowCount { get; set; }
        public int? VulnerabilityMediumCount { get; set; }
        public int? VulnerabilityImportantCount { get; set; }
        public int? VulnerabilityCriticalCount { get; set; }
        public string FailureReasonString { get; set; }
        public FailureReason? FailureReason { get; set; }
        public string FailureReasonDescription { get; set; }
        public DateTime? StateChanged { get; set; }
        public ScanType? ScanType { get; set; }
        public ThreatLevel? ThreatLevel { get; set; }
        public string ScanGroupId { get; set; }
        public bool? IsCompleted { get; set; }
        public int? EstimatedLaunchTime { get; set; }
    }

    public class BaseScanApiModel
    {
        public Guid BaseScanId { get; set; }
    }

    public class ApiScanStatusModel
    {
        public ScanTaskState State { get; set; }
        public int EstimatedSteps { get; set; }
        public int CompletedSteps { get; set; }
        public int EstimatedLaunchTime { get; set; }
    }

    public class VulnerabilityModel
    {
        public string Url { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
    }

    public enum ScheduleRunType
    {
        Once,
        Daily,
        Weekly,
        Monthly
    }

    public class NewScheduledScanApiModel
    {
        /// <summary>
        /// Required
        /// Max length: 99
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Required
        /// </summary>
        public string NextExecutionTime { get; set; }

        /// <summary>
        /// Required
        /// </summary>
        public ScheduleRunType ScheduleRunType { get; set; }

        /// <summary>
        /// Required
        /// </summary>
        public string TargetUri { get; set; }
        public Guid PolicyId { get; set; }
        public ScanTaskScope Scope { get; set; }
        public bool ExcludeLinks { get; set; }
        public List<ExcludedLinkModel> ExcludedLinks { get; set; }
        public UrlRewriteMode UrlRewriteMode { get; set; }
        public List<UrlRewriteRuleModel> UrlRewriteRules { get; set; }
        public string Cookies { get; set; }
        public string FormAuthUsername { get; set; }
    }

    public class UpdateScheduledScanApiModel
    {
        /// <summary>
        /// Required
        /// </summary>
        public Guid Id { get; set; }
        public bool Disabled { get; set; }

        /// <summary>
        /// Required
        /// Max length:99
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Required
        /// </summary>
        public string NextExecutionTime { get; set; }

        /// <summary>
        /// Required
        /// </summary>
        public ScheduleRunType ScheduleRunType { get; set; }

        /// <summary>
        /// Required
        /// </summary>
        public string TargetUri { get; set; }
        public Guid PolicyId { get; set; }
        public ScanTaskScope Scope { get; set; }
        public bool ExcludeLinks { get; set; }
        public List<ExcludedLinkModel> ExcludedLinks { get; set; }
        public UrlRewriteMode UrlRewriteMode { get; set; }
        public List<UrlRewriteRuleModel> UrlRewriteRules { get; set; }
        public string Cookies { get; set; }
        public string FormAuthUsername { get; set; }
    }

    public enum ScanTaskCreateType
    {
        Website,
        WebsiteGroup
    }

    public class UpdateScheduledScanModel
    {
        public Guid Id { get; set; }
        public string NextExecutionTime { get; set; }
        public string Name { get; set; }
        public bool EnableScheduling { get; set; }
        public ScheduleRunType ScheduleRunType { get; set; }
        public bool Disabled { get; set; }
        public string TargetWebsite { get; set; }
        public string TargetPath { get; set; }

        public string TargetUri { get; set; }
        public Guid PolicyId { get; set; }
        public ScanTaskScope Scope { get; set; }
        public bool ExcludeLinks { get; set; }
        public List<ExcludedLinkModel> ExcludedLinks { get; set; }
        public string ImportedLinks { get; set; }
        public bool DisplayOptions { get; set; }
        public ScanTaskCreateType CreateType { get; set; }

        public Guid WebsiteGroupId { get; set; }
        public UrlRewriteMode UrlRewriteMode { get; set; }
        public List<UrlRewriteRuleModel> UrlRewriteRules { get; set; }
        public string Cookies { get; set; }
        public string FormAuthUsername { get; set; }
    }

    public enum ReportType
    {
        Crawled,
        Scanned,
        Vulnerabilities,
        ScanDetail
    }

    public enum ReportFormat
    {
        Xml,
        Csv,
        Pdf,
        Html
    }

    public enum ContentFormat
    {
        Html,
        Markdown
    }
}
