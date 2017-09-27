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
using Android.Util;
using Android.Icu.Util;

namespace PWCApp
{
    [Activity(Label = "AssignJobP3")]
    public class AssignJobP3Activity : Activity
    {
        Button btnSelectDate, btnSelectTime, btnSelectEndTime;
        List<Employee> myList = GetEmployees.getEmployees();
        public static List<Employee> WorkerList;
        AutoCompleteTextView actxtWorkers;
        ListView lvSelectedEmps;
        TextView txtSelectedJob;
        //TextView txtTime;
        TextView txtTruckNo;
        string JobName, JobClient, JobNumber, JobArea, StartTime, EndTime, DateofJob;
        int JobID;
        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AssignJobP3);

            // Create your application here

            WorkerList = new List<Employee>();
            Button btnNext = FindViewById<Button>(Resource.Id.btnNextAJP3);
            Button btnBack = FindViewById<Button>(Resource.Id.btnBackAJP3);
            txtSelectedJob = FindViewById<TextView>(Resource.Id.txtSelectedJob);
            Button btnSelect = FindViewById<Button>(Resource.Id.btnAddWorker);
            lvSelectedEmps = FindViewById<ListView>(Resource.Id.lvCurrentWorkers);
           
            
            txtTruckNo = FindViewById<TextView>(Resource.Id.txtTruckNumbers);
            btnSelectDate = FindViewById<Button>(Resource.Id.btnSelectDate);
            btnSelectTime = FindViewById<Button>(Resource.Id.btnSelectTime);
            btnSelectEndTime = FindViewById<Button>(Resource.Id.btnSelectEndtime);

            btnSelectDate.Text = "Date: " +  DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            DateofJob = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            StartTime = "06:00:00";
            EndTime = "18:00:00";

            JobName = Intent.GetStringExtra("JobName");          
            JobID = Intent.GetIntExtra("JobID", 0);
            JobClient = Intent.GetStringExtra("JobClients");
            JobNumber = Intent.GetStringExtra("JobNumber");
            JobArea = Intent.GetStringExtra("JobArea");

            txtSelectedJob.Text = JobNumber + " - " + JobName + " - " + JobClient;
            actxtWorkers = FindViewById<AutoCompleteTextView>(Resource.Id.actxtWorker);
            var workerAdapter = new ArrayAdapter<string>(this, Resource.Layout.list_item, GetEmployees.getEmpNames());
           
            actxtWorkers.Adapter = workerAdapter;
            btnSelect.Click += BtnSelect_Click;
            btnBack.Click += BtnBack_Click;
            btnNext.Click += BtnNext_Click;
            lvSelectedEmps.ItemLongClick += LvSelectedEmps_ItemLongClick;
            btnSelectDate.Click += BtnSelectDate_Click;
            btnSelectTime.Click += BtnSelectTime_Click;
            btnSelectEndTime.Click += BtnSelectEndTime_Click;
        }

        private void BtnSelectEndTime_Click(object sender, EventArgs e)
        {
            TimePickerFragment frag1 = TimePickerFragment.NewInstance(delegate (TimeSpan time1)
            {
                btnSelectEndTime.Text = "End Time: " + time1.ToString();
                EndTime = time1.ToString();
            });
            frag1.Show(FragmentManager, TimePickerFragment.TAG);
        }

        private void BtnSelectTime_Click(object sender, EventArgs e)
        {
            TimePickerFragment frag = TimePickerFragment.NewInstance(delegate (TimeSpan time)
            {
                btnSelectTime.Text = "Start Time: " + time.ToString();
                StartTime = time.ToString();
            });
            frag.Show(FragmentManager, TimePickerFragment.TAG);
        }

        private void BtnSelectDate_Click(object sender, EventArgs e)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
            {
                btnSelectDate.Text = "Date: " + time.ToString("yyyy-MM-dd");
                DateofJob = time.ToString("yyyy-MM-dd");
            });
            frag.Show(FragmentManager, DatePickerFragment.TAG);
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            var msgInfo = new Intent(this, typeof(AssignJobP4Activity));

            msgInfo.PutExtra("JobTime", StartTime);
            msgInfo.PutExtra("JobEndTime", EndTime);
            msgInfo.PutExtra("JobDate", DateofJob);
            msgInfo.PutExtra("JobTruckNumbers", txtTruckNo.Text);
            msgInfo.PutExtra("JobName", JobName);
            msgInfo.PutExtra("JobID", JobID);
            msgInfo.PutExtra("JobClients", JobClient);
            msgInfo.PutExtra("JobNumber", JobNumber);
            msgInfo.PutExtra("JobArea", JobArea);

            StartActivity(msgInfo);

            //StartActivity(typeof(AssignJobP4Activity));

        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            Finish();
        }

        private void LvSelectedEmps_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            AlertDialog.Builder alertDialog = new AlertDialog.Builder(this);

            alertDialog.SetMessage("Do you want to remove selected worker from this job?");
            alertDialog.SetPositiveButton("Yes", delegate                         
            {
                var selectedEmp = WorkerList[e.Position];
                WorkerList.Remove(selectedEmp);
                lvSelectedEmps.Adapter = new SelectEmpAdapter(this, WorkerList);
                alertDialog.Dispose();
            });
            alertDialog.SetNegativeButton("Cancel", delegate          
            {
                alertDialog.Dispose();
            });
            alertDialog.Show();
            
        }

        private void BtnSelect_Click(object sender, EventArgs e)
        {
            if(actxtWorkers.Text != "")
            {
                Employee emp = new Employee();
                emp.empNAME = actxtWorkers.Text;
                emp.empMobile = GetEmployees.getEmpNumber(actxtWorkers.Text);
                WorkerList.Add(emp);

                lvSelectedEmps.Adapter = new SelectEmpAdapter(this, WorkerList);
                //Toast.MakeText(this, GetEmployees.getEmpNumber(actxtWorkers.Text), ToastLength.Long).Show();
            }   
            else
            {
                Toast.MakeText(this, "Input a valid name" , ToastLength.Long).Show();
            }
            actxtWorkers.Text = "";
        }



        public class DatePickerFragment : DialogFragment,
                                  DatePickerDialog.IOnDateSetListener
        {
            // TAG can be any string of your choice.
            public static readonly string TAG = "X:" + typeof(DatePickerFragment).Name.ToUpper();

            // Initialize this value to prevent NullReferenceExceptions.
            Action<DateTime> _dateSelectedHandler = delegate { };

            public static DatePickerFragment NewInstance(Action<DateTime> onDateSelected)
            {
                DatePickerFragment frag = new DatePickerFragment();
                frag._dateSelectedHandler = onDateSelected;
                return frag;
            }

            public override Dialog OnCreateDialog(Bundle savedInstanceState)
            {
                DateTime currently = DateTime.Now;
                DatePickerDialog dialog = new DatePickerDialog(Activity,
                                                               this,
                                                               currently.Year,
                                                               currently.Month,
                                                               currently.Day);
                return dialog;
            }

            public void OnDateSet(DatePicker view, int year, int monthOfYear, int dayOfMonth)
            {
                // Note: monthOfYear is a value between 0 and 11, not 1 and 12!
                DateTime selectedDate = new DateTime(year, monthOfYear + 1, dayOfMonth);
                Log.Debug(TAG, selectedDate.ToLongDateString());
                _dateSelectedHandler(selectedDate);
            }
        }

        //Time Picker
        public class TimePickerFragment : DialogFragment,
                                  TimePickerDialog.IOnTimeSetListener
        {
            // TAG can be any string of your choice.
            public static readonly string TAG = "Y:" + typeof(TimePickerFragment).Name.ToUpper();

            // Initialize this value to prevent NullReferenceExceptions.
            Action<TimeSpan> _timeSelectedHandler = delegate { };

            public static TimePickerFragment NewInstance(Action<TimeSpan> onTimeSet)
            {
                TimePickerFragment frag = new TimePickerFragment();
                frag._timeSelectedHandler = onTimeSet;
                return frag;
            }

            public override Dialog OnCreateDialog(Bundle savedInstanceState)
            {
                Calendar c = Calendar.Instance;
                int hour = c.Get(CalendarField.HourOfDay);
                int minute = c.Get(CalendarField.Minute);
                //bool is24HourView = false;
                TimePickerDialog dialog = new TimePickerDialog(Activity,
                                                               this,
                                                               hour,
                                                               minute,
                                                               false);
                return dialog;
            }

            public void OnTimeSet(TimePicker view, int hourOfDay, int minute)
            {
                //Do something when time chosen by user
                TimeSpan selectedTime = new TimeSpan(hourOfDay, minute, 00);
                Log.Debug(TAG, selectedTime.ToString());
                _timeSelectedHandler(selectedTime);
            }
        }
    }
}