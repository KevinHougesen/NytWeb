using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json; // Make sure to have this package installed for JSON operations

namespace InstagramApi
{
    public class InstagramBasicDisplayApi
    {
        private string _appId = "1320174152716541"; // Set your app ID
        private string _appSecret = "d92d0eedd459439af59da6876f164aa6"; // Set your app secret
        private string _redirectUrl = "https://ditnyt.dk/"; // Set your redirect URI
        private string _getCode = "";
        private string _apiBaseUrl = "https://api.instagram.com/";
        private string _graphBaseUrl = "https://graph.instagram.com/";
        private string _userAccessToken = "";
        private string _userAccessTokenExpires = "";

        public string AuthorizationUrl { get; private set; } = "";
        public bool HasUserAccessToken { get; private set; } = false;
        public string UserId { get; private set; } = "";

        public InstagramBasicDisplayApi(Dictionary<string, string> paramsDict)
        {
            _getCode = paramsDict.GetValueOrDefault("get_code");

            SetUserInstagramAccessToken(paramsDict);
            SetAuthorizationUrl();
        }

        public string GetUserAccessToken() => _userAccessToken;
        public string GetUserAccessTokenExpires() => _userAccessTokenExpires;

        private void SetAuthorizationUrl()
        {
            var getVars = new Dictionary<string, string>
            {
                {"app_id", _appId},
                {"redirect_uri", _redirectUrl},
                {"response_type", "code"}
            };

            var payload = JsonConvert.SerializeObject(new { app_id = _appId, redirect_url = _redirectUrl, scope = "user_profile,user_media", response_type = "code" });
            var jsonContent = new StringContent(payload, Encoding.UTF8, "application/json");

            // create url
            AuthorizationUrl = _apiBaseUrl + "oauth/authorize?" + jsonContent;
        }

        private async Task SetUserInstagramAccessToken(Dictionary<string, string> paramsDict)
        {
            if (paramsDict.ContainsKey("access_token")) // We have an access token
            {
                _userAccessToken = paramsDict["access_token"];
                HasUserAccessToken = true;
                UserId = paramsDict["user_id"];
            }
            else if (paramsDict.ContainsKey("get_code")) // Try and get an access token
            {
                var userAccessTokenResponse = await FetchUserAccessTokenAsync(_userAccessToken);
                _userAccessToken = userAccessTokenResponse.GetValueOrDefault("access_token");
                HasUserAccessToken = true;
                UserId = userAccessTokenResponse.GetValueOrDefault("user_id");

                // Get long-lived access token
                var longLivedAccessTokenResponse = await FetchLongLivedUserAccessTokenAsync();
                _userAccessToken = longLivedAccessTokenResponse.GetValueOrDefault("access_token");
                _userAccessTokenExpires = longLivedAccessTokenResponse.GetValueOrDefault("expires_in");
            }
        }

        private async Task<Dictionary<string, string>> FetchUserAccessTokenAsync(string auth)
        {
            var paramsDict = new Dictionary<string, string>
    {
        {"endpoint_url", $"{_apiBaseUrl}oauth/access_token"},
        {"type", "POST"},
        {
            "url_params", new Dictionary<string, string>
            {
                {"app_id", _appId},
                {"app_secret", _appSecret},
                {"grant_type", auth},
                {"redirect_uri", _redirectUrl},
                {"code", _getCode}
            }.ToString()
        }
    };

            return await MakeApiCall(paramsDict);
        }

        private async Task<Dictionary<string, string>> FetchLongLivedUserAccessTokenAsync()
        {
            var paramsDict = new Dictionary<string, string>
    {
        {"endpoint_url", $"{_graphBaseUrl}access_token"},
        {"type", "GET"},
        {
            "url_params", new Dictionary<string, string>
            {
                {"client_secret", _appSecret},
                {"grant_type", "ig_exchange_token"}
            }.ToString()
        }
    };

            return await MakeApiCall(paramsDict);
        }

        public async Task<Dictionary<string, string>> GetUserAsync()
        {
            var urlParams = new Dictionary<string, string>
            {
                {"fields", "id,username,media_count,account_type"},
                {"access_token", _userAccessToken}
            };

            var paramsDict = new Dictionary<string, string>
            {
                {"endpoint_url", $"{_graphBaseUrl}me"},
                {"type", "GET"},
                {"url_params", JsonConvert.SerializeObject(urlParams)} // Serialize the inner dictionary to a JSON string
            };

            return await MakeApiCall(paramsDict);
        }


        public async Task<Dictionary<string, string>> GetUsersMedia()
        {
            var urlParams = new Dictionary<string, string>
            {
                {"fields", "id,caption,media_type,media_url"},
                {"access_token", _userAccessToken}
            };

            var paramsDict = new Dictionary<string, string>
            {
                {"endpoint_url", $"{_graphBaseUrl}{UserId}/media"},
                {"type", "GET"},
                {"url_params", JsonConvert.SerializeObject(urlParams)} // Serialize the inner dictionary to a JSON string
            };

            return await MakeApiCall(paramsDict);
        }


        public async Task<Dictionary<string, string>> GetPaging(string pagingEndpoint)
        {
            var paramsDict = new Dictionary<string, string>
            {
                {"endpoint_url", pagingEndpoint},
                {"type", "GET"},
                {"url_params", new Dictionary<string, string>
                    {
                        {"paging", "true"},
                        {"access_token", _userAccessToken}
                    }.ToString()
                }
            };

            return await MakeApiCall(paramsDict);
        }

        public async Task<Dictionary<string, string>> GetMedia(string mediaId)
        {
            var paramsDict = new Dictionary<string, string>
            {
                {"endpoint_url", $"{_graphBaseUrl}{mediaId}"},
                {"type", "GET"},
                {"url_params", new Dictionary<string, string>
                    {
                        {"fields", "id,caption,media_type,media_url,permalink,thumbnail_url,timestamp,username"},
                        {"access_token", _userAccessToken}
                    }.ToString()
                }
            };

            return await MakeApiCall(paramsDict);
        }

        public async Task<Dictionary<string, string>> GetMediaChildren(string mediaId)
        {
            var paramsDict = new Dictionary<string, string>
            {
                {"endpoint_url", $"{_graphBaseUrl}{mediaId}/children"},
                {"type", "GET"},
                {"url_params", new Dictionary<string, string>
                    {
                        {"fields", "id,media_type,media_url,permalink,thumbnail_url,timestamp,username"},
                        {"access_token", _userAccessToken}
                    }.ToString()
                }
            };

            return await MakeApiCall(paramsDict);
        }

        private async Task<Dictionary<string, string>> MakeApiCall(Dictionary<string, string> paramsDict)
        {
            using HttpClient client = new HttpClient();

            try
            {
                HttpResponseMessage response;
                if (paramsDict["type"] == "POST")
                {
                    var content = new FormUrlEncodedContent(JsonConvert.DeserializeObject<Dictionary<string, string>>(paramsDict["url_params"]));
                    response = await client.PostAsync(paramsDict["endpoint_url"], content);
                }
                else // Assuming GET
                {
                    string url = BuildUrlWithParams(paramsDict["endpoint_url"], JsonConvert.DeserializeObject<Dictionary<string, string>>(paramsDict["url_params"]));
                    response = await client.GetAsync(url);
                }

                string responseString = await response.Content.ReadAsStringAsync();

                // Log the response string for debugging purposes
                Console.WriteLine("API Response: " + responseString);

                var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseString);
                return result ?? new Dictionary<string, string>(); // Return an empty dictionary instead of null
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine("Error making API call: " + ex.Message);
                return new Dictionary<string, string>(); // Return an empty dictionary on error
            }
        }


        private string BuildUrlWithParams(string baseUrl, Dictionary<string, string> parameters)
        {
            // Building a query string from the parameters dictionary.
            var queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);
            foreach (var param in parameters)
            {
                queryString[param.Key] = param.Value;
            }
            return $"{baseUrl}?{queryString}";
        }

    }
}
