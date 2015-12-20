using JieYiGuang.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JieYiGuang.Web.Models
{
    public class IndexViewModel
    {
        public List<T_News> Banners { get; set; }
        public List<T_News> NewsProduct { get; set; }
        public List<T_ClassNews> Products { get; set; }
        public T_News About { get; set; }
    }
}