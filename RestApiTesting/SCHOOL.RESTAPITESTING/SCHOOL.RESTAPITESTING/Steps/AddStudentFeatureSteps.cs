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
    public class AddStudentFeatureSteps
    {
        private String _Token;
        private bool _result;
        private IRestResponse _restResponse;
        private HttpStatusCode _statusCode;
        private StudentDtoResponse _studentDtoResp;
        private LoginUserDto _userResponseDto;
        //[Given(@"User Will enter Username and password and make request to login")]
        //public void GivenUserWillEnterUsernameAndPasswordAndMakeRequestToLogin()
        //{
        //    var obj = new
        //    {
        //        username = "b761",
        //        password = "761",
        //        grant_type = "password"
        //    };
        //    var request = new HttpRequestWrapper()
        //              .SetMethod(Method.POST)
        //              .SetResourse("/Login")
        //              .AddJsonContent(obj);

        //    _restResponse = new RestResponse();
        //    _restResponse = request.Execute();
        //    _statusCode = _restResponse.StatusCode;
        //    _userResponseDto = JsonConvert.DeserializeObject<LoginUserDto>(_restResponse.Content);
        //    _Token = _userResponseDto.access_token;
        //    Assert.AreEqual(_userResponseDto.token_type, "bearer");

        //}

        [Given(@"Will Add New Student Details Call to Add Student ""(.*)""")]
        public void GivenWillAddNewStudentDetailsCallToAddStudent(string name)
        {
            var obj = new
            {
                username = "b761",
                password = "761",
                grant_type = "password"
            };
            var request = new HttpRequestWrapper()
                      .SetMethod(Method.POST)
                      .SetResourse("/Login")
                      .AddJsonContent(obj);

            _restResponse = new RestResponse();
            _restResponse = request.Execute();
            _statusCode = _restResponse.StatusCode;
            _userResponseDto = JsonConvert.DeserializeObject<LoginUserDto>(_restResponse.Content);
            _Token = _userResponseDto.access_token;
            Assert.AreEqual(_userResponseDto.token_type, "bearer");

            Dictionary<string, string> header = new Dictionary<string, string>();
            header.Add("Authorization", "bearer " + _Token);
            AddEditStudentDto student = new AddEditStudentDto();
            student.Name = name;
            student.FName = "testfather";
            student.Email = "test@gmail.com";
            student.Dob = "11/11/1995";
            List<int> courses = new List<int>();
            courses.Add(1);
            courses.Add(2);
            courses.Add(3);
            student.Courses = courses;
            var Request = new HttpRequestWrapper()
                     .SetMethod(Method.POST)
                     .AddHeaders(header)
                     .SetResourse("/api/Student")
                     .AddJsonContent(student);

            _restResponse = new RestResponse();
            _restResponse = Request.Execute();
            _statusCode = _restResponse.StatusCode;
            _studentDtoResp = JsonConvert.DeserializeObject<StudentDtoResponse>(_restResponse.Content);
            Assert.AreEqual(_studentDtoResp.result, true);
        }

        [Then(@"the reponse Will get true")]
        public void ThenTheReponseWillGetTrue()
        {
            Assert.AreEqual(_studentDtoResp.result, true);
        }

    }
}
