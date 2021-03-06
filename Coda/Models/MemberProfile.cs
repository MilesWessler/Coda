﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coda.Controllers;
using ZipCodeCoords;

namespace Coda.Models
{
    public class MemberProfile
    {

        public int Id { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string UserName { get; set; }
        public string AboutMe { get; set; }
        [DisplayName("Zip Code")]
        public int ZipCode { get; set; }
        public double Latitude { get; set; } 
        public double Longitude { get; set; }
        public bool Rock { get; set; }
        [DisplayName("Classic Rock")]
        public bool ClassicRock { get; set; }
        [DisplayName("Punk Rock")]
        public bool PunkRock { get; set; }
        public bool Grunge { get; set; }
        public bool Metal { get; set; }
        public bool Blues { get; set; }
        [DisplayName("R&B")]
        public bool RAndB { get; set; }
        public bool Pop { get; set; }
        public bool Alternative { get; set; }
        [DisplayName("Would you like to connect with other members?")]
        public bool ConnectWithOtherMembers { get; set; }
        public byte[] Image { get; set; }
    }
}
