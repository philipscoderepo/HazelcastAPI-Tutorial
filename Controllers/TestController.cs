﻿using Microsoft.AspNetCore.Mvc;
using HazelcastAPI.Services;

namespace HazelcastAPI.Controllers
{
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IHazelcastService<string, int> _service;
        public TestController(IHazelcastService<string, int> service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("test/get-attempts/{user}")]
        public async Task<JsonResult> GetAttempts(string user)
        {
            var rec = await _service.GetRecordAsync(user).ConfigureAwait(false);
            return new JsonResult(rec);
        }

        [HttpGet]
        [Route("test/add-attempt/{user}")]
        public async Task<JsonResult> AddAttempt(string user)
        {
            var rec = await _service.GetRecordAsync(user).ConfigureAwait(false);
            await _service.PutRecordAsync(user, ++rec).ConfigureAwait(false);
            var newCount = await _service.GetRecordAsync(user).ConfigureAwait(false);
            return new JsonResult("new count: " + newCount);
        }

        [HttpGet]
        [Route("test/delete-record/{user}")]
        public async Task<JsonResult> DeleteRecord(string user)
        {
            await _service.DeleteRecordAsync(user).ConfigureAwait(false);
            return new JsonResult("Record deleted for " + user);
        }
     }
}
