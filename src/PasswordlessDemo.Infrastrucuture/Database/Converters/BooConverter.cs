using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;

namespace PasswordlessDemo.Infrastructure.Database.Converters
{
    public class BoolConverter : IPropertyConverter
    {
        public object FromEntry(DynamoDBEntry entry)
        {
            return entry.AsDynamoDBBool().Value;
        }

        public DynamoDBEntry ToEntry(object value)
        {
            return new DynamoDBBool((bool)value);
        }
    }
}
