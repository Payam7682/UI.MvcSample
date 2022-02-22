using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.MvcSample.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UI.MvcSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataOperationController : ControllerBase
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly string databaseName = "\\database.json";

        public DataOperationController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: api/<DataOperationController>
        [HttpGet]
        public async Task<List<UserModel>> GetAsync()
        {
            // DEFINE THE PATH WHERE WE WANT TO SAVE THE FILES.
            string sPath = _hostingEnvironment.WebRootPath + "\\Data";

            List<UserModel> userModels = new List<UserModel>();
            string database = await System.IO.File.ReadAllTextAsync(sPath + databaseName);
            userModels = JsonConvert.DeserializeObject<List<UserModel>>(database);

            if (userModels == null) userModels = new List<UserModel>();

            return userModels;
        }

        // POST api/<DataOperationController>
        [HttpPost]
        public async Task PostAsync([FromBody] UserModel model)
        {
            // DEFINE THE PATH WHERE WE WANT TO SAVE THE FILES.
            string sPath = _hostingEnvironment.WebRootPath + "\\Data";

            if (!System.IO.Directory.Exists(sPath))
                System.IO.Directory.CreateDirectory(sPath);

            if (!System.IO.File.Exists(sPath + databaseName))
                System.IO.File.Create(sPath + databaseName);

            List<UserModel> userModels = new List<UserModel>();
            string database = await System.IO.File.ReadAllTextAsync(sPath + databaseName);
            userModels = JsonConvert.DeserializeObject<List<UserModel>>(database);

            if (userModels == null) userModels = new List<UserModel>();

            userModels.Add(model);

            await System.IO.File.WriteAllTextAsync(sPath + databaseName, JsonConvert.SerializeObject(userModels));
        }
    }
}
