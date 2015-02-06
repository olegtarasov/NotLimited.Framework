using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NotLimited.Framework.Data.Entity
{
    public static class DatabaseHelpers
    {
        #region Drop tables script

        private const string DropTablesScript = @"declare @SQL nvarchar(max)

SELECT @SQL = STUFF((SELECT '; ALTER TABLE ' + 
							'[' + s.name + '].[' + t.name + ']' +
							' DROP CONSTRAINT [' + f.name +']'
					FROM sys.foreign_keys f
					INNER JOIN sys.TABLES t ON f.parent_object_id=t.object_id
					INNER JOIN sys.schemas s ON t.schema_id=s.schema_id
					WHERE t.is_ms_shipped=0
					FOR XML PATH('')), 1, 2, '');

--print @SQL;
 
execute (@SQL);
 
SELECT @SQL = STUFF((SELECT '; DROP TABLE ' + '[' + s.name + '].[' + t.name + ']'      
					 FROM sys.TABLES t
					 INNER JOIN sys.schemas s ON t.schema_id=s.schema_id
					 WHERE t.is_ms_shipped=0
					 FOR XML PATH('')), 1, 2, '');

--print @SQL;
execute (@SQL);";

        #endregion

        public static int DropTables(this Database database)
        {
            return database.ExecuteSqlCommand(DropTablesScript);
        }

        private static readonly Type _genericEnum = typeof(IEnumerable<>);

        public static IQueryable<T> IncludeVirtualTree<T>(this IQueryable<T> source)
        {
            return source.IncludeVirtualTree(typeof(T), new HashSet<Type>(), "");
        }

        private static IQueryable<T> IncludeVirtualTree<T>(this IQueryable<T> source, Type type, HashSet<Type> visited, string path)
        {
            var result = source;
            var props = type.GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(x =>
            {
                var getter = x.GetAccessors().First();
                return getter.IsVirtual && !getter.IsAbstract;
            });

            visited.Add(type);

            foreach (var prop in props)
            {
                string propPath = string.IsNullOrEmpty(path) ? prop.Name : path + "." + prop.Name;
                result = result.Include(propPath);

                var propType = GetPropertyType(prop);
                if (visited.Contains(propType))
                    continue;

                result = result.IncludeVirtualTree(propType, visited, propPath);
            }

            return result;
        }

        private static Type GetPropertyType(this PropertyInfo prop)
        {
            var type = prop.PropertyType;
            if (!type.IsGenericType)
                return type;

            var innerType = type.GenericTypeArguments[0];
            var enumerable = _genericEnum.MakeGenericType(innerType);
            if (enumerable.IsAssignableFrom(type))
                return innerType;

            throw new InvalidOperationException("Not enumerable generic type!");
        }
    }
}
