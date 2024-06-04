using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using System;
using System.Globalization;

namespace PasswordlessDemo.Infrastructure.Database.Converters
{
    public class DateTimeOffsetConverter : IPropertyConverter
    {
        public object FromEntry(DynamoDBEntry entry)
        {
            string entryAsIso8601 = entry?.AsString();

            return entryAsIso8601 == null ? null : DateTimeOffset.Parse(entryAsIso8601, null, DateTimeStyles.RoundtripKind);
        }

        public DynamoDBEntry ToEntry(object value)
        {
            return ((DateTimeOffset)value).ToString("o");
        }
    }
}
