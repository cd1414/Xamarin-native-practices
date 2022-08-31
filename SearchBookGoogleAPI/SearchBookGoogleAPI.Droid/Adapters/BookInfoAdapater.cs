using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using MyBookshelf.Core.Models;

namespace SearchBookGoogleAPI.Droid.Adapters
{
    public class BookInfoAdapter
        : BaseAdapter<BookInfo>
    {
        Activity context;
        List<BookInfo> items;

        public BookInfoAdapter(Activity _context, List<BookInfo> _items)
        {
            context = _context;
            items = _items;
        }

        public override int Count => items.Count;

        public override long GetItemId(int position) => position;

        public override BookInfo this[int position] => items[position];

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            BookInfo item = items[position];
            View view = convertView;

            if (item == null || item.VolumeInfo == null)
                return view;

            if (view == null)
                view = context.LayoutInflater.Inflate(Resource.Layout.Book, null);

            view.FindViewById<TextView>(Resource.Id.lblTitle).Text = item.VolumeInfo.Title;
            view.FindViewById<TextView>(Resource.Id.lblCountPages).Text = $"{item.VolumeInfo.PageCount} page(s)";

            if (item.VolumeInfo.Authors != null && item.VolumeInfo.Authors.Length > 0)
                view.FindViewById<TextView>(Resource.Id.lblAuthors).Text = string.Join(",", item.VolumeInfo.Authors);

            if (item.VolumeInfo.Description != null)
                view.FindViewById<TextView>(Resource.Id.lblDescription).Text = item.VolumeInfo.Description;

            return view;
        }
    }
}

