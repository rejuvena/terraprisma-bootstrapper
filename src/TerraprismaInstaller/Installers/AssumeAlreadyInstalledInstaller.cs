using System;
using Sentry;

namespace Rejuvena.Terraprisma.Installer.Installers;

public class AssumeAlreadyInstalledInstaller : IBootstrapInstaller
{
    void IBootstrapInstaller.SetupSentry() {
        SentrySdk.CaptureMessage("Assuming Sentry has been initialized and that Terraprisma has already been installed - AssumeAlreadyInstalledInstaller!");
    }

    void IBootstrapInstaller.Initialize() { }

    void IBootstrapInstaller.TearDown() { }
}