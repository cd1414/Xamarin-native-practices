using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Widget;
using AndroidX.AppCompat.App;
using BarcodeScanning.Core;
using ZXing.Mobile;

namespace BarcodeScanning.Droid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        Button btnScan;
        TextView txtResult;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            MobileBarcodeScanner.Initialize(Application);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);


            // get controls
            btnScan = FindViewById<Button>(Resource.Id.btnScan);
            txtResult = FindViewById<TextView>(Resource.Id.txtResult);

            // Add events
            btnScan.Click += BtnScan_Click;
        }

        private async void BtnScan_Click(object sender, System.EventArgs e)
        {
            string code = await BarcodeScanner.Scan();
            Log.Debug("Code", code);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
