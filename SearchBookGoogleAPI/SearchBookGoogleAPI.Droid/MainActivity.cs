using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.FloatingActionButton;
using MyBookshelf.Core.Models;
using SearchBookGoogleAPI.Core.Helpers;
using SearchBookGoogleAPI.Core.Services;
using SearchBookGoogleAPI.Droid.Adapters;
using ZXing.Mobile;

namespace SearchBookGoogleAPI.Droid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        EditText txtSearch;
        Button btnSearch;
        Button btnScan;
        FloatingActionButton fabGoTop;
        ListView lvBooks;
        TextView lblResultCount;

        BookInfoService bookInfoService;
        List<BookInfo> items;
        int totalItemCount;
        bool isLoading;

        private Android.App.AlertDialog loadingDialog;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            MobileBarcodeScanner.Initialize(this.Application);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            // Init
            items = new List<BookInfo>();
            bookInfoService = new BookInfoService();

            // Get controls
            txtSearch = FindViewById<EditText>(Resource.Id.txtSearch);
            lblResultCount = FindViewById<TextView>(Resource.Id.lblResultCount);
            btnSearch = FindViewById<Button>(Resource.Id.btnSearch);
            btnScan = FindViewById<Button>(Resource.Id.btnScan);
            fabGoTop = FindViewById<FloatingActionButton>(Resource.Id.fabGoTop);
            lvBooks = FindViewById<ListView>(Resource.Id.lvBooks);

            // Add Events
            btnSearch.Click += BtnSearch_Click;
            btnScan.Click += BtnScan_Click;
            fabGoTop.Click += FabGoTop_Click;
            lvBooks.Scroll += LvBooks_Scroll;
        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void LvBooks_Scroll(object sender, AbsListView.ScrollEventArgs e)
        {
            if (totalItemCount > 0)
            {
                if (totalItemCount < 10)
                    return;

                int lastVisibleItem = e.FirstVisibleItem + e.VisibleItemCount;
                if (!isLoading && (lastVisibleItem == totalItemCount))
                {
                    isLoading = true;
                    RefreshData(false);

                    fabGoTop.Visibility = ViewStates.Visible;
                }
            }
        }

        private void BtnSearch_Click(object sender, System.EventArgs e)
        {
            try
            {
                RefreshData(true);
            }
            catch (System.Exception ex)
            {
                ShowAlertDialog("Error", ex.Message);
            }
        }

        private void BtnScan_Click(object sender, System.EventArgs e)
        {
            try
            {
                Scan();
            }
            catch (System.Exception ex)
            {
                ShowAlertDialog("Error", ex.Message);
            }
        }

        private void FabGoTop_Click(object sender, System.EventArgs e)
        {
            lvBooks.SmoothScrollToPosition(0);
        }

        public virtual async void RefreshData(bool reset)
        {
            ShowLoadingDialog("Loading...");
            var state = lvBooks.OnSaveInstanceState();
            string search = txtSearch.Text;
            List<BookInfo> itemsSearch = await bookInfoService.RefreshDataAsync(search, reset);

            if (itemsSearch == null)
            {
                HideLoadingDialog();
                return;
            }

            if (reset)
            {
                items.Clear();
                fabGoTop.Visibility = ViewStates.Invisible;
            }


            items.AddRange(itemsSearch);
            totalItemCount = items.Count;


            if (items == null)
            {
                lvBooks.Adapter = null;
                return;
            }

            lvBooks.Adapter = new BookInfoAdapter(this, items);
            lblResultCount.Text = $"Showing {totalItemCount} result(s)";
            isLoading = false;

            if (!reset)
                lvBooks.OnRestoreInstanceState(state);

            HideLoadingDialog();
        }

        public virtual async void Scan()
        {
            string code = await BarcodeScanner.Scan();

            if (string.IsNullOrEmpty(code))
                return;

            txtSearch.Text = code;
            RefreshData(true);

        }

        public virtual void ShowAlertDialog(string title, string message)
        {
            Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(this);
            alert.SetTitle(title);
            alert.SetMessage(message);

            alert.Create().Show();
        }

        public void ShowLoadingDialog(string message)
        {
            Android.App.AlertDialog.Builder dialogBuilder = new Android.App.AlertDialog.Builder(this);

            var dialogView = this.LayoutInflater.Inflate(Resource.Layout.loading_dialog, null);
            dialogBuilder.SetView(dialogView);
            dialogBuilder.SetCancelable(false);

            var dialogText = dialogView.FindViewById<TextView>(Resource.Id.prbMessage);
            dialogText.Text = message;
            loadingDialog = dialogBuilder.Create();
            loadingDialog.Show();
        }


        public void HideLoadingDialog()
        {
            if (loadingDialog != null)
            {
                loadingDialog.Dismiss();
            }
        }
    }
}
