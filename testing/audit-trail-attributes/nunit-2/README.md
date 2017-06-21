# Audit Trail Annotations

Defects (aka bugs) are hard engineering lessons learned from our end users and it's really important to ensure that they never resurface otherwise that undermines their trust in the product and damages the team image.

The best way to ensure a bug never resurfaces is to commit a test that proves that it is resolved along with the actual fix. This test ("formal proof") will run every time a developer makes a change to the product to ensure it never resurfaces ever again.

Over time the actual product that is built will change shape. Sometimes the tests are no longer relevant and should be deleted but making this distinction after a couple sprints or even months have elapsed is really hard. That's why we use the `Issue()` attribute to tag defects so we can quickly navigate back to our bug tracker and figure out _why the test exists_.


```csharp
[Issue(IssueTracker issueTracker, int issueNumber, string description)]
```

```csharp
Issue(IssueTracker issueTracker, int issueNumber, string description, PlatformAffected platformsAffected)`

public enum PlatformAffected
{
    iOS = 1 << 0,
    Android = 1 << 1,
    UWP = 1 << 4,
    All = ~0,
    Default = 0
}
````

Example usage:

```csharp
[Test]
[Issue(IssueTracker.VisualStudioOnline, 1234, "User could access the app even when not authenticated", PlatformAffected.Android)]
public void WhenTheUserLogsOut_TheyShouldNot_BeAbleToAccessTheApp()
{
    // this attribute should be added to tests which were created as part
    // of fixing bugs/defects which where reported by end users.
}
```

### Disabling & Ignoring Tests

Any test that has been disabled either because it's flakey or is completely broken needs to be visible in the product backlog. Please create a PBI within Visual Studio online with the tag "tech debt" and fill in the `FragileTest` or `BrokenTest` attribute with the details.

![](https://github.com/ghuntley/xamarin-playground/blob/master/testing/audit-trail-attributes/nunit-2/screenshot.png?raw=true)


```csharp
[Test]
[BrokenTest(IssueTracker.VisualStudioOnline, 1234, "Automated UI testing of the notifications functionality will create unwanted notifications in SAP")]
public void WhenRedacted_WhenAllFieldsArePopulated_TheRedactedIsCreated()
{
    // use this attribute for tests that are not implemented or are broken.
    // the test runner will not execute the test.
}
```

```csharp
[FragileTest(IssueTracker.VisualStudioOnline, 1234, "Works only if there is internet connected")]
public void AFlakyTest_ThatNeedsToBe_TemporarilyDisabled()
{
    // use this attribute for tests that are flaky aka works intermittently.
    // the test runner will not execute the test.
}
```