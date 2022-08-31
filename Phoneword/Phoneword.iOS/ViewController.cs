using System;
using CoreGraphics;
using Foundation;
using Phoneword.Core;
using UIKit;

namespace Phoneword.iOS
{
    public partial class ViewController : UIViewController
    {
        string translatedNumber;
        UILabel lblPhoneword;
        UITextField txtPhoneword;
        UIButton btnTranslate;
        UIButton btnCall;

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            AddSubViews();
        }

        private void AddSubViews()
        {
            lblPhoneword = new UILabel();
            lblPhoneword.Frame = new CGRect(40, 80, 300, 40);
            lblPhoneword.Text = "Enter a Phoneword";

            txtPhoneword = new UITextField();
            txtPhoneword.Frame = new CGRect(40, 140, 300, 40);
            txtPhoneword.BorderStyle = UITextBorderStyle.RoundedRect;
            txtPhoneword.Text = "1-855-XAMARIN";

            btnTranslate = new UIButton(UIButtonType.System);
            btnTranslate.Frame = new CGRect(40, 200, 300, 40);
            btnTranslate.SetTitle("Translate", UIControlState.Normal);
            btnTranslate.TouchUpInside += BtnTranslate_TouchUpInside;

            btnCall = new UIButton(UIButtonType.System);
            btnCall.Frame = new CGRect(40, 260, 300, 40);
            btnCall.SetTitle("Call", UIControlState.Normal);
            btnCall.Enabled = false;
            btnCall.TouchUpInside += BtnCall_TouchUpInside;

            View.AddSubviews(lblPhoneword, txtPhoneword, btnTranslate, btnCall);
        }


        private void BtnTranslate_TouchUpInside(object sender, EventArgs e)
        {
            translatedNumber = PhoneTranslator.ToNumber(txtPhoneword.Text);
            txtPhoneword.ResignFirstResponder();

            if (string.IsNullOrEmpty(translatedNumber))
            {
                btnCall.SetTitle("Call", UIControlState.Normal);
                btnCall.Enabled = false;
            }
            else
            {
                btnCall.SetTitle($"Call {translatedNumber}", UIControlState.Normal);
                btnCall.Enabled = true;
            }
        }

        private void BtnCall_TouchUpInside(object sender, EventArgs e)
        {
            var url = new NSUrl($"tel:{translatedNumber}");

            if (!UIApplication.SharedApplication.OpenUrl(url))
            {
                var alert = UIAlertController.Create("Not Supported", "Scheme 'tel is not supported on this device", UIAlertControllerStyle.Alert);
                alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
                PresentViewController(alert, true, null);
            }
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}
