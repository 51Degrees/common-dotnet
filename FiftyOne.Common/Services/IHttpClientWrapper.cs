/* *********************************************************************
 * This Original Work is copyright of 51 Degrees Mobile Experts Limited.
 * Copyright 2025 51 Degrees Mobile Experts Limited, Davidson House,
 * Forbury Square, Reading, Berkshire, United Kingdom RG1 3EU.
 *
 * This Original Work is licensed under the European Union Public Licence
 * (EUPL) v.1.2 and is subject to its terms as set out below.
 *
 * If a copy of the EUPL was not distributed with this file, You can obtain
 * one at https://opensource.org/licenses/EUPL-1.2.
 *
 * The 'Compatible Licences' set out in the Appendix to the EUPL (as may be
 * amended by the European Commission) shall be deemed incompatible for
 * the purposes of the Work and the provisions of the compatibility
 * clause in Article 5 of the EUPL shall not apply.
 *
 * If using the Work as, or as part of, a network application, by
 * including the attribution notice(s) required under Article 5 of the EUPL
 * in the end user terms of the application under an appropriate heading,
 * such notice(s) shall fulfill the requirements of that article.
 * ********************************************************************* */

using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace FiftyOne.Common.Services
{
    /// <summary>
    /// Wrapper for an HttpClient instance.
    /// An implementation can do various things, such as
    /// limit the number of concurrent requests.
    /// </summary>
    public interface IHttpClientWrapper
    {
        /// <summary>
        /// Calls <see cref="HttpClient.GetAsync(string, CancellationToken)"/>.
        /// </summary>
        /// <param name="requestUri"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> GetAsync(string requestUri, CancellationToken cancellationToken);

        /// <summary>
        /// Calls <see cref="HttpClient.PostAsync(string, HttpContent, CancellationToken)"/>.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> PostAsync(string uri, HttpContent content, CancellationToken cancellationToken);
    }
}
