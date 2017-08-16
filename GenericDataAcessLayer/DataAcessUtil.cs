using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace GenericDataAcessLayer
{
    public class DataAcessUtil
    {
        public IEnumerable<T> Search<T>(object config)
            where T : new()
        {
            string tableName = typeof(T).GetTypeInfo().GetCustomAttribute<DatabaseTableName>().Name;



            return new List<T>();
        }

        public bool Update<T>(T obj) where T : new()
        {
            string spName = typeof(T).Name + "_" + nameof(Update);

            using (var connection = new SqlConnection(""))
            using (var cmd = new SqlCommand())
            {
                cmd.CommandText = spName;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;
                foreach (SqlParameter item in GetDatabaseProperties<T, DatabaseUpdate>(obj))
                {
                    cmd.Parameters.Add(item);
                }
                cmd.CommandTimeout = 300;
                connection.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException sqlError)
                {
                    return false;
                }
                return true;
            }
        }

        private IEnumerable<SqlParameter> GetDatabaseProperties<T1, T2>(T1 obj) where T1 : new() where T2 : DatabaseEvent
        {
            ICollection<SqlParameter> properties = new List<SqlParameter>();

            foreach (PropertyInfo property in typeof(T1).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => (((T2)p.GetCustomAttribute(typeof(T2))).VisibilityTargets & (DatabaseVisibility.Optional | DatabaseVisibility.Required)) != 0))
            {
                object value = property.GetValue(obj, null);
                Attribute databaseSize = property.GetCustomAttribute(typeof(DatabaseSize), true);
                int? size = null;
                if (databaseSize != null)
                {
                    size = ((DatabaseSize)databaseSize).Size;
                }
                if (((T2)property.GetCustomAttribute(typeof(T2), true)).VisibilityTargets == DatabaseVisibility.Optional)
                {
                    if (IsDefault(property, value) == false)
                    {
                        properties.Add(ConstructSqlParameter(property.Name, value, property.PropertyType, size));
                    }
                }
                else
                {
                    properties.Add(ConstructSqlParameter(property.Name, value, property.PropertyType, size));
                }
            }
            return properties;
        }

        private bool IsDefault(PropertyInfo property, object val)
        {
            object defaultValue = GetDefaultValue(property.PropertyType);

            if (property.PropertyType.Name == "Boolean" && val != null)
            {
                return false;
            }

            if (defaultValue == null)
            {
                return val == null;
            }

            return defaultValue.Equals(val);
        }

        private object GetDefaultValue(Type propertyType)
        {
            if (propertyType.GetTypeInfo().IsValueType)
            {
                return Activator.CreateInstance(propertyType);
            }

            return null;
        }

        private SqlParameter ConstructSqlParameter(string name, object value, Type propertyType, int? size)
        {
            bool isEnum = propertyType.GetTypeInfo().IsEnum;
            bool isNullable = propertyType.GetTypeInfo().IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>);
            if (isEnum)
            {
                value = Convert.ChangeType(value, Enum.GetUnderlyingType(propertyType));
            }
            SqlParameter parameter;
            if (isEnum)
            {
                parameter = new SqlParameter(name, value) { DbType = GetDBType(Enum.GetUnderlyingType(propertyType)) };
            }
            else if (isNullable)
            {
                parameter = new SqlParameter(name, value) { DbType = GetDBType(Nullable.GetUnderlyingType(propertyType)) };
            }
            else
            {
                parameter = new SqlParameter(name, value) { DbType = GetDBType(propertyType) };
            }

            if (size.HasValue)
            {
                parameter.Size = size.Value;
            }

            return parameter;
        }

        private DbType GetDBType(Type propertyType)
        {
            switch (propertyType.Name.ToLower())
            {
                case "guid":
                    return DbType.Guid;
                case "int32":
                    return DbType.Int32;
                case "string":
                    return DbType.String;
                case "datetime":
                    return DbType.DateTime;
                case "long":
                    return DbType.Currency;
                case "decimal":
                    return DbType.Currency;
                case "boolean":
                    return DbType.Boolean;
                case "byte[]":
                    return DbType.Binary;
                default:
                    throw new Exception("Property Type has not been handled!");
            }
        }
    }
}
