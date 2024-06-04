using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordlessDemo.Infrastructure.Database.Converters
{
    public sealed class GuidConverter : IPropertyConverter
    {
        public object FromEntry(DynamoDBEntry entry)
        {
            string entryAsString = entry?.AsString();

            return entryAsString == null ? null : Guid.Parse(entryAsString);
        }

        public DynamoDBEntry ToEntry(object value)
        {
            return value.ToString();
        }
    }
}
