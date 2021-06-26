using Moq;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PokedexTests
{

    public class TestHelpers
    {
        public static IRestClient MockRestClient<T>(HttpStatusCode httpStatusCode, string json)
        where T : new()
        {
            var data = JsonConvert.DeserializeObject<T>(json);
            var response = new Mock<IRestResponse<T>>();
            response.Setup(_ => _.StatusCode).Returns(httpStatusCode);
            response.Setup(_ => _.Data).Returns(data);

            var mockIRestClient = new Mock<IRestClient>();
            mockIRestClient.Setup(x => x.Execute<T>(It.IsAny<IRestRequest>())).Returns(response.Object);
            return mockIRestClient.Object;
        }

        public static string GetFileContents(string sampleFile)
        {
            var asm = Assembly.GetExecutingAssembly();
            var resource = string.Format("PokedexTests.Resources.{0}", sampleFile);
            using (var stream = asm.GetManifestResourceStream(resource))
            {
                if (stream != null)
                {
                    var reader = new StreamReader(stream);
                    return reader.ReadToEnd();
                }
            }
            return string.Empty;
        }
    }
}
