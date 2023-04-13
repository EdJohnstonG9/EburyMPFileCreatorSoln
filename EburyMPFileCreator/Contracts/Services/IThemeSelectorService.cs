using System;

using EburyMPFileCreator.Models;

namespace EburyMPFileCreator.Contracts.Services
{
    public interface IThemeSelectorService
    {
        void InitializeTheme();

        void SetTheme(AppTheme theme);

        AppTheme GetCurrentTheme();
    }
}
