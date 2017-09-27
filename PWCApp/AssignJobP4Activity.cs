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
using Android.Telephony;
using Android.Util;
using PWBackend;

namespace PWCApp
{
    [Activity(Label = "AssignJobP4")]
    public class AssignJobP4Activity : Activity
    {
        
        string JobTime, JobDate, JobTruckNo, JobName, JobClient, JobNumber, JobArea, JobInstructions, workersMSG, startTime, JobEndTime, endTime;
        int JobID;
        TextView txtMsg, txtExtraInstruct;
        JOBSHandler objJOBS = new JOBSHandler();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AssignJobP4);

            txtMsg = FindViewById<TextView>(Resource.Id.txtMessage);
            txtExtraInstruct = FindViewById<TextView>(Resource.Id.txtJobInstruct);
            Button btnSend = FindViewById<Button>(Resource.Id.btnSend);
            Button btnBack = FindViewById<Button>(Resource.Id.btnBackAJP4);
            Button btnSave = FindViewById<Button>(Resource.Id.btnSaveAJP4);
            txtMsg.Text = "";

            JobTime = Intent.GetStringExtra("JobTime");
            JobEndTime = Intent.GetStringExtra("JobEndTime");
            JobDate = Intent.GetStringExtra("JobDate");
            JobTruckNo = Intent.GetStringExtra("JobTruckNumbers");
            JobName = Intent.GetStringExtra("JobName");
            JobID = Intent.GetIntExtra("JobID", 0);
            JobClient = Intent.GetStringExtra("JobClients");
            JobNumber = Intent.GetStringExtra("JobNumber");
            JobArea = Intent.GetStringExtra("JobArea");

            workersMSG = "";
            foreach (var worker in AssignJobP3Activity.WorkerList)
            {
                workersMSG = workersMSG + worker.empNAME + ", ";
            }
           
            txtMsg.Text = "Team: " + workersMSG + "\n" + "Client: " + JobClient + "\n" + "Job Number: " + JobNumber + "\n" + "Area: " + JobArea + "\nTrucks: " + JobTruckNo + "\n" + "Date: " + JobDate + "\n" + "Start Time: " + JobTime + "\nEstimated end time: " + JobEndTime + "\n\nNote: " + txtExtraInstruct.Text;           
            btnSend.Click += BtnSend_Click;
            btnBack.Click += BtnBack_Click;
            btnSave.Click += BtnSave_Click;
            txtExtraInstruct.TextChanged += TxtExtraInstruct_TextChanged;
        }

        private void TxtExtraInstruct_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            txtMsg.Text = "Team: " + workersMSG + "\n" + "Client: " + JobClient + "\n" + "Job Number: " + JobNumber + "\n" + "Area: " + JobArea + "\nTrucks: " + JobTruckNo + "\n" + "Date: " + JobDate + "\n" + "Start Time: " + JobTime + "\nEstimated end time: " + JobEndTime + "\n\nNote: " + txtExtraInstruct.Text;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            JobInstructions = txtExtraInstruct.Text;
            JobsAssigned job = new JobsAssigned();
           // EmployeeJob emp1 = new EmployeeJob();
            foreach (var worker in AssignJobP3Activity.WorkerList)
            {
                EmployeeJob emp1 = new EmployeeJob();
                emp1.EmpNAME = worker.empNAME;
                job.EmployeeJobs.Add(emp1);
            }

            startTime = JobDate + "T00:" + JobTime;
            endTime = JobDate + "T00:" + JobEndTime;

            job.AssignJOBNUM = JobNumber;
            job.AssignCLIENT = JobClient;
            job.AssignWORK = JobName;
            job.AssignAREA = JobArea;
            job.AssignINSTRUCTIONS = JobInstructions;
            job.AssignTRUCK = JobTruckNo;
            job.TextSENT = null;
            job.AssignSTARTTIME = startTime;
            job.AssignENDTIME = endTime;
            

            try
            {
                objJOBS.ExecutePostRequest(job);
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, "Something went wrong:" + ex.Message, ToastLength.Long).Show();
            }
            Toast.MakeText(this, "Data saved", ToastLength.Long).Show();
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            Finish();
        }

        private void BtnSend_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (var i in AssignJobP3Activity.WorkerList)
                {
                    SmsManager.Default.SendTextMessage(i.empMobile, null, txtMsg.Text, null, null);
                    
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, "A message failed to send\n" + ex, ToastLength.Long).Show();
            }

            Toast.MakeText(this, "Messages sent", ToastLength.Long).Show();
        }




        
    }
}