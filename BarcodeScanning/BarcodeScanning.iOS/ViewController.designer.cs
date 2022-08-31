// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace BarcodeScanning.iOS
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		UIKit.UIButton btnScan { get; set; }

		[Outlet]
		UIKit.UITextField txtResult { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (txtResult != null) {
				txtResult.Dispose ();
				txtResult = null;
			}

			if (btnScan != null) {
				btnScan.Dispose ();
				btnScan = null;
			}
		}
	}
}
