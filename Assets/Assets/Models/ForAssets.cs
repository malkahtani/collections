using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using System.Linq;
using System.Web;

namespace Assets.Models
{
    public class ForAssets
    {
        public Asset myassets { get; set; }
        public List<User> myuser { get; set; }
        public List<AssetType> mytype { get; set; }
        public List<AssetLocation> mylocation { get; set; }
    }
}