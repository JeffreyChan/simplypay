using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace SimplePayment.Repository
{
    public static class DbContextExtension
    {
        public static IEnumerable<string> KeysFor(this DbContext context, Type entityType)
        {
            entityType = ObjectContext.GetObjectType(entityType);

            var metadataWorkspace =
                ((IObjectContextAdapter)context).ObjectContext.MetadataWorkspace;
            var objectItemCollection =
                (ObjectItemCollection)metadataWorkspace.GetItemCollection(DataSpace.OSpace);

            var ospaceType = metadataWorkspace
                .GetItems<EntityType>(DataSpace.OSpace)
                .SingleOrDefault(t => objectItemCollection.GetClrType(t) == entityType);

            if (ospaceType == null)
            {
                throw new ArgumentException(
                    string.Format(
                        "The type '{0}' is not mapped as an entity type.",
                        entityType.Name),
                    "entityType");
            }

            return ospaceType.KeyMembers.Select(k => k.Name);
        }

        public static object[] KeyValuesFor(this DbContext context, object entity)
        {
            var entry = context.Entry(entity);
            return context.KeysFor(entity.GetType())
                .Select(k => entry.Property(k).CurrentValue)
                .ToArray();
        }
    }
}
