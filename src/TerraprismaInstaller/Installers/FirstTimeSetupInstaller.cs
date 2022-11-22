using System;
using Sentry;
using Sentry.Reflection;

namespace Rejuvena.Terraprisma.Installer.Installers;

public class FirstTimeSetupInstaller : IBootstrapInstaller
{
    private IDisposable? SentryInstance = null;

    void IBootstrapInstaller.SetupSentry() {
        SentryInstance = SentrySdk.Init(
            options =>
            {
                options.Dsn = "https://f75b7a89a6f647c4b360764b57b224ac@sentry.tomat.dev/2";
                options.Debug = true;
                options.TracesSampleRate = 1.0;
                options.IsGlobalModeEnabled = true;
                options.AutoSessionTracking = true;
                options.Release = $"Rejuvena.Terraprisma.Installer v{GetType().Assembly.GetNameAndVersion().Version}";
            }
        );

        SentrySdk.CaptureMessage("Sentry successfully set up!");
    }

    void IBootstrapInstaller.Initialize() {
        SentrySdk.CaptureMessage("TEST: Something went wrong!", SentryLevel.Error);
    }

    void IBootstrapInstaller.TearDown() {
        SentryInstance?.Dispose();
    }
}