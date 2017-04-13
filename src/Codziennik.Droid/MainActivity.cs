using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms;

namespace Codziennik.Droid
{
    [Activity(Label = "Codziennik", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());

        }


        //public override bool OnOptionsItemSelected(IMenuItem item)
        //{
        //    var app = Application.Current;
        //    if (item.ItemId == 16908332) // This makes me feel dirty.
        //    {
        //        var navPage = ((app.MainPage.Navigation.ModalStack[0] as MasterDetailPage).Detail as NavigationPage);

        //        if (app != null && navPage.Navigation.NavigationStack.Count > 0)
        //        {
        //            int index = navPage.Navigation.NavigationStack.Count - 1;

        //            var currentPage = navPage.Navigation.NavigationStack[index];

        //            var vm = currentPage.BindingContext as ViewModel;

        //            if (vm != null)
        //            {
        //                var answer = vm.OnBackButtonPressed();
        //                if (answer)
        //                    return true;
        //            }

        //        }
        //    }

        //    return base.OnOptionsItemSelected(item);
        //}

    }
}

