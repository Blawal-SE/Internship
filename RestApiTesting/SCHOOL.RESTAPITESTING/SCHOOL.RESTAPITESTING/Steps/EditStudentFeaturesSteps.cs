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
    public class EditStudentFeaturesSteps
    {
        AddEditStudentDto model = new AddEditStudentDto();
        private string _Token;
        IDictionary<string, string> user = new Dictionary<string, string>();
        private IRestResponse _restResponse;
        private LoginUserDto _userResponseDto;

        [Given(@"the user is login")]
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

        [Given(@"the user Enter Edit Student of Id (.*) with some EditRecordDetaol")]
        public void GivenTheUserEnterEditStudentOfIdWithSomeEditRecordDetaol(int id)
        {
            model.Id = id;
            model.Name = "EditiedRecord";
            model.FName = "EditedFather Name";
            model.Email = "Editedtest@gmail.com";
            model.Phone = "03485048818";
            model.Dob = "12/12/2020";
            int[] courses = { 1, 2, 3 };
            model.Courses = new List<int>(courses);
            model.Password = "1231231";
            model.ConfirmPassword = "1231231";
        }

        [When(@"the Request to Edit record Made")]
        public void WhenTheRequestToEditRecordMade()
        {
            Dictionary<string, string> header = new Dictionary<string, string>();
            header.Add("Authorization", "bearer " + _Token);
            var Request = new HttpRequestWrapper()
                     .SetMethod(Method.PUT)
                     .AddHeaders(header)
                     .SetResourse("/api/Student")
                     .AddJsonContent(model);
            _restResponse = new RestResponse();
            _restResponse = Request.Execute();

        }

        [Then(@"the result should be true")]
        public void ThenTheResultShouldBeTrue()
        {
            Assert.AreEqual("true", _restResponse.Content);
            Assert.AreEqual(HttpStatusCode.OK, _restResponse.StatusCode);
        }
    }
}
