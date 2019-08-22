using System;
using System.Runtime.Serialization;
using ZLibrary.Model;

namespace ZLibrary.Web.Controllers.Items
{
    public class SearchParametersDto
    {
        [DataMember(Name = "keyword")]
        public string Keyword { get; set; }

        [DataMember(Name = "orderByValue")]
        public long OrderByValue { get; set; }
    }
}