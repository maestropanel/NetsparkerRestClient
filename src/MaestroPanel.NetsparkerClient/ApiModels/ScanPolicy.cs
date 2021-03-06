﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaestroPanel.NetsparkerClient.Model
{
    public enum UserRole
    {
        None,
        AccountMember,
        AccountAdmin,
        AppAdmin
    }

    public class ScanPolicySettingItemModel
    {
        public Guid Id { get; set; }
        public bool IsShared { get; set; }
        public string Name { get; set; }
        public string NameWithAccessModifier { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }
        public UserRole UserRole { get; set; }
        public string UserName { get; set; }
        public bool IsDefault { get; set; }
        public List<WebsiteGroupModel> Groups { get; set; }
    }

    public class ScanPolicySettingApiModel
    {
        public Guid Id { get; set; }
        public bool IsReadonly { get; set; }

        public bool IsShared { get; set; }

        /// <summary>
        /// Required
        /// </summary>
        public string Name { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// Required
        /// </summary>
        public CrawlingSettingModel CrawlingSettings { get; set; }
        /// <summary>
        /// Required
        /// </summary>
        public AttackingSettingModel AttackingSettings { get; set; }
        /// <summary>
        /// Required
        /// </summary>
        public Custom404SettingModel Custom404Settings { get; set; }
        /// <summary>
        /// Required
        /// </summary>
        public ScopeSettingModel ScopeSettings { get; set; }
        public List<IgnorePatternSettingModel> IgnorePatternSettings { get; set; }
        public List<FormValueSettingModel> FormValueSettings { get; set; }
        /// <summary>
        /// Required
        /// </summary>
        public BruteForceSettingModel BruteForceSettings { get; set; }
        public List<AutoCompleteSettingModel> AutoCompleteSettings { get; set; }
        /// <summary>
        /// Required
        /// </summary>
        public HttpRequestSettingModel HttpRequestSettings { get; set; }
        public List<CustomHttpHeaderSetting> CustomHttpHeaderSettings { get; set; }
        public bool EnableKnowledgebase { get; set; }
        public List<SensitiveKeywordSettingModel> SensitiveKeywordSettings { get; set; }
        public List<SecurityCheckGroupModel> SecurityCheckGroups { get; set; }
        public List<Guid> SelectedGroups { get; set; }
    }

    public class NewScanPolicySettingModel
    {
        public Guid Id { get; set; }
        public bool IsShared { get; set; }

        /// <summary>
        /// Required
        /// </summary>
        public string Name { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// Required
        /// </summary>
        public CrawlingSettingModel CrawlingSettings { get; set; }

        /// <summary>
        /// Required
        /// </summary>
        public AttackingSettingModel AttackingSettings { get; set; }

        /// <summary>
        /// Required
        /// </summary>
        public Custom404SettingModel Custom404Settings { get; set; }

        /// <summary>
        /// Required
        /// </summary>
        public ScopeSettingModel ScopeSettings { get; set; }
        public List<IgnorePatternSettingModel> IgnorePatternSettings { get; set; }
        public List<FormValueSettingModel> FormValueSettings { get; set; }

        /// <summary>
        /// Required
        /// </summary>
        public BruteForceSettingModel BruteForceSettings { get; set; }
        public List<AutoCompleteSettingModel> AutoCompleteSettings { get; set; }

        /// <summary>
        /// Required
        /// </summary>
        public HttpRequestSettingModel HttpRequestSettings { get; set; }
        public List<CustomHttpHeaderSetting> CustomHttpHeaderSettings { get; set; }
        public bool EnableKnowledgebase { get; set; }
        public List<SensitiveKeywordSettingModel> SensitiveKeywordSettings { get; set; }
        public List<SecurityCheckGroupModel> SecurityCheckGroups { get; set; }
        public List<Guid> SelectedGroups { get; set; }
    }

    public class UpdateScanPolicySettingModel
    {
        public bool IsShared { get; set; }

        /// <summary>
        /// Required
        /// </summary>
        public string Name { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// Required
        /// </summary>
        public CrawlingSettingModel CrawlingSettings { get; set; }

        /// <summary>
        /// Required
        /// </summary>
        public AttackingSettingModel AttackingSettings { get; set; }

        /// <summary>
        /// Required
        /// </summary>
        public Custom404SettingModel Custom404Settings { get; set; }

        /// <summary>
        /// Required
        /// </summary>
        public ScopeSettingModel ScopeSettings { get; set; }
        public List<IgnorePatternSettingModel> IgnorePatternSettings { get; set; }
        public List<FormValueSettingModel> FormValueSettings { get; set; }

        /// <summary>
        /// Required
        /// </summary>
        public BruteForceSettingModel BruteForceSettings { get; set; }
        public List<AutoCompleteSettingModel> AutoCompleteSettings { get; set; }

        /// <summary>
        /// Required
        /// </summary>
        public HttpRequestSettingModel HttpRequestSettings { get; set; }
        public List<CustomHttpHeaderSetting> CustomHttpHeaderSettings { get; set; }
        public bool EnableKnowledgebase { get; set; }
        public List<SensitiveKeywordSettingModel> SensitiveKeywordSettings { get; set; }
        public List<SecurityCheckGroupModel> SecurityCheckGroups { get; set; }
        public List<Guid> SelectedGroups { get; set; }
    }

    public class AttackingSettingModel
    {
        /// <summary>
        /// Range: inclusive between 1 and 200
        /// </summary>
        public int MaxParametersToAttack { get; set; }
        public string AntiCsrfTokenNames { get; set; }
        public bool UseExtraParameters { get; set; }
    }

    public class Custom404SettingModel
    {
        public bool DisableAuto404Detection { get; set; }
        public string Custom404RegEx { get; set; }
        /// <summary>
        /// Range: inclusive between 0 and 1000
        /// </summary>
        public int Max404PagesToTest { get; set; }
        /// <summary>
        /// Range: inclusive between 0 and 250
        /// </summary>
        public int Maximum404Signature { get; set; }
    }

    public class ScopeSettingModel
    {
        public bool CaseSensitiveScope { get; set; }
        public bool ByPassScopeForStaticChecks { get; set; }
        public string RestrictedExtensions { get; set; }
    }

    public enum IgnorePatternHttpMethod
    {
        POST,
        GET
    }

    public class IgnorePatternSettingModel
    {
        /// <summary>
        /// Required
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Required
        /// </summary>
        public string Pattern { get; set; }
        /// <summary>
        /// Required
        /// </summary>
        public IgnorePatternHttpMethod ParameterType { get; set; }
    }

    public class CrawlingSettingModel
    {
        /// <summary>
        /// Range: inclusive between 50 and 15000
        /// </summary>
        public int MaximumCrawlerUrlCount { get; set; }

        /// <summary>
        /// Range: inclusive between 1 and 9999
        /// </summary>
        public int MaximumSignature { get; set; }

        /// <summary>
        /// Range: inclusive between 1 and 9999
        /// </summary>
        public int PageVisitLimit { get; set; }
        public bool WaitResourceFinder { get; set; }
        public bool EnableTextParser { get; set; }
        public bool EnableDomParser { get; set; }
    }

    public enum FormValueMatch
    {
        RegEx,
        Exact,
        Contains,
        Starts,
        Ends
    }

    public class FormValueSettingModel
    {
        /// <summary>
        /// Required
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Required
        /// </summary>
        public string Value { get; set; }
        public string Pattern { get; set; }
        public FormValueMatch Match { get; set; }
        public string Type { get; set; }
        public bool Force { get; set; }
    }

    public class BruteForceSettingModel
    {
        public bool EnableAuthBruteForce { get; set; }
        /// <summary>
        /// Range: inclusive between 1 and 5000
        /// </summary>
        public int MaxBruteForce { get; set; }
    }

    public class AutoCompleteSettingModel
    {
        /// <summary>
        /// Required
        /// </summary>
        public string Value { get; set; }
    }

    public class HttpRequestSettingModel
    {
        public string UserAgent { get; set; }
        /// <summary>
        /// Range: inclusive between 1 and 1000
        /// </summary>
        public int RequestTimeout { get; set; }
        public string Accept { get; set; }
        public string AcceptCharset { get; set; }
        public string AcceptLanguage { get; set; }
        public bool EnableCookies { get; set; }
        public bool HttpKeepAlive { get; set; }
        public bool EnableGzipAndDeflate { get; set; }
        /// <summary>
        /// Range: inclusive between 1 and 25
        /// </summary>
        public int ThreadCount { get; set; }
    }

    public class CustomHttpHeaderSetting
    {
        /// <summary>
        /// Required
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Required
        /// </summary>
        public string Value { get; set; }
        public bool Enabled { get; set; }
    }

    public class SensitiveKeywordSettingModel
    {
        /// <summary>
        /// Required
        /// </summary>
        public string Pattern { get; set; }
    }

    public class SecurityCheckSetting
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public List<string> AvailableValues { get; set; }
    }

    public class ScanPolicyPatternModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Enabled { get; set; }
    }

    public enum SecurityCheckGroupType
    {
        Engine,
        ResourceModifier
    }

    public class SecurityCheckGroupModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Enabled { get; set; }

        public SecurityCheckGroupType Type { get; set; }
        public List<ScanPolicyPatternModel> Patterns { get; set; }
        public List<SecurityCheckSetting> Settings { get; set; }
    }

    public enum ScanPolicyDeleteResult
    {
        Ok,
        NotExists,
        AlreadyUsing
    }
}
