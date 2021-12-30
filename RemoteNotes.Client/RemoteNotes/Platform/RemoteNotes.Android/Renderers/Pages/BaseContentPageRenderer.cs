using System;
using System.ComponentModel;
using Android.Content;
using Android.OS;
using Android.Views;
using Core.Enums;
using Core.Pages;
using Plugin.CurrentActivity;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace RemoteNotes.Android.Renderers.Pages
{
    public class BaseContentPageRenderer : PageRenderer
    {
        public BaseContentPageRenderer(
            Context context)
            : base(context)
        {
        }

        #region -- Overrides --

        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                UpdateStatusBar();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (BaseContentPage.StatusBarVisibilityProperty.PropertyName == e.PropertyName)
            {
                UpdateStatusBar();
            }

            if (BaseContentPage.StatusBarColorProperty.PropertyName == e.PropertyName)
            {
                UpdateStatusBar();
            }
        }

        #endregion

        #region -- Protected implementation --

        protected virtual void UpdateStatusBar()
        {

            if (Build.VERSION.SdkInt >= BuildVersionCodes.R)
                SetupNewCompat();
            else
                SetupOldCompat();
        }

        private void SetupNewCompat()
        {
            // not implemented yet
            throw new NotImplementedException("implement SetupNewCompat()");
        }

        private void SetupOldCompat()
        {
            var page = Element as BaseContentPage;

            UpdateSafeAreaProperties(page.StatusBarVisibility);
            UpdateStatusBarColor(page.StatusBarColor);
            UpdateStatusbarIconStyle(page.IconsMode);
        }

        protected virtual void UpdateSafeAreaProperties(EStatusBarVisibility status)
        {
            switch (status)
            {
                case EStatusBarVisibility.Default:
                case EStatusBarVisibility.Visible:
                    CrossCurrentActivity.Current.Activity.Window.DecorView.SystemUiVisibility |= (StatusBarVisibility)SystemUiFlags.Visible;
                    break;
                case EStatusBarVisibility.Invisible:
                    CrossCurrentActivity.Current.Activity.Window.DecorView.SystemUiVisibility |= (StatusBarVisibility)SystemUiFlags.Fullscreen;
                    break;
                case EStatusBarVisibility.Transarent:
                    CrossCurrentActivity.Current.Activity.Window.AddFlags(WindowManagerFlags.LayoutNoLimits);
                    break;
                default:
                    throw new InvalidEnumArgumentException("Incorrect status bar status exception throw (unhandled status)");
            }
        }

        protected virtual void UpdateStatusbarIconStyle(EStatusBarIconsMode mode)
        {
            switch (mode)
            {
                case EStatusBarIconsMode.Default:
                case EStatusBarIconsMode.Light:
                    CrossCurrentActivity.Current.Activity.Window.DecorView.SystemUiVisibility |= (StatusBarVisibility)SystemUiFlags.Visible;
                    break;
                case EStatusBarIconsMode.Dark:
                    CrossCurrentActivity.Current.Activity.Window.DecorView.SystemUiVisibility |= (StatusBarVisibility)SystemUiFlags.LightStatusBar;
                    break;
                default:
                    throw new InvalidEnumArgumentException("Incorrect status bar icon mode exception throw (unhandled mode)");
            }
        }

        protected virtual void UpdateStatusBarColor(Color color)
        {
            var page = Element as BaseContentPage;

            if (page.StatusBarVisibility == EStatusBarVisibility.Default || page.StatusBarVisibility == EStatusBarVisibility.Visible)
            {
                CrossCurrentActivity.Current.Activity.Window.SetStatusBarColor(color.ToAndroid());
            }
        }

        #endregion

    }
}
