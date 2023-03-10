using System;

namespace EasyUI.Interfaces
{
    public interface IFade
    {
        void FadeIn(bool waitBeforeFade = true, Action onFadeIn = null);
        void FadeOut(bool waitBeforeFade = true, Action onFadeOut = null);
    }
}
