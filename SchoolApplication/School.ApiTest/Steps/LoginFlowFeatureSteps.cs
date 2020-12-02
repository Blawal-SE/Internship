using NUnit.Framework;
using RestSharp;
using School.ApiTest.DTO;
using School.ApiTest.Helper;
using System;
using System.Net;
using TechTalk.SpecFlow;

namespace School.ApiTest.Steps
{
    [Binding]
    public class LoginFlowFeatureSteps
    {
        LoginUserDto user = new LoginUserDto();
        private IRestResponse _restResponse;
        private HttpStatusCode _statusCode;
        [Given(@"the user name is ""(.*)""")]
        public void GivenTheUserNameIs(string username)
        {
            user.username = username;
        }

        [Given(@"the password  is ""(.*)""")]
        public void GivenThePasswordIs(int password)
        {
            user.password = password.ToString();
        }
        [Given(@"the grant_type is ""(.*)""")]
        public void GivenTheGrant_TypeIs(string granttype)
        {
            user.grant_type = granttype;
        }
        [When(@"the login request send")]
        public void WhenTheLoginRequestSend()
        {
            var request = new HttpRequestWrapper()
                           .SetMethod(Method.POST)
                           .SetResourse("/Login")
                           .AddJsonContent(user);

            _restResponse = new RestResponse();
            _restResponse = request.Execute();
            _statusCode = _restResponse.StatusCode;
        }
        [Then(@"the In Response Ushould be (.*)")]
        public void ThenTheInResponseUshouldBe(int p0)
        {
            Assert.Equals(_statusCode, 200);
        }
    }
}
