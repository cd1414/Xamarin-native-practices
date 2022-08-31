using System.Threading.Tasks;
using ZXing;
using ZXing.Mobile;

namespace SearchBookGoogleAPI.Core.Helpers
{
    public static class BarcodeScanner
    {
        public static async Task<string> Scan()
        {
            MobileBarcodeScanner MScanner = new MobileBarcodeScanner();
            Result result = await MScanner.Scan();

            if (result == null)
                return string.Empty;

            return result.Text;
        }
    }
}

