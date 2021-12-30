using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Core.Enums;
using Core.Pages;
using Foundation;
using Plugin.DeviceInfo;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace RemoteNotes.iOS.Renderers.Pages
{
    public class BaseContentPageRenderer : PageRenderer
    {
        private readonly nint STATUS_BAR_TAG = 987654321;

        private UIView _statusBar = default(UIView);

        #region -- Overrides --

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                UpdateStatusBar();
                e.NewElement.PropertyChanged += OnElementPropertyChanged;
            }

            if (e.OldElement != null)
            {
                e.OldElement.PropertyChanged -= OnElementPropertyChanged;
            }
        }

        private async void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (BaseContentPage.StatusBarVisibilityProperty.PropertyName == e.PropertyName)
            {
                UpdateStatusBar();
            }

            if (BaseContentPage.StatusBarColorProperty.PropertyName == e.PropertyName)
            {
                await Task.Delay(500);
                UpdateStatusBar();
            }
        }

        #endregion

        #region -- Protected implementation --

        protected virtual void UpdateStatusBar()
        {
            var page = Element as BaseContentPage;

            UpdateSafeAreaProperties(page.StatusBarVisibility);
            UpdateStatusBarColor(page.StatusBarColor);
            UpdateStatusbarIconStyle(page.IconsMode);
        }

        protected virtual void UpdateStatusbarIconStyle(EStatusBarIconsMode mode)
        {
            var page = Element as BaseContentPage;

            if (page.StatusBarVisibility == EStatusBarVisibility.Default ||
                page.StatusBarVisibility == EStatusBarVisibility.Visible ||
                page.StatusBarVisibility == EStatusBarVisibility.Transarent)
            {
                switch (mode)
                {
                    case EStatusBarIconsMode.Default:
                    case EStatusBarIconsMode.Dark:
                        Device.BeginInvokeOnMainThread(() => UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.Default, true));
                        break;
                    case EStatusBarIconsMode.Light:
                        Device.BeginInvokeOnMainThread(() => UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.LightContent, true));
                        break;
                    default:
                        throw new InvalidEnumArgumentException("Incorrect status bar icon mode exception throw (unhandled mode)");
                }
            }
        }

        protected virtual void UpdateSafeAreaProperties(EStatusBarVisibility status)
        {
            switch (status)
            {
                case EStatusBarVisibility.Default:
                case EStatusBarVisibility.Visible:
                    ShowStatusBarWithSafeArea();
                    break;
                case EStatusBarVisibility.Invisible:
                    UIApplication.SharedApplication.StatusBarHidden = true;
                    break;
                case EStatusBarVisibility.Transarent:
                    ShowStatusBarWithoutSafeArea();
                    break;
                default:
                    throw new InvalidEnumArgumentException("Incorrect status bar status exception throw (unhandled status)");
            }
        }

        protected virtual async void UpdateStatusBarColor(Color color)
        {
            var page = Element as BaseContentPage;

            if (CrossDeviceInfo.Current.VersionNumber.Major >= 13)
            {
                // HACK: In native code MakeKeyAndVisible method takes time to init KeyWindow, need to wait
                if (UIApplication.SharedApplication.KeyWindow == null)
                {
                    await Task.Delay(200);
                }

                _statusBar = UIApplication.SharedApplication.KeyWindow?.ViewWithTag(STATUS_BAR_TAG);

                if (_statusBar == null)
                {
                    _statusBar = AddStatusBar();
                }
                else
                {
                    _statusBar.RemoveFromSuperview();
                    _statusBar = AddStatusBar();
                }
            }
            else
            {
                _statusBar = UIApplication.SharedApplication.ValueForKey(new NSString("statusBar")) as UIView;
            }

            if (_statusBar != null &&
                _statusBar.RespondsToSelector(new ObjCRuntime.Selector("setBackgroundColor:")) &&
                (page.StatusBarVisibility == EStatusBarVisibility.Default ||
                page.StatusBarVisibility == EStatusBarVisibility.Visible))
            {
                _statusBar.BackgroundColor = color.ToUIColor();
            }
        }

        #endregion

        #region -- Private helpers --

        private void ShowStatusBarWithSafeArea()
        {
            var page = Element as BaseContentPage;

            UIApplication.SharedApplication.StatusBarHidden = false;

            page.On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

            if (CrossDeviceInfo.Current.VersionNumber.Major <= 10)
            {
                page.Padding = new Thickness(0, 20, 0, 0);
            }
        }

        private void ShowStatusBarWithoutSafeArea()
        {
            var page = Element as BaseContentPage;

            UIApplication.SharedApplication.StatusBarHidden = false;

            page.On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(false);

            if (CrossDeviceInfo.Current.VersionNumber.Major <= 10)
            {
                page.Padding = new Thickness(0, 0, 0, 0);
            }
        }

        private UIView AddStatusBar()
        {
            var statusBarView = new UIView(UIApplication.SharedApplication.StatusBarFrame);

            statusBarView.Tag = STATUS_BAR_TAG;

            UIApplication.SharedApplication.KeyWindow?.AddSubview(statusBarView);

            return statusBarView;
        }

        #endregion
    }
}
