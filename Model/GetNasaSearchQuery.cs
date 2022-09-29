using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FluentValidation;
using System.ComponentModel.DataAnnotations;
using MediatR;
using System.Threading;
using System.Net.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace NasaApiApp.Model
{
    public class GetNasaSearchQuery : IRequest<NasaResponse>
    {
        [StringLength(150)]
        public string q { get; set; }

        [RegularExpression(@"\d{4}", ErrorMessage = "start date must be year.")]
        public string year_start { get; set; }

        [RegularExpression(@"\d{4}", ErrorMessage = "end date must be year.")]
        public string year_end { get; set; }

        [RegularExpression("image|audio", ErrorMessage = "media type should be either image or audio")]
        public string media_type { get; set; }       
       
    }

    public class GetNasaSearchQueryHandler : IRequestHandler<GetNasaSearchQuery, NasaResponse>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<GetNasaSearchQueryHandler> _logger;

        public GetNasaSearchQueryHandler(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<GetNasaSearchQueryHandler> logger)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _logger = logger;
        }
        public async Task<NasaResponse> Handle(GetNasaSearchQuery request, CancellationToken cancellationToken)
        {
            HttpClient client = _httpClientFactory.CreateClient();
            NasaResponse response = null;
            string baseurl = _configuration.GetSection("NasaApi:Url").Value;           
           
            var queryParams = new Dictionary<string, string>();
            if (request.q != null)
                queryParams.Add(nameof(request.q), request.q);

            if (request.year_start != null)
                queryParams.Add(nameof(request.year_start), request.year_start);

            if (request.year_end != null)
                queryParams.Add(nameof(request.year_end), request.year_end);

            if (request.media_type != null)
                queryParams.Add(nameof(request.media_type), request.media_type);
            string url = QueryHelpers.AddQueryString($"{baseurl}", queryParams);

            HttpResponseMessage responseMessage = await client.GetAsync(url);
            if (responseMessage.IsSuccessStatusCode)
            {
                var result = responseMessage.Content.ReadAsStringAsync();
                response = JsonConvert.DeserializeObject<NasaResponse>(result.Result);
            }

            return await Task.FromResult(response);
        }
    }



}
