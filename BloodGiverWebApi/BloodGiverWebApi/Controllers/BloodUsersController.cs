using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Http;
using BloodGiverWebApi.Helpers;
using BloodGiverWebApi.Models;

namespace BloodGiverWebApi.Controllers
{
    [Authorize]
    public class BloodUsersController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public IHttpActionResult Post([FromBody]BloodUser bloodUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stream = new MemoryStream(bloodUser.ImageArray);
            var guid = Guid.NewGuid().ToString();

            var file = string.Format("{0}.jpg", guid);
            var folder = "~/Content/Users";
            var fullPath = String.Format("{0}/{1}", folder, file);
            var isFileUploaded = FileHelper.UploadPhoto(stream, folder, file);
            if (isFileUploaded)
            {
                bloodUser.ImagePath = fullPath;
            }

            var user = new BloodUser()
            {
                UserName = bloodUser.UserName,
                BloodGroup = bloodUser.BloodGroup,
                Email = bloodUser.Email,
                Phone = bloodUser.Phone,
                Country = bloodUser.Country,
                Date = bloodUser.Date,
                ImagePath = bloodUser.ImagePath
            };

            db.BloodUsers.Add(user);
            db.SaveChanges();
           
            return StatusCode(HttpStatusCode.Created);
        }
        

        public IEnumerable<BloodUser> Get(string bloodGroup, string country)
        {
            return db.BloodUsers.Where(u => u.BloodGroup.StartsWith(bloodGroup) && u.Country.StartsWith(country)).ToList();
        }

        public IEnumerable<BloodUser> Get()
        {
            return db.BloodUsers.OrderByDescending(u => u.Date);
        }

    }
}