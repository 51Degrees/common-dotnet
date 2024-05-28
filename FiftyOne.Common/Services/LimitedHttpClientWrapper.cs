﻿using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FiftyOne.Common.Services
{
    /// <summary>
    /// Wrapper which limits the number of concurrent requests
    /// to the HttpClient. Requests made which would take the number
    /// of active requests over the limit will not be processed, and
    /// instead return a null response.
    /// </summary>
    public class LimitedHttpClientWrapper : IHttpClientWrapper
    {
        private readonly ILogger<LimitedHttpClientWrapper> _logger;
        private readonly HttpClient _client;
        private readonly int _maxConcurrent;
        private readonly ConcurrentDictionary<int, Task> _requests;

        /// <summary>
        /// Number of current requests that are being processed.
        /// Note that there may be a slight delay (no more than a millisecond
        /// or two) as the removal relies on a continuation.
        /// </summary>
        public int CurrentRequests => _requests.Count;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">
        /// Logger to use for errors.
        /// </param>
        /// <param name="httpClient">
        /// HttpClient to call.
        /// </param>
        /// <param name="maxConcurrent">
        /// Maximum number of requests to allow simultaniously.
        /// </param>
        public LimitedHttpClientWrapper(
            ILogger<LimitedHttpClientWrapper> logger,
            HttpClient httpClient,
            int maxConcurrent)
        {
            _logger = logger;
            _client = httpClient;
            _maxConcurrent = maxConcurrent;
            _requests = new ConcurrentDictionary<int, Task>();
        }

        public Task<HttpResponseMessage> GetAsync(
            string requestUri,
            CancellationToken cancellationToken)
        {
            return WrapRequest(() => _client.GetAsync(requestUri, cancellationToken));
        }

        public Task<HttpResponseMessage> PostAsync(
            string uri,
            HttpContent content,
            CancellationToken cancellationToken)
        {
            return WrapRequest(() => _client.PostAsync(uri, content, cancellationToken));
        }

        /// <summary>
        /// Wraps a request to the client with logic to limit.
        /// </summary>
        /// <param name="getResponse">
        /// Function to call the client, either GET or POST.
        /// </param>
        /// <returns>
        /// Result of the getResponse function, or null.
        /// </returns>
        /// <exception cref="Exception">
        /// If a request could not be added to the dictionary.
        /// </exception>
        private Task<HttpResponseMessage> WrapRequest(
            Func<Task<HttpResponseMessage>> getResponse)
        {
            if (_requests.Count < _maxConcurrent)
            {
                var request = getResponse();
                if (_requests.TryAdd(request.Id, request) == false)
                {
                    throw new Exception("Failed to add request.");
                }
                request.ContinueWith(t =>
                {
                    if (_requests.TryRemove(request.Id, out var removed) == false)
                    {
                        _logger.LogError("Failed to remove completed request.");
                    }
                });
                return request;
            }
            else
            {
                return Task.FromResult<HttpResponseMessage>(null);
            }
        }
    }
}
