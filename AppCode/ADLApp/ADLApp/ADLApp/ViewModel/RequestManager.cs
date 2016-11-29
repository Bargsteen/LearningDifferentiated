﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;
using RestSharp;
using ADLApp.Models;
using Newtonsoft.Json;
using System.Net;
using RestSharp.Deserializers;

namespace ADLApp.ViewModel
{
    class RequestManager : IAssignmentLoader, IAnswerSender, ILocationLoader, ILogin
    {
        private readonly IRestClient _rClient = new RestClient("http://adlearning.azurewebsites.net/api");

        /// <summary>
        /// Gets assignment based from AssigmentLoader, with the initialized client.
        /// Needs a Method from the controller with right input.
        /// </summary>
        /// <param name="resourceLocation"></param>
        /// <returns></returns>
        public async Task<Assignment> GetAssignment(string resourceLocation)
        {
            TaskFactory tf = new TaskFactory();
            RestRequest request = new RestRequest("/location/" + resourceLocation, Method.GET);
            IRestResponse response = await GetDataAsString(request);

            //Check object it has to create. Switch on a data in the json format("assignmentType":"MultipleChoice" for example
            if (response.Content != "Lokationen eksisterer ikke" || response.Content != "Lokationen har ikke nogen opgave")
                return await tf.StartNew(() => JsonConvert.DeserializeObject<MultipleChoiceAssignment>(response.Content));
            else return null;
        }
        private async Task<IRestResponse> GetDataAsString(RestRequest request)
        {
            request.RequestFormat = DataFormat.Json;
            IRestResponse response = await _rClient.ExecuteGetTaskAsync(request);
            return response;
        }
        public async Task<string> SendAnswer(Answer answer)
        {
            RestRequest request = new RestRequest($"/SendAnswer", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(answer);
            IRestResponse resp = await _rClient.ExecutePostTaskAsync(request);
            return resp.Content;
        }

        public async Task<List<Location>> GetLocations(string resource)
        {
            RestRequest request = new RestRequest(resource, Method.GET);
            request.RequestFormat = DataFormat.Json;
            IRestResponse<List<Location>> response = await _rClient.ExecuteGetTaskAsync<List<Location>>(request);
            return response.Data;
        }

        public async Task<IRestResponse> Login(LoginModel userinfo)
        {
            RestRequest request = new RestRequest("GetIdentity", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(userinfo);
            IRestResponse resp = await _rClient.ExecutePostTaskAsync(request);
            return resp;
        }
    }
}
