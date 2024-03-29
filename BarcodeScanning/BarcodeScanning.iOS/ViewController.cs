﻿using BarcodeScanning.Core;
using Foundation;
using System;
using UIKit;

namespace BarcodeScanning.iOS
{
    public partial class ViewController : UIViewController
    {
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            btnScan.TouchUpInside += BtnScan_TouchUpInside;
        }

        private async void BtnScan_TouchUpInside(object sender, EventArgs e)
        {
            string code = await BarcodeScanner.Scan();
            txtResult.Text = code;
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}
