using RemoteNotes.Domain.Contract.ViewModel;
using RemoteNotes.UI.Control.Enums;
using Xamarin.Forms;

namespace RemoteNotes.UI.Control.Pages
{
    public abstract class BaseContentPage : ContentPage
    {
        #region -- Public properties --

        public static BindableProperty StatusBarVisibilityProperty = BindableProperty.Create(
            nameof(StatusBarVisibility),
            typeof(EStatusBarVisibility),
            typeof(BaseContentPage),
            EStatusBarVisibility.Default);

        public EStatusBarVisibility StatusBarVisibility
        {
            get { return (EStatusBarVisibility)GetValue(StatusBarVisibilityProperty); }
            set { SetValue(StatusBarVisibilityProperty, value); }
        }

        public static BindableProperty HasNavigationBarProperty = BindableProperty.Create(
            nameof(HasNavigationBar),
            typeof(bool),
            typeof(BaseContentPage),
            true,
            propertyChanged: OnHasNavigationBarPropertyChanged);

        public bool HasNavigationBar
        {
            get { return (bool)GetValue(HasNavigationBarProperty); }
            set { SetValue(HasNavigationBarProperty, value); }
        }

        public static BindableProperty StatusBarColorProperty = BindableProperty.Create(
            nameof(StatusBarColor),
            typeof(Color),
            typeof(BaseContentPage));

        public Color StatusBarColor
        {
            get { return (Color)GetValue(StatusBarColorProperty); }
            set { SetValue(StatusBarColorProperty, value); }
        }

        public static BindableProperty IconsModeProperty = BindableProperty.Create(
            nameof(IconsMode),
            typeof(EStatusBarIconsMode),
            typeof(BaseContentPage));

        public EStatusBarIconsMode IconsMode
        {
            get { return (EStatusBarIconsMode)GetValue(IconsModeProperty); }
            set { SetValue(IconsModeProperty, value); }
        }

        #endregion

        #region -- Overrides --

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is IAppearable appearable)
            {
                appearable.OnAppearing();
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            if (BindingContext is IDisappearable appearable)
            {
                appearable.OnDisappearing();
            }
        }

        #endregion

        #region -- Private static method --

        private static void OnHasNavigationBarPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            NavigationPage.SetHasNavigationBar(bindable, (bool)newValue);
        }

        #endregion
    }
}
