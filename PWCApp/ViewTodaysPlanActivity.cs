using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace PWCApp
{
    [Activity(Label = "ViewTodaysPlanActivity", MainLauncher = false, Icon = "@drawable/icon")]
    public class ViewTodaysPlanActivity : Activity
    {
        Button btnSendAll, btnBack;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ViewTodaysPlan);
            // Create your application here




            btnSendAll = FindViewById<Button>(Resource.Id.btnSendAll);
            btnBack = FindViewById<Button>(Resource.Id.btnBackVTP);
        }
    }
}