namespace Microsoft.ComponentDetection.Detectors.Tests.SwiftPM;

using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.ComponentDetection.Contracts.TypedComponent;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PackageUrl;

[TestClass]
public class SwiftPMComponentTests
{
    [TestMethod]
    public void Constructor_ShouldInitializeProperties()
    {
        var name = "alamofire";
        var version = "5.9.1";
        var packageUrl = "https://github.com/Alamofire/Alamofire";
        var hash = "f455c2975872ccd2d9c81594c658af65716e9b9a";

        var component = new SwiftPMComponent(name, version, packageUrl, hash);

        component.Name.Should().Be(name);
        component.Version.Should().Be(version);
        component.Type.Should().Be(ComponentType.SwiftPM);
        component.Id.Should().Be($"{name} {version} - {component.Type}");
    }

    [TestMethod]
    public void Constructor_ShouldThrowException_WhenNameIsNull()
    {
        Action action = () => new SwiftPMComponent(null, "5.9.1", "https://github.com/Alamofire/Alamofire", "f455c2975872ccd2d9c81594c658af65716e9b9a");
        action.Should().Throw<ArgumentException>().WithMessage("*name*");
    }

    [TestMethod]
    public void Constructor_ShouldThrowException_WhenVersionIsNull()
    {
        Action action = () => new SwiftPMComponent("alamofire", null, "https://github.com/Alamofire/Alamofire", "f455c2975872ccd2d9c81594c658af65716e9b9a");
        action.Should().Throw<ArgumentException>().WithMessage("*version*");
    }

    [TestMethod]
    public void Constructor_ShouldThrowException_WhenPackageUrlIsNull()
    {
        Action action = () => new SwiftPMComponent("alamofire", "5.9.1", null, "f455c2975872ccd2d9c81594c658af65716e9b9a");
        action.Should().Throw<ArgumentException>().WithMessage("*packageUrl*");
    }

    [TestMethod]
    public void Constructor_ShouldThrowException_WhenHashIsNull()
    {
        Action action = () => new SwiftPMComponent("alamofire", "5.9.1", "https://github.com/Alamofire/Alamofire", null);
        action.Should().Throw<ArgumentException>().WithMessage("*hash*");
    }

    [TestMethod]
    public void PackageURL_ShouldReturnCorrectPackageURL()
    {
        var name = "alamofire";
        var version = "5.9.1";
        var packageUrl = "https://github.com/Alamofire/Alamofire";
        var hash = "f455c2975872ccd2d9c81594c658af65716e9b9a";

        var component = new SwiftPMComponent(name, version, packageUrl, hash);

        var expectedPackageURL = new PackageURL(
            type: "swift",
            @namespace: "github.com",
            name: name,
            version: hash,
            qualifiers: new SortedDictionary<string, string>
            {
                { "repository_url", packageUrl },
            },
            subpath: null);

        component.PackageURL.Should().BeEquivalentTo(expectedPackageURL);
    }
}
