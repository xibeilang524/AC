﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AC.Base.Tables
{
    /// <summary>
    /// 设备字符属性。
    /// </summary>
    [Database.Table("设备字符属性", Database.TableCreateRuleOptions.Only, "")]
    public struct DevicePropertyString
    {
        /// <summary>
        /// 数据表名
        /// </summary>
        public const string TableName = "DevicePropertyString";

        /// <summary>
        /// 设备编号
        /// </summary>
        [Database.Column("设备编号", "", Database.DataTypeOptions.Int, true, true)]
        public const string DeviceId = "DeviceId";

        /// <summary>
        /// 设备属性类型
        /// </summary>
        [Database.Column("设备属性类型", "", 250, true, true)]
        public const string PropertyType = "PropertyType";

        /// <summary>
        /// 序号
        /// </summary>
        [Database.Column("序号", "", Database.DataTypeOptions.Int, true, true)]
        public const string SerialNumber = "SerialNumber";

        /// <summary>
        /// 字符串数值
        /// </summary>
        [Database.Column("字符串数值", "", 250, false, true)]
        public const string DataValue = "DataValue";
    }
}
