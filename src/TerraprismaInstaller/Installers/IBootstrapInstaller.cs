using System;

namespace Rejuvena.Terraprisma.Installer.Installers;

public interface IBootstrapInstaller
{
    void SetupSentry();

    void Initialize();

    void TearDown();
}