using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using MyBookshelf.Core.Models;
using Newtonsoft.Json;

namespace SearchBookGoogleAPI.Core.Services
{
    public class BookInfoService
    {
        string baseURI;
        string filter;
        int page;
        int maxResults;
        int startIndex;

        public BookInfoService()
        {
            baseURI = "https://www.googleapis.com/books/v1/volumes";
            maxResults = 10;
        }

        public async Task<List<BookInfo>> RefreshDataAsync(string filter, bool refresh)
        {
            if (string.IsNullOrEmpty(filter))
                return null;

            if (page == 0 || refresh)
            {
                page = 1;
                startIndex = 0;
            }
            else
            {
                page++;
                startIndex = page * maxResults;
            }

            Uri searchUri = GetSearchUri(filter);

            List<BookInfo> items = new List<BookInfo>();

            using (HttpClient client = new HttpClient())
            {
                string content = await client.GetStringAsync(searchUri);
                var books = JsonConvert.DeserializeObject<Books>(content);

                if (books.Items != null)
                    items = new List<BookInfo>(books.Items);
            }

            return items;
        }

        public Uri GetSearchUri(string filter)
        {
            string uri = $"{baseURI}?startIndex={startIndex}";

            if (string.IsNullOrEmpty(filter))
                return new Uri(uri);

            return new Uri($"{uri}&q=\"{filter}\"");
        }
        public void SetCurrentPage(int newPage) => page = newPage;
    }
}

