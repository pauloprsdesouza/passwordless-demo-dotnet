using Amazon.DynamoDBv2.DataModel;
using PasswordlessDemo.Infrastructure.Database.Converters;
using System;

namespace PasswordlessDemo.Infrastructure.Database.Datamodel.BaseModels
{
    [DynamoDBTable("passwordless-demo")]
    public class DynamoBaseModel
    {
        [DynamoDBHashKey]
        public string PK { get; set; }

        [DynamoDBRangeKey]
        public string SK { get; set; }

        [DynamoDBProperty(typeof(GuidConverter))]
        public Guid Id { get; set; }

        [DynamoDBProperty(typeof(DateTimeOffsetConverter))]
        public DateTimeOffset CreatedAt { get; set; }

        [DynamoDBProperty(typeof(DateTimeOffsetConverter))]
        public DateTimeOffset? UpdatedAt { get; set; }

        public void InitializePK(string pkIdentifier)
        {
            var derivedTypeName = GetType().Name;
            PK = $"{derivedTypeName}#{pkIdentifier}";
        }
    }
}
