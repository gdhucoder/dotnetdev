using Dapper;
using Dapper.Contrib.Extensions;
using System;

namespace NLayerWebApiDemo.Model
{
    [Table("Cats")]
    public class Cats
    {

        public int id { get; set; }

        public String Name { get; set; }

        public DateTime Birthday { get; set; }
    }

}
