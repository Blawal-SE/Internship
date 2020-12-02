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
    public class DeleteStudentSteps
    {
        AddEditStudentDto model = new AddEditStudentDto();
        private string _Token;
        private int _id;
        IDictionary<string, string> user = new Dictionary<string, string>();
        private IRestResponse _restResponse;
        private LoginUserDto _userResponseDto;
        [Given(@"the user is  login")]
        public void GivenTheUserIsLogin()
        {
            user.Add("username", "b761");
            user.Add("password", "761");
            user.Add("grant_type", "password");
            var request = new HttpRequestWrapper()
                      .SetMethod(Method.POST)
                      .SetResourse("/Login")
                      .AddParameterWithoutObject(user);

            _restResponse = new RestResponse();
            _restResponse = request.Execute();
            _userResponseDto = JsonConvert.DeserializeObject<LoginUserDto>(_restResponse.Content);
            _Token = _userResponseDto.access_token;
            Assert.AreEqual(_userResponseDto.token_type, "bearer");
        }

        [Given(@"the Id of Student to delete is (.*)")]
        public void GivenTheIdOfStudentToDeleteIs(int Id)
        {
            Dictionary<string, string> header = new Dictionary<string, string>();
            header.Add("Authorization", "bearer " + _Token);
            _id = Id;
            var Request = new HttpRequestWrapper()
                     .SetMethod(Method.GET)
                     .AddHeaders(header)
                     .SetResourse("/api/Student/?id=" + _id);
            _restResponse = new RestResponse();
            _restResponse = Request.Execute();
            model = JsonConvert.DeserializeObject<AddEditStudentDto>(_restResponse.Content);

            Assert.AreEqual(HttpStatusCode.OK, _restResponse.StatusCode);
            Assert.AreNotEqual(model, null, "This Record Does Not exist in Database Change Id Please And Try Again This Test");

        }

        [When(@"the Student of that Id get and Request to Delete record Made")]
        public void WhenTheStudentOfThatIdGetAndRequestToDeleteRecordMade()
        {
            Dictionary<string, string> header = new Dictionary<string, string>();
            header.Add("Authorization", "bearer " + _Token);
            var Request = new HttpRequestWrapper()
                    .SetMethod(Method.DELETE)
                    .AddHeaders(header)
                    .SetResourse("/api/Student/?id=" + _id);
            _restResponse = new RestResponse();
            _restResponse = Request.Execute();
        }

        [Then(@"the result will  be true")]
        public void ThenTheResultWillBeTrue()
        {
            Assert.AreEqual(HttpStatusCode.OK, _restResponse.StatusCode);
            Assert.AreEqual("true", _restResponse.Content);
        }
    }
}
