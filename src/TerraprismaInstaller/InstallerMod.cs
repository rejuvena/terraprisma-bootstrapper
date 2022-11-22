using System.IO;
using JetBrains.Annotations;
using Rejuvena.Terraprisma.Installer.Installers;
using Terraria;
using Terraria.ModLoader;

namespace Rejuvena.Terraprisma.Installer;

[UsedImplicitly]
public class InstallerMod : Mod
{
    /// <summary>
    ///     The most convenient sign that we are in a Terraprisma environment.
    /// </summary>
    public const string TYPE_TERRARPRISMA_WASHERE = "Terraprisma.WasHere";

    /// <summary>
    ///     The lock file used to indicate that a Terraprisma installation *should* exist; indicates whether an installation is clean.
    /// </summary>
    public const string TERRAPRISMA_FILE_LOCK = "terraprisma.lock";

    public InstallerMod() {
        // Make an initializer.
        var initializer = MakeBootstrapInstaller();

        // Initialize sentry for convenient logging and reporting.
        initializer.SetupSentry();

        // Perform actual initialization.
        initializer.Initialize();
    }

    /// <summary>
    ///     Creates a <see cref="IBootstrapInstaller"/> instance to handle installation with.
    /// </summary>
    /// <returns>An instance of <see cref="IBootstrapInstaller"/> defining installation behaviors.</returns>
    private static IBootstrapInstaller MakeBootstrapInstaller() {
        // If the Terraria assembly contains a type named "Terraprisma.WasHere", then we're running in a Terraprisma environment.
        if (typeof(Main).Assembly.GetType(TYPE_TERRARPRISMA_WASHERE) is not null) return new AssumeAlreadyInstalledInstaller();

        // If we aren't running in a Terraprisma environment, yet the terraprisma.lock file exists, then something got overwritten - update or poor uninstallation?
        // Regardless, handle appropriately (clean up after ourselves and perform the expected first-time installation).
        if (File.Exists(Path.Combine(Main.SavePath, TERRAPRISMA_FILE_LOCK))) return new UncleanInstallationInstaller(new FirstTimeSetupInstaller());

        // If everything seems clean and we aren't in a Terraprisma environment, use the first-time installation process.
        return new FirstTimeSetupInstaller();
    }
}