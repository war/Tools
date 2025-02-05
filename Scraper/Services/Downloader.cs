﻿using Scraper.Services.Interfaces;

namespace Scraper.Services
{
    public class Downloader : IDownloader
    {
        private HttpClient _client;
        private readonly IThrottler _throttler;

        public Downloader(IThrottler throttler)
        {
			_throttler = throttler;
			_client = new HttpClient();
            //_client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/89.0.4389.82 Safari/537.36");
		}

        public async Task<string> DownloadPageAsync(string url)
        {
            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            
            await _throttler.ThrottleRequests();

			return responseBody;
        }
    }
}
