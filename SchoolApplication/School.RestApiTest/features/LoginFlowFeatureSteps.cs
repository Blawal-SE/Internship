using Microsoft.VisualStudio.TestTools.UnitTesting;
using School.RestApiTest.DTO;
using System;
using System.Net.Http;
using TechTalk.SpecFlow;
using Newtonsoft.Json;
using System.Net;

namespace School.RestApiTest.features
{
    [Binding]
    public class LoginFlowFeatureSteps
    {
        LoginUserDto dto = new LoginUserDto();
        [Given(@"the user name is ""(.*)""")]
        public void GivenTheUserNameIs(string username)
        {
            dto.username = username;
        }
        [Given(@"the password  is ""(.*)""")]
        public void GivenThePasswordIs(string password)
        {
            dto.password = password;
        }
        [Given(@"the grant_type is ""(.*)""")]
        public void GivenTheGrant_TypeIs(string granttype)
        {
            dto.grant_type = granttype;

        }
        [When(@"the login request send")]
        public async void WhenTheLoginRequestSend()
        {
            WebRequest request = WebRequest.Create("https://localhost:44393/api/Student");
            request.Method = "Get";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            dto = JsonConvert.DeserializeObject<LoginUserDto>(JsonConvert.SerializeObject(response));

            // var response = await client.PostAsync("");
            //  Assert.Equals(dto.username, dto.password);
        }
        [Then(@"the In Response UserName password Must matches with given parameter and should get accesstoken")]
        public void ThenTheInResponseUserNamePasswordMustMatchesWithGivenParameterAndShouldGetAccesstoken()
        {

        }
    }
}
