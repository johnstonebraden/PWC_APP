using Android.App;
using Android.Widget;
using Android.OS;
using PWBackend;
using System;
using Android.Views;

namespace PWCApp
{
    [Activity(Label = "PWCApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        JOBSHandler objRest = new JOBSHandler();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            RequestWindowFeature(WindowFeatures.NoTitle);
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            Button btnAssign = FindViewById<Button>(Resource.Id.btnAssign);
            Button btnEditEmployees = FindViewById<Button>(Resource.Id.btnModEmployees);
            Button btnViewPlan = FindViewById<Button>(Resource.Id.btnViewPlan);

            btnAssign.Click += BtnAssign_Click;
            btnEditEmployees.Click += BtnEditEmployees_Click;
            btnViewPlan.Click += BtnViewPlan_Click;
            
            //EmployeeJob ej = new EmployeeJob();
            //EmployeeJob ej2 = new EmployeeJob();
            //JobsAssigned jaItem = new JobsAssigned();     //create a loop to add multiple employees
            //jaItem.assignAREA = "asdg";
            //jaItem.assignCLIENT = "fgsd";
            //jaItem.assignINSTRUCTIONS = "asf sada";
            //jaItem.assignJOBNUM = " asd";
            //jaItem.assignTIME = DateTime.Now;
            //jaItem.assignTRUCK = "safsad";
            //jaItem.assignWORK = " ada";
            //jaItem.txtSENT = DateTime.Now;

            
            
            //ej.assignID = jaItem.assignID;
            //ej.EmpNAME = "TestEmployee";
            //ej2.assignID = jaItem.assignID;
            //ej2.EmpNAME = "TestEmployee2";
            //jaItem.EmployeeJobs.Add(ej);
            //jaItem.EmployeeJobs.Add(ej2);

            //try
            //{
            //    objRest.ExecutePostRequest(jaItem);
            //}
            //catch (Exception ex)
            //{

            //    Toast.MakeText(this, "Something went wrong:" + ex.Message, ToastLength.Long).Show();
            //}









        }

        private void BtnViewPlan_Click(object sender, System.EventArgs e)
        {
            //StartActivity(typeof());
        }

        private void BtnEditEmployees_Click(object sender, System.EventArgs e)
        {
            StartActivity(typeof(EditEmployees1));
        }

        private void BtnAssign_Click(object sender, System.EventArgs e)
        {
            StartActivity(typeof(AssignJobP2Activity));
        }
    }
}



