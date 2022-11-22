using System;
using Sentry;

namespace Rejuvena.Terraprisma.Installer.Installers;

public class UncleanInstallationInstaller : IBootstrapInstaller
{
    public IBootstrapInstaller RealInstaller { get; }

    public UncleanInstallationInstaller(IBootstrapInstaller realInstaller) {
        RealInstaller = realInstaller;
    }

    void IBootstrapInstaller.SetupSentry() {
        RealInstaller.SetupSentry();
        SentrySdk.CaptureMessage("Found unclean installation - using UncleanInstallationInstaller...");
    }

    void IBootstrapInstaller.Initialize() {
        RealInstaller.Initialize();
    }

    void IBootstrapInstaller.TearDown() {
        RealInstaller.TearDown();
    }
}