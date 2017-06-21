using System;
using System.Diagnostics;
using System.Reflection;
using NUnit.Core;
using NUnit.Framework;

namespace NUnit.Framework;
{
    public enum IssueTracker
    {
        [System.ComponentModel.Description("https://REPLACEME.visualstudio.com/REPLACEME/_workitems/edit")]
        VisualStudioOnline,
        None
    }

    [Flags]
    public enum PlatformAffected
    {
        iOS = 1 << 0,
        Android = 1 << 1,
        UWP = 1 << 4,
        All = ~0,
        Default = 0
    }

    [Conditional("DEBUG")]
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class BrokenTestAttribute : Attribute, ITestAction
    {
        public BrokenTestAttribute(IssueTracker issueTracker, int issueNumber, string description)
        {
            IssueTracker = issueTracker;
            IssueNumber = issueNumber;
            Description = description;
            PlatformsAffected = PlatformAffected.Default;
        }

        public BrokenTestAttribute(IssueTracker issueTracker, int issueNumber, string description,
            PlatformAffected platformsAffected)
        {
            IssueTracker = issueTracker;
            IssueNumber = issueNumber;
            Description = description;
            PlatformsAffected = platformsAffected;
        }

        public string Description { get; }
        public int IssueNumber { get; }
        public IssueTracker IssueTracker { get; }
        public PlatformAffected PlatformsAffected { get; }

        public void BeforeTest(TestDetails testDetails)
        {
            Assert.Ignore($"{PlatformsAffected} - {Description}: {IssueTracker.GetDescription()}/{IssueNumber}");
        }

        public void AfterTest(TestDetails testDetails)
        {
        }

        public ActionTargets Targets { get; private set; }
    }

    [Conditional("DEBUG")]
    public class FragileTestAttribute : Attribute, ITestAction
    {
        public FragileTestAttribute(IssueTracker issueTracker, int issueNumber, string description)
        {
            IssueTracker = issueTracker;
            IssueNumber = issueNumber;
            Description = description;
            PlatformsAffected = PlatformAffected.Default;
        }

        public FragileTestAttribute(IssueTracker issueTracker, int issueNumber, string description,
            PlatformAffected platformsAffected)
        {
            IssueTracker = issueTracker;
            IssueNumber = issueNumber;
            Description = description;
            PlatformsAffected = platformsAffected;
        }

        public string Description { get; }
        public int IssueNumber { get; }
        public IssueTracker IssueTracker { get; }
        public PlatformAffected PlatformsAffected { get; }

        public void BeforeTest(TestDetails testDetails)
        {
            Assert.Ignore($"{PlatformsAffected} - {Description}: {IssueTracker.GetDescription()}/{IssueNumber}");
        }

        public void AfterTest(TestDetails testDetails)
        {
        }

        public ActionTargets Targets { get; private set; }

    }

    [Conditional("DEBUG")]
    [AttributeUsage(
        AttributeTargets.Class |
        AttributeTargets.Method,
        AllowMultiple = true
    )]
    public class IssueAttribute : Attribute
    {
        public IssueAttribute(IssueTracker issueTracker, int issueNumber, string description)
        {
            IssueTracker = issueTracker;
            IssueNumber = issueNumber;
            Description = description;
            PlatformsAffected = PlatformAffected.Default;
        }

        public IssueAttribute(IssueTracker issueTracker, int issueNumber, string description,
            PlatformAffected platformsAffected)
        {
            IssueTracker = issueTracker;
            IssueNumber = issueNumber;
            Description = description;
            PlatformsAffected = platformsAffected;
        }

        public string Description { get; }
        public int IssueNumber { get; }
        public IssueTracker IssueTracker { get; }
        public PlatformAffected PlatformsAffected { get; }
    }
}