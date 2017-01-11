using System;
using System.Collections.Generic;
using System.Text;
using SQLite.Net.Attributes;

namespace ProjectMato.iOS.Model
{
    [Table("SettingTable")]

    public class SettingTable : BaseTable
    {
        [PrimaryKey, AutoIncrement]
        public int SettingId { get; set; }

        public static class Properties
        {
            public const string SettingId = "SettingId";

            public const string Name = "Name";
        }

        public SettingTable()
        {
            Value = false;
            StrValue = "未定义";
        }

        public SettingTable(string name, string key, bool value):this()
        {
            Name = name;
            Key = key;
            Value = value;

        }
        public SettingTable(string name, string key, string strValue):this()
        {
            Name = name;
            Key = key;
            StrValue = strValue;

        }
        public string Name { get; set; }

        public bool Value { get; set; }

        public string Key { get; set; }

        public string StrValue { get; set; }

        public bool IsHidden { get; set; }

    }
}
