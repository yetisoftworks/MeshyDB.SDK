// <copyright file="CustomDateTimeOffsetSerializer.cs" company="Yeti Softworks LLC">
// Copyright (c) Yeti Softworks LLC. All rights reserved.
// </copyright>

using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace MeshyDB.SDK.Resolvers
{
    /// <summary>
    /// Class resolving serialization of DateTimeOffet for a Bson document.
    /// </summary>
    internal class CustomDateTimeOffsetSerializer : DateTimeOffsetSerializer
    {
        private static bool isAdded = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomDateTimeOffsetSerializer"/> class.
        /// </summary>
        internal CustomDateTimeOffsetSerializer()
            : base(BsonType.String)
        {
        }

        /// <summary>
        /// Registers Serializer for use with BsonSerializer.
        /// </summary>
        internal static void AddSeralizer()
        {
            if (!isAdded)
            {
                BsonSerializer.RegisterSerializer(new CustomDateTimeOffsetSerializer());
                isAdded = true;
            }
        }
    }
}
