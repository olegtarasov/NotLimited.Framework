using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
    }
}
