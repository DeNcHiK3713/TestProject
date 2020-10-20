using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace VkStatus.Utils
{
    class UserResponse
    {
        public IEnumerable<User> response { get; set; }
    }
    class User
    {
        public string status { get; set; }
    }
    public static class VkUtils
    {
        public const string EmptyStatus = "";
        public const string ApiUrl = "https://api.vk.com/method/users.get?user_ids={0}&fields=status&v=5.124&access_token={1}";
        public static string GetId(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException(nameof(url));
            }
            var pos = url.LastIndexOf("/");
            if (pos == -1)
            {
                throw new Exception("Wrong url!");
            }
            var id = url.Substring(pos + 1);
            pos = id.IndexOf("?");
            if (pos != -1)
            {
                id = id.Substring(0, pos);
            }
            return id;
        }
        public static string GetStatus(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException(nameof(url));
            }
            if (string.IsNullOrEmpty(Properties.Settings.Default.AccessToken))
            {
                throw new Exception("Need AccessToken in app.config!");
            }
            var id = GetId(url);
            var requestUrl = string.Format(ApiUrl, id, Properties.Settings.Default.AccessToken);
            var request = WebRequest.Create(requestUrl);
            var response = request.GetResponse();
            string json = null;
            using (var stream = response.GetResponseStream())
            {
                using (var reader = new StreamReader(stream))
                {
                    json = reader.ReadToEnd();
                }
            }
            response.Close();
            var userResponse = JsonConvert.DeserializeObject<UserResponse>(json);
            var user = userResponse.response.First();
            return user.status;
        }
    }
}