using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hsp.Demo.Web.Models
{
    public class ViewModels
    {
    }

    public class SourceClass
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string KeyId { get; set; }
    }

    public class DestinationClass
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Guid KeyId { get; set; } = Guid.NewGuid();
    }




}