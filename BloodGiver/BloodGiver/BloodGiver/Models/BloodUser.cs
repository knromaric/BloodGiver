using System;

namespace BloodGiver.Models
{
    public class BloodUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public string BloodGroup { get; set; }
        public string ImagePath { get; set; }
        public string FullLogoPath
        {
            get
            {
                if (string.IsNullOrEmpty(ImagePath))
                {
                    return string.Empty; 
                }

                return String.Format("http://bloodgiver.gear.host/{0}", ImagePath.Substring(1));
            }
        }
        public int Date { get; set; }
        public object ImageArray { get; set; }
    }
}
