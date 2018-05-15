using System.Net.Http;
using System.Threading.Tasks;

namespace WebAutomation.Core.Support
{
    public static class ZEndpoint
    {
        public static async Task<bool> LinkIsValidAsync(string absolutePath)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", "UW Thomas");
                HttpResponseMessage response = await client.GetAsync(absolutePath);
                return (response.StatusCode == System.Net.HttpStatusCode.OK);
            }
        }
    }
}
