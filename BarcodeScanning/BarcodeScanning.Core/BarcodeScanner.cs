using System;
using System.Threading.Tasks;
using ZXing.Mobile;

namespace BarcodeScanning.Core
{
    public static class BarcodeScanner
    {
        public static async Task<string> Scan()
        {
            var MScanner = new MobileBarcodeScanner();
            var result = await MScanner.Scan();

            if (result == null) return string.Empty;

            return result.Text;
        }
    }
}

