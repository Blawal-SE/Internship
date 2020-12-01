using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using SCHOOL.RESTAPITESTING.DTO;
using SCHOOL.RESTAPITESTING.Helper;
using System;
using System.Collections.Generic;
using System.Net;
using TechTalk.SpecFlow;

namespace SCHOOL.RESTAPITESTING.Steps
{
    [Binding]
    public class LoginFlowFeatureSteps
    {
        IDictionary<string, string> user = new Dictionary<string, string>();

        LoginUserDto userResponse = new LoginUserDto();
        private IRestResponse _restResponse;
        private HttpStatusCode _statusCode;
        [Given(@"the user name is ""(.*)""")]
        public void GivenTheUserNameIs(string username)
        {
            user.Add("username", username);
        }

        [Given(@"the password  is ""(.*)""")]
        public void GivenThePasswordIs(int password)
        {
            user.Add("password", password.ToString());
        }
        [Given(@"the grant_type is ""(.*)""")]
        public void GivenTheGrant_TypeIs(string granttype)
        {
            user.Add("grant_type", granttype);
        }
        [When(@"the login request send")]
        public void WhenTheLoginRequestSend()
        {
            var request = new HttpRequestWrapper()
                           .SetMethod(Method.POST)
                           .SetResourse("/Login")
                           .AddParameterWithoutObject(user)
                           ;

            _restResponse = new RestResponse();
            _restResponse = request.Execute();
            _statusCode = _restResponse.StatusCode;
            userResponse = JsonConvert.DeserializeObject<LoginUserDto>(_restResponse.Content);
        }

        [Then(@"the In Response token type should be ""(.*)""")]
        public void ThenTheInResponseTokenTypeShouldBe(string tokentype)
        {
            Assert.AreEqual(userResponse.token_type, tokentype);
        }
        [Then(@"the In Response error should be ""(.*)""")]
        public void ThenTheInResponseErrorShouldBe(string error)
        {
            Assert.AreEqual(userResponse.error, error);
        }


    }
}
