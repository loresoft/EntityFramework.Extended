﻿using EntityFramework.Mapping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace EntityFramework.Batch
{
    public class EntityDataReader<TEntity> : IDataReader where TEntity : class
    {
        private IEnumerable<TEntity> _entities;
        private IEnumerator<TEntity> _entityEnumerator;
        private EntityMap _entityMap;
        private IDictionary<string, int> _columnNameIndex;
        private PropertyInfo[] _propInfos;
        private object[] _values;
        private TEntity _current;

        public EntityDataReader(IEnumerable<TEntity> entities, EntityMap entityMap)
        {
            if (entities == null)
                throw new ArgumentNullException("entities");
            _entities = entities;
            _entityEnumerator = _entities.GetEnumerator();
            _entityEnumerator.Reset();

            if (entityMap == null)
                throw new ArgumentNullException("entityMap");
            _entityMap = entityMap;
            
            _values = new object[FieldCount];
            _columnNameIndex = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            Type tEntity = typeof(TEntity), tDec = typeof(decimal), tNDec = typeof(decimal?);
            _propInfos = new PropertyInfo[FieldCount];
            for (int idx = _entityMap.PropertyMaps.Count-1, i = 0; idx >= 0; idx--, i++)
            {
                var propMap = _entityMap.PropertyMaps[idx];
                _columnNameIndex[propMap.ColumnName] = i;
                if (propMap.AutoGeneratedColumn) continue;
                _propInfos[i] = tEntity.GetProperty(propMap.PropertyName);
            }

        }

        public object this[string name]
        {
            get
            {
                if (!_columnNameIndex.ContainsKey(name)) throw new IndexOutOfRangeException("No column named '" + name + "'");
                return _values[_columnNameIndex[name]];
            }
        }

        public object this[int i]
        {
            get
            {
                return _values[i];
            }
        }

        public int Depth
        {
            get
            {
                return 0;
            }
        }

        public int FieldCount
        {
            get
            {
                return _entityMap.PropertyMaps.Count;
            }
        }

        public bool IsClosed
        {
            get
            {
                return _entities == null;
            }
        }

        public int RecordsAffected
        {
            get
            {
                return -1;
            }
        }

        public void Close()
        {
            Dispose();
        }

        public void Dispose()
        {
            _entities = null;
            _entityEnumerator = null;
            _entityMap = null;
            _columnNameIndex = null;
            _values = null;
            _current = null;
        }

        public bool GetBoolean(int i)
        {
            return (bool)_values[i];
        }

        public byte GetByte(int i)
        {
            return (byte)_values[i];
        }

        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public char GetChar(int i)
        {
            return (char)_values[i];
        }

        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public IDataReader GetData(int i)
        {
            if (i == 0) return this;
            return null;
        }

        public string GetDataTypeName(int i)
        {
            throw new NotImplementedException();
        }

        public DateTime GetDateTime(int i)
        {
            return (DateTime)_values[i];
        }

        public decimal GetDecimal(int i)
        {
            return (decimal)_values[i];
        }

        public double GetDouble(int i)
        {
            return (double)_values[i];
        }

        public Type GetFieldType(int i)
        {
            return typeof(TEntity).GetProperty(_entityMap.PropertyMaps[i].PropertyName).PropertyType;
        }

        public float GetFloat(int i)
        {
            return (float)_values[i];
        }

        public Guid GetGuid(int i)
        {
            return (Guid)_values[i];
        }

        public short GetInt16(int i)
        {
            return (short)_values[i];
        }

        public int GetInt32(int i)
        {
            return (int)_values[i];
        }

        public long GetInt64(int i)
        {
            return (long)_values[i];
        }

        public string GetName(int i)
        {
            return _entityMap.PropertyMaps[i].ColumnName;
        }

        public int GetOrdinal(string name)
        {
            if (!_columnNameIndex.ContainsKey(name)) throw new IndexOutOfRangeException("No column named '" + name + "'");
            return _columnNameIndex[name];
        }

        public DataTable GetSchemaTable()
        {
            throw new NotImplementedException();
        }

        public string GetString(int i)
        {
            return (string)_values[i];
        }

        public object GetValue(int i)
        {
            return _values[i];
        }

        public int GetValues(object[] values)
        {
            Array.Copy(_values, values, _values.Length);
            return _values.Length;
        }

        public bool IsDBNull(int i)
        {
            return _values[i] == null;
        }

        public bool NextResult()
        {
            return false;
        }

        public bool Read()
        {
            bool isNext = _entityEnumerator.MoveNext();
            _current = isNext ? _entityEnumerator.Current : null;
            if (_current != null) for (int i=0; i<_propInfos.Length; i++) if (_propInfos[i] != null) _values[i] = _propInfos[i].GetValue(_current);
            return isNext;
        }
    }
}
