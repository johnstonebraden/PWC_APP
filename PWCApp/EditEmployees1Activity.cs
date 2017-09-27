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
using PWBackend;

namespace PWCApp
{
    [Activity(Label = "EditEmployees1")]
    public class EditEmployees1 : Activity
    {
        ListView lvEmployees;
        List<Employee> myList;

        EmployeesHandler obj = new EmployeesHandler();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            RequestWindowFeature(WindowFeatures.NoTitle);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.EditEmployees1);
            // Create your application here

            Button btnBack = FindViewById<Button>(Resource.Id.btnBackEE1);
            lvEmployees = FindViewById<ListView>(Resource.Id.lvWorkersEE1);
            myList = obj.ExecuteGetRequest();
            lvEmployees.Adapter = new EmployeeAdapter(this, myList);
            
            btnBack.Click += BtnBack_Click;
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            Finish();
        }
    }
}