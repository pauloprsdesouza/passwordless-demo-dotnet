using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordlessDemo.Infrastructure.Database.Converters
{
    public sealed class EnumConverter<TEnum> : IPropertyConverter where TEnum : Enum
    {
        public object FromEntry(DynamoDBEntry entry)
        {
            string entryAsString = entry?.AsString();

            return entryAsString == null ? null : (TEnum)Enum.Parse(typeof(TEnum), entryAsString, ignoreCase: true);
        }

        public DynamoDBEntry ToEntry(object value)
        {
            return ((TEnum)value).ToString();
        }
    }
}
