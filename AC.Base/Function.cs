using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace AC.Base
{
    /// <summary>
    /// 常用函数集
    /// </summary>
    public class Function : AC.Base.Drives.Function
    {
        /// <summary>
        /// 16进制字符串转换byte[]
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static byte[] HexToByteArray(string s)
        {
            byte[] buffer = new byte[s.Length / 2];
            for (int i = 0; i < s.Length; i += 2)
                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
            return buffer;
        }

        /// <summary>
        /// byte[]转换16进制字符串
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ByteArrayToHex(byte[] data)
        {
            StringBuilder sb = new StringBuilder(data.Length * 3);
            foreach (byte b in data)
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0'));
            return sb.ToString().Trim().ToUpper();
        }

        #region << 数字函数 >>

        /*
        /// <summary>
        /// 将传入的字符串里的数字文字转为数字，如：“三年”转为“3年”
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToNumericString(string value)
        {
            string strValue = value;
            strValue = Microsoft.VisualBasic.Strings.StrConv(strValue, Microsoft.VisualBasic.VbStrConv.SimplifiedChinese, 0);
            strValue = Microsoft.VisualBasic.Strings.StrConv(strValue, Microsoft.VisualBasic.VbStrConv.Narrow, 0);

            strValue = strValue.Replace("一百", "100");
            strValue = strValue.Replace("九十", "90");
            strValue = strValue.Replace("八十", "80");
            strValue = strValue.Replace("七十", "70");
            strValue = strValue.Replace("六十", "60");
            strValue = strValue.Replace("五十", "50");
            strValue = strValue.Replace("四十", "40");
            strValue = strValue.Replace("三十", "30");
            strValue = strValue.Replace("二十", "20");
            strValue = strValue.Replace("一十", "10");
            strValue = strValue.Replace("十", "10");
            strValue = strValue.Replace("一", "1");
            strValue = strValue.Replace("二", "2");
            strValue = strValue.Replace("三", "3");
            strValue = strValue.Replace("四", "4");
            strValue = strValue.Replace("五", "5");
            strValue = strValue.Replace("六", "6");
            strValue = strValue.Replace("七", "7");
            strValue = strValue.Replace("八", "8");
            strValue = strValue.Replace("九", "9");
            strValue = strValue.Replace("○", "0");
            strValue = strValue.Replace("壹", "1");
            strValue = strValue.Replace("贰", "2");
            strValue = strValue.Replace("叁", "3");
            strValue = strValue.Replace("肆", "4");
            strValue = strValue.Replace("伍", "5");
            strValue = strValue.Replace("陆", "6");
            strValue = strValue.Replace("柒", "7");
            strValue = strValue.Replace("捌", "8");
            strValue = strValue.Replace("玖", "9");
            strValue = strValue.Replace("零", "0");

            return strValue;
        }
        */

        /// <summary>
        /// 将数字显示在页面或控件上
        /// </summary>
        /// <param name="value">输入的数字</param>
        /// <returns></returns>
        public static string OutInt(object value)
        {
            return OutInt(value, null, "", null, "", "");
        }

        /// <summary>
        /// 将数字显示在页面或控件上
        /// </summary>
        /// <param name="value">输入的数字</param>
        /// <param name="replaceNull">如果输入为 DBNull 或“空”时返回的值</param>
        /// <returns></returns>
        public static string OutInt(object value, string replaceNull)
        {
            return OutInt(value, null, replaceNull, null, "", "");
        }

        /// <summary>
        /// 将数字加上前缀、后缀后，显示在页面或控件上。
        /// </summary>
        /// <param name="value">输入的数字</param>
        /// <param name="replaceNull">如果输入为 DBNull 或“空”时返回的值</param>
        /// <param name="prefix">前缀。如（“重量：234.56公斤”，其中“重量：”为前缀）</param>
        /// <param name="postfix">后缀。如（“重量：234.56公斤”，其中“公斤”为后缀）</param>
        /// <returns></returns>
        public static string OutInt(object value, string replaceNull, string prefix, string postfix)
        {
            return OutInt(value, null, replaceNull, null, prefix, postfix);
        }

        /// <summary>
        /// 将数字乘以一个倍率并加上前缀、后缀后，显示在页面或控件上。
        /// </summary>
        /// <param name="value">输入的数字</param>
        /// <param name="multiple">倍率，如果输入的 value 数字有效并且 multiple 不等于 0，将返回“value * multiple”的结果，如果不希望乘以倍率则可以传入 null。</param>
        /// <param name="replaceNull">如果输入为 DBNull 或“空”时返回的值</param>
        /// <param name="replaceValue">替换数值。如果 value 等于该值，则返回 replaceNull 设定的字符串（该比较发生在乘以倍率之后）。如果无需进行比较替换该参数可传入 null。</param>
        /// <param name="prefix">前缀。如（“重量：234公斤”，其中“重量：”为前缀）</param>
        /// <param name="postfix">后缀。如（“重量：234公斤”，其中“公斤”为后缀）</param>
        /// <returns>处理后的可用于显示在页面或控件上的字符串</returns>
        public static string OutInt(object value, int? multiple, string replaceNull, int? replaceValue, string prefix, string postfix)
        {
            try
            {
                if (value == System.DBNull.Value)
                {
                    return replaceNull;
                }
                else if (value == null)
                {
                    return replaceNull;
                }

                int intValue = Convert.ToInt32(value);

                if (multiple != null && multiple != 0)
                {
                    intValue = intValue * (int)multiple;
                }

                if (replaceValue != null)
                {
                    if (intValue == (decimal)replaceValue)
                    {
                        return replaceNull;                                             //替换数值
                    }
                }

                return prefix + intValue + postfix;
            }
            catch
            {
                return replaceNull;
            }
        }

        /// <summary>
        /// 将数字显示在页面或控件上
        /// </summary>
        /// <param name="value">输入的数字</param>
        /// <returns></returns>
        public static string OutDecimal(object value)
        {
            return OutDecimal(value, -1, null, "", null, "", "");
        }

        /// <summary>
        /// 指定数字保留的小数位数显示在页面或控件上（可作为百分比输出，如“98.94%”）
        /// </summary>
        /// <param name="value">输入的数字</param>
        /// <param name="decimalDigits">保留的小数位数。如果该值为“-1”则不处理小数位数，有多少位就显示多少位。</param>
        /// <returns></returns>
        public static string OutDecimal(object value, int decimalDigits)
        {
            return OutDecimal(value, decimalDigits, null, "", null, "", "");
        }

        /// <summary>
        /// 指定数字保留的小数位数显示在页面或控件上（可作为百分比输出，如“98.94%”）
        /// </summary>
        /// <param name="value">输入的数字</param>
        /// <param name="decimalDigits">保留的小数位数。如果该值为“-1”则不处理小数位数，有多少位就显示多少位。</param>
        /// <param name="replaceNull">如果输入为 DBNull 或“空”时返回的值</param>
        /// <returns></returns>
        public static string OutDecimal(object value, int decimalDigits, string replaceNull)
        {
            return OutDecimal(value, decimalDigits, null, replaceNull, null, "", "");
        }

        /// <summary>
        /// 指定数字保留的小数位数并加上前缀、后缀后，显示在页面或控件上（可作为百分比输出，如“98.94%”）
        /// </summary>
        /// <param name="value">输入的数字</param>
        /// <param name="decimalDigits">保留的小数位数。如果该值为“-1”则不处理小数位数，有多少位就显示多少位。</param>
        /// <param name="replaceNull">如果输入为 DBNull 或“空”时返回的值</param>
        /// <param name="prefix">前缀。如（“重量：234.56公斤”，其中“重量：”为前缀）</param>
        /// <param name="postfix">后缀。如（“重量：234.56公斤”，其中“公斤”为后缀）</param>
        /// <returns></returns>
        public static string OutDecimal(object value, int decimalDigits, string replaceNull, string prefix, string postfix)
        {
            return OutDecimal(value, decimalDigits, null, replaceNull, null, prefix, postfix);
        }

        /// <summary>
        /// 将数字乘以一个倍率并加上前缀、后缀截取小数位数后，显示在页面或控件上（可作为百分比输出，如“98.94%”）
        /// </summary>
        /// <param name="value">输入的数字</param>
        /// <param name="decimalDigits">保留的小数位数。如果该值为“-1”则不处理小数位数，有多少位就显示多少位。</param>
        /// <param name="multiple">倍率，如果输入的 value 数字有效并且 multiple 不等于 0，将返回“value * multiple”的结果，如果不希望乘以倍率则可以传入 null。</param>
        /// <param name="replaceNull">如果输入为 DBNull 或“空”时返回的值</param>
        /// <param name="replaceValue">替换数值。如果 value 等于该值，则返回 replaceNull 设定的字符串（该比较发生在乘以倍率之后）。如果无需进行比较替换该参数可传入 null。</param>
        /// <param name="prefix">前缀。如（“重量：234.56公斤”，其中“重量：”为前缀）</param>
        /// <param name="postfix">后缀。如（“重量：234.56公斤”，其中“公斤”为后缀）</param>
        /// <returns>处理后的可用于显示在页面或控件上的字符串</returns>
        public static string OutDecimal(object value, int decimalDigits, decimal? multiple, string replaceNull, decimal? replaceValue, string prefix, string postfix)
        {
            try
            {
                if (value == System.DBNull.Value)
                {
                    return replaceNull;
                }
                else if (value == null)
                {
                    return replaceNull;
                }

                decimal decValue = Convert.ToDecimal(value);

                if (multiple != null && multiple != 0)
                {
                    decValue = decValue * (decimal)multiple;
                }

                if (replaceValue != null)
                {
                    if (decValue == (decimal)replaceValue)
                    {
                        return replaceNull;                                             //替换数值
                    }
                }

                if (decValue % 1 == 0)                                                  //没有小数位数
                {
                    return prefix + (long)decValue + postfix;
                }
                else
                {
                    if (decimalDigits < 0)
                    {
                        return prefix + decValue + postfix;
                    }
                    else
                    {
                        return prefix + Math.Round(decValue, decimalDigits) + postfix;
                    }
                }
            }
            catch
            {
                return replaceNull;
            }
        }

        #endregion

        #region << 数学运算函数 >>

        /// <summary>
        /// 获取一组数值的最大、最小、平均数等分析数据，values 的长度必须是 CurvePointOptions 中指定的长度之一。
        /// </summary>
        /// <param name="values">欲分析的数据数组。</param>
        /// <param name="upperUpperValue">上上限数值。该参数为 null 表示不分析 UpperUpperTimes 超上上限累计时间和 UpperUpperCount 超上上限次数。如果传入该参数则必须同时传入 upperValue 且必须大于 upperValue。</param>
        /// <param name="upperValue">上限数值。该参数为 null 表示不分析 UpperTimes 超上限累计时间和 UpperCount 超上限次数。</param>
        /// <param name="lowerValue">下限数值。该参数为 null 表示不分析 LowerTimes 超下限累计时间和 LowerCount 超下限次数。</param>
        /// <param name="lowerLowerValue">下下限数值。该参数为 null 表示不分析 LowerLowerTimes 超下下限累计时间和 LowerLowerCount 超下下限次数。如果传入该参数则必须同时传入 lowerValue 且必须大于 upperValue。</param>
        /// <returns></returns>
        public static DataAnalysisValue DataAnalysis(decimal?[] values, decimal? upperUpperValue, decimal? upperValue, decimal? lowerValue, decimal? lowerLowerValue)
        {
            if (upperUpperValue != null)
            {
                if (upperValue == null)
                {
                    throw new Exception("传入 upperUpperValue 则必须同时传入 upperValue。");
                }
                else if (upperUpperValue <= upperValue)
                {
                    throw new Exception("upperUpperValue 必须大于 upperValue。");
                }
            }
            if (lowerLowerValue != null)
            {
                if (lowerValue == null)
                {
                    throw new Exception("传入 lowerLowerValue 则必须同时传入 lowerValue。");
                }
                else if (lowerLowerValue >= lowerValue)
                {
                    throw new Exception("lowerLowerValue 必须小于 lowerValue。");
                }
            }

            foreach (int intEnumValue in Enum.GetValues(typeof(AC.Base.Drives.CurvePointOptions)))
            {
                AC.Base.Drives.CurvePointOptions _CurvePoint = (AC.Base.Drives.CurvePointOptions)intEnumValue;
                if (AC.Base.Drives.CurvePointExtensions.GetPointCount(_CurvePoint) == values.Length)
                {
                    decimal? decMaxValue = null;
                    int intMaxValueIndex = -1;

                    decimal? decMinValue = null;
                    int intMinValueIndex = -1;

                    decimal? decSumValue = null;
                    int intSumCount = 0;

                    int intUpperUpperTimes = 0;                     // 超上上限累计时间(秒)
                    int intUpperUpperCount = 0;                     // 超上上限次数(次)
                    int intUpperTimes = 0;                          // 超上限累计时间(秒)
                    int intUpperCount = 0;                          // 超上限次数(次)
                    int intLowerTimes = 0;                          // 超下限累计时间(秒)
                    int intLowerCount = 0;                          // 超下限次数(次)
                    int intLowerLowerTimes = 0;                     // 超下下限累计时间(秒)
                    int intLowerLowerCount = 0;                     // 超下下限次数(次)

                    for (int intIndex = 0; intIndex < values.Length; intIndex++)
                    {
                        if (values[intIndex] != null)
                        {
                            if (decMaxValue == null)
                            {
                                decMaxValue = values[intIndex];
                                intMaxValueIndex = intIndex;

                                decMinValue = values[intIndex];
                                intMinValueIndex = intIndex;

                                decSumValue = values[intIndex];
                                intSumCount++;
                            }
                            else
                            {
                                if (values[intIndex] > decMaxValue)
                                {
                                    decMaxValue = values[intIndex];
                                    intMaxValueIndex = intIndex;
                                }
                                else if (values[intIndex] < decMinValue)
                                {
                                    decMinValue = values[intIndex];
                                    intMinValueIndex = intIndex;
                                }

                                decSumValue += values[intIndex];
                                intSumCount++;
                            }

                            if (upperValue != null && values[intIndex] > upperValue)
                            {
                                intUpperCount++;
                                intUpperTimes += AC.Base.Drives.CurvePointExtensions.GetTimeSpan(_CurvePoint);

                                if (upperUpperValue != null && values[intIndex] > upperUpperValue)
                                {
                                    intUpperUpperCount++;
                                    intUpperUpperTimes += AC.Base.Drives.CurvePointExtensions.GetTimeSpan(_CurvePoint);
                                }
                            }

                            if (lowerValue != null && values[intIndex] < lowerValue)
                            {
                                intLowerCount++;
                                intLowerTimes += AC.Base.Drives.CurvePointExtensions.GetTimeSpan(_CurvePoint);

                                if (lowerLowerValue != null && values[intIndex] < lowerLowerValue)
                                {
                                    intLowerLowerCount++;
                                    intLowerLowerTimes += AC.Base.Drives.CurvePointExtensions.GetTimeSpan(_CurvePoint);
                                }
                            }
                        }
                    }

                    DataAnalysisValue _Value = new DataAnalysisValue();
                    if (intSumCount > 0)
                    {
                        _Value.MaxValue = decMaxValue;
                        _Value.MaxDateTime = Function.ToDateTime(10101, AC.Base.Drives.CurvePointExtensions.GetTimeNum(_CurvePoint, intMaxValueIndex));

                        _Value.MinValue = decMinValue;
                        _Value.MinDateTime = Function.ToDateTime(10101, AC.Base.Drives.CurvePointExtensions.GetTimeNum(_CurvePoint, intMinValueIndex));

                        _Value.AvgValue = decSumValue / (decimal)intSumCount;
                        _Value.MaxMinDiff = _Value.MaxValue - _Value.MinValue;
                        _Value.MaxMinDiffRatio = _Value.MaxMinDiff / _Value.MaxValue;
                        _Value.AvgRatio = _Value.AvgValue / _Value.MaxValue;

                        if (upperValue != null)
                        {
                            _Value.UpperCount = intUpperCount;
                            _Value.UpperTimes = intUpperTimes;

                            if (upperUpperValue != null)
                            {
                                _Value.UpperUpperCount = intUpperUpperCount;
                                _Value.UpperUpperTimes = intUpperUpperTimes;
                            }
                        }

                        if (lowerValue != null)
                        {
                            _Value.LowerCount = intLowerCount;
                            _Value.LowerTimes = intLowerTimes;

                            if (lowerLowerValue != null)
                            {
                                _Value.LowerLowerCount = intLowerLowerCount;
                                _Value.LowerLowerTimes = intLowerLowerTimes;
                            }
                        }
                    }
                    return _Value;
                }
            }

            throw new Exception("未发现 " + values.Length + " 个点的曲线类型。");
        }

        #endregion

        #region << 字符串函数 >>

        /*
        /// <summary>
        /// 字符串转换选项。
        /// </summary>
        public enum ToStringOptions
        {
            /// <summary>
            /// 从此实例的开始位置和末尾移除空白字符的所有匹配项。
            /// </summary>
            Trim = 1,

            /// <summary>
            /// 从此实例的末尾移除空白字符的所有匹配项。
            /// </summary>
            TrimEnd = 1 << 1,

            /// <summary>
            /// 从此实例的开始位置移除空白字符的所有匹配项。
            /// </summary>
            TrimStart = 1 << 2,

            /// <summary>
            /// 将繁体中文字符转换为简体中文字符。
            /// </summary>
            SimplifiedChinese = 1 << 3,

            /// <summary>
            /// 将简体中文字符转换为繁体中文字符。
            /// </summary>
            TraditionalChinese = 1 << 4,

            /// <summary>
            /// 将字符串中每个单词的第一个字母转换为大写。
            /// </summary>
            ProperCase = 1 << 5,

            /// <summary>
            /// 将字符串中的窄（半角）字符转换为宽（全角）字符。
            /// </summary>
            Wide = 1 << 6,

            /// <summary>
            /// 将字符串中的宽（全角）字符转换为窄（半角）字符。
            /// </summary>
            Narrow = 1 << 7,
        }

        /// <summary>
        /// 将输入的对象转为字符串，并按选项设定对字符串进行处理。
        /// </summary>
        /// <param name="value">欲转换的字符串。</param>
        /// <param name="options">转换选项。</param>
        /// <returns></returns>
        public static string ToString(object value, ToStringOptions options)
        {
            try
            {
                if (value == System.DBNull.Value)
                {
                    return "";
                }
                else if (value == null)
                {
                    return "";
                }
                else
                {
                    string strValue = Convert.ToString(value);

                    if ((options & ToStringOptions.Trim) == ToStringOptions.Trim)
                    {
                        strValue = strValue.Trim();
                    }

                    if ((options & ToStringOptions.TrimEnd) == ToStringOptions.TrimEnd)
                    {
                        strValue = strValue.TrimEnd();
                    }

                    if ((options & ToStringOptions.TrimStart) == ToStringOptions.TrimStart)
                    {
                        strValue = strValue.TrimStart();
                    }

                    if ((options & ToStringOptions.SimplifiedChinese) == ToStringOptions.SimplifiedChinese)
                    {
                        strValue = Microsoft.VisualBasic.Strings.StrConv(strValue, Microsoft.VisualBasic.VbStrConv.SimplifiedChinese, 0);
                    }

                    if ((options & ToStringOptions.TraditionalChinese) == ToStringOptions.TraditionalChinese)
                    {
                        strValue = Microsoft.VisualBasic.Strings.StrConv(strValue, Microsoft.VisualBasic.VbStrConv.TraditionalChinese, 0);
                    }

                    if ((options & ToStringOptions.ProperCase) == ToStringOptions.ProperCase)
                    {
                        strValue = Microsoft.VisualBasic.Strings.StrConv(strValue, Microsoft.VisualBasic.VbStrConv.ProperCase, 0);
                    }

                    if ((options & ToStringOptions.Wide) == ToStringOptions.Wide)
                    {
                        strValue = Microsoft.VisualBasic.Strings.StrConv(strValue, Microsoft.VisualBasic.VbStrConv.Wide, 0);
                    }

                    if ((options & ToStringOptions.Narrow) == ToStringOptions.Narrow)
                    {
                        strValue = Microsoft.VisualBasic.Strings.StrConv(strValue, Microsoft.VisualBasic.VbStrConv.Narrow, 0);
                    }

                    return strValue;
                }
            }
            catch
            {
                return "";
            }
        }
        */

        /// <summary>
        /// 将输入的对象转为字符串。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToString(object value)
        {
            try
            {
                if (value == System.DBNull.Value)
                {
                    return "";
                }
                else if (value == null)
                {
                    return "";
                }
                else
                {
                    return Convert.ToString(value);
                }
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 确保将传入的对象转为字符串，并除去两头空格。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToTrim(object value)
        {
            try
            {
                if (value == System.DBNull.Value)
                {
                    return "";
                }
                else if (value == null)
                {
                    return "";
                }
                else
                {
                    return Convert.ToString(value).Trim();
                }
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 查找指定的字符串在此字符串中的第 findIndex 个匹配项的索引。
        /// </summary>
        /// <param name="value">在此字符串中进行查找</param>
        /// <param name="findValue">欲查找的字符串</param>
        /// <param name="findIndex">查找第几个匹配项。该值应该 >=1</param>
        /// <param name="startIndex">开始查找的索引，该值通常为 0，如果等于“-1”表示使用该默认值</param>
        /// <param name="comparisonType">用于指定字符串比较是使用当前区域还是固定区域、使用字词还是序号排序规则以及是否区分大小写</param>
        /// <returns></returns>
        public static int IndexOf(string value, string findValue, int findIndex, int startIndex, StringComparison comparisonType)
        {
            if (startIndex == -1)
            {
                startIndex = 0;
            }
            return IndexOf(value, findValue, findIndex, 0, startIndex, comparisonType);
        }

        private static int IndexOf(string value, string findValue, int findIndex, int currentFindIndex, int startIndex, StringComparison comparisonType)
        {
            int intIndexOf = value.IndexOf(findValue, startIndex, comparisonType);
            if (intIndexOf > -1)
            {
                currentFindIndex++;

                if (currentFindIndex >= findIndex)
                {
                    return intIndexOf;
                }
                else
                {
                    return IndexOf(value, findValue, findIndex, currentFindIndex, intIndexOf + findValue.Length, comparisonType);
                }
            }
            else
            {
                return intIndexOf;
            }
        }

        /// <summary>
        /// 查找指定的字符串在此字符串中最后的第 findIndex 个匹配项的索引。
        /// </summary>
        /// <param name="value">在此字符串中进行查找</param>
        /// <param name="findValue">欲查找的字符串</param>
        /// <param name="findIndex">查找第几个匹配项。该值应该 >=1</param>
        /// <param name="startIndex">开始查找的索引，该值通常为 value.length，如果等于“-1”表示使用该默认值</param>
        /// <param name="comparisonType">用于指定字符串比较是使用当前区域还是固定区域、使用字词还是序号排序规则以及是否区分大小写</param>
        /// <returns></returns>
        public static int LastIndexOf(string value, string findValue, int findIndex, int startIndex, StringComparison comparisonType)
        {
            if (startIndex == -1)
            {
                startIndex = value.Length;
            }
            return LastIndexOf(value, findValue, findIndex, 0, startIndex, comparisonType);
        }

        private static int LastIndexOf(string value, string findValue, int findIndex, int currentFindIndex, int startIndex, StringComparison comparisonType)
        {
            int intIndexOf = value.LastIndexOf(findValue, startIndex, comparisonType);
            if (intIndexOf > -1)
            {
                currentFindIndex++;

                if (currentFindIndex >= findIndex)
                {
                    return intIndexOf;
                }
                else
                {
                    return LastIndexOf(value, findValue, findIndex, currentFindIndex, intIndexOf - 1, comparisonType);
                }
            }
            else
            {
                return intIndexOf;
            }
        }

        /// <summary>
        /// 字符串比较。如果2个字符串一样则返回“true”（包括一样的内容或者2个字符串都是0长度字符串或者都是 null 值。比较时将忽略字符串两头的空格），否则返回“false”
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public static bool StringEquals(object value1, object value2)
        {
            try
            {
                if ((value1 == System.DBNull.Value && value2 == System.DBNull.Value) ||
                    (value1 == System.DBNull.Value && value2 == null) ||
                    (value1 == null && value2 == System.DBNull.Value) ||
                    (value1 == null && value2 == null))
                {
                    return true;
                }
                else if (Convert.ToString(value1).Trim().Equals(Convert.ToString(value2).Trim()))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 计算传入的字符串的字节长度
        /// </summary>
        /// <param name="value">欲计算字节长度的字符串</param>
        /// <returns></returns>
        public static int GetByteLength(object value)
        {
            int intLength = 0;
            try
            {
                char[] charValue = Convert.ToString(value).ToCharArray();

                for (int intIndex = 0; intIndex < charValue.Length; intIndex++)
                {
                    byte[] bytChar = System.Text.Encoding.Default.GetBytes(charValue, intIndex, 1);
                    intLength += bytChar.Length;
                }
            }
            catch
            {
            }
            return intLength;
        }

        /// <summary>
        /// 将字符串中的窄字符（半角字符）转换为宽字符（全角字符）
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns></returns>
        public static string StringToWide(string value)
        {
            StringBuilder result = new StringBuilder(value.Length, value.Length);
            for (int i = 0; i < value.Length; i++)
            {
                if (value[i] == 32)
                {
                    result.Append('　');
                }
                else if (value[i] == 46)
                {
                    result.Append('。');
                }
                else if (value[i] >= 33 && value[i] <= 126)
                {
                    result.Append((char)(value[i] + 65248));
                }
                else
                {
                    result.Append(value[i]);
                }
            }
            return result.ToString();
        }

        /// <summary>
        /// 将字符串中的全角字符转换为半角字符
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns></returns>
        public static string StringToNarrow(string value)
        {
            StringBuilder result = new StringBuilder(value.Length, value.Length);
            for (int i = 0; i < value.Length; i++)
            {
                if (value[i] == 12288)
                {
                    result.Append(' ');
                }
                else if (value[i] == 12290)
                {
                    result.Append('.');
                }
                else if (value[i] >= 65281 && value[i] <= 65374)
                {
                    result.Append((char)(value[i] - 65248));
                }
                else
                {
                    result.Append(value[i]);
                }
            }
            return result.ToString();
        }

        /// <summary>
        /// 将输入的汉字转为汉字拼音首字母连接的字符串
        /// </summary>
        /// <param name="value">欲转成汉字拼音首字母的汉字字符串</param>
        /// <param name="isCapital">是否大写。true：大写；false：小写</param>
        /// <param name="notChineseDisplay">非中文汉字是否显示。true：显示；false：不显示</param>
        /// <returns></returns>
        public static string GetChineseSpell(object value, bool isCapital, bool notChineseDisplay)
        {
            string strSpell = "";
            try
            {
                char[] charValue = Convert.ToString(value).ToCharArray();

                for (int intIndex = 0; intIndex < charValue.Length; intIndex++)
                {
                    byte[] bytChar = System.Text.Encoding.Default.GetBytes(charValue, intIndex, 1);

                    if (bytChar.Length > 1)
                    {
                        int intChar0 = (short)bytChar[0];
                        int intChar1 = (short)bytChar[1];
                        int intCode = (intChar0 << 8) + intChar1;
                        int[] areacode = { 45217, 45253, 45761, 46318, 46826, 47010, 47297, 47614, 48119, 48119, 49062, 49324, 49896, 50371, 50614, 50622, 50906, 51387, 51446, 52218, 52698, 52698, 52698, 52980, 53689, 54481 };
                        for (int i = 0; i < 26; i++)
                        {
                            int max = 55290;
                            if (i != 25)
                            {
                                max = areacode[i + 1];
                            }

                            if (areacode[i] <= intCode && intCode < max)
                            {
                                if (isCapital)
                                {
                                    strSpell += Encoding.Default.GetString(new byte[] { (byte)(65 + i) });
                                }
                                else
                                {
                                    strSpell += Encoding.Default.GetString(new byte[] { (byte)(97 + i) });
                                }
                                break;
                            }
                        }
                    }
                    else
                    {
                        if (notChineseDisplay)
                        {
                            strSpell += charValue[intIndex];
                        }
                    }
                }
                return strSpell;
            }
            catch
            {
                return strSpell;
            }
        }

        /// <summary>
        /// 获取小写、显示非中文汉字的用空格分隔的中文全拼
        /// </summary>
        /// <param name="value">欲转成汉字拼音的汉字字符串</param>
        /// <returns></returns>
        public static string GetChineseFullSpell(object value)
        {
            return GetChineseFullSpell(value, false, true, " ");
        }

        /// <summary>
        /// 获取中文全拼
        /// </summary>
        /// <param name="value">欲转成汉字拼音的汉字字符串</param>
        /// <param name="isCapital">是否大写。true：大写；false：小写</param>
        /// <param name="notChineseDisplay">非中文汉字是否显示。true：显示；false：不显示</param>
        /// <param name="space">各汉字拼音之间的分隔符。默认没有分隔符，所有拼音都连在一起</param>
        /// <returns></returns>
        public static string GetChineseFullSpell(object value, bool isCapital, bool notChineseDisplay, string space)
        {
            #region 编码索引
            string[] _spellMusicCode = new string[] { "a", "ai", "an", "ang", "ao", "ba", "bai", "ban", "bang", "bao", "bei", "ben", "beng", "bi", "bian", "biao", "bie", "bin", "bing", "bo", "bu", "ca", "cai", "can", "cang", "cao", "ce", "ceng", "cha", "chai", "chan", "chang", "chao", "che", "chen", "cheng", "chi", "chong", "chou", "chu", "chuai", "chuan", "chuang", "chui", "chun", "chuo", "ci", "cong", "cou", "cu", "cuan", "cui", "cun", "cuo", "da", "dai", "dan", "dang", "dao", "de", "deng", "di", "dian", "diao", "die", "ding", "diu", "dong", "dou", "du", "duan", "dui", "dun", "duo", "e", "en", "er", "fa", "fan", "fang", "fei", "fen", "feng", "fu", "fou", "ga", "gai", "gan", "gang", "gao", "ge", "ji", "gen", "geng", "gong", "gou", "gu", "gua", "guai", "guan", "guang", "gui", "gun", "guo", "ha", "hai", "han", "hang", "hao", "he", "hei", "hen", "heng", "hong", "hou", "hu", "hua", "huai", "huan", "huang", "hui", "hun", "huo", "jia", "jian", "jiang", "qiao", "jiao", "jie", "jin", "jing", "jiong", "jiu", "ju", "juan", "jue", "jun", "ka", "kai", "kan", "kang", "kao", "ke", "ken", "keng", "kong", "kou", "ku", "kua", "kuai", "kuan", "kuang", "kui", "kun", "kuo", "la", "lai", "lan", "lang", "lao", "le", "lei", "leng", "li", "lia", "lian", "liang", "liao", "lie", "lin", "ling", "liu", "long", "lou", "lu", "luan", "lue", "lun", "luo", "ma", "mai", "man", "mang", "mao", "me", "mei", "men", "meng", "mi", "mian", "miao", "mie", "min", "ming", "miu", "mo", "mou", "mu", "na", "nai", "nan", "nang", "nao", "ne", "nei", "nen", "neng", "ni", "nian", "niang", "niao", "nie", "nin", "ning", "niu", "nong", "nu", "nuan", "nue", "yao", "nuo", "o", "ou", "pa", "pai", "pan", "pang", "pao", "pei", "pen", "peng", "pi", "pian", "piao", "pie", "pin", "ping", "po", "pou", "pu", "qi", "qia", "qian", "qiang", "qie", "qin", "qing", "qiong", "qiu", "qu", "quan", "que", "qun", "ran", "rang", "rao", "re", "ren", "reng", "ri", "rong", "rou", "ru", "ruan", "rui", "run", "ruo", "sa", "sai", "san", "sang", "sao", "se", "sen", "seng", "sha", "shai", "shan", "shang", "shao", "she", "shen", "sheng", "shi", "shou", "shu", "shua", "shuai", "shuan", "shuang", "shui", "shun", "shuo", "si", "song", "sou", "su", "suan", "sui", "sun", "suo", "ta", "tai", "tan", "tang", "tao", "te", "teng", "ti", "tian", "tiao", "tie", "ting", "tong", "tou", "tu", "tuan", "tui", "tun", "tuo", "wa", "wai", "wan", "wang", "wei", "wen", "weng", "wo", "wu", "xi", "xia", "xian", "xiang", "xiao", "xie", "xin", "xing", "xiong", "xiu", "xu", "xuan", "xue", "xun", "ya", "yan", "yang", "ye", "yi", "yin", "ying", "yo", "yong", "you", "yu", "yuan", "yue", "yun", "za", "zai", "zan", "zang", "zao", "ze", "zei", "zen", "zeng", "zha", "zhai", "zhan", "zhang", "zhao", "zhe", "zhen", "zheng", "zhi", "zhong", "zhou", "zhu", "zhua", "zhuai", "zhuan", "zhuang", "zhui", "zhun", "zhuo", "zi", "zong", "zou", "zu", "zuan", "zui", "zun", "zuo", "", "ei", "m", "n", "dia", "cen", "nou", "jv", "qv", "xv", "lv", "nv" };

            //拼音索引表
            int[,] _spellCodeIndex = new int[,]{
				{354,279,331,0,190,39,284,67,249,167,353,19,133,92,129,152,31,385,105,131,84,348,348,329,133,0,376,116,283,349,344,87,133,0,0,0,284,202,0,0,0,0,0,0,342,345,0,276,199,243,0,0,52,0,375,102,88,176,170,348,136,180,375,0,354,294,299,94,344,368,344,55,347,333,131,347,353,369,166,74,325,92,0,329,6,19,0,306,0,332,372,114,107,20,33,370,279,258,79,191,243,328,220,83,43,324,62,376,229,354,64,73,329,348,336,92,2,349,232,282,84,305,389,357,134,332,343,336,363,332,217,208,335,340,377,389,13,232,348,294,348,374,107,181,378,21,250,14,371,47,346,369,14,332,250,284,10,121,110,333,91,78,194,19,113,123,102,251,47,337,282,320,106,157,348,101,349,189,340,3,175,77,178,29,37,343,377,263,125,294,56},
				{302,214,130,132,313,373,393,39,404,134,286,313,282,320,20,201,334,318,354,330,340,101,148,329,348,84,167,122,350,332,270,229,330,190,331,339,62,134,375,157,294,123,334,125,16,309,81,344,11,354,336,116,374,375,19,133,220,389,167,323,169,43,387,0,91,310,286,187,304,383,337,237,241,88,167,299,245,80,375,387,178,295,171,122,68,386,12,329,134,200,23,125,347,276,245,350,36,243,264,376,45,325,354,19,309,345,83,305,328,75,34,283,140,62,29,11,367,120,220,369,388,345,353,340,367,47,84,84,375,190,92,348,335,343,294,71,368,373,315,11,360,245,325,296,375,297,331,355,261,217,357,126,180,63,305,109,129,330,278,243,252,35,40,270,11,334,352,220,304,301,346,78,19,333,359,97,270,162,352,362,387,231,295,381,354,368,389,279,43,131,107},
				{370,247,349,62,335,174,11,234,130,166,175,182,243,332,304,350,68,392,278,127,132,318,392,240,160,32,101,241,308,381,325,92,20,121,45,312,278,220,82,332,354,75,170,20,97,332,332,193,347,130,242,127,83,377,2,269,348,137,216,304,348,58,150,0,125,40,128,276,359,24,18,3,303,59,30,158,348,130,243,188,329,214,248,208,32,169,404,152,10,354,16,360,375,294,353,109,247,246,164,308,325,173,40,31,333,311,335,164,179,360,221,305,345,179,202,360,338,265,265,316,32,208,208,130,345,284,0,0,0,0,282,69,0,0,120,329,265,167,354,107,310,0,92,348,184,254,345,184,367,132,98,132,184,377,96,340,353,140,348,189,284,188,134,147,193,84,335,189,303,89,116,226,84,193,242,125,296,186,316,97,131,247,68,88,349,2,164,25,194,384,52},
				{294,74,130,170,330,70,79,79,134,40,374,83,198,375,83,83,139,88,237,150,107,258,43,92,243,53,92,57,323,136,164,84,250,64,193,171,376,17,129,129,278,31,94,43,153,74,77,129,47,131,69,179,363,355,54,0,276,43,54,164,81,31,241,0,43,386,89,240,74,247,385,125,92,345,123,74,375,329,98,283,125,367,139,244,42,31,381,175,164,231,278,147,98,117,367,385,166,134,172,102,128,102,125,125,314,92,125,125,375,31,390,196,164,378,344,131,145,335,375,348,378,196,160,135,147,346,321,334,197,152,129,169,0,131,90,193,37,159,352,0,135,247,175,21,157,139,190,68,340,329,348,343,346,283,168,175,234,284,92,244,128,251,333,348,342,79,135,314,134,57,181,343,343,404,164,34,333,251,357,133,10,0,14,87,87,348,231,134,220,91,75},
				{95,55,133,95,203,133,348,126,141,116,250,79,245,361,152,116,354,102,121,57,153,166,166,298,70,133,250,330,143,345,250,343,209,389,284,330,323,323,335,278,92,288,152,330,335,254,346,248,280,37,340,330,153,92,75,330,270,345,372,107,344,62,368,0,227,0,245,344,375,183,309,281,313,391,81,363,355,164,63,246,5,102,345,164,32,243,164,345,345,355,114,190,262,250,0,162,70,381,282,282,282,348,56,285,136,6,267,284,385,331,334,92,357,265,48,98,249,37,0,114,192,183,341,386,74,347,223,123,348,39,246,335,239,117,114,354,221,342,263,223,90,137,404,91,326,241,329,128,357,37,283,319,326,75,284,135,230,326,197,254,62,284,377,312,330,348,241,237,386,340,348,334,344,250,332,320,239,17,84,346,123,303,55,321,393,21,302,332,123,241,77},
				{75,375,378,343,334,149,284,311,397,341,322,160,314,338,353,189,209,337,0,109,357,173,239,183,389,110,332,160,238,167,6,192,404,299,84,107,356,353,97,97,11,107,348,243,316,345,323,164,249,34,362,344,69,241,62,246,0,114,0,0,167,159,276,0,0,328,349,241,321,244,3,363,209,354,310,157,330,2,400,385,368,358,334,333,384,329,326,309,97,241,241,57,347,386,316,52,110,344,241,372,167,332,350,98,363,104,179,0,62,251,320,0,305,3,57,354,372,377,30,322,0,360,220,190,116,357,119,355,92,376,325,276,340,120,367,345,167,354,271,139,127,345,278,230,300,351,325,350,0,11,0,362,123,309,220,334,244,273,352,114,335,180,29,106,263,321,92,227,329,332,386,175,321,0,301,0,269,0,199,244,14,5,166,391,378,196,304,128,38,128,139},
				{304,282,2,334,174,334,116,121,104,340,332,363,32,238,60,180,116,86,305,347,269,128,334,117,181,254,334,334,92,153,334,109,396,127,340,320,304,343,329,392,79,139,121,24,43,358,57,354,319,330,241,109,166,340,121,349,343,220,294,345,350,55,369,0,385,216,356,335,127,355,2,352,325,354,230,59,336,73,58,336,232,349,391,214,62,158,302,328,331,347,74,339,377,368,130,32,343,185,375,175,121,228,353,220,349,351,186,114,372,164,172,106,202,196,345,164,175,173,196,57,35,236,232,333,185,330,74,148,345,31,350,0,156,302,334,46,119,123,381,372,334,367,164,31,30,164,348,179,360,297,0,365,125,358,378,158,212,0,0,121,349,213,317,141,355,132,233,357,48,121,75,104,314,316,104,253,260,104,303,104,349,122,107,178,104,44,325,355,251,148,240},
				{355,355,344,317,316,316,317,177,121,348,176,176,344,316,313,240,175,150,344,384,297,297,385,58,242,331,278,212,196,124,375,343,348,246,186,261,319,82,14,337,158,131,92,62,337,226,305,250,385,98,303,132,346,0,175,249,96,342,6,171,378,84,116,0,375,5,0,228,193,92,110,404,47,37,349,115,72,371,84,101,220,74,102,29,346,335,285,19,114,55,138,58,108,3,337,332,9,239,11,348,349,107,340,383,246,83,80,252,352,137,331,62,159,135,246,372,169,117,21,132,347,62,344,251,299,374,323,134,178,374,146,279,68,304,23,315,13,332,74,348,375,384,241,134,208,143,154,208,125,89,354,344,97,163,80,344,154,3,282,203,316,36,119,14,166,328,381,10,354,329,129,264,347,94,387,354,120,75,220,84,92,186,279,320,349,83,376,129,130,113,89},
				{45,332,237,162,337,120,71,323,341,330,92,150,350,352,139,297,284,189,327,36,316,252,376,164,231,9,361,72,373,329,374,343,373,2,95,345,140,326,335,172,106,159,279,231,13,35,175,175,223,243,186,196,381,290,174,16,27,375,370,140,63,35,385,0,92,104,244,246,62,52,345,302,366,243,325,383,127,278,278,81,240,318,317,79,252,73,392,62,283,121,74,304,61,329,82,120,304,55,347,378,125,354,244,127,144,348,232,63,126,347,342,304,158,134,118,58,255,243,343,332,330,2,344,59,264,130,404,152,175,345,304,325,121,173,173,265,164,170,31,343,345,162,6,323,270,382,348,181,378,382,116,154,116,340,154,285,183,73,285,348,350,126,227,368,15,299,253,171,54,341,335,203,331,355,323,375,247,0,96,241,188,188,35,363,136,90,306,303,345,14,0},
				{357,335,6,76,309,228,135,335,63,346,119,139,367,12,120,81,64,343,145,5,327,303,329,5,126,166,74,357,126,284,82,123,14,176,74,313,243,133,218,29,332,79,92,114,57,353,131,129,326,376,224,145,376,357,232,37,356,382,345,201,336,82,83,0,355,70,20,385,371,208,355,315,341,375,75,196,14,282,75,110,340,78,374,193,8,84,171,386,254,332,346,182,386,134,258,68,92,87,333,123,135,96,125,125,209,375,373,332,113,101,343,149,345,194,169,229,344,353,345,332,309,321,251,295,114,332,280,283,335,183,84,329,143,157,50,313,337,201,354,238,299,375,107,382,237,318,332,354,345,329,330,345,354,294,164,134,383,241,332,385,68,175,75,75,404,190,48,239,134,23,171,334,286,241,121,84,328,328,304,81,81,310,208,251,131,131,243,63,337,116,323},
				{157,349,377,203,84,178,0,154,349,344,134,164,63,332,117,117,346,220,201,267,124,354,354,325,309,262,186,57,264,247,121,328,243,45,191,84,129,71,348,376,120,190,345,350,341,129,325,374,249,375,335,320,166,184,254,294,233,325,321,50,116,245,10,0,340,354,102,388,220,355,261,263,172,186,226,357,180,153,282,373,355,301,261,194,350,297,211,306,227,159,203,10,232,348,354,341,355,348,141,352,208,391,130,381,363,201,160,372,116,116,5,264,180,235,97,329,127,320,369,191,332,332,196,168,166,117,102,61,375,340,348,117,330,153,220,345,31,128,186,84,79,345,348,111,128,84,284,299,244,166,341,336,211,68,348,24,2,210,214,180,311,39,130,47,354,236,261,263,208,345,303,350,243,356,190,14,282,337,208,70,172,355,158,345,171,128,255,158,332,350,290},
				{335,251,189,164,176,345,378,158,136,386,336,294,303,353,381,343,192,48,323,344,348,386,164,342,20,200,212,350,176,261,248,316,262,220,348,131,384,378,114,284,348,311,330,36,253,95,334,367,284,282,120,189,147,23,360,355,249,110,373,125,214,19,119,0,246,284,214,130,214,375,354,10,151,214,404,246,116,375,214,282,325,335,151,121,137,348,348,10,246,173,10,83,404,72,238,381,238,0,346,381,343,378,72,59,129,189,189,286,279,196,168,332,332,154,353,211,227,324,324,324,220,74,153,376,88,97,380,88,88,330,14,129,310,64,284,373,335,208,330,0,182,174,237,335,81,335,404,28,335,134,136,168,136,378,335,238,321,332,164,345,59,121,173,258,329,107,282,40,0,356,8,220,4,329,129,92,82,323,243,241,129,393,393,346,134,89,342,238,164,311,389},
				{84,320,232,5,152,250,238,311,171,37,237,0,9,314,342,15,110,143,179,75,212,65,175,76,77,87,251,348,198,284,3,325,119,189,164,92,314,325,353,97,169,220,128,75,347,21,253,83,203,164,353,332,261,59,282,36,316,94,90,331,349,354,202,0,140,345,252,38,302,170,117,134,157,193,154,154,389,97,344,89,178,178,171,74,374,349,68,107,374,325,232,129,389,148,68,349,386,363,120,354,346,83,249,346,309,348,375,284,75,378,369,404,345,186,107,92,119,313,329,354,387,158,143,345,345,325,387,29,301,143,246,309,316,72,330,327,25,305,350,129,139,172,329,127,13,63,243,352,212,54,284,267,295,387,126,168,141,31,65,399,66,316,174,369,369,5,26,250,244,391,391,59,59,330,354,232,173,333,374,20,246,128,345,160,369,168,168,246,74,392,127},
				{136,220,128,220,136,369,348,342,203,347,347,348,212,345,92,335,143,330,62,5,391,325,261,59,171,129,354,356,349,263,129,169,330,173,173,350,330,134,31,350,325,345,203,251,33,51,176,63,63,212,345,345,345,203,345,150,120,337,33,169,0,240,240,0,249,375,371,348,130,300,251,0,238,358,66,212,82,224,375,116,57,56,321,84,196,348,348,237,242,135,263,288,291,127,373,284,253,56,102,237,370,270,323,56,276,241,70,115,374,340,325,348,9,237,65,95,226,306,189,124,308,121,376,282,16,104,363,198,9,131,31,375,329,14,14,370,189,127,31,82,188,9,59,192,40,129,332,158,125,19,88,92,313,363,101,185,246,319,344,336,129,382,344,47,314,348,375,311,375,334,316,102,148,188,353,21,19,36,157,92,295,286,247,354,191,296,363,333,81,133,75},
				{325,172,331,296,375,21,247,133,133,174,349,330,175,348,40,316,294,336,191,32,329,81,152,0,150,14,273,170,168,404,92,350,332,313,352,164,313,345,62,238,121,200,121,82,250,345,361,348,77,270,77,284,64,116,329,318,126,6,282,370,306,84,62,0,33,373,348,251,280,355,131,64,370,126,244,231,14,281,304,125,91,78,14,147,125,17,334,304,104,244,114,208,104,323,136,92,175,121,121,348,348,348,356,356,326,345,354,37,64,352,234,37,385,348,92,376,324,62,171,84,324,330,324,374,375,131,48,375,48,0,92,375,387,377,157,335,129,125,309,331,233,84,325,227,330,374,234,309,60,375,17,376,34,220,121,186,173,333,10,250,161,258,313,87,258,107,348,303,330,79,350,326,326,329,92,330,129,323,48,354,284,52,303,384,246,122,338,324,332,141,134},
				{348,10,193,229,147,8,215,369,116,389,353,62,318,353,84,330,14,353,341,48,19,120,342,232,286,330,304,352,196,0,284,330,160,113,152,197,375,335,220,65,109,325,340,334,294,149,249,121,37,348,338,99,170,37,279,381,356,231,60,121,334,183,352,0,355,232,356,354,354,143,372,170,128,245,309,21,243,330,187,348,113,36,329,164,167,48,348,164,203,75,329,48,60,32,187,164,100,100,241,146,310,178,330,140,103,208,73,104,369,355,130,92,170,354,110,251,304,309,212,46,187,336,329,301,387,125,352,60,203,357,312,45,248,27,15,122,387,309,11,341,325,91,325,14,341,119,190,352,241,305,349,336,184,242,143,2,245,345,264,387,294,350,252,114,357,297,241,220,295,120,92,97,134,43,208,335,139,374,352,26,343,282,20,330,335,122,352,346,164,272,306},
				{349,340,303,357,194,95,48,234,198,104,37,24,24,24,52,193,370,314,5,290,100,252,362,133,166,223,295,246,349,404,279,317,182,332,281,247,62,375,404,135,241,241,354,237,168,295,353,38,375,314,36,241,250,231,11,248,128,37,166,237,153,121,374,0,349,349,330,330,57,304,74,319,297,136,27,334,79,82,160,160,332,193,168,329,24,136,50,332,304,283,232,348,332,216,57,304,131,295,128,325,341,246,246,134,272,144,350,348,170,273,137,118,2,347,155,331,37,354,349,56,188,208,188,72,241,196,332,187,39,375,221,40,346,20,375,152,152,353,84,172,192,36,121,31,158,118,341,255,31,92,134,119,281,166,201,196,305,136,382,382,356,91,125,68,65,23,124,375,153,369,244,91,125,354,353,330,369,330,330,250,116,116,75,348,309,184,63,279,348,348,345},
				{249,164,79,263,359,370,356,354,329,107,282,367,348,241,329,0,258,330,246,124,354,5,372,375,375,0,345,324,15,134,326,116,356,246,282,374,357,323,265,239,239,6,215,363,357,19,375,201,240,220,373,367,346,242,208,347,37,232,124,186,250,40,344,0,369,228,199,243,320,7,375,134,155,242,243,112,213,95,392,348,335,273,121,7,375,155,74,74,216,373,110,128,68,263,169,367,175,281,136,169,134,344,332,354,345,20,94,309,36,301,243,186,216,134,231,125,345,299,335,367,83,148,249,123,316,239,363,296,354,385,323,375,392,380,368,175,296,345,9,325,323,246,281,193,187,84,59,328,251,356,387,310,316,12,328,393,331,241,36,272,178,247,89,62,122,241,286,323,171,344,374,167,98,348,340,345,374,23,19,309,388,144,285,129,194,94,188,272,231,221,125},
				{374,353,243,345,201,341,232,325,269,212,284,387,373,343,352,15,346,119,345,390,340,242,92,309,156,156,35,133,316,122,93,68,334,335,243,367,11,220,325,0,329,387,93,96,246,261,252,388,300,300,261,231,143,220,59,375,221,335,249,332,130,192,348,0,72,278,302,367,238,164,306,148,375,321,331,326,244,310,373,75,221,251,367,329,372,141,261,277,300,301,168,38,43,104,231,375,241,296,166,36,37,100,175,174,387,330,116,379,244,305,117,375,244,102,350,127,375,212,182,31,223,40,297,381,128,278,243,234,126,220,96,243,92,235,160,73,332,267,102,365,348,343,36,334,114,101,302,209,170,374,325,142,92,26,336,304,78,34,136,334,12,196,385,325,240,246,73,117,128,302,220,379,166,352,202,175,385,368,240,46,330,58,273,247,129,150,278,335,367,5,134},
				{347,75,188,189,92,303,59,158,134,347,263,347,347,208,328,129,18,214,91,375,375,305,196,125,335,169,304,7,296,175,356,256,375,346,162,268,286,51,209,332,229,164,157,119,350,179,173,243,243,243,158,332,350,186,278,350,51,335,372,179,186,196,37,0,390,176,304,277,63,321,305,128,158,179,102,102,241,343,142,348,107,8,238,63,147,193,329,110,27,285,104,340,128,35,373,74,354,7,340,74,212,63,252,348,143,345,241,315,334,74,346,331,193,378,2,62,373,286,168,250,338,348,128,278,128,385,348,166,14,303,334,334,342,241,241,136,368,0,8,354,158,325,283,124,124,354,128,315,69,375,244,250,369,385,385,385,246,170,385,40,71,378,129,108,229,353,0,241,232,172,84,80,131,371,348,280,125,0,375,79,234,79,369,150,299,354,92,92,123,335,311},
				{350,62,340,31,346,284,193,193,384,45,329,11,8,129,283,116,80,132,341,82,246,116,345,363,80,282,134,346,80,341,238,373,171,109,196,15,340,122,387,331,81,348,326,68,2,19,4,173,152,311,284,120,341,153,340,128,130,375,314,114,87,333,107,0,137,332,154,377,330,283,372,372,107,109,323,310,385,391,377,278,208,330,241,374,348,345,323,170,32,324,334,81,341,348,357,121,340,193,347,350,286,325,247,184,201,158,346,45,220,301,240,128,139,109,327,32,241,109,345,164,92,187,360,335,109,196,48,208,370,121,107,341,42,168,304,131,235,170,330,348,129,120,56,347,347,164,304,314,334,81,282,371,109,348,333,337,278,128,10,131,345,2,347,263,188,164,35,152,65,0,220,123,404,261,173,179,176,277,305,345,378,0,348,116,286,26,283,366,243,340,155},
				{349,245,82,232,82,171,148,0,405,311,159,391,194,324,319,241,350,387,324,314,159,160,173,0,286,0,258,6,74,311,164,249,133,14,373,47,263,243,354,329,343,79,386,258,372,74,183,37,348,91,95,375,126,278,323,133,183,375,355,81,68,332,45,0,286,117,336,215,39,295,356,130,92,184,232,4,82,348,84,201,116,347,378,336,220,75,371,357,391,283,74,175,295,201,373,303,330,99,84,196,348,347,347,284,212,14,348,208,156,110,226,79,376,56,47,350,84,286,280,295,282,254,352,375,389,57,97,240,5,84,84,393,17,367,294,378,229,284,99,29,220,133,172,186,164,261,362,0,352,343,241,373,348,333,373,164,273,140,19,258,7,258,19,386,39,348,47,392,391,77,77,354,78,95,369,164,349,375,284,84,263,348,148,248,271,342,74,44,354,360,0},
				{350,129,172,369,343,268,373,11,350,155,131,238,12,265,330,159,172,83,241,326,137,107,349,249,353,173,231,382,62,342,316,362,353,14,62,107,375,258,11,332,119,323,221,124,311,92,334,404,151,399,82,295,294,69,246,350,134,154,385,31,325,131,164,0,19,331,0,306,375,157,166,385,164,241,19,178,295,243,241,23,31,372,81,229,239,122,36,362,169,354,354,97,103,68,89,324,54,79,36,369,241,355,345,354,251,348,282,330,385,297,157,388,388,82,35,247,212,323,175,109,245,377,134,134,36,393,244,344,232,385,386,18,231,66,40,32,187,117,125,102,330,59,102,63,375,251,0,84,94,231,278,348,282,74,84,325,325,124,387,125,348,330,345,345,369,354,110,328,233,14,220,149,340,267,346,156,345,12,121,153,129,153,294,83,320,375,198,184,148,116,313},
				{354,353,295,355,346,373,233,347,242,347,292,354,325,186,309,92,129,262,120,161,251,333,284,304,357,282,40,232,124,348,373,84,221,189,97,130,302,220,335,355,386,194,297,124,220,129,120,107,243,180,355,261,284,375,326,313,261,305,368,294,283,143,330,0,97,241,142,300,226,306,91,45,373,400,92,129,244,332,186,340,89,385,320,127,346,63,124,375,59,173,271,330,92,166,121,352,243,104,87,87,317,117,282,391,231,353,126,116,119,102,348,348,90,141,102,102,323,375,382,357,159,35,387,164,339,247,290,314,100,363,297,162,175,167,189,404,128,297,143,16,175,168,372,367,286,182,211,346,311,231,378,276,251,125,48,92,345,342,375,343,375,391,48,240,286,117,153,373,278,330,45,63,81,196,329,256,170,172,127,332,266,79,369,160,357,292,318,36,305,188,334},
				{136,336,121,92,221,320,214,265,314,366,82,248,345,113,246,97,172,160,90,330,283,386,270,92,69,131,125,40,55,286,126,170,203,349,121,278,391,341,36,88,134,391,348,240,83,121,58,92,299,237,36,40,379,121,92,129,124,368,125,244,59,348,16,0,295,281,164,349,306,303,190,241,317,19,123,92,243,208,214,348,90,140,349,264,247,345,241,189,371,102,45,92,153,238,61,40,91,190,353,375,120,243,162,169,268,175,164,51,404,192,121,223,404,375,90,70,355,356,81,385,296,166,126,40,247,378,404,345,164,378,246,129,75,297,118,212,354,173,157,128,332,0,134,339,171,350,278,349,353,350,333,216,20,369,166,134,290,281,391,48,251,250,25,0,354,179,164,390,176,305,136,345,158,158,378,179,164,6,202,354,171,101,348,332,340,33,330,241,143,349,136},
				{330,340,331,153,159,336,296,335,340,41,349,243,336,243,42,98,349,354,334,347,346,304,253,223,116,334,119,340,230,348,334,340,0,166,40,348,75,354,46,119,21,21,134,243,375,273,37,273,376,301,299,164,363,354,164,153,75,372,326,196,220,283,340,0,136,242,249,297,247,348,347,347,24,187,65,373,357,326,39,309,130,279,349,64,133,153,51,57,70,126,166,18,70,125,125,378,283,348,335,252,127,220,343,121,252,241,250,121,71,348,334,100,186,2,129,232,31,0,0,232,169,124,269,198,320,343,77,261,332,134,249,221,0,229,134,74,14,270,184,299,286,320,143,302,270,286,198,184,314,261,369,272,369,188,175,65,136,241,346,205,18,282,330,330,247,344,68,346,357,374,19,58,291,161,208,79,249,224,363,190,64,385,42,323,79,56,320,183,353,236,243},
				{354,354,316,133,375,375,243,131,131,319,137,114,251,15,107,376,227,342,120,353,340,340,357,345,57,353,116,123,405,196,329,38,227,14,375,226,386,358,172,363,83,186,358,320,373,373,348,78,310,294,152,350,134,348,124,376,121,12,363,378,353,97,116,0,237,386,375,297,36,294,134,342,283,342,84,92,100,57,349,377,347,121,359,36,325,115,125,294,77,337,84,386,375,349,329,142,0,345,309,197,162,348,189,251,238,334,348,297,152,245,134,237,338,375,120,194,329,250,348,131,284,0,31,130,169,249,325,107,183,353,20,69,114,348,374,158,107,164,94,166,45,114,348,314,160,331,38,132,299,36,229,332,282,154,237,107,247,258,316,334,313,75,357,164,291,294,162,291,70,357,113,337,306,46,370,202,355,320,68,328,134,281,167,122,302,245,60,135,116,104,345},
				{377,231,109,32,243,80,37,175,134,164,372,237,310,354,208,175,189,131,171,178,250,354,282,116,116,355,157,247,243,375,349,238,12,355,326,267,81,355,143,241,281,273,241,232,330,355,262,119,125,218,329,313,83,328,309,325,264,27,110,345,114,189,184,0,350,345,247,283,186,359,122,200,102,37,75,166,241,241,186,310,49,325,24,193,238,340,92,125,83,348,349,284,335,374,304,354,14,193,284,316,283,352,134,314,317,249,249,346,173,123,355,201,226,353,251,382,167,31,345,384,110,386,187,350,156,83,174,325,331,350,238,130,345,301,355,332,220,188,385,36,303,302,321,147,272,367,284,178,180,325,359,329,326,244,363,284,2,357,349,297,301,284,2,226,340,327,25,192,91,342,120,335,62,375,350,129,91,299,128,121,349,363,290,45,334,354,339,116,14,16,375},
				{126,147,282,279,189,5,175,340,353,31,79,352,103,187,247,354,92,344,33,330,92,175,173,130,104,295,375,87,244,128,48,45,381,223,308,347,330,189,305,305,304,166,321,303,88,231,386,141,231,370,382,340,155,243,345,290,334,52,309,126,387,350,114,0,336,286,119,387,154,340,375,235,286,127,238,346,121,129,329,224,92,325,334,243,344,330,300,73,193,266,297,373,387,348,375,323,304,349,335,347,367,270,111,14,31,286,240,170,325,273,273,36,132,117,203,160,53,114,107,357,172,114,84,109,110,332,125,278,330,354,175,214,354,170,283,58,119,363,335,354,284,342,171,323,386,352,150,24,166,63,347,341,373,182,57,348,299,232,134,302,246,385,216,328,246,230,273,276,325,40,302,295,313,273,378,166,208,330,286,189,214,350,350,188,130,241,241,391,328,306,349},
				{349,72,47,123,247,158,343,143,325,18,97,243,350,18,155,81,25,185,360,325,238,360,175,164,353,346,175,294,375,350,70,324,121,335,226,282,16,31,196,172,125,273,36,97,18,123,332,175,246,261,164,131,334,350,299,325,322,342,378,290,157,72,79,0,116,157,286,171,350,208,92,166,392,350,170,348,125,31,56,255,125,158,79,290,355,385,83,281,162,158,48,250,352,243,78,136,345,350,332,360,176,345,164,189,278,304,58,128,31,350,109,378,158,158,202,323,176,343,332,345,88,345,354,101,334,334,121,114,381,367,335,37,359,241,376,82,215,326,240,348,232,139,226,345,227,198,168,101,336,101,101,15,55,331,374,378,143,371,84,6,335,335,171,385,228,303,346,314,278,325,120,377,220,340,113,251,119,339,339,332,349,344,377,220,284,325,314,342,359,139,331},
				{341,374,238,121,101,34,121,35,0,169,84,132,335,240,313,385,313,106,159,345,340,37,261,116,330,286,123,343,148,356,334,330,382,250,62,335,241,0,404,345,134,170,154,122,319,330,52,329,114,134,84,328,48,83,237,248,267,348,248,336,348,127,354,0,89,232,350,324,283,98,280,0,0,94,325,35,110,153,376,331,357,83,166,337,128,14,350,378,325,317,278,330,341,218,31,345,132,132,186,325,367,130,248,262,119,79,249,299,346,169,129,362,98,357,201,284,167,96,305,33,357,20,347,335,330,329,357,110,334,348,357,200,278,248,220,189,243,350,329,95,345,244,14,16,387,175,125,174,231,299,348,314,136,387,116,348,375,325,172,254,223,257,132,154,51,366,125,330,330,348,334,37,120,57,347,343,345,343,334,137,61,319,282,127,82,330,354,170,314,280,82},
				{343,158,186,305,348,132,187,131,0,350,348,342,158,303,24,330,252,387,243,121,378,171,354,348,335,371,121,55,0,19,264,334,343,130,39,306,220,110,158,16,261,169,196,267,404,169,5,343,152,356,168,164,175,136,168,345,330,335,173,347,24,255,356,0,158,48,314,100,134,34,189,305,158,378,158,171,354,371,374,228,36,325,107,136,347,389,189,43,143,361,65,244,352,244,226,33,125,43,354,367,190,226,20,43,353,70,36,215,161,320,258,183,80,89,345,361,11,294,353,91,198,368,251,251,386,183,145,243,164,239,89,375,12,251,45,124,83,143,134,40,330,11,179,129,180,270,325,184,73,314,127,330,164,70,169,225,234,20,330,39,325,256,39,6,92,30,385,110,141,229,115,344,349,119,382,152,376,198,232,134,348,337,348,171,232,208,220,353,342,57,20},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{148,332,341,196,371,129,343,278,302,314,160,385,330,331,11,375,345,276,107,131,225,81,334,232,241,16,349,157,125,244,154,345,387,375,374,344,169,0,387,0,83,328,355,278,317,344,335,124,233,353,220,300,238,194,296,180,355,56,354,284,109,244,348,0,373,25,109,182,126,198,31,5,90,52,82,14,14,120,240,170,354,382,220,330,334,285,0,128,91,341,70,121,150,332,332,214,15,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{123,263,188,169,353,131,285,175,302,332,189,255,203,179,332,241,136,220,386,175,297,249,86,161,6,114,62,42,88,354,241,354,32,114,329,84,129,344,187,9,356,136,357,136,349,57,236,244,348,88,237,84,341,49,96,124,280,47,254,283,282,348,389,0,172,14,373,130,128,125,164,101,332,377,345,339,346,340,297,246,349,343,10,333,331,102,38,340,229,160,58,350,326,75,313,309,329,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{329,36,137,186,11,313,332,40,107,341,345,341,339,84,330,171,164,130,301,83,323,63,236,369,273,193,354,157,193,324,354,310,40,229,36,75,32,13,175,251,64,229,244,385,246,78,130,70,129,122,354,186,45,341,309,337,56,262,193,125,325,264,119,0,335,42,125,381,346,166,251,71,344,120,172,284,261,301,220,326,329,373,130,350,180,306,172,164,159,310,244,54,136,371,18,316,32,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{154,381,48,348,241,131,362,249,5,166,187,349,117,325,329,61,339,366,343,250,58,170,168,248,297,102,131,79,130,172,92,121,131,2,362,58,128,103,304,150,119,273,299,310,40,354,130,175,233,286,391,158,330,386,341,264,328,87,162,70,164,375,262,0,164,360,341,309,102,299,156,173,175,164,158,350,330,333,325,100,59,360,347,20,228,375,164,0,333,0,8,230,80,57,0,0,321,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{116,348,0,135,32,37,0,58,188,383,237,15,377,0,47,350,241,332,174,62,223,381,170,329,57,327,350,345,310,107,32,247,282,31,31,265,297,282,175,83,367,84,237,66,384,188,14,172,300,172,198,357,84,94,129,84,310,198,0,126,321,84,201,0,198,198,92,310,87,14,55,375,177,226,348,117,354,354,198,137,348,172,65,117,58,383,126,36,32,319,162,29,172,65,170,126,39,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{232,65,65,245,57,286,375,200,14,89,377,331,340,35,342,241,344,52,14,387,342,375,78,375,81,134,278,212,308,314,129,354,121,304,37,186,131,334,316,183,334,298,240,74,285,348,231,370,100,180,170,92,310,347,37,232,193,72,143,344,50,282,376,0,375,354,83,346,35,316,104,326,119,148,349,136,330,100,348,43,162,0,63,370,282,50,348,387,296,273,64,174,404,246,84,293,127,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{172,160,332,81,304,349,110,332,100,325,216,354,325,352,162,164,286,57,170,170,17,92,37,346,129,374,185,164,123,157,92,341,350,349,352,304,63,179,176,176,20,0,6,78,78,245,92,362,184,224,47,171,196,92,231,90,75,107,14,39,243,2,334,0,109,120,109,363,52,109,334,347,109,128,2,337,120,179,234,110,128,88,228,249,252,367,97,137,137,377,367,0,371,70,241,350,11,1,1,2,2,2,2,2,2,2,2,2,2,2,2,2,3,3,3,3,3,3,3,3,3,4,4,4,5,5,5,5,5,5,5,5,5,6,6,6,6,6,6,6,6,6,6,6,6,6,224,6,6,6,6,7,7,7,7,7,7,7,7,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,9,9,9,9,9,9,9,9,9,9,9,9,10,10,10,10,10},
				{371,110,110,20,323,354,79,84,249,59,175,369,164,130,340,140,341,3,404,340,377,58,3,97,164,88,324,241,355,332,336,330,83,193,194,284,357,226,80,186,332,284,350,374,220,282,123,55,373,152,340,282,186,196,378,373,284,65,208,386,33,80,226,0,173,314,375,62,204,194,343,373,346,196,376,196,186,334,107,119,36,380,135,75,182,332,330,154,278,310,178,323,163,284,248,169,374,10,10,10,10,10,10,10,10,10,10,10,10,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,12,12,12,12,13,13,13,13,13,13,14,14,14,14,14,14,14,14,14,14,14,14,14,14,14,14,14,14,14,232,14,14,14,14,15,15,15,15,15,15,15,15,15,15,15,15,16,16,16,16,17,17,17,17,18,18,18,18,18,18,19,19,19,19,19,19,19},
				{164,157,391,135,232,122,175,363,377,354,122,180,331,337,121,103,359,45,125,186,115,341,310,340,78,328,102,189,327,147,58,310,248,196,82,363,370,348,69,147,196,292,48,404,37,187,102,119,292,330,240,282,168,34,332,347,342,329,325,125,347,2,121,0,125,371,186,39,272,214,343,220,356,188,190,236,190,162,152,341,190,123,175,188,173,251,182,330,305,140,378,246,354,293,385,136,282,19,19,20,20,20,20,20,20,20,20,20,20,20,20,20,20,20,20,20,20,20,21,20,21,21,21,21,21,21,21,21,21,22,23,23,23,23,23,23,23,23,23,23,23,24,24,24,24,24,24,24,25,25,25,25,25,26,26,26,26,26,27,27,27,27,27,28,28,29,29,29,29,29,29,29,29,29,29,29,30,30,30,31,31,31,31,31,31,31,31,31,31,32,32},
				{115,350,377,128,366,356,6,66,241,386,329,372,242,244,57,136,164,84,326,75,141,375,129,236,75,372,299,321,130,196,389,354,179,320,75,198,378,217,237,171,240,161,75,238,352,152,145,251,378,152,75,242,175,325,2,349,345,237,160,114,247,325,127,0,0,247,252,31,159,114,354,334,331,183,179,352,34,34,345,276,154,97,0,117,175,35,219,295,385,145,345,383,146,36,241,387,247,32,32,32,370,32,32,32,32,32,32,32,33,33,33,33,33,33,33,33,33,34,34,34,34,34,34,35,35,35,35,35,35,35,35,35,35,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,37,37,37,284,37,37,37,37,37,37,37,37,37,37,37,37,38,38,38,38,38,39,39,39,39,39,39,39,39,39,39,39,39,40,40,40,40,40,40,40,40,40,40},
				{170,137,226,193,369,110,330,163,349,329,241,178,203,264,345,66,320,350,143,325,293,373,71,331,58,309,203,310,241,73,83,252,252,180,95,335,75,172,309,305,143,232,325,310,344,241,166,325,357,383,156,0,220,381,31,241,127,231,172,175,140,43,35,0,349,162,234,241,378,52,387,46,178,92,278,160,366,125,330,66,63,226,358,127,62,164,125,330,370,127,332,354,383,127,143,363,162,40,40,40,40,40,40,41,42,42,42,42,42,42,42,43,43,382,43,43,43,44,44,44,44,44,45,45,45,45,45,45,45,46,33,47,47,47,47,47,47,47,47,47,47,47,47,48,48,48,48,48,48,49,50,50,50,50,51,51,51,52,52,52,52,52,52,52,52,53,53,53,54,54,54,54,54,54,55,55,55,55,55,55,56,56,56,56,56,56,56,56,56,56,56},
				{129,40,347,252,58,348,232,232,354,236,241,348,143,125,354,264,228,47,20,346,180,336,152,162,162,375,164,179,79,252,228,350,164,173,173,196,290,100,158,22,345,259,356,302,180,335,220,375,80,72,376,0,348,284,353,375,311,84,189,298,186,30,404,0,354,329,378,102,331,375,90,90,291,130,282,87,154,62,306,97,100,391,171,175,59,375,14,40,121,353,349,386,123,373,355,329,332,56,57,57,57,57,57,57,57,57,57,57,57,57,57,57,57,58,58,58,58,58,59,59,59,59,59,59,59,59,59,59,59,59,60,60,60,61,61,61,61,61,61,61,62,62,62,62,62,62,62,62,368,62,62,62,62,62,62,62,62,62,62,63,63,63,63,63,63,63,63,63,63,63,63,63,63,63,63,64,64,64,64,64,64,64,64,311,65,65,65,65,65,65,65},
				{346,375,348,186,294,62,11,373,352,92,90,305,294,180,302,84,341,241,354,241,294,304,57,102,299,164,216,332,59,164,356,375,360,162,262,335,316,258,386,199,375,332,209,249,82,184,357,375,131,375,354,148,8,232,208,164,353,232,20,171,209,393,375,0,134,123,314,375,155,110,349,386,375,258,70,378,216,240,90,363,320,154,88,131,309,316,344,178,175,97,393,384,9,241,375,154,163,66,66,66,66,66,66,66,66,66,67,68,68,68,68,68,68,68,68,68,68,69,69,69,69,69,69,69,69,70,70,70,70,70,70,70,70,70,70,70,70,70,70,71,71,71,71,71,71,72,72,72,72,73,73,73,73,73,319,73,73,73,74,74,74,74,74,74,74,74,74,74,74,74,75,75,75,75,75,75,75,75,75,75,75,75,75,76,77,77,77,77,77,77,77},
				{231,170,391,354,297,0,348,330,15,92,84,232,221,129,376,387,340,36,59,332,386,354,340,375,142,97,261,299,261,92,141,278,189,375,92,175,297,92,350,326,249,0,348,120,245,92,334,240,128,385,376,391,404,299,216,273,121,255,221,354,236,386,318,0,326,36,123,152,404,228,273,385,164,360,344,354,282,131,220,160,220,10,19,321,378,228,64,329,321,43,220,334,36,147,57,385,340,77,78,78,78,78,78,78,78,78,79,79,79,79,79,79,79,79,79,79,79,79,79,79,79,79,79,80,80,80,80,80,80,80,80,80,80,80,81,81,81,81,81,81,81,81,81,81,81,81,82,82,82,82,82,82,82,82,82,82,82,82,82,82,82,83,83,83,83,83,83,83,83,83,83,83,83,83,83,83,0,85,84,84,84,84,84,84,84,84,84,84,84,84,84},
				{297,100,385,328,321,347,248,220,220,311,33,310,64,174,168,330,329,153,43,371,151,151,36,52,168,362,51,127,248,70,362,173,245,40,284,84,243,40,114,241,109,283,82,191,250,378,171,207,19,7,294,114,373,64,348,286,131,250,237,164,381,28,61,0,53,322,131,140,131,161,231,37,183,378,323,128,298,246,344,383,355,108,130,232,348,68,278,331,378,199,97,245,193,10,278,229,171,84,84,84,84,84,84,84,84,84,84,84,240,84,84,84,84,84,84,84,84,84,84,84,84,84,84,84,84,84,84,84,84,86,86,87,87,87,87,87,87,88,88,88,88,88,88,88,88,88,88,88,89,89,89,89,89,89,89,89,89,90,90,90,90,90,90,90,90,90,90,91,91,91,91,91,91,91,91,91,91,91,105,91,91,91,91,91,0,93,93,94,94,94,94},
				{217,84,79,88,79,284,184,334,248,193,179,102,250,37,349,220,14,155,128,357,263,92,108,376,157,58,27,84,316,164,159,134,100,332,107,352,331,375,36,298,378,393,313,124,90,88,152,357,222,334,240,157,388,225,232,91,303,99,354,125,371,116,374,0,367,377,175,170,253,84,367,97,243,137,383,27,389,20,341,15,300,332,237,337,116,348,356,45,404,329,68,334,92,129,337,186,79,94,94,94,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,96,96,96,96,96,96,96,96,96,97,97,97,97,97,97,97,97,97,97,97,97,97,97,97,97,97,97,98,98,98,98,98,98,99,99,99,100,100,100,100,100,100,100,100,100,100,100,101,101,101,102,102,102,102,102,102,102,102,102,102,102,102,102,102,102,102,103,103,103,104,104,104,104,104,104,105},
				{71,83,378,114,245,249,191,243,97,284,357,110,305,356,39,267,374,212,243,334,173,231,70,385,40,284,378,244,173,125,21,121,14,378,48,345,360,390,232,234,354,381,368,277,104,348,116,31,147,237,362,92,404,92,209,301,51,64,301,161,167,334,20,0,189,294,305,168,57,84,125,193,153,56,128,381,160,334,175,284,360,241,225,241,232,88,134,175,175,345,58,269,379,96,243,166,284,106,106,106,106,106,106,106,107,107,107,107,107,107,107,107,107,107,107,107,107,107,107,107,107,107,107,12,108,108,109,109,109,109,109,109,109,109,109,110,110,110,110,110,110,110,110,110,110,196,110,110,110,110,110,110,110,111,111,112,112,112,112,113,113,113,113,113,114,114,114,114,114,114,114,114,114,115,115,115,115,115,115,115,116,116,116,116,116,116,116,116,116,116,116},
				{158,153,354,356,109,373,303,309,212,306,348,241,308,381,377,226,296,377,243,385,308,175,175,125,320,350,354,157,173,245,166,158,243,356,376,250,166,15,71,390,164,284,179,350,356,385,354,79,282,372,282,405,110,208,53,370,243,368,232,8,329,276,141,0,262,14,299,349,372,189,303,116,88,134,196,377,164,297,114,314,273,170,382,7,160,77,250,110,332,84,164,356,175,134,241,7,370,116,116,116,116,116,116,116,117,117,117,117,117,117,117,117,117,118,118,118,118,118,119,119,119,119,119,119,119,119,119,119,119,119,119,119,120,120,120,120,120,120,120,120,120,120,120,120,120,120,121,121,121,121,121,121,121,121,121,121,121,121,121,121,121,121,121,121,121,121,121,122,122,122,122,122,122,123,123,123,123,123,123,123,123,123,123,92,92,92,92,92,92,92,92},
				{117,117,270,305,15,190,387,125,164,335,84,221,11,97,339,124,382,305,270,82,126,196,270,270,221,330,167,150,20,119,286,92,332,317,212,164,393,62,212,311,158,294,133,95,374,133,353,92,29,377,343,356,126,354,129,323,258,326,249,199,386,315,215,0,85,129,286,384,232,373,276,114,375,92,82,357,258,57,130,80,389,133,367,6,84,375,241,377,114,367,330,84,348,282,20,378,250,92,92,92,92,92,92,92,92,92,92,92,92,92,92,92,92,92,92,92,92,92,92,92,92,92,92,92,92,92,92,92,92,92,92,92,92,92,92,92,92,92,92,92,92,92,124,124,124,124,124,124,124,124,124,124,124,124,124,124,124,124,124,125,125,125,125,125,125,125,125,125,125,125,125,125,125,125,125,125,125,125,125,125,125,125,125,140,125,125,125,125,125,125,125},
				{171,378,280,88,346,84,320,373,56,40,284,376,341,389,132,8,250,196,286,391,152,131,258,108,348,129,378,39,149,196,136,152,116,47,119,94,306,335,148,334,251,87,179,343,231,332,84,331,314,261,311,349,162,335,135,106,65,314,294,126,333,121,136,0,125,341,375,326,373,404,36,249,286,9,314,334,323,332,94,339,309,339,335,114,330,84,313,299,72,154,84,131,116,375,345,132,83,125,125,125,125,125,125,125,125,126,126,126,126,126,126,126,126,126,126,126,126,126,128,128,128,128,128,128,128,128,128,128,128,128,128,128,220,128,128,128,128,128,128,128,128,128,128,128,128,128,129,129,129,129,129,129,129,129,129,134,129,129,129,129,129,129,129,129,129,92,129,129,129,129,129,129,129,130,130,130,130,130,130,130,130,130,130,130,130,130,130,130,130,130,130},
				{92,340,258,387,282,74,169,404,167,306,251,280,241,384,241,323,374,332,285,348,306,323,89,324,13,383,23,104,389,178,172,348,369,14,46,171,190,241,245,310,387,122,388,330,386,337,167,130,81,265,193,354,387,79,404,340,350,279,241,340,333,125,143,0,332,264,190,241,71,376,62,193,191,355,347,10,294,249,15,119,94,387,190,325,84,325,340,96,191,335,166,387,233,357,349,309,98,130,130,131,131,131,131,131,131,131,131,131,131,131,131,131,131,131,131,131,131,131,131,131,131,131,131,131,132,132,133,133,133,133,133,133,133,133,133,133,133,133,133,133,133,133,133,134,134,134,134,134,134,134,134,134,134,134,134,134,134,134,134,134,134,134,134,134,134,134,134,134,135,135,135,135,135,135,135,136,136,136,136,136,136,128,136,136,136,137,137,137,137,137,137},
				{375,357,36,31,56,331,355,387,340,350,325,94,0,350,130,348,383,208,9,116,226,377,125,301,251,290,357,331,299,330,261,306,84,357,373,90,263,116,366,308,341,297,373,387,306,120,23,14,83,50,164,301,349,330,387,162,381,243,182,375,404,198,234,0,166,341,387,92,334,299,404,13,348,362,198,244,122,332,92,276,339,254,341,299,127,366,393,375,278,270,170,354,79,256,46,392,125,137,137,137,137,137,138,138,138,91,139,139,139,139,139,140,140,140,140,140,140,141,141,141,141,141,141,141,142,142,142,142,143,143,143,143,143,143,143,143,143,143,143,143,143,143,143,144,144,144,144,145,145,146,146,146,146,147,147,147,147,148,148,148,148,148,148,148,149,149,149,149,149,150,150,150,150,151,151,152,152,152,152,152,152,152,152,153,153,153,153,153,153,153,153},
				{256,31,265,339,121,117,390,330,244,357,55,350,121,330,273,125,126,119,362,48,335,385,14,304,348,216,299,348,277,340,92,18,243,158,240,343,241,231,220,196,162,335,390,152,353,340,162,332,31,128,175,31,350,278,333,332,391,390,179,330,158,162,166,0,114,258,378,373,65,330,350,306,357,332,299,335,85,85,241,20,237,333,371,89,350,100,392,304,25,241,327,350,162,304,175,294,89,153,153,153,154,154,154,154,155,155,155,155,156,156,156,156,156,156,156,157,157,157,158,158,158,158,158,158,158,158,158,158,158,158,158,158,158,159,159,159,159,159,159,159,160,160,160,160,160,160,160,160,160,161,161,162,162,162,162,162,162,162,162,162,162,162,163,163,163,164,164,164,164,164,164,164,164,164,164,164,164,164,164,164,164,164,164,164,164,164,164,164,164,164,164},
				{0,282,97,378,134,188,193,98,309,135,84,282,99,385,354,3,78,294,180,172,232,78,33,325,14,92,38,172,135,189,371,179,232,92,92,176,189,55,346,353,353,82,6,346,97,353,361,186,348,378,348,261,253,244,119,348,348,346,244,332,354,159,355,0,79,278,82,278,166,400,244,114,38,82,114,37,52,84,331,230,156,238,175,375,354,330,335,330,143,121,121,334,276,114,126,377,276,164,164,164,164,164,164,164,164,167,166,166,166,166,166,166,166,166,166,166,166,166,166,166,167,167,167,167,167,167,167,167,167,167,167,168,168,168,168,168,168,168,168,161,168,168,168,168,169,169,169,169,169,170,170,170,170,170,170,170,170,170,170,170,170,171,171,171,171,171,171,171,171,171,171,171,171,171,171,172,172,172,172,172,172,172,172,172,172,172,173,173,173,173,173},
				{37,341,37,387,323,121,115,110,234,166,250,5,170,230,127,5,121,341,59,0,96,96,96,65,264,200,381,313,232,348,250,124,134,40,178,129,367,174,92,160,123,353,118,348,246,357,57,114,375,0,373,34,374,353,385,311,405,344,372,0,109,283,169,0,131,14,62,104,326,340,237,48,66,0,313,134,48,153,166,166,327,153,166,166,48,283,295,313,153,372,375,57,214,245,208,313,313,173,173,173,173,174,174,174,174,174,174,175,175,175,175,175,175,175,175,175,175,175,175,175,175,175,175,175,175,175,175,404,404,404,404,404,404,404,404,404,404,404,404,404,404,176,176,176,176,176,176,177,177,178,178,178,178,178,178,178,179,179,179,179,179,179,179,179,179,179,179,179,180,180,180,180,180,180,180,180,180,181,181,181,181,181,181,182,182,182,182,182,182,182,182},
				{173,371,294,297,371,348,249,144,26,241,62,119,37,258,263,355,29,246,82,229,227,353,254,326,241,232,330,330,144,65,81,6,20,340,310,386,148,375,208,237,386,377,332,198,250,143,37,346,227,375,338,77,113,386,153,374,311,52,186,335,335,181,335,0,213,326,326,339,145,353,113,228,278,313,186,45,282,250,307,391,92,353,336,320,36,318,69,203,232,97,164,370,299,129,167,291,16,182,183,183,183,183,183,183,184,184,184,184,184,184,184,184,184,184,184,184,185,186,186,186,186,186,186,186,186,186,186,186,186,186,186,186,186,187,187,187,188,188,188,188,188,188,188,188,189,189,189,189,189,189,189,189,189,189,189,189,189,189,190,190,190,190,190,190,190,190,190,191,191,191,191,191,191,191,191,192,192,193,193,193,193,193,193,194,194,194,194,194,194,195,196},
				{178,233,162,251,115,57,205,200,258,282,383,134,68,232,123,328,186,264,381,37,179,223,62,3,203,289,357,376,262,316,325,136,124,71,14,32,345,321,285,305,297,383,348,168,92,232,335,404,223,32,175,123,227,41,126,175,381,175,128,350,404,342,399,0,314,208,168,52,153,334,319,226,375,128,329,52,0,333,0,82,379,57,150,216,136,40,135,156,166,319,241,52,18,343,263,356,361,196,196,196,196,196,196,196,196,196,196,196,196,196,196,196,196,197,197,197,198,198,198,198,198,198,198,198,198,198,198,198,198,198,198,199,199,199,199,199,199,199,200,200,200,200,200,201,201,201,202,203,203,203,203,203,204,205,205,206,207,208,208,208,208,208,208,208,208,208,208,208,209,209,209,209,209,209,209,210,210,211,211,212,212,212,212,212,212,212,213,214,214,214,214},
				{332,16,337,154,169,345,175,123,358,179,250,361,176,208,360,332,328,132,170,132,128,92,109,339,190,212,91,125,375,375,339,303,332,29,330,354,337,134,133,336,281,133,310,284,304,240,240,100,241,310,331,59,79,224,303,79,79,175,171,331,248,227,149,0,84,362,83,164,354,159,0,20,243,0,120,387,15,198,65,69,9,29,348,25,174,56,342,220,61,58,244,175,348,92,125,328,241,214,214,215,215,215,215,216,216,216,216,217,217,217,405,218,219,219,221,221,221,221,222,223,223,223,223,223,223,223,224,224,224,224,224,224,225,225,225,225,225,225,226,226,226,226,226,226,226,226,227,227,227,227,227,228,228,228,228,228,228,228,229,229,229,229,229,229,229,229,229,230,230,231,231,231,231,231,231,231,231,231,231,231,231,231,231,232,232,232,232,232,232,232,232},
				{175,175,31,290,125,237,345,345,26,161,313,311,128,231,348,29,190,88,354,331,386,121,299,375,319,325,241,326,258,84,378,117,136,92,5,388,184,259,108,349,353,348,245,232,309,62,164,352,11,348,378,208,224,19,339,220,332,114,393,68,65,212,116,0,186,283,98,189,325,84,386,375,92,48,0,342,179,164,261,375,35,354,339,386,169,329,92,102,47,96,101,211,354,386,229,198,376,232,232,232,232,232,232,232,232,232,233,233,233,233,234,234,234,234,235,235,236,236,236,236,236,237,237,237,237,237,237,237,237,237,238,238,238,238,238,238,238,238,239,240,240,240,240,240,240,240,240,240,240,240,240,240,240,10,241,241,241,241,241,241,241,241,241,241,241,241,241,241,241,241,241,241,241,241,241,241,241,241,241,241,241,241,241,241,241,241,241,241,241,241,242},
				{243,378,119,84,87,302,42,131,77,3,127,37,237,243,160,286,382,55,27,354,70,325,164,69,84,258,349,240,357,299,36,216,329,17,330,94,378,196,382,393,320,249,301,35,231,330,337,350,34,137,345,54,107,54,124,324,353,262,334,332,326,129,201,0,198,316,0,183,47,62,250,68,388,156,175,325,258,154,240,386,90,104,178,39,44,369,187,164,10,246,135,246,62,276,130,371,375,242,242,243,243,243,243,243,243,243,243,243,243,243,243,243,243,243,243,243,243,243,243,243,243,244,244,244,244,244,244,244,244,127,127,127,127,127,127,127,127,127,127,127,127,127,127,127,245,245,245,245,245,246,246,246,246,246,246,246,246,246,246,246,247,247,247,247,247,247,247,247,247,247,247,247,247,248,248,249,249,249,249,249,249,249,249,250,250,250,250,250,250,250,250},
				{94,149,246,345,324,13,377,125,170,304,310,59,116,110,45,32,119,84,157,276,345,348,311,241,323,27,373,363,133,14,348,188,0,228,66,249,143,333,323,354,354,84,166,341,201,27,328,45,334,233,198,3,350,155,155,126,190,393,393,389,262,330,347,0,345,250,125,84,404,125,230,114,114,115,345,316,386,247,189,120,282,87,377,243,325,20,325,92,71,220,301,251,373,343,284,158,387,250,250,250,250,250,251,251,251,251,251,251,251,251,251,251,251,252,252,252,252,252,252,252,252,253,253,254,254,254,254,255,255,255,255,255,256,256,256,257,257,258,258,258,258,258,258,258,258,258,258,259,259,260,261,261,261,261,261,261,261,261,261,261,262,262,262,263,263,263,263,263,263,263,263,263,263,264,264,265,265,265,266,266,267,267,268,268,268,269,269,269,269,270,270},
				{220,355,186,357,286,381,100,254,342,0,325,353,296,349,284,45,284,357,373,263,164,252,355,164,134,330,40,340,316,172,328,63,243,389,238,54,355,40,354,226,240,199,330,82,357,267,25,189,300,194,296,172,330,97,159,91,54,305,179,341,385,102,387,0,103,393,311,27,229,0,282,159,171,311,339,184,314,378,3,166,387,237,340,130,317,129,325,318,26,354,348,386,14,175,21,370,162,270,270,271,271,271,272,272,272,272,273,273,273,274,275,276,276,276,29,276,276,276,276,276,277,277,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,279,279,279,279,279,279,279,279,280,280,280,280,280,280,280,280,280,280,280,281,281,281,281,281,281,281,281,281,281,281,281,282,282,282,282,282,282,282,282,282,282,282,282,282,282,282,282,283,283,283,283,283,283},
				{244,182,345,171,330,234,103,107,62,175,281,279,62,343,20,62,367,282,341,116,5,189,404,376,238,126,189,48,211,121,137,349,278,349,104,35,116,276,147,243,180,361,363,164,325,92,343,283,188,223,31,63,265,162,354,127,378,149,125,181,357,10,353,0,175,220,75,309,81,136,81,263,82,153,292,344,340,84,305,329,68,294,334,330,268,357,280,125,300,171,354,331,294,216,341,357,354,283,283,283,283,283,284,284,284,284,284,284,284,284,284,284,284,284,282,284,284,284,284,284,284,284,284,284,284,284,284,284,284,284,284,284,284,284,284,284,284,284,284,284,284,284,284,284,284,284,284,284,284,285,285,285,285,285,285,285,285,285,285,286,286,286,286,286,286,286,286,286,286,286,286,286,286,286,286,286,286,286,286,286,286,286,286,286,286,286,286,286,286,286,286},
				{334,109,2,121,121,92,386,333,323,192,163,126,24,282,273,166,143,355,55,375,305,369,300,345,79,66,335,97,378,125,142,268,343,220,7,72,236,355,214,377,328,234,241,362,35,373,77,208,350,48,334,241,78,125,354,153,15,385,189,158,130,248,245,0,0,332,297,404,348,340,335,348,156,162,128,62,375,11,220,196,119,228,296,304,318,248,127,325,172,121,223,142,357,10,164,378,378,286,287,287,288,288,288,288,289,289,290,290,290,291,291,291,291,292,292,292,292,293,293,293,293,294,294,294,294,294,294,294,294,294,294,294,294,294,284,294,294,295,295,295,295,295,295,295,295,296,296,296,296,297,297,297,297,297,297,297,297,297,339,297,297,298,298,298,299,299,299,299,299,299,299,299,299,299,299,300,300,300,301,301,301,301,301,301,301,301,302,302,302,302,302},
				{2,170,341,246,157,363,329,265,265,246,175,297,318,183,357,237,354,343,92,132,341,249,297,132,83,20,333,348,332,354,134,332,332,349,244,350,173,315,117,356,171,220,186,158,153,158,92,305,182,162,162,121,295,375,325,153,118,164,92,162,118,179,92,0,153,175,125,0,308,162,251,334,348,176,187,17,375,334,134,116,340,54,84,340,175,116,109,128,134,10,345,369,369,153,18,330,286,302,302,302,302,303,303,303,303,303,303,303,303,303,304,304,304,304,304,304,304,304,304,304,304,304,304,304,304,304,304,304,305,305,305,305,305,305,305,305,305,305,305,305,305,306,306,306,306,306,306,306,306,306,306,306,307,308,308,308,308,309,309,309,309,309,309,309,309,309,309,309,309,309,309,309,310,310,310,310,310,310,310,310,311,311,311,311,311,312,312,312,313,313,313},
				{64,249,66,0,136,372,347,354,107,386,333,80,75,6,37,243,326,356,356,137,241,314,375,355,252,353,246,241,376,198,324,82,82,108,84,254,228,208,307,237,62,134,164,84,367,340,232,232,332,64,17,19,369,312,97,350,179,47,121,183,84,169,348,0,332,164,348,237,245,281,348,324,196,245,102,248,160,337,129,249,124,356,34,11,107,341,373,240,332,167,14,355,353,129,57,63,121,313,313,313,313,313,313,313,314,314,314,314,314,314,314,314,314,314,314,314,314,315,315,315,315,316,316,316,316,316,316,316,316,316,316,316,317,317,318,318,318,318,318,318,319,319,319,320,320,320,320,320,320,320,320,320,320,320,321,321,321,321,321,321,321,322,322,323,323,323,323,323,323,323,323,323,323,323,323,323,323,323,323,323,324,324,324,324,324,324,324,324,324,324,325},
				{295,134,241,354,137,330,178,164,65,306,154,107,107,9,81,325,319,357,301,243,325,208,325,167,68,75,8,385,324,24,189,0,156,129,313,184,340,190,129,284,341,345,262,325,84,355,186,325,264,335,331,350,284,376,305,378,387,309,355,188,156,70,249,0,330,328,357,354,350,126,8,294,47,330,327,166,226,261,92,329,339,107,348,232,117,348,70,207,331,116,121,180,348,326,350,308,376,325,325,325,325,325,325,325,325,325,325,325,325,325,325,325,325,325,325,325,325,325,325,325,325,325,325,325,325,325,325,325,325,326,326,326,326,326,326,326,326,326,326,327,327,327,328,328,328,328,328,328,328,328,328,329,329,329,329,329,329,329,329,329,329,329,329,329,329,329,329,329,329,329,329,329,329,329,329,329,329,329,329,329,330,330,330,330,330,330,330,330,330,330,330},
				{25,0,241,311,279,62,175,325,375,35,250,232,354,125,174,246,349,126,326,334,182,372,372,180,354,172,48,164,182,334,32,196,391,294,249,307,375,231,127,250,17,168,102,330,241,381,81,168,136,136,349,309,128,256,334,329,376,343,294,40,36,58,164,0,320,348,131,55,241,92,333,281,246,350,30,164,364,341,378,363,335,183,335,241,261,125,109,385,129,18,110,79,162,129,156,193,164,330,330,330,330,330,330,330,330,330,330,330,330,330,330,330,330,330,330,332,330,330,330,330,330,331,331,331,331,331,331,331,331,331,331,276,331,331,332,332,332,332,332,332,332,332,332,332,332,332,332,332,332,332,332,332,332,332,332,332,332,332,332,332,333,333,333,333,333,333,333,333,333,333,333,333,333,333,333,333,333,333,333,333,334,334,334,342,334,334,334,334,334,334,334},
				{249,212,175,70,334,378,173,164,173,227,347,232,279,347,350,286,330,24,250,251,24,182,129,378,385,120,405,229,376,181,77,143,192,330,140,355,250,171,341,286,314,333,354,116,325,59,38,325,59,384,88,354,348,84,230,129,376,57,348,376,129,375,335,0,254,375,137,30,348,254,320,171,228,220,393,14,280,134,149,342,373,348,224,84,62,84,103,375,375,254,348,320,199,96,341,372,250,334,334,334,334,334,334,334,335,335,335,335,335,335,124,335,335,335,335,335,335,335,335,335,335,335,335,335,335,336,336,336,336,336,336,336,336,336,336,337,337,337,337,337,337,337,337,337,108,337,337,337,337,337,338,338,338,338,338,338,338,339,339,339,339,339,339,339,339,339,340,340,340,340,340,340,340,340,340,340,340,340,340,40,340,340,340,340,340,341,341,341,341,341,341},
				{354,208,20,0,221,241,148,258,126,392,196,129,77,263,378,102,349,138,337,0,154,211,286,335,154,164,135,282,129,373,172,92,348,21,382,291,253,164,339,304,154,306,355,171,37,32,59,167,229,81,355,345,70,375,348,241,104,144,241,309,309,84,376,0,335,65,154,317,353,353,355,84,354,317,345,348,228,57,357,302,96,118,261,355,200,132,301,226,271,211,350,129,118,148,166,164,284,341,341,341,341,342,342,342,342,342,342,343,343,343,343,343,343,343,343,343,343,343,343,343,343,344,344,344,344,344,344,344,344,344,344,344,344,344,344,344,344,345,345,345,345,345,345,345,345,345,345,345,345,345,345,345,345,345,345,345,345,345,345,345,345,345,345,345,345,345,345,345,345,345,346,346,346,346,346,346,346,346,346,346,346,346,346,346,346,346,346,220,220,220,220},
				{404,348,65,335,332,325,16,26,92,278,239,14,240,125,381,125,54,92,57,358,79,81,333,343,17,256,182,158,5,363,121,26,299,216,57,166,58,286,369,14,158,240,375,0,286,321,284,11,335,20,35,157,173,330,332,158,372,56,134,360,284,125,348,0,158,344,330,83,84,0,6,110,92,92,332,100,15,345,340,136,233,184,189,189,235,284,294,92,373,136,189,311,166,220,375,137,330,220,220,220,220,220,220,220,220,220,220,220,347,347,347,347,347,347,347,347,347,347,347,347,347,347,347,348,348,348,348,348,348,348,348,348,348,348,348,348,348,348,348,348,348,348,348,348,348,348,348,348,348,348,348,348,348,348,348,348,348,348,348,348,348,348,348,348,348,348,348,348,348,348,348,348,348,348,348,348,349,349,349,349,349,349,349,349,349,349,349,349,349,349,349},
				{278,325,330,310,354,158,75,70,246,227,92,194,350,96,250,369,130,100,61,125,179,250,125,325,136,250,179,158,282,92,100,345,310,249,130,50,375,33,92,57,375,335,251,91,284,102,335,122,249,337,208,241,175,367,14,337,279,95,375,342,40,330,348,0,175,136,330,345,330,345,66,84,249,249,128,92,79,343,64,114,30,306,340,129,348,258,343,349,278,241,320,92,343,349,75,82,344,349,350,350,350,350,350,350,350,350,350,350,350,350,350,350,350,350,350,350,351,352,352,352,352,352,352,352,352,352,352,352,352,352,352,352,353,353,353,353,353,353,353,353,353,353,353,353,353,353,353,353,353,353,353,353,354,354,354,354,354,354,354,354,354,354,354,354,354,354,354,354,354,354,354,354,354,354,354,354,354,354,354,354,354,340,354,354,354,354,354,354,354,354,354},
				{220,295,282,349,349,136,334,204,35,353,375,338,80,336,191,281,345,268,384,340,348,348,297,37,110,282,110,340,373,378,374,96,386,369,97,84,125,65,171,309,346,217,226,377,88,348,134,220,367,348,348,250,371,237,14,338,250,20,55,389,306,378,47,0,372,352,340,343,348,120,110,284,245,334,284,112,70,115,102,251,121,129,117,87,346,325,282,377,314,189,194,179,121,345,338,98,77,354,354,354,354,354,354,355,355,355,355,355,355,355,355,355,355,355,355,355,355,355,355,355,355,355,355,356,356,356,356,220,356,356,356,356,356,357,357,357,357,357,357,357,357,357,357,357,357,358,358,358,359,359,359,359,359,359,359,360,360,360,360,361,361,361,362,362,362,362,362,362,362,362,362,362,362,362,362,362,363,363,363,363,364,365,366,366,28,366,367,367,367,367,367},
				{19,311,348,162,378,152,250,329,92,375,258,50,159,75,152,348,313,57,11,31,353,145,127,246,287,3,354,334,36,129,332,329,329,90,295,21,121,131,356,373,356,70,117,32,291,129,143,250,48,334,299,324,332,81,157,302,348,208,349,377,232,385,31,0,35,384,92,241,304,383,325,134,247,68,374,393,388,243,385,167,125,92,331,178,282,16,117,233,354,335,340,233,284,341,284,122,117,367,367,367,278,367,358,367,367,367,368,368,368,368,368,368,369,369,369,369,369,369,369,369,369,369,369,369,369,369,369,369,369,370,370,370,370,370,370,370,370,370,370,370,370,370,370,370,371,371,371,371,371,371,371,371,371,371,372,372,372,372,372,372,372,372,372,372,373,373,373,373,373,373,373,373,373,373,373,373,373,373,373,373,374,374,374,374,374,374,374,374,374,374,374},
				{75,376,309,335,84,240,313,158,241,354,386,381,330,121,349,315,332,201,35,83,378,346,345,120,341,91,221,340,197,347,325,337,308,377,278,238,318,120,123,91,350,189,334,189,331,244,373,342,309,297,9,37,369,348,126,355,335,334,306,220,220,375,354,0,234,48,164,196,196,279,372,195,125,363,389,166,404,362,340,103,330,385,5,5,130,372,348,334,126,182,371,332,321,57,340,366,273,373,374,374,374,375,375,375,375,375,375,375,375,375,375,375,375,375,375,375,375,375,375,375,375,375,375,375,375,375,375,375,375,375,375,375,375,375,375,375,375,375,375,375,375,375,375,375,376,376,376,376,376,376,376,376,376,376,376,377,377,377,377,377,377,377,377,377,377,377,377,377,377,378,378,378,378,378,378,378,378,378,378,378,378,378,378,378,378,378,378,378,378,378,378},
				{330,367,72,374,334,158,75,350,136,92,392,127,20,121,381,329,365,367,375,127,304,365,240,283,341,362,304,58,299,332,92,128,369,400,348,2,369,121,335,348,348,278,255,400,243,72,302,116,377,109,348,350,140,354,125,121,70,372,341,360,162,282,325,0,31,164,348,15,372,345,75,39,325,39,220,31,255,349,158,35,335,212,119,360,348,58,369,345,70,320,258,338,338,14,377,282,340,378,378,378,378,379,371,380,381,381,381,381,390,381,382,382,382,382,382,382,382,383,383,383,383,383,383,384,384,385,385,385,385,385,385,385,385,372,385,385,386,386,386,386,386,386,386,386,359,386,386,386,386,386,386,387,387,387,387,387,387,387,388,388,388,388,389,389,389,389,389,389,389,389,390,390,391,391,391,391,392,392,393,393,393,367,393,393,393,393,0,0,0,0,0},
				{243,114,331,92,114,107,173,330,330,168,107,70,173,241,164,61,15,286,332,83,375,375,345,345,40,121,319,348,348,345,6,115,75,40,345,154,87,134,240,330,119,378,124,82,330,123,326,119,62,387,82,348,3,232,199,232,96,199,353,196,294,119,184,0,196,3,354,208,14,354,124,317,184,330,348,354,40,304,119,136,11,373,357,84,23,95,307,348,108,323,236,123,79,304,323,368,375,40,92,329,87,209,268,232,93,36,91,203,75,286,354,235,14,320,220,220,375,62,336,349,153,354,90,306,63,92,200,212,92,241,189,11,273,97,363,281,54,345,136,294,347,345,250,238,102,153,15,363,98,353,27,348,326,131,148,102,139,156,92,345,323,150,234,136,127,123,348,314,324,57,66,370,161,268,348,198,258,354,232,344,321,329,32,25,141,378,214,138,353,348,96},
				{77,378,284,14,386,77,102,233,78,181,307,283,152,84,312,348,37,184,110,82,175,170,121,87,233,386,124,340,364,128,87,361,125,350,343,373,281,18,18,249,281,42,361,377,157,360,47,35,279,310,229,94,332,181,125,299,84,304,48,48,375,92,370,0,70,130,338,45,357,10,359,157,83,25,92,283,348,390,84,96,269,363,168,348,7,35,323,375,383,16,357,366,57,360,345,240,278,314,320,208,86,92,77,353,149,140,378,348,311,30,128,216,197,39,345,164,249,164,354,237,352,294,83,243,267,225,385,286,179,328,14,309,100,146,134,82,345,335,92,325,387,404,305,18,221,37,330,131,125,128,133,314,341,57,314,319,281,243,389,356,51,62,330,343,114,104,31,153,10,240,114,84,84,297,294,326,345,20,103,184,335,176,239,19,350,179,162,167,116,169,332},
				{323,350,130,382,332,361,14,70,286,345,0,341,173,382,361,357,14,377,83,357,330,338,330,36,36,331,305,388,164,278,248,349,332,386,136,246,62,47,373,316,62,330,369,136,129,250,40,123,342,311,74,88,301,50,330,371,297,349,250,125,252,385,325,0,175,58,249,386,309,250,37,120,127,127,128,362,356,77,389,329,143,136,35,227,356,224,241,320,348,209,171,186,148,386,242,378,250,295,237,376,194,345,129,114,278,223,134,204,97,110,62,371,250,348,152,162,98,129,121,282,96,251,374,122,340,127,90,152,395,388,385,325,354,282,31,299,35,125,342,347,75,354,341,3,62,386,233,196,58,297,284,189,372,125,365,127,136,345,369,35,57,130,393,329,243,131,8,63,393,11,337,87,375,212,388,44,232,325,120,325,330,107,248,152,183,329,80,19,232,11,347},
				{369,226,378,208,348,92,74,74,378,392,380,333,65,233,375,129,23,55,250,286,316,50,145,212,334,21,392,198,286,128,127,0,125,241,328,325,129,252,212,134,212,178,175,163,328,251,389,245,348,62,387,231,374,354,73,45,352,375,367,35,320,305,84,0,389,257,367,318,244,369,63,309,92,172,360,14,38,175,168,305,375,297,330,153,92,385,244,375,226,387,166,362,254,318,134,332,376,62,303,124,375,378,150,245,343,357,164,350,90,330,84,232,304,345,135,345,349,370,238,278,388,171,83,40,119,181,250,280,110,91,188,340,335,296,335,136,125,278,58,32,294,15,12,249,12,75,78,286,92,352,110,340,329,91,373,152,232,348,164,241,8,88,173,63,175,34,62,320,208,198,5,344,65,68,139,278,279,203,87,349,36,284,104,343,169,355,375,3,348,232,209},
				{20,50,136,170,302,127,127,168,73,100,55,14,14,134,127,73,39,92,329,356,209,375,356,375,40,71,325,173,170,332,325,158,255,335,212,302,250,252,51,390,330,153,241,170,57,82,309,74,95,159,258,179,2,92,134,146,0,345,186,141,250,404,160,0,320,375,345,309,59,350,354,34,367,102,137,356,336,56,341,79,258,278,152,286,319,241,303,75,199,241,184,264,152,243,381,114,116,231,316,272,56,148,65,349,163,115,91,355,182,352,167,37,336,232,348,26,128,200,70,243,92,323,338,241,333,84,355,357,81,92,164,75,134,232,375,265,332,32,48,246,329,243,241,278,15,378,147,348,196,88,235,173,6,198,134,254,247,37,84,171,211,349,184,350,248,193,311,243,348,256,14,47,134,314,121,378,313,127,84,258,337,251,121,343,194,241,128,38,126,179,350},
				{250,152,62,171,56,5,373,79,152,346,231,11,97,97,228,378,261,75,6,377,375,220,143,372,375,284,237,77,95,134,334,101,344,139,251,377,386,375,281,167,354,280,353,355,349,372,323,84,247,377,348,171,372,369,167,386,121,324,46,121,140,348,231,0,243,103,209,237,100,11,178,225,167,264,262,92,346,332,42,49,292,344,353,114,286,84,386,84,357,12,369,354,357,306,97,373,331,343,93,130,181,300,114,377,140,14,284,328,353,75,186,353,164,316,332,84,299,353,62,282,100,167,350,45,131,241,330,295,130,200,241,6,286,32,312,354,119,14,84,316,57,52,345,389,58,125,323,350,97,107,242,83,282,333,325,31,139,241,153,330,75,10,224,313,174,225,341,124,373,284,263,196,76,11,327,109,92,164,9,125,293,159,350,354,297,188,69,330,166,50,170},
				{355,175,334,33,381,325,341,342,372,128,369,21,168,82,79,170,91,273,140,119,348,92,383,77,354,125,114,162,229,164,164,175,170,56,377,167,357,47,391,15,156,47,348,15,15,0,8,47,15,233,216,216,373,46,348,259,79,284,263,31,88,354,348,0,241,367,324,316,384,309,333,309,75,376,34,208,375,373,378,306,348,250,121,68,353,200,348,129,169,343,73,132,353,249,62,131,348,250,147,340,168,121,343,136,265,391,92,188,79,241,114,335,114,325,348,327,296,14,109,303,263,343,332,90,164,123,250,113,79,212,189,216,348,152,166,55,348,330,361,228,353,168,86,88,309,187,317,35,84,236,215,129,128,358,348,404,137,310,347,2,199,92,104,7,134,239,169,243,100,65,367,344,246,354,3,341,19,153,355,286,76,41,125,293,369,221,271,179,350,375,107},
				{0,329,166,301,121,97,157,12,54,378,231,353,377,130,354,46,309,302,355,221,305,299,345,37,309,284,373,353,357,75,123,302,325,201,220,39,343,302,284,375,355,297,180,100,370,372,22,37,297,73,284,174,375,54,256,243,341,354,348,75,168,284,181,0,341,369,308,77,15,15,164,355,353,179,164,313,241,278,354,263,323,84,141,18,336,282,355,53,123,9,134,15,321,152,102,284,148,372,335,175,392,51,88,119,232,337,385,123,390,202,348,318,56,284,21,37,92,147,59,161,367,1,220,84,198,348,303,164,75,14,11,104,246,349,358,138,86,98,171,68,214,74,203,353,294,152,92,282,121,55,169,348,334,14,47,101,121,339,348,225,150,74,92,192,189,367,216,93,197,181,37,160,94,76,367,301,362,330,393,92,83,363,221,191,170,381,377,306,116,52,276},
				{106,331,92,115,337,330,102,221,159,345,36,69,404,84,354,159,124,94,20,330,250,334,241,247,377,388,237,162,208,353,239,333,134,352,127,348,186,267,11,286,354,357,115,153,333,333,296,305,194,330,263,40,386,388,347,329,333,357,127,352,184,33,175,0,168,381,116,127,323,127,340,61,14,343,14,366,325,374,184,170,74,188,347,272,150,83,188,155,166,360,31,353,241,345,31,360,251,351,57,20,66,159,164,287,46,367,55,201,164,153,129,352,153,133,296,349,37,129,174,148,328,121,246,5,297,70,143,212,110,35,301,91,1,76,109,398,2,2,301,111,314,37,229,162,26,234,241,350,13,296,62,189,231,136,168,240,41,128,222,246,175,28,61,109,130,342,348,269,232,263,29,123,202,325,125,201,178,116,171,353,354,247,354,119,325,375,229,305,39,363,104},
				{330,360,375,354,349,391,184,373,69,373,355,84,310,242,238,39,391,46,353,349,240,391,106,383,175,158,345,306,369,304,383,332,70,304,249,45,357,78,143,296,39,54,357,352,4,367,126,234,35,354,164,362,348,126,304,238,216,348,345,348,210,263,285,0,345,171,189,189,210,336,128,284,189,345,15,284,348,330,130,249,348,168,59,371,171,238,249,6,373,375,6,176,84,200,64,332,127,325,328,182,370,84,79,92,241,243,241,250,344,332,5,399,158,6,116,143,68,124,339,56,96,184,193,348,68,127,343,374,160,157,295,345,97,334,104,146,136,261,220,322,359,325,354,54,174,386,186,283,295,92,370,170,61,18,348,63,37,227,50,343,346,115,157,330,32,120,220,375,128,250,270,79,249,3,101,180,215,357,331,228,81,261,150,285,300,14,135,164,354,332,349},
				{147,42,386,79,354,329,107,89,241,183,260,62,294,330,348,30,348,316,330,405,243,249,125,371,347,349,224,80,373,337,69,356,376,84,232,265,336,75,136,73,96,349,243,8,330,258,33,215,82,357,348,246,232,104,114,349,137,64,348,376,330,87,260,0,123,303,141,355,175,0,326,74,386,208,316,284,193,232,143,171,19,303,116,20,232,354,294,393,21,377,310,124,373,284,389,375,134,298,348,104,179,208,281,50,189,116,29,325,325,186,203,370,131,136,168,335,343,119,42,123,300,349,68,284,305,319,330,258,354,37,348,333,20,354,122,29,296,196,339,130,270,381,202,232,329,102,228,339,333,320,3,354,14,94,5,130,31,335,170,350,286,59,53,31,329,375,223,38,329,139,32,43,295,15,215,116,40,231,55,346,393,208,84,33,348,348,314,345,27,139,343},
				{312,348,348,341,371,228,110,273,283,389,389,20,378,37,358,238,314,243,84,368,184,345,84,164,356,232,346,8,20,129,250,340,374,198,330,330,62,124,198,304,282,348,294,152,138,11,385,337,114,128,37,145,179,237,284,197,143,349,137,377,38,333,314,0,196,162,92,354,340,258,392,375,293,164,332,337,251,232,348,378,333,194,149,220,332,332,339,137,29,160,92,232,263,189,348,349,101,143,357,11,295,243,153,154,348,309,251,245,337,81,32,324,39,116,52,357,153,75,163,383,127,14,297,243,352,131,127,38,40,170,188,310,121,289,345,325,114,193,141,302,404,154,133,159,354,32,330,326,122,75,250,252,110,310,252,140,382,226,244,270,241,294,29,83,355,198,190,73,189,97,15,326,108,325,161,88,286,173,175,346,294,74,171,184,179,341,226,320,114,193,131},
				{3,67,353,273,142,243,294,0,64,107,356,375,145,249,334,372,353,361,309,54,98,114,376,316,404,188,159,323,386,357,11,297,354,31,313,20,107,124,114,135,83,31,323,375,320,341,329,354,311,152,385,177,337,246,282,107,177,347,134,366,134,332,312,0,183,240,164,226,356,36,90,164,307,19,378,373,316,172,391,134,32,355,125,89,64,306,32,178,149,171,232,175,164,244,239,135,193,119,325,169,124,373,349,121,378,92,340,121,306,343,126,172,116,343,263,297,329,157,325,385,135,399,9,330,186,119,378,241,330,295,70,385,232,190,88,81,48,282,100,175,289,335,345,190,249,296,120,340,230,125,341,328,186,345,246,143,281,183,350,240,164,263,302,122,14,339,84,305,227,194,120,350,334,158,26,116,179,119,166,378,348,175,341,88,286,294,278,280,314,31,157},
				{391,231,3,232,332,344,383,164,143,146,302,154,70,383,44,386,374,12,212,387,74,345,66,348,243,383,92,354,130,100,184,32,319,330,166,306,97,330,286,373,404,188,175,117,16,86,157,144,80,329,200,323,116,60,332,0,123,167,78,187,139,346,284,0,166,104,332,70,316,325,323,84,262,92,75,137,373,309,367,116,346,71,331,354,145,283,120,325,84,371,29,245,284,114,153,310,127,299,164,57,31,166,263,240,14,109,385,107,335,350,356,82,109,6,10,102,58,189,353,35,214,243,243,329,168,243,119,125,125,388,344,329,132,363,348,77,124,131,56,115,227,21,164,249,334,309,253,153,325,119,175,42,120,249,331,5,96,302,172,332,170,134,335,191,299,156,343,121,317,375,142,375,92,75,31,330,134,31,131,217,189,84,14,354,40,293,81,345,329,354,14},
				{127,115,315,387,119,347,193,125,71,125,295,153,116,341,372,129,373,15,376,386,339,347,186,225,2,87,243,186,301,302,227,331,166,301,139,389,347,400,327,261,305,301,244,164,293,383,20,226,268,232,271,89,386,329,350,120,311,172,139,300,284,296,323,0,109,373,373,179,348,355,305,212,330,124,91,180,135,295,389,301,0,83,326,199,175,301,223,389,317,339,100,341,166,296,182,196,179,130,386,102,215,354,294,55,377,278,245,344,256,286,176,128,236,29,164,237,321,332,301,62,325,75,131,16,129,32,14,31,217,5,355,313,329,96,196,232,2,236,37,164,345,244,234,32,162,370,330,278,14,211,196,290,86,86,84,217,386,129,136,10,361,294,84,388,348,217,303,334,117,233,164,241,143,383,24,375,329,5,172,278,16,48,31,92,333,128,354,377,91,323,152},
				{14,325,172,62,270,387,348,175,145,244,52,241,32,305,182,352,31,83,131,16,286,404,339,48,173,360,360,26,164,331,330,141,290,13,370,243,36,175,117,92,240,299,244,238,170,273,339,332,36,153,294,172,203,120,235,299,79,127,251,346,305,333,354,0,128,392,168,245,160,73,336,360,241,125,376,61,344,350,73,136,400,360,240,312,0,370,66,278,139,125,81,299,175,135,121,354,166,357,232,286,88,335,84,377,84,40,56,148,108,126,94,334,309,171,241,81,279,103,74,285,172,251,323,386,143,333,309,191,121,294,15,96,383,193,130,373,263,90,164,348,125,18,234,182,162,195,272,335,168,366,126,243,127,119,390,220,92,42,359,352,66,92,325,18,193,136,143,173,63,56,238,193,124,77,95,340,344,113,220,179,330,121,166,241,350,241,116,154,345,48,323},
				{385,272,243,385,162,14,312,341,347,74,104,305,250,82,55,348,2,387,343,64,378,113,383,92,212,110,123,247,18,350,153,214,340,125,125,243,29,375,192,164,162,92,390,152,279,231,156,70,356,46,404,16,10,175,332,151,173,75,175,125,158,20,243,0,220,31,333,125,330,100,25,212,162,51,250,226,179,390,176,393,348,136,305,378,158,330,346,337,228,337,183,344,375,332,354,348,32,35,134,184,354,355,331,203,2,305,130,120,350,52,48,341,370,240,24,250,175,14,360,326,325,357,306,329,280,241,29,180,164,232,191,220,265,125,40,36,48,334,80,224,378,200,375,372,173,133,237,175,331,334,353,375,320,375,171,96,62,164,320,36,142,160,344,256,375,373,101,241,313,155,133,117,113,102,129,176,135,3,340,79,97,84,136,386,301,171,40,82,70,243,371},
				{375,350,120,386,20,305,261,13,172,341,156,31,370,370,133,5,65,136,168,208,187,180,289,278,278,187,345,14,107,14,278,243,141,13,114,266,270,332,332,125,193,331,291,69,367,203,369,231,331,171,100,14,266,110,323,91,91,78,40,333,102,193,0,0,154,167,404,313,276,134,356,356,31,250,170,305,277,154,345,326,345,354,122,354,326,333,10,333,250,220,326,226,349,325,349,155,252,179,44,167,104,125,62,134,49,282,201,367,166,158,92,236,134,249,71,44,35,404,29,134,341,186,350,373,81,302,300,335,90,52,90,293,18,261,378,335,130,244,241,40,305,378,116,88,356,247,320,136,127,246,175,392,330,134,355,162,345,170,20,29,353,5,196,50,279,310,357,166,234,57,92,18,348,258,75,97,143,175,375,348,373,116,164,220,284,375,251,175,372,209,324},
				{158,281,251,0,310,212,302,139,110,252,43,323,69,241,153,305,323,234,332,330,121,31,232,305,119,302,326,0,107,139,281,121,302,119,161,336,348,375,349,346,69,75,283,229,145,357,375,232,36,75,250,62,171,375,19,74,286,299,77,102,354,137,127,0,337,45,329,331,278,283,240,373,332,59,229,348,72,178,349,134,373,175,283,332,349,378,346,259,331,38,345,349,354,62,325,212,383,46,386,49,175,170,325,125,244,124,92,92,140,61,87,125,361,223,171,21,13,366,232,238,86,156,88,109,304,90,363,336,357,102,110,360,184,354,32,208,241,283,347,33,345,121,21,107,102,341,153,2,194,319,343,220,330,202,12,284,152,348,375,386,87,130,373,157,249,92,57,84,31,92,330,62,354,96,130,250,125,126,236,184,97,329,97,92,134,125,233,142,245,301,7},
				{3,129,241,345,121,357,329,305,92,59,5,330,349,268,256,170,318,61,232,299,354,345,82,208,77,92,59,349,375,173,330,164,164,252,375,348,246,243,119,96,391,116,358,354,39,299,107,328,290,119,134,358,352,92,330,172,164,221,342,358,92,92,199,0,85,330,198,82,227,357,37,346,3,329,63,58,116,64,198,35,367,171,241,377,114,369,349,378,319,171,68,350,329,171,171,114,349,91,20,184,198,52,125,270,286,32,175,240,250,235,59,332,42,68,344,349,143,357,79,37,128,70,65,353,355,104,356,328,261,120,131,264,303,95,384,199,220,243,173,68,138,175,124,282,377,393,98,373,250,375,131,101,68,345,150,268,106,233,373,189,319,179,54,228,323,211,131,345,81,354,387,66,125,49,201,190,321,75,286,36,350,91,404,18,308,375,41,97,188,272,278},
				{181,181,357,172,188,18,329,325,155,330,348,57,308,354,173,56,92,227,346,325,0,330,92,329,188,162,164,301,2,81,56,173,171,348,83,164,10,110,110,110,19,247,310,373,36,247,167,131,310,81,190,10,310,121,347,66,29,243,258,62,70,329,258,0,246,215,349,268,199,321,389,8,348,220,306,14,129,114,228,19,349,302,306,129,3,112,95,242,313,350,299,311,341,146,13,302,370,166,170,354,330,348,276,336,330,16,268,134,296,16,16,286,96,97,116,81,92,158,354,229,184,369,131,208,172,348,346,325,73,244,284,116,378,341,303,347,346,329,107,187,33,345,116,354,325,71,10,341,15,318,172,182,279,357,348,354,79,299,332,136,51,294,306,340,330,164,116,132,116,81,284,294,332,375,250,116,84,393,189,375,47,373,311,241,31,330,385,330,255,307,304},
				{232,155,156,367,9,0,249,249,281,280,198,125,62,0,306,302,335,226,91,19,155,305,174,121,127,342,92,125,126,31,302,116,332,243,70,321,125,158,325,258,84,321,251,91,325,280,107,32,155,262,357,281,325,91,84,306,96,357,90,14,342,299,70,0,321,70,84,133,332,335,332,92,358,161,231,350,350,357,231,3,349,333,116,347,66,247,153,333,292,107,340,348,340,97,295,153,241,72,124,121,405,213,346,386,252,243,193,307,241,72,184,187,89,354,354,302,342,191,92,88,58,117,34,73,344,385,15,83,78,2,164,173,367,314,62,156,320,84,337,183,331,127,368,68,203,91,328,241,72,11,66,35,377,129,62,341,15,372,103,271,247,250,73,61,126,22,188,20,140,375,84,84,340,190,147,73,191,57,283,355,348,299,386,37,197,157,125,62,301,344,208},
				{108,354,323,82,73,62,63,226,238,171,34,131,162,246,127,75,75,325,335,155,282,348,348,143,72,354,237,162,311,124,315,121,153,124,179,313,36,350,357,116,107,131,318,318,236,157,318,386,386,44,66,157,304,107,243,151,389,332,246,348,269,309,75,0,75,345,326,345,354,381,345,332,336,348,355,271,310,310,126,153,162,160,234,380,182,50,220,109,127,97,343,345,121,278,263,188,18,299,232,265,296,153,184,143,194,234,36,140,170,97,66,14,251,310,79,373,281,323,317,84,89,97,164,345,232,158,164,92,366,110,100,135,130,86,348,238,371,168,316,42,278,187,30,405,21,303,134,8,243,80,141,315,123,224,354,374,97,143,238,21,20,356,198,304,63,293,284,341,302,14,208,232,74,142,160,77,353,36,124,203,347,36,64,349,139,378,66,67,117,251,105},
				{332,236,175,170,212,251,313,132,239,350,354,162,83,67,98,84,331,369,228,268,84,303,169,92,341,334,134,16,294,325,346,220,296,139,296,79,172,330,172,234,234,172,16,16,16,168,16,273,83,339,346,369,294,220,172,81,79,81,284,24,92,66,294,0,320,369,384,258,354,346,284,349,79,79,300,349,378,348,393,14,129,306,10,47,312,294,10,284,74,106,258,310,128,124,19,220,314,276,64,374,273,38,305,3,263,160,157,307,145,366,164,90,75,54,177,172,139,125,159,246,134,1,244,221,12,60,143,154,97,123,229,135,304,386,245,139,294,75,29,296,119,2,174,244,81,186,196,91,135,199,172,348,124,18,16,305,182,179,352,389,341,62,31,136,240,175,73,158,240,51,244,61,123,385,348,29,16,376,282,54,375,14,386,196,286,404,92,84,159,143,258},
				{47,333,346,135,77,161,330,20,205,75,21,137,69,297,354,330,220,154,104,284,125,383,19,332,21,347,304,81,370,325,100,75,218,357,116,120,121,369,115,110,337,82,325,97,29,295,305,20,90,330,153,172,296,332,347,326,196,305,182,14,354,339,130,0,270,318,381,278,37,57,348,241,256,36,325,333,369,82,106,188,345,196,31,333,179,360,202,66,320,74,124,161,330,69,137,104,97,373,92,273,209,84,255,102,128,109,330,238,65,116,352,133,355,10,373,97,68,175,250,37,294,77,375,98,339,176,20,164,116,354,332,309,329,191,3,11,45,116,75,47,186,329,220,125,350,372,172,168,128,133,354,116,175,100,19,66,129,164,278,164,353,88,143,57,367,228,378,341,124,344,348,375,160,329,54,332,276,378,81,97,325,354,354,57,156,348,115,54,174,124,272},
				{347,20,82,20,208,14,238,316,107,81,125,3,2,332,357,82,236,336,180,354,237,243,62,320,372,37,343,378,375,229,336,260,268,357,326,375,57,404,353,20,10,150,320,348,250,240,250,132,238,371,355,231,377,134,378,217,134,232,389,124,171,373,368,0,84,346,284,14,320,320,294,172,180,233,306,375,261,308,68,343,251,282,132,77,106,20,378,349,179,377,57,106,172,134,295,246,183,37,196,8,92,120,16,179,350,368,173,349,39,8,157,348,63,232,63,250,348,295,330,248,384,15,220,311,69,143,354,343,134,354,348,29,199,258,130,186,226,58,242,91,144,166,36,166,125,16,40,309,14,134,74,55,11,10,404,15,158,37,372,244,263,226,286,340,137,53,130,162,386,33,294,123,160,305,223,174,126,400,196,65,66,57,171,214,104,153,5,304,107,241,108},
				{167,107,316,341,318,137,75,36,337,294,175,383,377,281,233,154,306,157,387,143,241,241,345,81,272,345,91,220,329,233,48,233,243,81,120,243,123,354,309,251,331,387,153,262,294,98,320,318,296,243,36,375,172,231,308,330,26,70,345,355,388,334,278,0,241,375,290,175,330,179,370,196,220,24,234,48,250,14,375,354,340,117,20,297,334,170,369,73,172,320,28,63,334,312,345,179,369,129,110,350,143,107,75,381,212,182,271,109,263,236,116,243,249,92,30,121,91,188,84,232,265,332,109,129,95,69,349,37,107,97,143,164,353,254,367,249,171,36,353,248,124,203,375,294,250,313,155,241,128,346,197,282,372,280,329,164,40,84,244,247,241,330,354,81,104,104,348,232,311,251,323,159,188,45,261,201,84,153,143,84,296,354,353,174,249,15,197,246,5,182,183},
				{131,348,347,372,236,377,345,361,404,308,333,92,290,134,330,119,164,234,260,349,234,337,246,387,297,153,290,325,325,325,354,88,348,141,10,11,368,309,334,149,318,94,233,354,299,238,334,227,136,54,196,168,174,334,361,309,18,151,175,90,127,142,127,0,160,272,154,309,80,339,254,57,154,18,78,232,78,309,10,14,262,84,77,261,250,95,356,231,379,280,301,309,164,18,309,231,295,180,355,330,37,305,227,284,120,26,234,305,330,333,376,370,288,184,231,121,226,278,123,188,31,166,192,164,70,250,85,350,247,331,284,378,354,92,70,92,125,371,386,116,248,238,55,283,363,96,164,294,311,124,15,37,147,14,332,345,251,374,137,284,89,224,280,334,247,363,245,378,267,243,320,14,57,146,355,334,373,153,120,115,96,81,164,14,37,297,192,69,175,71,102},
				{374,387,292,125,74,116,156,241,166,373,231,180,270,182,182,275,340,169,243,243,202,155,214,18,255,69,69,203,333,330,69,107,69,69,133,354,354,345,164,336,102,387,172,335,279,186,241,241,86,340,354,318,241,167,88,234,14,241,340,39,345,369,354,0,59,258,129,6,114,320,64,92,354,117,245,301,108,319,196,129,282,8,355,232,404,326,116,175,358,80,82,199,353,233,196,110,331,63,360,61,20,157,377,354,354,38,330,212,405,42,278,348,14,376,8,80,91,175,378,363,330,280,325,188,285,26,38,188,246,211,124,249,276,14,62,244,301,129,305,330,332,189,6,164,311,330,47,24,170,387,270,115,360,47,340,262,249,126,93,92,348,171,330,378,81,125,233,110,348,189,375,241,241,220,59,84,250,133,245,169,386,360,201,372,126,37,66,88,377,348,97},
				{335,107,232,171,320,20,249,237,84,14,92,325,250,64,20,353,103,232,209,374,303,228,84,367,134,97,284,68,56,302,242,286,115,373,77,3,325,371,378,349,169,179,314,348,348,19,325,128,148,335,332,91,121,160,84,142,339,74,137,309,190,280,367,0,301,246,354,205,372,103,94,297,329,249,282,240,119,311,164,276,276,142,188,36,164,388,330,352,282,386,241,374,333,205,45,92,64,393,320,332,194,375,345,277,36,316,162,154,229,116,309,340,106,305,160,21,128,330,134,164,343,284,54,73,248,342,50,17,10,302,125,84,244,375,84,278,164,320,124,20,303,153,127,14,332,332,92,128,167,92,46,118,37,375,63,20,375,125,65,41,376,134,74,54,233,262,212,226,241,40,136,240,79,50,378,170,31,169,390,335,375,64,196,339,196,232,116,136,279,97,391},
				{245,97,377,68,157,81,208,348,154,175,133,32,131,178,171,388,164,188,387,375,209,116,354,62,284,282,119,309,115,374,378,156,387,364,15,15,119,251,364,325,325,354,45,262,367,120,166,345,249,249,125,14,75,346,84,330,332,331,325,116,284,267,341,0,326,243,109,329,227,272,172,180,284,284,154,386,308,302,220,91,352,243,241,326,267,282,166,5,161,121,193,92,311,250,125,282,182,95,297,375,386,247,167,354,164,326,313,92,229,81,276,349,2,332,181,35,134,10,311,386,349,354,46,328,193,355,320,383,300,137,134,179,250,39,248,176,329,360,197,5,172,11,336,353,80,6,237,209,175,297,84,115,303,102,129,325,77,92,128,333,343,94,164,166,125,284,311,103,276,119,92,247,171,388,81,154,32,97,208,209,64,284,386,82,65,75,249,84,120,15,272},
				{330,249,16,92,92,378,126,339,381,352,370,141,342,17,354,250,333,20,128,343,297,120,392,320,278,79,136,170,343,191,330,366,333,82,100,115,150,364,272,369,88,102,350,164,32,162,286,2,263,92,354,116,286,164,169,179,192,373,333,75,175,100,164,0,332,59,92,319,6,110,353,367,20,232,142,314,364,115,150,367,137,352,241,282,14,32,309,326,325,251,126,227,243,325,330,116,88,5,241,302,100,220,161,16,342,182,193,352,102,278,392,164,55,346,55,127,182,125,134,262,96,11,129,315,148,97,62,115,91,143,14,174,242,151,18,70,186,6,345,167,334,324,37,333,345,312,306,352,0,154,184,254,311,92,386,339,251,133,18,119,169,185,121,189,92,137,378,189,241,5,281,170,56,40,353,331,348,250,70,164,247,24,3,82,353,329,345,330,249,107,367},
				{369,100,252,348,84,164,375,21,345,84,371,92,83,263,345,284,83,194,10,355,375,116,246,102,82,326,369,284,354,85,220,136,136,232,119,373,10,345,344,374,80,83,326,223,56,91,263,171,192,84,320,326,164,15,375,91,355,47,250,334,37,57,134,0,220,97,376,354,346,354,344,312,354,310,350,72,329,77,98,2,375,345,113,334,124,169,378,346,348,114,175,263,197,91,258,334,339,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{377,37,179,113,209,75,176,124,92,316,135,320,240,329,135,354,20,137,137,14,330,137,134,316,131,309,75,75,152,116,329,282,157,128,226,175,232,286,84,344,385,231,249,243,11,64,175,252,125,134,316,344,355,241,164,347,383,146,74,154,283,241,131,0,348,348,247,386,157,68,241,317,94,134,250,348,392,92,286,0,37,191,262,344,249,309,116,309,75,129,184,84,45,316,345,110,355,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{233,154,186,116,350,375,329,134,68,244,80,116,350,355,332,327,284,110,40,305,331,267,172,92,116,243,343,107,47,47,348,220,345,92,164,310,147,309,309,348,316,180,334,90,310,35,92,317,372,5,220,348,223,37,375,172,352,404,14,290,385,354,329,0,136,349,309,294,128,348,117,14,350,297,120,79,128,168,345,90,133,332,332,316,181,392,354,350,175,317,332,342,348,232,378,179,330,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{348,92,363,354,369,347,346,232,214,116,189,350,188,62,356,354,162,21,175,110,173,290,356,350,251,250,164,176,284,244,334,171,342,113,377,329,154,243,94,355,297,110,249,348,327,172,92,348,348,119,369,188,290,175,130,171,125,332,54,125,125,345,353,0,50,228,50,228,378,125,189,354,172,35,253,170,208,133,253,131,294,333,345,124,189,164,370,131,241,171,345,50,181,110,33,84,190,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{190,84,228,250,250,197,84,345,157,190,37,83,250,190,196,196,388,0,82,120,130,101,310,315,117,152,114,37,111,348,57,330,319,196,243,369,345,186,345,356,305,345,345,345,373,373,384,24,348,186,369,345,70,175,82,193,355,50,250,371,321,378,375,0,188,5,17,320,14,371,189,373,386,97,68,82,355,32,90,241,355,305,308,286,81,326,81,64,320,376,250,283,284,284,313,330,131,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{122,330,345,316,294,332,162,220,329,329,148,335,110,339,327,367,216,202,386,368,92,386,92,92,37,35,35,110,344,349,335,10,363,335,386,37,345,367,311,171,171,40,251,335,349,212,133,220,46,357,354,40,348,208,367,388,250,357,345,354,75,328,348,0,54,388,63,40,130,344,110,349,183,227,328,227,345,183,173,95,143,55,171,55,249,249,17,44,110,136,335,356,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}
            };

            //罗马字母
            string[] _charIndex = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "", "", "", "", "", "", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "", "", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "", "", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "", "" };

            //希腊字母
            string[] _charIndex2 = new string[] { "a", "b", "g", "d", "e", "z", "e", "th", "i", "k", "l", "m", "n", "x", "o", "p", "r", "s", "t", "u", "ph", "kh", "ps", "o" };
            #endregion

            Encoding encoding = Encoding.GetEncoding("GB2312");

            byte[] bytValue = encoding.GetBytes(Convert.ToString(value));

            int intIndex = 0;
            int intCharNum;
            string strValue = "";

            while (intIndex < bytValue.Length)
            {
                // 是否为GBK 字符
                if ((bytValue[intIndex] >= 129) && (bytValue[intIndex + 1] >= 64))
                {
                    switch ((int)bytValue[intIndex])
                    {
                        case 163:	// 全角 ASCII
                            {
                                if (notChineseDisplay)
                                {
                                    strValue += ((char)(bytValue[intIndex + 1] - 128)).ToString();
                                }
                                break;
                            }

                        case 162: // 罗马数字
                            {
                                if (bytValue[intIndex + 1] > 160)
                                {
                                    strValue += _charIndex[(int)bytValue[intIndex + 1] - 160];
                                }
                                break;
                            }

                        case 166: // 希腊字母
                            {
                                break;
                            }

                        default:
                            {
                                intCharNum = _spellCodeIndex[(int)bytValue[intIndex] - 129, bytValue[intIndex + 1] - 64] - 1;

                                // 小于等于“0”的表示：无此汉字，不能翻译的字符, GBK 保留
                                if (intCharNum > 0)
                                {
                                    if (strValue.Length > 0 && space.Length > 0)
                                    {
                                        if (strValue.Substring(strValue.Length - 1, 1) != space)
                                        {
                                            strValue += space;
                                        }
                                    }

                                    if (isCapital)
                                    {
                                        strValue += _spellMusicCode[intCharNum].ToUpper() + space;
                                    }
                                    else
                                    {
                                        strValue += _spellMusicCode[intCharNum] + space;
                                    }
                                }
                                break;
                            }
                    }
                    intIndex += 2;
                }
                else // 在 GBK 字符集外, 即半角字符
                {
                    if (notChineseDisplay || (((bytValue[intIndex] >= 97) && (bytValue[intIndex] <= 122)) || ((bytValue[intIndex] >= 65) && (bytValue[intIndex] <= 90)) || ((bytValue[intIndex] >= 48) && (bytValue[intIndex] <= 57))))
                    {
                        strValue += ((char)bytValue[intIndex]).ToString();
                    }

                    intIndex++;
                }

            }

            if (strValue.Length > 0 && space.Length > 0)
            {
                if (strValue.Substring(strValue.Length - 1, 1) == space)
                {
                    strValue = strValue.Substring(0, strValue.Length - 1);
                }
            }
            return strValue;
        }

        /// <summary>
        /// 将字符串截取至指定长度
        /// </summary>
        /// <param name="value">欲处理的字符串</param>
        /// <param name="interceptLength">截取长度。返回的字符串字节长度一定不超过该值。如果该值为“0”则不进行截取操作</param>
        /// <returns></returns>
        public static string StringInterceptFill(object value, int interceptLength)
        {
            return StringInterceptFill(value, interceptLength, 0, "", false);
        }

        /// <summary>
        /// 将字符串填充至指定长度
        /// </summary>
        /// <param name="value">欲处理的字符串</param>
        /// <param name="fillLength">填充长度。当字符串的字节长度没有达到该值则使用 fillValue 进行填充，使整个字符串的字节长度小于等于指定的填充长度。如果该值为“0”则不进行填充操作</param>
        /// <param name="fillValue">如果设定了该值，并且 value 的长度没有达到 fillLength 的设定值，则进行字符串的填充操作，以使该字符串长度达到指定值。当填充值为空时将使用空格代替</param>
        /// <param name="fillRight">填充位置。true：填充在字符串的右侧； false：填充在字符串的左侧</param>
        /// <returns></returns>
        public static string StringInterceptFill(object value, int fillLength, string fillValue, bool fillRight)
        {
            return StringInterceptFill(value, 0, fillLength, fillValue, fillRight);
        }

        /// <summary>
        /// 字符串截取与填充。该函数将先执行截取操作，然后再执行填充操作。
        /// </summary>
        /// <param name="value">欲处理的字符串</param>
        /// <param name="interceptLength">截取长度。返回的字符串字节长度一定不超过该值。如果该值为“0”则不进行截取操作</param>
        /// <param name="fillLength">填充长度。当字符串的字节长度没有达到该值则使用 fillValue 进行填充，使整个字符串的字节长度小于等于指定的填充长度。如果该值为“0”则不进行填充操作</param>
        /// <param name="fillValue">如果设定了该值，并且 value 的长度没有达到 fillLength 的设定值，则进行字符串的填充操作，以使该字符串长度达到指定值。当填充值为空时将使用空格代替</param>
        /// <param name="fillRight">填充位置。true：填充在字符串的右侧； false：填充在字符串的左侧</param>
        /// <returns></returns>
        public static string StringInterceptFill(object value, int interceptLength, int fillLength, string fillValue, bool fillRight)
        {
            string strVaue = "";
            try
            {
                if (value == System.DBNull.Value)
                {
                    strVaue = "";
                }
                else if (value == null)
                {
                    strVaue = "";
                }
                else
                {
                    strVaue = Convert.ToString(value);
                }

                //截取操作
                if (interceptLength > 0)
                {
                    int intLength = 0;
                    string strInterceptVaue = "";

                    for (int intIndex = 0; intIndex < strVaue.Length; intIndex++)
                    {
                        string strChar = strVaue.Substring(intIndex, 1);
                        int intCharLength = Encoding.Default.GetBytes(strChar).Length;
                        if ((intLength + intCharLength) <= interceptLength)
                        {
                            strInterceptVaue += strChar;
                            intLength += intCharLength;
                        }
                        else
                        {
                            break;
                        }
                    }

                    strVaue = strInterceptVaue;
                }

                //填充操作
                if (fillLength > 0)
                {
                    int intLength = 0;

                    for (int intIndex = 0; intIndex < strVaue.Length; intIndex++)
                    {
                        intLength += Encoding.Default.GetBytes(strVaue.Substring(intIndex, 1)).Length;
                    }

                    if (fillValue == null)
                    {
                        fillValue = " ";
                    }
                    else if (fillValue.Length == 0)
                    {
                        fillValue = " ";
                    }

                    int intFillValueLength = GetByteLength(fillValue);

                    while ((intLength + intFillValueLength) <= fillLength)
                    {
                        if (fillRight)
                        {
                            strVaue = strVaue + fillValue;
                        }
                        else
                        {
                            strVaue = fillValue + strVaue;
                        }
                        intLength += intFillValueLength;
                    }
                }
            }
            catch
            {
            }
            return strVaue;
        }

        /// <summary>
        /// 数字转为汉字后的样式
        /// </summary>
        public enum CharacterStyle
        {
            /// <summary>
            /// 汉字的“一二三”
            /// </summary>
            Character = 0,

            /// <summary>
            /// 大写的“壹贰叁”
            /// </summary>
            Capitalization = 1
        }

        /// <summary>
        /// 将输入的数字转为汉字数字描述
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToCharacter(string value)
        {
            return ToCharacter(value, CharacterStyle.Character);
        }

        /// <summary>
        /// 将输入的数字转为汉字数字描述
        /// </summary>
        /// <param name="value"></param>
        /// <param name="characterStyle">数字转为汉字后的样式</param>
        /// <returns></returns>
        public static string ToCharacter(int value, CharacterStyle characterStyle)
        {
            return ToCharacter(value.ToString(), CharacterStyle.Character);
        }

        /// <summary>
        /// 将输入的数字转为汉字数字描述
        /// </summary>
        /// <param name="value">欲转换的数字</param>
        /// <param name="characterStyle">数字转为汉字后的样式</param>
        /// <returns></returns>
        public static string ToCharacter(object value, CharacterStyle characterStyle)
        {
            if (IsNumeric(value))
            {
                string strValue = "";
                string[] strChar = new string[0];

                switch (characterStyle)
                {
                    case CharacterStyle.Character:
                        strChar = new string[] { "○", "一", "二", "三", "四", "五", "六", "七", "八", "九" };
                        break;
                    case CharacterStyle.Capitalization:
                        strChar = new string[] { "零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖" };
                        break;
                }

                foreach (System.Char chrValue in (string)value)
                {
                    if (chrValue == 45)
                    {
                        strValue += "负";
                    }
                    else if (chrValue == 46)
                    {
                        strValue += "点";
                    }
                    else
                    {
                        strValue += strChar[chrValue - 48];
                    }
                }

                return strValue;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 输出字符到页面或控件上
        /// </summary>
        /// <param name="value">输入的字符串</param>
        /// <returns>处理后的字符串</returns>
        public static string OutString(object value)
        {
            return OutString(value, "", "", "");
        }

        /// <summary>
        /// 输出字符到页面或控件上
        /// </summary>
        /// <param name="value">输入的字符串</param>
        /// <param name="replaceNull">如果输入的字符串为 DBNull 或“空”时返回的值</param>
        /// <returns>处理后的字符串</returns>
        public static string OutString(object value, string replaceNull)
        {
            return OutString(value, replaceNull, "", "");
        }

        /// <summary>
        /// 输出字符到页面或控件上
        /// </summary>
        /// <param name="value">输入的字符串</param>
        /// <param name="replaceNull">如果输入的字符串为 DBNull 或“空”时返回的值</param>
        /// <param name="postfix">后缀</param>
        /// <returns>处理后的字符串</returns>
        public static string OutString(object value, string replaceNull, string postfix)
        {
            return OutString(value, replaceNull, "", postfix);
        }

        /// <summary>
        /// 输出字符到页面或控件上
        /// </summary>
        /// <param name="value">输入的字符串</param>
        /// <param name="replaceNull">如果输入的字符串为 DBNull 或“空”时返回的值</param>
        /// <param name="prefix">前缀</param>
        /// <param name="postfix">后缀</param>
        /// <returns>处理后的字符串</returns>
        public static string OutString(object value, string replaceNull, string prefix, string postfix)
        {
            try
            {
                if (value == System.DBNull.Value)
                {
                    return replaceNull;
                }
                else if (value == null)
                {
                    return replaceNull;
                }
                else
                {
                    if (Convert.ToString(value).TrimEnd(new char[] { ' ', '　', '\t' }).Length > 0)
                    {
                        return prefix + Convert.ToString(value).TrimEnd(new Char[] { ' ', '　', '\t' }) + postfix;
                    }
                    else
                    {
                        return replaceNull;
                    }
                }
            }
            catch
            {
                return replaceNull;
            }
        }

        /// <summary>
        /// 移除所有的换行符
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string RemoveNewLine(object value)
        {
            try
            {
                string strHtml = Convert.ToString(value);
                return strHtml.Replace("\r", "").Replace("\n", "");
            }
            catch
            {
                return "";
            }
        }

        #endregion

        #region << HTML 相关 >>

        /// <summary>
        /// 将输入的字符串转为适合 HTML 页面显示的内容
        /// </summary>
        /// <param name="value">HTML 内容</param>
        /// <returns></returns>
        public static string OutHtml(object value)
        {
            return OutHtml(value, "", "", "");
        }

        /// <summary>
        /// 将输入的字符串转为适合 HTML 页面显示的内容
        /// </summary>
        /// <param name="value">HTML 内容</param>
        /// <param name="replaceNull">如果输入的字符串为 DBNull 或“空”时返回的值</param>
        /// <returns></returns>
        public static string OutHtml(object value, string replaceNull)
        {
            return OutHtml(value, replaceNull, "", "");
        }

        /// <summary>
        /// 将输入的字符串加上前缀、后缀后转为适合 HTML 页面显示的内容
        /// </summary>
        /// <param name="value">HTML 内容</param>
        /// <param name="replaceNull">如果输入的字符串为 DBNull 或“空”时返回的值</param>
        /// <param name="prefix">前缀</param>
        /// <param name="postfix">后缀</param>
        /// <returns></returns>
        public static string OutHtml(object value, string replaceNull, string prefix, string postfix)
        {
            try
            {
                if (value == System.DBNull.Value)
                {
                    return replaceNull;
                }
                else if (value == null)
                {
                    return replaceNull;
                }
                else
                {
                    string strValue = Convert.ToString(value).TrimEnd(new Char[] { ' ' });

                    if (strValue.Length == 0)
                    {
                        return replaceNull;
                    }
                    else
                    {
                        strValue = strValue.Replace("&", "&amp;");
                        strValue = strValue.Replace("<", "&lt;");
                        strValue = strValue.Replace(">", "&gt;");
                        strValue = strValue.Replace("\"", "&quot;");
                        strValue = strValue.Replace(" ", "&nbsp;");
                        strValue = strValue.Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;");
                        strValue = strValue.Replace("\r\n", "<br>");
                        strValue = strValue.Replace("\n", "<br>");
                        return prefix + strValue + postfix;
                    }
                }
            }
            catch
            {
                return replaceNull;
            }
        }


        /// <summary>
        /// 将字符串输出为 JavaScript 语法所需的格式，如将换行替换为“\r\n”
        /// </summary>
        /// <param name="value"></param>
        /// <param name="replaceNull"></param>
        /// <param name="prefix"></param>
        /// <param name="postfix"></param>
        /// <returns></returns>
        public static string OutJavaScript(object value, string replaceNull, string prefix, string postfix)
        {
            try
            {
                if (value == System.DBNull.Value)
                {
                    return replaceNull;
                }
                else if (value == null)
                {
                    return replaceNull;
                }
                else
                {
                    string strValue = Convert.ToString(value);

                    if (strValue.Length == 0)
                    {
                        return replaceNull;
                    }
                    else
                    {
                        strValue = strValue.Replace("\r\n", "\\r\\n");
                        strValue = strValue.Replace("\r", "\\r");
                        strValue = strValue.Replace("\n", "\\n");
                        strValue = strValue.Replace("\"", "\\\"");
                        return prefix + strValue + postfix;
                    }
                }
            }
            catch
            {
                return replaceNull;
            }
        }


        /// <summary>
        /// 移除所有 HTML 标记。例如“&lt;p align=&quot;center&quot;&gt;居中&lt;/p&gt;”将返回“居中”
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string RemoveHTML(object value)
        {
            try
            {
                string strHtml = Convert.ToString(value);
                strHtml = System.Text.RegularExpressions.Regex.Replace(strHtml, "<(.|\n)+?>", "");
                strHtml = strHtml.Replace("&nbsp;", " ");
                strHtml = strHtml.Replace("&lt;", "<");
                strHtml = strHtml.Replace("&gt;", ">");
                strHtml = strHtml.Replace("&quot;", "\"");
                strHtml = strHtml.Replace("&amp;", "&");

                return strHtml;
            }
            catch
            {
                return "";
            }
        }

        private const string HtmlElementEvent = "onactivate|onafterprint|onbeforeactivate|onbeforecut|onbeforedeactivate|onbeforeeditfocus|onbeforepaste|onbeforeprint|onbeforeunload|onclick|oncontextmenu|oncontrolselect|oncut|ondblclick|ondeactivate|ondrag|ondragend|ondragenter|ondragleave|ondragover|ondragstart|ondrop|onfilterchange|onfocusin|onfocusout|onkeydown|onkeypress|onkeyup|onload|onlosecapture|onmousedown|onmouseenter|onmouseleave|onmousemove|onmouseout|onmouseover|onmouseup|onmousewheel|onmove|onmoveend|onmovestart|onpaste|onpropertychange|onreadystatechange|onresizeend|onresizestart|onscroll|onselect|onselectstart|onunload";

        /// <summary>
        /// 移除 HTML 中的脚本。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string RemoveHtmlScript(object value)
        {
            try
            {
                string strHtml = Convert.ToString(value);
                strHtml = System.Text.RegularExpressions.Regex.Replace(strHtml, @"<script(.|\n)*?</script([^>])*>", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                string strElementEventMatch = @"<\w[^>]*(" + HtmlElementEvent + ")(.|\n)*?>";
                strHtml = System.Text.RegularExpressions.Regex.Replace(strHtml, strElementEventMatch, new System.Text.RegularExpressions.MatchEvaluator(RemoveHtmlScriptElement));

                return strHtml;
            }
            catch
            {
                return "";
            }
        }

        private static string RemoveHtmlScriptElement(System.Text.RegularExpressions.Match match)
        {
            string strElement = match.Value;

            System.Text.RegularExpressions.Match matEvent = null;
            do
            {
                matEvent = System.Text.RegularExpressions.Regex.Match(strElement, "(" + HtmlElementEvent + ")\\s*=\\s*\\S", System.Text.RegularExpressions.RegexOptions.IgnoreCase);      //查找值是用单引号、双引号或没有引号括起来的
                if (matEvent.Success)
                {
                    string strAttributeName = System.Text.RegularExpressions.Regex.Match(matEvent.Value, @"\w*").Value;
                    string strSign = matEvent.Value.Substring(matEvent.Value.Length - 1, 1);                              //查找“element = ”后面的第一个字符
                    if (strSign == "\"" || strSign == "\'")
                    {
                        strElement = System.Text.RegularExpressions.Regex.Replace(strElement, strAttributeName + "\\s*=\\s*" + strSign + "(.|\n)*?" + strSign, "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    }
                    else
                    {
                        strElement = System.Text.RegularExpressions.Regex.Replace(strElement, strAttributeName + "\\s*=\\s*\\S*\\s*?[^>]", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    }
                }
            }
            while (matEvent.Success);

            return strElement;
        }

        /// <summary>
        /// 获取 HTML 元素指定的属性值。例如“&lt;input name='key' value='123'&gt;”指定属性名称为“value”可以获得返回值“123”
        /// </summary>
        /// <param name="element">一个完整的 HTML 元素内容</param>
        /// <param name="attributeName">欲获取的属性名称</param>
        /// <returns></returns>
        public static string GetElementAttributeValue(string element, string attributeName)
        {
            if (element == null)
            {
                return "";
            }

            try
            {
                System.Text.RegularExpressions.Match matSign = System.Text.RegularExpressions.Regex.Match(element, attributeName + "\\s*=\\s*\\S", System.Text.RegularExpressions.RegexOptions.IgnoreCase);      //查找值是用单引号、双引号或没有引号括起来的

                string strSign = matSign.Value.Substring(matSign.Value.Length - 1, 1);                              //查找“element = ”后面的第一个字符
                if (strSign == "\"" || strSign == "\'")
                {
                    System.Text.RegularExpressions.Match matValue = System.Text.RegularExpressions.Regex.Match(element, attributeName + "\\s*=\\s*" + strSign + "(.|\n)*?" + strSign, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    int intStartIndex = matValue.Value.IndexOf(strSign) + 1;
                    return matValue.Value.Substring(intStartIndex, matValue.Value.LastIndexOf(strSign) - intStartIndex);
                }
                else
                {
                    System.Text.RegularExpressions.Match matState = System.Text.RegularExpressions.Regex.Match(element, attributeName + "\\s*=\\s*", System.Text.RegularExpressions.RegexOptions.IgnoreCase);       //取前导字符，以便替换掉内容开头的部分
                    System.Text.RegularExpressions.Match matValue = System.Text.RegularExpressions.Regex.Match(element, attributeName + "\\s*=\\s*\\S*\\s*?[^>]", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    return matValue.Value.Substring(matState.Length).TrimEnd();
                }
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 将 HTML 元素中指定的属性值更改为指定值。如果指定的属性不存在，则将插入该属性及属性值。
        /// </summary>
        /// <param name="element">一个完整的 HTML 元素内容</param>
        /// <param name="attributeName">欲设置的属性名称</param>
        /// <param name="attributeValue">设置的属性值</param>
        /// <returns></returns>
        public static string SetElementAttributeValue(string element, string attributeName, string attributeValue)
        {
            if (element == null)
            {
                return "";
            }

            try
            {
                string strValue;
                if (attributeValue.IndexOf("\"") > -1)
                {
                    strValue = attributeName + "=\"" + attributeValue + "\"";
                }
                else
                {
                    strValue = attributeName + "='" + attributeValue + "'";
                }

                System.Text.RegularExpressions.Match matSign = System.Text.RegularExpressions.Regex.Match(element, attributeName + "\\s*=\\s*\\S", System.Text.RegularExpressions.RegexOptions.IgnoreCase);      //查找值是用单引号、双引号或没有引号括起来的
                if (matSign.Success)
                {
                    string strSign = matSign.Value.Substring(matSign.Value.Length - 1, 1);                              //查找“element = ”后面的第一个字符
                    if (strSign == "\"" || strSign == "\'")
                    {
                        return System.Text.RegularExpressions.Regex.Replace(element, attributeName + "\\s*=\\s*" + strSign + "(.|\n)*?" + strSign, strValue, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    }
                    else
                    {
                        return System.Text.RegularExpressions.Regex.Replace(element, attributeName + "\\s*=\\s*\\S*\\s*?[^>]", strValue, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    }
                }
                else
                {
                    int intInsertIndex = element.IndexOf("/>");
                    if (intInsertIndex == -1)
                    {
                        intInsertIndex = element.IndexOf(">");
                    }
                    if (intInsertIndex > -1)
                    {
                        if (element.Substring(intInsertIndex - 1, 1).Equals(" "))
                        {
                            element = element.Insert(intInsertIndex, strValue);
                        }
                        else
                        {
                            element = element.Insert(intInsertIndex, " " + strValue);
                        }
                    }
                    return element;
                }
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 移除 HTML 元素中指定的属性值。
        /// </summary>
        /// <param name="element">一个完整的 HTML 元素内容</param>
        /// <param name="attributeName">欲删除的属性名称</param>
        /// <returns></returns>
        public static string RemoveElementAttributeValue(string element, string attributeName)
        {
            if (element == null)
            {
                return "";
            }

            try
            {
                System.Text.RegularExpressions.Match matSign = System.Text.RegularExpressions.Regex.Match(element, attributeName + "\\s*=\\s*\\S", System.Text.RegularExpressions.RegexOptions.IgnoreCase);      //查找值是用单引号、双引号或没有引号括起来的

                string strSign = matSign.Value.Substring(matSign.Value.Length - 1, 1);                              //查找“element = ”后面的第一个字符
                if (strSign == "\"" || strSign == "\'")
                {
                    return System.Text.RegularExpressions.Regex.Replace(element, attributeName + "\\s*=\\s*" + strSign + "(.|\n)*?" + strSign, "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                }
                else
                {
                    return System.Text.RegularExpressions.Regex.Replace(element, attributeName + "\\s*=\\s*\\S*\\s*?[^>]", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                }
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 将 HTML 转换为可以直接显示的文本。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string HTMLToText(object value)
        {
            try
            {
                string strHtml = Convert.ToString(value);
                strHtml = System.Text.RegularExpressions.Regex.Replace(strHtml, "<br(.|\n)*?>", "\r\n", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                strHtml = System.Text.RegularExpressions.Regex.Replace(strHtml, "<p(.|\n)*?>", "\r\n", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                strHtml = System.Text.RegularExpressions.Regex.Replace(strHtml, "<(.|\n)+?>", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                strHtml = strHtml.Replace("&nbsp;", " ");
                strHtml = strHtml.Replace("&lt;", "<");
                strHtml = strHtml.Replace("&gt;", ">");
                strHtml = strHtml.Replace("&quot;", "\"");
                strHtml = strHtml.Replace("&amp;", "&");

                return strHtml;
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 将传入的文本转化为适于浏览器显示的 HTML 字符。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string TextToHTML(string value)
        {
            try
            {
                value = value.Replace("&", "&amp;");
                value = value.Replace(" ", "&nbsp;");
                value = value.Replace("<", "&lt;");
                value = value.Replace(">", "&gt;");
                value = value.Replace("\"", "&quot;");
                value = value.Replace("\r\n", "<br>");
                value = value.Replace("\n", "<br>");
                return value;
            }
            catch
            {
                return "";
            }
        }

        #endregion

        #region << 日期时间函数 >>

        /// <summary>
        /// 系统内的日期格式
        /// </summary>
        public enum DateStyle
        {
            /// <summary>
            /// 自动识别该日期格式
            /// </summary>
            Automatism = 0,

            /// <summary>
            /// 4位年，2位月，2位日。例如：20010310（2001年3月10日）
            /// </summary>
            YYYYMMDD = 1,

            /// <summary>
            /// 4位年，2位月，2位零。例如：20010300（2001年3月）
            /// </summary>
            YYYYMM00 = 2,

            /// <summary>
            /// 4位年，2位周，2位零。例如：20010300（2001年第三周）
            /// </summary>
            YYYYWW00 = 3,

            /// <summary>
            /// 4位年，2位季度，2位零。例如：20010300（2001年第三季度）
            /// </summary>
            YYYYQQ00 = 4,

            /// <summary>
            /// 4位年，4位零。例如：20010000（2001年）
            /// </summary>
            YYYY0000 = 5
        }

        /// <summary>
        /// 输出的日期样式
        /// </summary>
        public enum OutDateStyle
        {
            /// <summary>
            /// 输出样式：YYYY-MM-DD
            /// </summary>
            /// <remarks></remarks>
            YMD_Sign,
            /// <summary>
            /// 输出样式：YYYY年MM月DD日
            /// </summary>
            /// <remarks></remarks>
            YMD_Num,
            /// <summary>
            /// 输出样式：ＹＹＹＹ年ＭＭ月ＤＤ日 （其中全角的字母表示汉字文字的数字）
            /// </summary>
            /// <remarks></remarks>
            YMD_Char,

            /// <summary>
            /// 输出样式：YY-MM-DD
            /// </summary>
            YMD_ShortSign,
            /// <summary>
            /// 输出样式：YY年MM月DD日
            /// </summary>
            YMD_ShortNum,

            /// <summary>
            /// 输出样式：YYYY年MM月DD日 星期Ｗ （其中全角的字母表示汉字文字的数字）
            /// </summary>
            /// <remarks></remarks>
            YMDW_Num,
            /// <summary>
            /// 输出样式：ＹＹＹＹ年ＭＭ月ＤＤ日 星期Ｗ （其中全角的字母表示汉字文字的数字）
            /// </summary>
            /// <remarks></remarks>
            YMDW_Char,

            /// <summary>
            /// 输出样式：YYYY年 第N周
            /// </summary>
            /// <remarks></remarks>
            YW_Num,
            /// <summary>
            /// 输出样式：ＹＹＹＹ年 第Ｎ周 （其中全角的字母表示汉字文字的数字）
            /// </summary>
            /// <remarks></remarks>
            YW_Char,

            /// <summary>
            /// 输出样式：YYYY年 第N周(MM月DD日 - MM月DD日)
            /// </summary>
            /// <remarks></remarks>
            YWmd_Num,
            /// <summary>
            /// 输出样式：ＹＹＹＹ年 第Ｎ周(MM月DD日 - MM月DD日) （其中全角的字母表示汉字文字的数字）
            /// </summary>
            /// <remarks></remarks>
            YWmd_Char,

            /// <summary>
            /// 输出样式：YYYY-MM
            /// </summary>
            /// <remarks></remarks>
            YM_Sign,
            /// <summary>
            /// 输出样式：YYYY年MM月
            /// </summary>
            /// <remarks></remarks>
            YM_Num,
            /// <summary>
            /// 输出样式：ＹＹＹＹ年ＭＭ月 （其中全角的字母表示汉字文字的数字）
            /// </summary>
            /// <remarks></remarks>
            YM_Char,

            /// <summary>
            /// 输出样式：YYYY年 第Q季度
            /// </summary>
            /// <remarks></remarks>
            YQ_Num,
            /// <summary>
            /// 输出样式：ＹＹＹＹ年 第Ｑ季度 （其中全角的字母表示汉字文字的数字）
            /// </summary>
            /// <remarks></remarks>
            YQ_Char,

            /// <summary>
            /// 输出样式：YYYY年
            /// </summary>
            /// <remarks></remarks>
            Y_Num,
            /// <summary>
            /// 输出样式：ＹＹＹＹ年 （其中全角的字母表示汉字文字的数字）
            /// </summary>
            /// <remarks></remarks>
            Y_Char,

            /// <summary>
            /// 输出样式：MM月DD日
            /// </summary>
            /// <remarks></remarks>
            MD_Num,
            /// <summary>
            /// 输出样式：ＭＭ月ＤＤ日 （其中全角的字母表示汉字文字的数字）
            /// </summary>
            /// <remarks></remarks>
            MD_Char,

            /// <summary>
            /// 输出样式：星期Ｗ （其中全角的字母表示汉字文字的数字）
            /// </summary>
            /// <remarks></remarks>
            W_Char,
        }

        /// <summary>
        /// 输出的时间样式
        /// </summary>
        public enum OutTimeStyle
        {
            /// <summary>
            /// 输出样式：HH:MM:SS
            /// </summary>
            HMS_Sign = 1,

            /// <summary>
            /// 输出样式：HH时MM分SS秒
            /// </summary>
            HMS_Num = 2,

            /// <summary>
            /// 输出样式： ＨＨ时ＭＭ分ＳＳ秒
            /// </summary>
            HMS_Char = 3,

            /// <summary>
            /// 输出样式：AM/PM HH:MM:SS
            /// </summary>
            THMS_Sign = 4,

            /// <summary>
            /// 输出样式：AM/PM HH时MM分SS秒
            /// </summary>
            THMS_Num = 5,

            /// <summary>
            /// 输出样式：上午/下午 ＨＨ时ＭＭ分ＳＳ秒
            /// </summary>
            THMS_Char = 6,

            /// <summary>
            /// 输出样式：HH:MM
            /// </summary>
            HM_Sign = 7,

            /// <summary>
            /// 输出样式：HH时MM分
            /// </summary>
            HM_Num = 8,

            /// <summary>
            /// 输出样式：ＨＨ时ＭＭ分
            /// </summary>
            HM_Char = 9,

            /// <summary>
            /// 输出样式：AM/PM HH:MM
            /// </summary>
            THM_Sign = 10,

            /// <summary>
            /// 输出样式：AM/PM HH时MM分
            /// </summary>
            THM_Num = 11,

            /// <summary>
            /// 输出样式：上午/下午 ＨＨ时ＭＭ分
            /// </summary>
            THM_Char = 12,

            /// <summary>
            /// 输出样式：HH时
            /// </summary>
            H_Num = 13,

            /// <summary>
            /// 输出样式：ＨＨ时
            /// </summary>
            H_Char = 14,

            /// <summary>
            /// 输出样式：AM/PM HH时
            /// </summary>
            TH_Num = 15,

            /// <summary>
            /// 输出样式：上午/下午 ＨＨ时
            /// </summary>
            TH_Char = 16,
        }

        /// <summary>
        /// 指示在调用与日期相关的函数时如何确定日期间隔和设置日期间隔的格式。
        /// </summary>
        public enum DateInterval
        {
            /// <summary>
            /// 秒
            /// </summary>
            Second,

            /// <summary>
            /// 分钟
            /// </summary>
            Minute,

            /// <summary>
            /// 小时
            /// </summary>
            Hour,

            /// <summary>
            /// 天
            /// </summary>
            Day,

            /// <summary>
            /// 星期
            /// </summary>
            Weekday,

            /// <summary>
            /// 月
            /// </summary>
            Month,

            /// <summary>
            /// 季度
            /// </summary>
            Season,

            /// <summary>
            /// 年
            /// </summary>
            Year
        }

        /// <summary>
        /// 表示 8 位整型日期和 6 位整型时间的结构
        /// </summary>
        public struct IntDateTime
        {
            private int m_DateNum;
            /// <summary>
            /// 8位的整型日期
            /// </summary>
            public int DateNum
            {
                get { return m_DateNum; }
                set { m_DateNum = value; }
            }

            private int m_TimeNum;
            /// <summary>
            /// 6位的整型时间
            /// </summary>
            public int TimeNum
            {
                get { return m_TimeNum; }
                set { m_TimeNum = value; }
            }
        }

        /// <summary>
        /// 将传入的整型日期格式化为正确的整型日期。例如传入 20090431 返回 20090501
        /// </summary>
        /// <param name="dateNum"></param>
        /// <returns></returns>
        public static int DateSerial(int dateNum)
        {
            return DateSerial(dateNum / 10000, (dateNum / 100) % 100, dateNum % 100);
        }

        /// <summary>
        /// 根据指定的年月日参数获取整型日期。并格式化为正确的整型日期。
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <param name="day">日</param>
        /// <returns>整型年月日 YYYYMMDD</returns>
        public static int DateSerial(int year, int month, int day)
        {
            if (year == 0)
            {
                return 0;
            }
            else
            {
                DateTime dtm = new DateTime();
                dtm = dtm.AddYears(year - 1);

                if (month > 0)
                {
                    dtm = dtm.AddMonths(month - 1);
                }

                if (day > 0)
                {
                    dtm = dtm.AddDays(day - 1);
                }

                int intDateNum = dtm.Year * 10000;

                if (month > 0)
                {
                    intDateNum += dtm.Month * 100;
                }

                if (day > 0)
                {
                    intDateNum += dtm.Day;
                }

                return intDateNum;
            }
        }

        /// <summary>
        /// 将传入的整型时间格式化为正确的整型时间。例如传入 232860 返回 232900
        /// </summary>
        /// <param name="timeNum">整型时间</param>
        /// <returns></returns>
        public static int TimeSerial(int timeNum)
        {
            return TimeSerial(timeNum / 10000, (timeNum / 100) % 100, timeNum % 100);
        }

        /// <summary>
        /// 根据指定时分秒获取整型时间，并格式化为正确的整型时间。
        /// </summary>
        /// <param name="hour">时</param>
        /// <param name="minute">分</param>
        /// <param name="second">秒</param>
        /// <returns></returns>
        public static int TimeSerial(int hour, int minute, int second)
        {
            DateTime dtm = new DateTime();
            dtm = dtm.AddHours(hour);
            dtm = dtm.AddMinutes(minute);
            dtm = dtm.AddSeconds(second);
            return dtm.Hour * 10000 + dtm.Minute * 100 + dtm.Second;
        }


        /// <summary>
        /// 获取系统整型日期值
        /// </summary>
        /// <returns></returns>
        public static int GetIntDate()
        {
            DateTime dtmValue = DateTime.Today;
            return dtmValue.Year * 10000 + dtmValue.Month * 100 + dtmValue.Day;
        }

        /// <summary>
        /// 获取系统整型时间值
        /// </summary>
        /// <returns></returns>
        public static int GetIntTime()
        {
            DateTime dtmValue = DateTime.Now;
            return dtmValue.Hour * 10000 + dtmValue.Minute * 100 + dtmValue.Second;
        }

        /// <summary>
        /// 将传入的日期转为 YYYYMMDD 格式的整型日期。函数将自动判断传入的参数是日期型还是 8 位整型日期。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ToIntDate(object value)
        {
            try
            {
                if (value == System.DBNull.Value)
                {
                    return 0;
                }
                else if (value == null)
                {
                    return 0;
                }
                else if (IsNumeric(value))                                           //尝试转为数字型
                {
                    return ToIntDate(Convert.ToInt32(value));
                }
                else
                {                                                               //尝试转为日期型
                    DateTime dtmValue = Convert.ToDateTime(value);
                    return ToIntDate(dtmValue, DateStyle.YYYYMMDD);
                }
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 将传入的日期转为 YYYYMMDD 格式的整型日期
        /// </summary>
        /// <param name="dateTime">DateTime 型的日期</param>
        /// <returns></returns>
        public static int ToIntDate(DateTime dateTime)
        {
            return ToIntDate(dateTime.Year * 10000 + dateTime.Month * 100 + dateTime.Day, DateStyle.YYYYMMDD, DateStyle.YYYYMMDD);
        }

        /// <summary>
        /// 将传入的日期转为指定格式的整型日期
        /// </summary>
        /// <param name="dateTime">DateTime 型日期时间</param>
        /// <param name="outDateStyle">希望输出的日期格式</param>
        /// <returns></returns>
        public static int ToIntDate(DateTime dateTime, DateStyle outDateStyle)
        {
            return ToIntDate(dateTime.Year * 10000 + dateTime.Month * 100 + dateTime.Day, DateStyle.YYYYMMDD, outDateStyle);
        }

        /// <summary>
        /// 自动判断传入的整型日期格式并转换为 YYYYMMDD 格式的整型日期
        /// </summary>
        /// <param name="dateNum">8位整型日期</param>
        /// <returns></returns>
        public static int ToIntDate(int dateNum)
        {
            if ((dateNum % 100) != 0)
            {
                return ToIntDate(dateNum, DateStyle.YYYYMMDD, DateStyle.YYYYMMDD);
            }
            else if ((dateNum % 10000) != 0)
            {
                if (((dateNum / 10000) % 100) > 12)
                {
                    return ToIntDate(dateNum, DateStyle.YYYYWW00, DateStyle.YYYYMMDD);
                }
                else
                {
                    return ToIntDate(dateNum, DateStyle.YYYYMM00, DateStyle.YYYYMMDD);
                }
            }
            else
            {
                return ToIntDate(dateNum, DateStyle.YYYY0000, DateStyle.YYYYMMDD);
            }
        }

        /// <summary>
        /// 指定传入的整型日期格式并转为 YYYYMMDD 格式的日期
        /// </summary>
        /// <param name="dateNum">8位整型日期</param>
        /// <param name="inDateStyle">输入的整型日期格式</param>
        /// <returns></returns>
        public static int ToIntDate(int dateNum, DateStyle inDateStyle)
        {
            return ToIntDate(dateNum, inDateStyle, DateStyle.YYYYMMDD);
        }

        /// <summary>
        /// 将传入的指定格式的整型日期转为指定格式的整型日期
        /// </summary>
        /// <param name="dateNum">8位整型日期</param>
        /// <param name="inDateStyle">输入的整型日期格式</param>
        /// <param name="outDateStyle">希望输出的日期格式</param>
        /// <returns></returns>
        public static int ToIntDate(int dateNum, DateStyle inDateStyle, DateStyle outDateStyle)
        {
            DateTime dtm = ToDateTime(dateNum, 0, inDateStyle);

            int intDateValue = 0;

            switch (outDateStyle)
            {
                case DateStyle.YYYYMMDD:
                    intDateValue = dtm.Year * 10000 + dtm.Month * 100 + dtm.Day;
                    break;

                case DateStyle.YYYYWW00:
                    intDateValue = dtm.Year * 10000 + ((dtm.DayOfYear + (int)(new DateTime(dtm.Year, 1, 1).DayOfWeek) - 1) / 7 + 1) * 100;
                    break;

                case DateStyle.YYYYMM00:
                    intDateValue = dtm.Year * 10000 + dtm.Month * 100;
                    break;

                case DateStyle.YYYYQQ00:
                    intDateValue = dtm.Year * 10000 + ((dtm.Month + 2) / 3) * 100;
                    break;

                case DateStyle.YYYY0000:
                    intDateValue = dtm.Year * 10000;
                    break;
            }

            return intDateValue;
        }

        /// <summary>
        /// 将传入的日期转为 HHMMSS 格式的整型时间。函数将自动判断传入的参数是日期型还是 6 位整型时间，如果是 6 位整型时间此函数还将格式化返回时间的正确性。
        /// </summary>
        /// <param name="value">8 位整型日期或者 DateTime 型的日期</param>
        /// <returns></returns>
        public static int ToIntTime(object value)
        {
            try
            {
                if (IsNumeric(value))                                           //尝试转为数字型
                {
                    int intValue = Convert.ToInt32(value);
                    DateTime dtmValue = new DateTime();
                    dtmValue = dtmValue.AddSeconds(intValue % 100);
                    dtmValue = dtmValue.AddMinutes((intValue / 100) % 100);
                    dtmValue = dtmValue.AddHours(intValue / 10000);
                    return dtmValue.Hour * 10000 + dtmValue.Minute * 100 + dtmValue.Second;
                }
                else
                {                                                               //尝试转为日期型
                    DateTime dtmValue = Convert.ToDateTime(value);
                    return dtmValue.Hour * 10000 + dtmValue.Minute * 100 + dtmValue.Second;
                }
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 按指定日期样式将输入的整型日期转为日期型日期
        /// </summary>
        /// <param name="dateNum">8位整型日期</param>
        /// <returns></returns>
        public static DateTime ToDateTime(int dateNum)
        {
            return ToDateTime(dateNum, 0, DateStyle.Automatism);
        }

        /// <summary>
        /// 自动判断日期样式，并将输入的整型日期、时间转为日期型日期时间
        /// </summary>
        /// <param name="dateNum">8位整型日期</param>
        /// <param name="timeNum">6位整型时间</param>
        /// <returns></returns>
        public static DateTime ToDateTime(int dateNum, int timeNum)
        {
            return ToDateTime(dateNum, timeNum, DateStyle.Automatism);
        }

        /// <summary>
        /// 按指定日期样式将输入的整型日期转为日期型日期
        /// </summary>
        /// <param name="dateNum">8位整型日期</param>
        /// <param name="inDateStyle">输入的整型日期格式</param>
        /// <returns></returns>
        public static DateTime ToDateTime(int dateNum, DateStyle inDateStyle)
        {
            return ToDateTime(dateNum, 0, inDateStyle);
        }

        /// <summary>
        /// 按指定日期样式将输入的整型日期、时间转为日期型日期时间
        /// </summary>
        /// <param name="dateNum">8位整型日期</param>
        /// <param name="timeNum">6位整型时间</param>
        /// <param name="inDateStyle">输入的整型日期格式</param>
        /// <returns>日期型日期</returns>
        public static DateTime ToDateTime(int dateNum, int timeNum, DateStyle inDateStyle)
        {
            DateTime dtm = new DateTime();

            if (dateNum > 10000 && dateNum <= 99991231)
            {
                //自动判断日期类型
                if (inDateStyle == DateStyle.Automatism)
                {
                    if ((dateNum % 100) != 0)
                    {
                        inDateStyle = DateStyle.YYYYMMDD;
                    }
                    else if ((dateNum % 10000) != 0)
                    {
                        if (((dateNum / 10000) % 100) > 12)
                        {
                            inDateStyle = DateStyle.YYYYWW00;
                        }
                        else
                        {
                            inDateStyle = DateStyle.YYYYMM00;
                        }
                    }
                    else
                    {
                        inDateStyle = DateStyle.YYYY0000;
                    }
                }

                switch (inDateStyle)
                {
                    case DateStyle.YYYYMMDD:
                        dtm = dtm.AddYears((dateNum / 10000) - 1);
                        dtm = dtm.AddMonths(((dateNum / 100) % 100) - 1);
                        dtm = dtm.AddDays((dateNum % 100) - 1);
                        break;

                    case DateStyle.YYYYWW00:
                        dtm = new DateTime((dateNum / 10000), 1, 1);                                         //该年度1月1日的日期
                        int intFirstDayOfWeek = (int)dtm.DayOfWeek;                                             //1月1日距周的起始日相差几天
                        dtm = dtm.AddDays(-intFirstDayOfWeek + (((dateNum / 100) % 100) - 1) * 7);           //将日期改变成1月1日所在周的起始日的日期，并增加指定的周数
                        break;

                    case DateStyle.YYYYMM00:
                        dtm = dtm.AddYears((dateNum / 10000) - 1);
                        dtm = dtm.AddMonths(((dateNum / 100) % 100) - 1);

                        if (dtm.Day != 0)
                        {
                            dtm = dtm.AddDays(1 - dtm.Day);
                        }
                        break;

                    case DateStyle.YYYYQQ00:
                        dtm = new DateTime((dateNum / 10000), 1, 1);                                         //该年度1月1日的日期
                        dtm = dtm.AddMonths((((dateNum / 100) % 100) - 1) * 3);
                        break;

                    case DateStyle.YYYY0000:
                        dtm = dtm.AddYears((dateNum / 10000) - 1);
                        dtm = dtm.AddMonths(((dateNum / 100) % 100) - 1);
                        dtm = dtm.AddDays((dateNum % 100) - 1);

                        if (dtm.Day != 0)
                        {
                            dtm = dtm.AddDays(1 - dtm.Day);
                        }

                        if (dtm.Month != 0)
                        {
                            dtm = dtm.AddMonths(1 - dtm.Month);
                        }
                        break;
                }
            }

            if (timeNum > 0)
            {
                dtm = dtm.AddHours(timeNum / 10000);
                dtm = dtm.AddMinutes((timeNum / 100) % 100);
                dtm = dtm.AddSeconds(timeNum % 100);
            }
            return dtm;
        }

        /// <summary>
        /// 自动判断传入的整型日期格式，并计算增减指定值后的整型日期，结果以 YYYYMMDD 格式 IntDateTime 结构返回。
        /// </summary>
        /// <param name="interval">设置日期或时间间隔的格式</param>
        /// <param name="addNumber">要增减的日期或时间的间隔数据</param>
        /// <param name="dateNum">8 位整型日期</param>
        /// <returns></returns>
        public static IntDateTime DoDateAdd(DateInterval interval, int addNumber, int dateNum)
        {
            if ((dateNum % 100) != 0)
            {
                return DoDateAdd(interval, addNumber, dateNum, DateStyle.YYYYMMDD, DateStyle.YYYYMMDD, 0);
            }
            else if ((dateNum % 10000) != 0)
            {
                if (((dateNum / 10000) % 100) > 12)
                {
                    return DoDateAdd(interval, addNumber, dateNum, DateStyle.YYYYWW00, DateStyle.YYYYMMDD, 0);
                }
                else
                {
                    return DoDateAdd(interval, addNumber, dateNum, DateStyle.YYYYMM00, DateStyle.YYYYMMDD, 0);
                }
            }
            else
            {
                return DoDateAdd(interval, addNumber, dateNum, DateStyle.YYYY0000, DateStyle.YYYYMMDD, 0);
            }
        }

        /// <summary>
        /// 计算整型日期增减指定值后的整型日期，结果以 IntDateTime 结构返回。
        /// </summary>
        /// <param name="interval">设置日期或时间间隔的格式</param>
        /// <param name="addNumber">要增减的日期或时间的间隔数据</param>
        /// <param name="dateNum">8 位整型日期</param>
        /// <param name="inDateStyle">输入的整型日期格式</param>
        /// <param name="outDateStyle">希望输出的日期格式</param>
        /// <returns></returns>
        public static IntDateTime DoDateAdd(DateInterval interval, int addNumber, int dateNum, DateStyle inDateStyle, DateStyle outDateStyle)
        {
            return DoDateAdd(interval, addNumber, dateNum, inDateStyle, outDateStyle, 0);
        }

        /// <summary>
        /// 计算整型时间增减指定值后的整型时间，结果以 IntDateTime 结构返回。此函数不考虑 dateNum 参数，可以传入 null
        /// </summary>
        /// <param name="interval">设置日期或时间间隔的格式</param>
        /// <param name="addNumber">要增减的日期或时间的间隔数据</param>
        /// <param name="dateNum">传入 null</param>
        /// <param name="timeNum">6 位整型时间</param>
        /// <returns></returns>
        public static IntDateTime DoDateAdd(DateInterval interval, int addNumber, int? dateNum, int timeNum)
        {
            return DoDateAdd(interval, addNumber, 0, DateStyle.YYYYMMDD, null, timeNum);
        }

        /// <summary>
        /// 计算整型日期时间增减指定值后的整型日期时间，结果以 IntDateTime 结构返回。
        /// </summary>
        /// <param name="interval">设置日期或时间间隔的格式</param>
        /// <param name="addNumber">要增减的日期或时间的间隔数据</param>
        /// <param name="dateTime">DateTime 格式的日期时间</param>
        /// <param name="outDateStyle">希望输出的日期格式</param>
        /// <returns></returns>
        public static IntDateTime DoDateAdd(DateInterval interval, int addNumber, DateTime dateTime, DateStyle outDateStyle)
        {
            return DoDateAdd(interval, addNumber, ToIntDate(dateTime, DateStyle.YYYYMMDD), DateStyle.YYYYMMDD, outDateStyle, ToIntTime(dateTime));
        }

        /// <summary>
        /// 计算整型日期时间增减指定值后的整型日期时间，结果以 IntDateTime 结构返回。
        /// </summary>
        /// <param name="interval">设置日期或时间间隔的格式</param>
        /// <param name="addNumber">要增减的日期或时间的间隔数据</param>
        /// <param name="intDateTime">IntDateTime 格式的日期时间</param>
        /// <param name="outDateStyle">希望输出的日期格式</param>
        /// <returns></returns>
        public static IntDateTime DoDateAdd(DateInterval interval, int addNumber, IntDateTime intDateTime, DateStyle outDateStyle)
        {
            return DoDateAdd(interval, addNumber, intDateTime.DateNum, DateStyle.YYYYMMDD, outDateStyle, intDateTime.TimeNum);
        }

        /// <summary>
        /// 计算整型日期时间增减指定值后的整型日期时间，结果以 IntDateTime 结构返回。
        /// </summary>
        /// <param name="interval">设置日期或时间间隔的格式</param>
        /// <param name="addNumber">要增减的日期或时间的间隔数据</param>
        /// <param name="dateNum">8 位整型日期</param>
        /// <param name="inDateStyle">输入的日期格式</param>
        /// <param name="outDateStyle">希望输出的日期格式</param>
        /// <param name="timeNum">6 位整型时间</param>
        /// <returns></returns>
        public static IntDateTime DoDateAdd(DateInterval interval, int addNumber, int dateNum, DateStyle inDateStyle, DateStyle? outDateStyle, int timeNum)
        {
            DateTime dtm = ToDateTime(dateNum, timeNum, (DateStyle)inDateStyle);

            switch (interval)
            {
                case DateInterval.Second:
                    dtm = dtm.AddSeconds(addNumber);
                    break;

                case DateInterval.Minute:
                    dtm = dtm.AddMinutes(addNumber);
                    break;

                case DateInterval.Hour:
                    dtm = dtm.AddHours(addNumber);
                    break;

                case DateInterval.Day:
                    dtm = dtm.AddDays(addNumber);
                    break;

                case DateInterval.Weekday:
                    dtm = dtm.AddDays(addNumber * 7);
                    break;

                case DateInterval.Month:
                    dtm = dtm.AddMonths(addNumber);
                    break;

                case DateInterval.Season:
                    dtm = dtm.AddMonths(addNumber * 3);
                    break;

                case DateInterval.Year:
                    dtm = dtm.AddYears(addNumber);
                    break;
            }

            IntDateTime idt = new IntDateTime();

            if (null != outDateStyle)
            {
                idt.DateNum = ToIntDate(dtm, (DateStyle)outDateStyle);
            }

            idt.TimeNum = dtm.Hour * 10000 + dtm.Minute * 100 + dtm.Second;

            return idt;
        }

        /// <summary>
        /// 判断2个日期之间相差的时间值。公式将以 dateTime2 - dateTime1 进行计算
        /// </summary>
        /// <param name="interval">指示在调用与日期相关的函数时如何确定日期间隔和设置日期间隔的格式。</param>
        /// <param name="dateTime1">日期1。通常为之前的日期</param>
        /// <param name="dateTime2">日期2。通常为之后的日期</param>
        /// <returns></returns>
        public static int DoDateDiff(DateInterval interval, DateTime dateTime1, DateTime dateTime2)
        {
            int intValue = 0;
            switch (interval)
            {
                case DateInterval.Second:
                    TimeSpan timeSpanSecond = dateTime2 - dateTime1;
                    intValue = (int)timeSpanSecond.TotalSeconds;
                    break;

                case DateInterval.Minute:
                    TimeSpan timeSpanMinute = dateTime2 - dateTime1;
                    intValue = (int)timeSpanMinute.TotalMinutes;
                    break;

                case DateInterval.Hour:
                    TimeSpan timeSpanHour = dateTime2 - dateTime1;
                    intValue = (int)timeSpanHour.TotalHours;
                    break;

                case DateInterval.Day:
                    TimeSpan timeSpanDay = dateTime2 - dateTime1;
                    intValue = (int)timeSpanDay.TotalDays;
                    break;

                case DateInterval.Weekday:
                    break;

                case DateInterval.Month:
                    int intMonty1 = dateTime1.Year * 12 + dateTime1.Month;
                    int intMonty2 = dateTime2.Year * 12 + dateTime2.Month;
                    intValue = intMonty2 - intMonty1;
                    break;

                case DateInterval.Season:
                    break;

                case DateInterval.Year:
                    intValue = dateTime2.Year - dateTime1.Year;
                    break;

            }
            return intValue;
        }

        /// <summary>
        /// 判断2个日期之间相差的时间值。公式将以 dateTime2 - dateTime1 进行计算
        /// </summary>
        /// <param name="interval">指示在调用与日期相关的函数时如何确定日期间隔和设置日期间隔的格式</param>
        /// <param name="dateNum1">日期1。通常为之前的日期</param>
        /// <param name="dateStyle1">日期1的日期格式</param>
        /// <param name="dateNum2">日期2。通常为之后的日期</param>
        /// <param name="dateStyle2">日期2的日期格式</param>
        /// <returns></returns>
        public static int DoDateDiff(DateInterval interval, int dateNum1, DateStyle dateStyle1, int dateNum2, DateStyle dateStyle2)
        {
            return DoDateDiff(interval, ToDateTime(dateNum1, dateStyle1), ToDateTime(dateNum2, dateStyle2));
        }

        /// <summary>
        /// 判断2个整型日期和时间之间相差的时间值。公式将以 (dateTime2 + timeNum1) - (dateTime1 + timeNum2) 进行计算
        /// </summary>
        /// <param name="interval">指示在调用与日期相关的函数时如何确定日期间隔和设置日期间隔的格式</param>
        /// <param name="dateNum1">日期1。通常为之前的日期</param>
        /// <param name="dateStyle1">日期1的日期格式</param>
        /// <param name="timeNum1">时间1。</param>
        /// <param name="dateNum2">日期2。通常为之后的日期</param>
        /// <param name="dateStyle2">日期2的日期格式</param>
        /// <param name="timeNum2">时间2。</param>
        /// <returns></returns>
        public static int DoDateDiff(DateInterval interval, int dateNum1, DateStyle dateStyle1, int timeNum1, int dateNum2, DateStyle dateStyle2, int timeNum2)
        {
            return DoDateDiff(interval, ToDateTime(dateNum1, timeNum1, dateStyle1), ToDateTime(dateNum2, timeNum2, dateStyle2));
        }

        /// <summary>
        /// 判断2个时间之间相差的时间值。公式将以 timeNum2 - timeNum1 进行计算
        /// </summary>
        /// <param name="interval">指示在调用与日期相关的函数时如何确定日期间隔和设置日期间隔的格式</param>
        /// <param name="timeNum1">时间1。</param>
        /// <param name="timeNum2">时间2。</param>
        /// <returns></returns>
        public static int DoDateDiff(DateInterval interval, int timeNum1, int timeNum2)
        {
            DateTime dtm1 = new DateTime();
            DateTime dtm2 = new DateTime();

            dtm1 = dtm1.AddHours(timeNum1 / 10000);
            dtm1 = dtm1.AddMinutes((timeNum1 / 100) % 100);
            dtm1 = dtm1.AddSeconds(timeNum1 % 100);

            dtm2 = dtm2.AddHours(timeNum2 / 10000);
            dtm2 = dtm2.AddMinutes((timeNum2 / 100) % 100);
            dtm2 = dtm2.AddSeconds(timeNum2 % 100);

            return DoDateDiff(interval, dtm1, dtm2);
        }

        /// <summary>
        /// 将输入的日期、时间按指定格式转换为适合页面显示的日期字符串
        /// </summary>
        /// <param name="dateTime">需要输出的日期型日期和时间</param>
        /// <param name="outDateStyle">输出的日期样式</param>
        /// <returns>适合页面显示的日期字符串</returns>
        public static string OutDateTime(DateTime dateTime, OutDateStyle outDateStyle)
        {
            return OutDateTime(ToIntDate(dateTime), DateStyle.YYYYMMDD, outDateStyle, null, null, "", "", "");
        }

        /// <summary>
        /// 将输入的日期、时间按指定格式转换为适合页面显示的日期字符串
        /// </summary>
        /// <param name="dateTime">需要输出的日期型日期和时间</param>
        /// <param name="outDateStyle">输出的日期样式</param>
        /// <param name="replaceNull">如果传入的参数为空是返回的值</param>
        /// <returns>适合页面显示的日期字符串</returns>
        public static string OutDateTime(DateTime dateTime, OutDateStyle outDateStyle, string replaceNull)
        {
            return OutDateTime(ToIntDate(dateTime), DateStyle.YYYYMMDD, outDateStyle, null, null, replaceNull, "", "");
        }

        /// <summary>
        /// 将输入的日期、时间按指定格式转换为适合页面显示的日期字符串
        /// </summary>
        /// <param name="dateTime">需要输出的日期型日期和时间</param>
        /// <param name="outDateStyle">输出的日期样式</param>
        /// <param name="replaceNull">如果传入的参数为空是返回的值</param>
        /// <param name="prefix">前缀</param>
        /// <param name="postfix">后缀</param>
        /// <returns>适合页面显示的日期字符串</returns>
        public static string OutDateTime(DateTime dateTime, OutDateStyle outDateStyle, string replaceNull, string prefix, string postfix)
        {
            return OutDateTime(ToIntDate(dateTime), DateStyle.YYYYMMDD, outDateStyle, null, null, replaceNull, prefix, postfix);
        }

        /// <summary>
        /// 将输入的日期、时间按指定格式转换为适合页面显示的时间字符串
        /// </summary>
        /// <param name="dateTime">需要输出的日期型日期和时间</param>
        /// <param name="outTimeStyle">输出的时间样式</param>
        /// <returns>适合页面显示的时间字符串</returns>
        public static string OutDateTime(DateTime dateTime, OutTimeStyle outTimeStyle)
        {
            return OutDateTime(null, null, null, ToIntTime(dateTime), outTimeStyle, "", "", "");
        }

        /// <summary>
        /// 将输入的日期、时间按指定格式转换为适合页面显示的时间字符串
        /// </summary>
        /// <param name="dateTime">需要输出的日期型日期和时间</param>
        /// <param name="outTimeStyle">输出的时间样式</param>
        /// <param name="replaceNull">如果传入的参数为空是返回的值</param>
        /// <returns>适合页面显示的时间字符串</returns>
        public static string OutDateTime(DateTime dateTime, OutTimeStyle outTimeStyle, string replaceNull)
        {
            return OutDateTime(null, null, null, ToIntTime(dateTime), outTimeStyle, replaceNull, "", "");
        }

        /// <summary>
        /// 将输入的日期、时间按指定格式转换为适合页面显示的时间字符串
        /// </summary>
        /// <param name="dateTime">需要输出的日期型日期和时间</param>
        /// <param name="outTimeStyle">输出的时间样式</param>
        /// <param name="replaceNull">如果传入的参数为空是返回的值</param>
        /// <param name="prefix">前缀</param>
        /// <param name="postfix">后缀</param>
        /// <returns>适合页面显示的时间字符串</returns>
        public static string OutDateTime(DateTime dateTime, OutTimeStyle outTimeStyle, string replaceNull, string prefix, string postfix)
        {
            return OutDateTime(null, null, null, ToIntTime(dateTime), outTimeStyle, replaceNull, prefix, postfix);
        }

        /// <summary>
        /// 将输入的日期、时间按指定格式转换为适合页面显示的日期、时间字符串
        /// </summary>
        /// <param name="dateTime">需要输出的日期型日期和时间</param>
        /// <param name="outDateStyle">输出的日期样式</param>
        /// <param name="outTimeStyle">输出的时间样式</param>
        /// <returns>适合页面显示的日期时间字符串</returns>
        public static string OutDateTime(DateTime dateTime, OutDateStyle outDateStyle, OutTimeStyle outTimeStyle)
        {
            return OutDateTime(ToIntDate(dateTime), DateStyle.YYYYMMDD, outDateStyle, ToIntTime(dateTime), outTimeStyle, "", "", "");
        }

        /// <summary>
        /// 将输入的日期、时间按指定格式转换为适合页面显示的日期、时间字符串
        /// </summary>
        /// <param name="dateTime">需要输出的日期型日期和时间</param>
        /// <param name="outDateStyle">输出的日期样式</param>
        /// <param name="outTimeStyle">输出的时间样式</param>
        /// <param name="replaceNull">如果传入的参数为空是返回的值</param>
        /// <returns>适合页面显示的日期时间字符串</returns>
        public static string OutDateTime(DateTime dateTime, OutDateStyle outDateStyle, OutTimeStyle outTimeStyle, string replaceNull)
        {
            return OutDateTime(ToIntDate(dateTime), DateStyle.YYYYMMDD, outDateStyle, ToIntTime(dateTime), outTimeStyle, replaceNull, "", "");
        }

        /// <summary>
        /// 将输入的日期、时间按指定格式转换为适合页面显示的日期、时间字符串
        /// </summary>
        /// <param name="dateTime">需要输出的日期型日期和时间</param>
        /// <param name="outDateStyle">输出的日期样式</param>
        /// <param name="outTimeStyle">输出的时间样式</param>
        /// <param name="replaceNull">如果传入的参数为空是返回的值</param>
        /// <param name="prefix">前缀</param>
        /// <param name="postfix">后缀</param>
        /// <returns>适合页面显示的日期时间字符串</returns>
        public static string OutDateTime(DateTime dateTime, OutDateStyle outDateStyle, OutTimeStyle outTimeStyle, string replaceNull, string prefix, string postfix)
        {
            return OutDateTime(ToIntDate(dateTime), DateStyle.YYYYMMDD, outDateStyle, ToIntTime(dateTime), outTimeStyle, replaceNull, prefix, postfix);
        }

        /// <summary>
        /// 将输入的日期按指定格式转换为适合页面显示的日期字符串
        /// </summary>
        /// <param name="dateNum">8位整型日期</param>
        /// <param name="inDateStyle">输入的日期格式</param>
        /// <param name="outDateStyle">输出的日期样式</param>
        /// <returns>适合页面显示的日期字符串</returns>
        public static string OutDateTime(object dateNum, DateStyle inDateStyle, OutDateStyle outDateStyle)
        {
            return OutDateTime(dateNum, inDateStyle, outDateStyle, null, null, "", "", "");
        }

        /// <summary>
        /// 将输入的日期按指定格式转换为适合页面显示的日期字符串
        /// </summary>
        /// <param name="dateNum">8位整型日期</param>
        /// <param name="inDateStyle">输入的日期格式</param>
        /// <param name="outDateStyle">输出的日期样式</param>
        /// <param name="replaceNull">如果传入的参数为空是返回的值</param>
        /// <returns>适合页面显示的日期字符串</returns>
        public static string OutDateTime(object dateNum, DateStyle inDateStyle, OutDateStyle outDateStyle, string replaceNull)
        {
            return OutDateTime(dateNum, inDateStyle, outDateStyle, null, null, replaceNull, "", "");
        }

        /// <summary>
        /// 将输入的日期按指定格式转换为适合页面显示的日期字符串
        /// </summary>
        /// <param name="dateNum">8位整型日期</param>
        /// <param name="inDateStyle">输入的日期格式</param>
        /// <param name="outDateStyle">输出的日期样式</param>
        /// <param name="replaceNull">如果传入的参数为空是返回的值</param>
        /// <param name="prefix">前缀</param>
        /// <param name="postfix">后缀</param>
        /// <returns>适合页面显示的日期字符串</returns>
        public static string OutDateTime(object dateNum, DateStyle inDateStyle, OutDateStyle outDateStyle, string replaceNull, string prefix, string postfix)
        {
            return OutDateTime(dateNum, inDateStyle, outDateStyle, null, null, replaceNull, prefix, postfix);
        }

        /// <summary>
        /// 将输入的时间按指定格式转换为适合页面显示的时间字符串
        /// </summary>
        /// <param name="timeNum">6位整型时间</param>
        /// <param name="outTimeStyle">输出的时间样式</param>
        /// <returns></returns>
        public static string OutDateTime(object timeNum, OutTimeStyle outTimeStyle)
        {
            return OutDateTime(null, null, null, timeNum, outTimeStyle, "", "", "");
        }

        /// <summary>
        /// 将输入的时间按指定格式转换为适合页面显示的时间字符串
        /// </summary>
        /// <param name="timeNum">6位整型时间</param>
        /// <param name="outTimeStyle">输出的时间样式</param>
        /// <param name="replaceNull">如果传入的参数为空是返回的值</param>
        /// <returns>适合页面显示的时间字符串</returns>
        public static string OutDateTime(object timeNum, OutTimeStyle outTimeStyle, string replaceNull)
        {
            return OutDateTime(null, null, null, timeNum, outTimeStyle, replaceNull, "", "");
        }

        /// <summary>
        /// 将输入的时间按指定格式转换为适合页面显示的时间字符串
        /// </summary>
        /// <param name="timeNum">6位整型时间</param>
        /// <param name="outTimeStyle">输出的时间样式</param>
        /// <param name="replaceNull">如果传入的参数为空是返回的值</param>
        /// <param name="prefix">前缀</param>
        /// <param name="postfix">后缀</param>
        /// <returns>适合页面显示的时间字符串</returns>
        public static string OutDateTime(object timeNum, OutTimeStyle outTimeStyle, string replaceNull, string prefix, string postfix)
        {
            return OutDateTime(null, null, null, timeNum, outTimeStyle, replaceNull, prefix, postfix);
        }

        /// <summary>
        /// 将输入的日期、时间按指定格式转换为适合页面显示的日期、时间字符串
        /// </summary>
        /// <param name="dateNum">8位整型日期</param>
        /// <param name="inDateStyle">输入的日期格式</param>
        /// <param name="outDateStyle">输出的日期样式</param>
        /// <param name="timeNum">6位整型时间</param>
        /// <param name="outTimeStyle">输出的时间样式</param>
        /// <returns>适合页面显示的日期时间字符串</returns>
        public static string OutDateTime(object dateNum, DateStyle inDateStyle, OutDateStyle outDateStyle, object timeNum, OutTimeStyle outTimeStyle)
        {
            return OutDateTime(dateNum, inDateStyle, outDateStyle, timeNum, outTimeStyle, "", "", "");
        }

        /// <summary>
        /// 将输入的日期、时间按指定格式转换为适合页面显示的日期、时间字符串
        /// </summary>
        /// <param name="dateNum">8位整型日期</param>
        /// <param name="inDateStyle">输入的日期格式</param>
        /// <param name="outDateStyle">输出的日期样式</param>
        /// <param name="timeNum">6位整型时间</param>
        /// <param name="outTimeStyle">输出的时间样式</param>
        /// <param name="replaceNull">如果传入的参数为空是返回的值</param>
        /// <returns>适合页面显示的日期时间字符串</returns>
        public static string OutDateTime(object dateNum, DateStyle? inDateStyle, OutDateStyle? outDateStyle, object timeNum, OutTimeStyle? outTimeStyle, string replaceNull)
        {
            return OutDateTime(dateNum, inDateStyle, outDateStyle, timeNum, outTimeStyle, replaceNull, "", "");
        }

        /// <summary>
        /// 将输入的日期、时间按指定格式转换为适合页面显示的日期、时间字符串
        /// </summary>
        /// <param name="dateNum">8位整型日期</param>
        /// <param name="inDateStyle">输入的日期格式</param>
        /// <param name="outDateStyle">输出的日期样式</param>
        /// <param name="timeNum">6位整型时间</param>
        /// <param name="outTimeStyle">输出的时间样式</param>
        /// <param name="replaceNull">如果传入的参数为空是返回的值</param>
        /// <param name="prefix">前缀</param>
        /// <param name="postfix">后缀</param>
        /// <returns>适合页面显示的日期时间字符串</returns>
        public static string OutDateTime(object dateNum, DateStyle? inDateStyle, OutDateStyle? outDateStyle, object timeNum, OutTimeStyle? outTimeStyle, string replaceNull, string prefix, string postfix)
        {
            try
            {
                string strValue = "";

                if (dateNum != System.DBNull.Value && dateNum != null && outDateStyle != null)
                {
                    if (null == inDateStyle)
                    {
                        inDateStyle = DateStyle.Automatism;
                    }
                    DateTime dtm = ToDateTime((int)dateNum, (DateStyle)inDateStyle);

                    switch (outDateStyle)
                    {
                        case OutDateStyle.YMD_Sign:
                            strValue += dtm.Year + "-" + OutDateTimeNum(dtm.Month) + "-" + OutDateTimeNum(dtm.Day);
                            break;

                        case OutDateStyle.YMD_Num:
                            strValue += dtm.Year + "年" + dtm.Month + "月" + dtm.Day + "日";
                            break;

                        case OutDateStyle.YMD_Char:
                            strValue += ToCharacter(dtm.Year, CharacterStyle.Character) + "年" + ToCharacter(dtm.Month, CharacterStyle.Character) + "月" + ToCharacter(dtm.Day, CharacterStyle.Character) + "日";
                            break;

                        case OutDateStyle.YMD_ShortSign:
                            strValue += dtm.ToString("yy-MM-dd");
                            break;

                        case OutDateStyle.YMD_ShortNum:
                            strValue += dtm.ToString("yy年MM月dd日");
                            break;

                        case OutDateStyle.YMDW_Num:
                            strValue += dtm.Year + "年" + dtm.Month + "月" + dtm.Day + "日 " + GetWeekdayName(dtm.DayOfWeek);
                            break;

                        case OutDateStyle.YMDW_Char:
                            strValue += ToCharacter(dtm.Year, CharacterStyle.Character) + "年" + ToCharacter(dtm.Month, CharacterStyle.Character) + "月" + ToCharacter(dtm.Day, CharacterStyle.Character) + "日 " + GetWeekdayName(dtm.DayOfWeek);
                            break;

                        case OutDateStyle.YW_Num:
                            strValue += ToCharacter(dtm.Year, CharacterStyle.Character) + "年 第" + ToCharacter(GetWeekOfYear(dtm), CharacterStyle.Character) + "周";
                            break;

                        case OutDateStyle.YW_Char:
                            strValue += ToCharacter(dtm.Year, CharacterStyle.Character) + "年 第" + ToCharacter(GetWeekOfYear(dtm), CharacterStyle.Character) + "周";
                            break;

                        case OutDateStyle.YWmd_Num:
                            DateTime dtmYWmd_Num = dtm.AddDays(-(int)dtm.DayOfWeek);
                            strValue += dtmYWmd_Num.Year + "年 第" + GetWeekOfYear(dtmYWmd_Num) + "周 (" + dtmYWmd_Num.Month + "月" + dtmYWmd_Num.Day + "日－" + dtmYWmd_Num.AddDays(6).Month + "月" + dtmYWmd_Num.AddDays(6).Day + "日)";
                            break;

                        case OutDateStyle.YWmd_Char:
                            DateTime dtmYWmd_Char = dtm.AddDays(-(int)dtm.DayOfWeek);
                            strValue += ToCharacter(dtmYWmd_Char.Year, CharacterStyle.Character) + "年 第" + ToCharacter(GetWeekOfYear(dtmYWmd_Char), CharacterStyle.Character) + "周 (" + dtmYWmd_Char.Month + "月" + dtmYWmd_Char.Day + "日－" + dtmYWmd_Char.AddDays(6).Month + "月" + dtmYWmd_Char.AddDays(6).Day + "日)";
                            break;

                        case OutDateStyle.YM_Sign:
                            strValue += dtm.Year + "-" + OutDateTimeNum(dtm.Month);
                            break;

                        case OutDateStyle.YM_Num:
                            strValue += dtm.Year + "年" + dtm.Month + "月";
                            break;

                        case OutDateStyle.YM_Char:
                            strValue += ToCharacter(dtm.Year, CharacterStyle.Character) + "年" + ToCharacter(dtm.Month, CharacterStyle.Character) + "月";
                            break;

                        case OutDateStyle.YQ_Num:
                            strValue += dtm.Year + "年 第" + ((dtm.Month + 2) / 3) + "季度";
                            break;

                        case OutDateStyle.YQ_Char:
                            strValue += ToCharacter(dtm.Year, CharacterStyle.Character) + "年 第" + ToCharacter(((dtm.Month + 2) / 3), CharacterStyle.Character) + "季度";
                            break;

                        case OutDateStyle.Y_Num:
                            strValue += dtm.Year + "年";
                            break;

                        case OutDateStyle.Y_Char:
                            strValue += ToCharacter(dtm.Year, CharacterStyle.Character) + "年";
                            break;

                        case OutDateStyle.MD_Num:
                            strValue += dtm.Month + "月" + dtm.Day + "日";
                            break;

                        case OutDateStyle.MD_Char:
                            strValue += ToCharacter(dtm.Month, CharacterStyle.Character) + "月" + ToCharacter(dtm.Day, CharacterStyle.Character) + "日";
                            break;

                        case OutDateStyle.W_Char:
                            strValue += GetWeekdayName(dtm.DayOfWeek);
                            break;
                    }
                }

                if (null != outDateStyle && null != outTimeStyle)
                {
                    if (null == dateNum && null == timeNum)
                    {
                        return replaceNull;
                    }

                    if (null != dateNum && null != timeNum)
                    {
                        strValue += " ";
                    }
                }

                if (null != timeNum && null != outTimeStyle)
                {
                    DateTime dtm = ToDateTime(0, Function.ToInt(timeNum));

                    switch (outTimeStyle)
                    {
                        case OutTimeStyle.HMS_Sign:
                            strValue += OutDateTimeNum(dtm.Hour) + ":" + OutDateTimeNum(dtm.Minute) + ":" + OutDateTimeNum(dtm.Second);
                            break;

                        case OutTimeStyle.HMS_Num:
                            strValue += dtm.Hour + "时" + OutDateTimeNum(dtm.Minute) + "分" + OutDateTimeNum(dtm.Second) + "秒";
                            break;

                        case OutTimeStyle.HMS_Char:
                            strValue += ToCharacter(dtm.Hour, CharacterStyle.Character) + "时" + ToCharacter(dtm.Minute, CharacterStyle.Character) + "分" + ToCharacter(dtm.Second, CharacterStyle.Character) + "秒";
                            break;

                        case OutTimeStyle.THMS_Sign:
                            if ((dtm.Hour) < 12)
                            {
                                strValue += "AM ";
                            }
                            else
                            {
                                strValue += "PM ";
                            }
                            strValue += (dtm.Hour % 12) + ":" + OutDateTimeNum(dtm.Minute) + ":" + OutDateTimeNum(dtm.Second);
                            break;

                        case OutTimeStyle.THMS_Num:
                            if ((dtm.Hour) < 12)
                            {
                                strValue += "上午 ";
                            }
                            else
                            {
                                strValue += "下午 ";
                            }
                            strValue += (dtm.Hour % 12) + "时" + OutDateTimeNum(dtm.Minute) + "分" + OutDateTimeNum(dtm.Second) + "秒";
                            break;

                        case OutTimeStyle.THMS_Char:
                            if ((dtm.Hour) < 12)
                            {
                                strValue += "上午 ";
                            }
                            else
                            {
                                strValue += "下午 ";
                            }
                            strValue += ToCharacter(dtm.Hour % 12, CharacterStyle.Character) + "时" + ToCharacter(dtm.Minute, CharacterStyle.Character) + "分" + ToCharacter(dtm.Second, CharacterStyle.Character) + "秒";
                            break;

                        case OutTimeStyle.HM_Sign:
                            strValue += OutDateTimeNum(dtm.Hour) + ":" + OutDateTimeNum(dtm.Minute);
                            break;

                        case OutTimeStyle.HM_Num:
                            strValue += dtm.Hour + "时" + OutDateTimeNum(dtm.Minute) + "分";
                            break;

                        case OutTimeStyle.HM_Char:
                            strValue += ToCharacter(dtm.Hour, CharacterStyle.Character) + "时" + ToCharacter(dtm.Minute, CharacterStyle.Character) + "分";
                            break;

                        case OutTimeStyle.THM_Sign:
                            if ((dtm.Hour) < 12)
                            {
                                strValue += "AM ";
                            }
                            else
                            {
                                strValue += "PM ";
                            }
                            strValue += (dtm.Hour % 12) + ":" + OutDateTimeNum(dtm.Minute);
                            break;

                        case OutTimeStyle.THM_Num:
                            if ((dtm.Hour) < 12)
                            {
                                strValue += "上午 ";
                            }
                            else
                            {
                                strValue += "下午 ";
                            }
                            strValue += (dtm.Hour % 12) + "时" + OutDateTimeNum(dtm.Minute) + "分";
                            break;

                        case OutTimeStyle.THM_Char:
                            if ((dtm.Hour) < 12)
                            {
                                strValue += "上午 ";
                            }
                            else
                            {
                                strValue += "下午 ";
                            }
                            strValue += ToCharacter(dtm.Hour % 12, CharacterStyle.Character) + "时" + ToCharacter(dtm.Minute, CharacterStyle.Character) + "分";
                            break;

                        case OutTimeStyle.H_Num:
                            strValue += dtm.Hour + "时";
                            break;

                        case OutTimeStyle.H_Char:
                            strValue += ToCharacter(dtm.Hour, CharacterStyle.Character) + "时";
                            break;

                        case OutTimeStyle.TH_Num:
                            if ((dtm.Hour) < 12)
                            {
                                strValue += "上午 ";
                            }
                            else
                            {
                                strValue += "下午 ";
                            }
                            strValue += (dtm.Hour % 12) + "时";
                            break;

                        case OutTimeStyle.TH_Char:
                            if ((dtm.Hour) < 12)
                            {
                                strValue += "上午 ";
                            }
                            else
                            {
                                strValue += "下午 ";
                            }
                            strValue += ToCharacter(dtm.Hour % 12, CharacterStyle.Character) + "时";
                            break;
                    }
                }

                return prefix + strValue + postfix;
            }
            catch
            {
                return replaceNull;
            }
        }

        /// <summary>
        /// 将小于10的数字加上前缀“0”，用于“OutDateTime”函数输出时间时使用
        /// </summary>
        /// <param name="num">数字</param>
        /// <returns></returns>
        private static string OutDateTimeNum(int num)
        {
            if (num < 10)
            {
                return "0" + num;
            }
            else
            {
                return num.ToString();
            }

        }

        /// <summary>
        /// 获得传入的整型日期是该年中的第几周
        /// </summary>
        /// <param name="dateNum">需要计算周数的整型日期</param>
        /// <returns>该日期是该年度中的第几周</returns>
        public static int GetWeekOfYear(int dateNum)
        {
            DateTime dtm = new DateTime();
            dtm = dtm.AddYears((dateNum / 10000) - 1);
            dtm = dtm.AddMonths(((dateNum / 100) % 100) - 1);
            dtm = dtm.AddDays((dateNum % 100) - 1);
            return GetWeekOfYear(dtm);
        }

        /// <summary>
        /// 获得传入的日期是该年中的第几周
        /// </summary>
        /// <param name="dateTime">需要计算周数的日期</param>
        /// <returns>该日期是该年度中的第几周</returns>
        public static int GetWeekOfYear(DateTime dateTime)
        {
            DateTime dtm = new DateTime(dateTime.Year, 1, 1);                                                //该年度1月1日的日期
            int intFirstDayOfWeek = (int)dtm.DayOfWeek;                                             //1月1日距周的起始日相差几天
            dtm = dtm.AddDays(-intFirstDayOfWeek);                                                  //将日期改变成1月1日所在周的起始日的日期
            return ((dateTime.Subtract(dtm).Days) / 7) + 1;
        }

        /// <summary>
        /// 将传入的数字星期转为汉字星期描述。“0”代表星期日，“6”代表星期六
        /// </summary>
        /// <param name="weekday">星期的数字描述</param>
        /// <returns>汉字的星期描述</returns>
        public static string GetWeekdayName(int weekday)
        {
            return GetWeekdayName((System.DayOfWeek)weekday);
        }

        /// <summary>
        /// 将传入的 DayOfWeek 行星期转为汉字星期描述
        /// </summary>
        /// <param name="weekday">DayOfWeek 的星期枚举</param>
        /// <returns>汉字的星期描述</returns>
        public static string GetWeekdayName(System.DayOfWeek weekday)
        {
            switch (weekday)
            {
                case DayOfWeek.Monday:
                    return "星期一";
                case DayOfWeek.Tuesday:
                    return "星期二";
                case DayOfWeek.Wednesday:
                    return "星期三";
                case DayOfWeek.Thursday:
                    return "星期四";
                case DayOfWeek.Friday:
                    return "星期五";
                case DayOfWeek.Saturday:
                    return "星期六";
                default:
                    return "星期日";
            }
        }

        #endregion

        #region << SQL 函数 >>

        /// <summary>
        /// 格式化整数，为 Insert 或 UpDate 做准备。
        /// </summary>
        /// <param name="value">准备存入数据库的数值</param>
        /// <returns></returns>
        public static string SqlInt(object value)
        {
            return SqlInt(value, 0, null);
        }

        /// <summary>
        /// 格式化整数，为 Insert 或 UpDate 做准备。
        /// </summary>
        /// <param name="value">准备存入数据库的数值</param>
        /// <param name="replaceNull">如果输入的数据无效或者类型转换时发生错误，将使用该值替代。默认情况下该值应为“Null”，以表示在数据库中存入一个空值。</param>
        /// <returns></returns>
        public static string SqlInt(object value, string replaceNull)
        {
            return SqlInt(value, 0, replaceNull);
        }

        /// <summary>
        /// 格式化整数并将此数字乘以一个倍率，为 Insert 或 UpDate 做准备。
        /// </summary>
        /// <param name="value">准备存入数据库的数值</param>
        /// <param name="multiple">倍率，如果输入的数字有效时将 value 乘以此数字。如果此参数输入“0”则表示不进行倍率运算。</param>
        /// <param name="replaceNull">如果输入的数据无效或者类型转换时发生错误，将使用该值替代。默认情况下该值应为“Null”，以表示在数据库中存入一个空值。</param>
        /// <returns></returns>
        public static string SqlInt(object value, int multiple, string replaceNull)
        {
            try
            {
                if (value != System.DBNull.Value && value != null)
                {
                    if (multiple == 0)
                    {
                        return Convert.ToInt32(value).ToString();
                    }
                    else
                    {
                        return (Convert.ToInt32(value) * multiple).ToString();
                    }
                }

            }
            catch
            {

            }

            if (replaceNull != null)
            {
                return replaceNull;
            }
            else
            {
                return "Null";
            }
        }


        /// <summary>
        /// 格式化长整数，为 Insert 或 UpDate 做准备。
        /// </summary>
        /// <param name="value">准备存入数据库的数值</param>
        /// <returns></returns>
        public static string SqlLong(object value)
        {
            return SqlLong(value, 0, null);
        }

        /// <summary>
        /// 格式化长整数，为 Insert 或 UpDate 做准备。
        /// </summary>
        /// <param name="value">准备存入数据库的数值</param>
        /// <param name="replaceNull">如果输入的数据无效或者类型转换时发生错误，将使用该值替代。默认情况下该值应为“Null”，以表示在数据库中存入一个空值。</param>
        /// <returns></returns>
        public static string SqlLong(object value, string replaceNull)
        {
            return SqlLong(value, 0, replaceNull);
        }

        /// <summary>
        /// 格式化长整数并将此数字乘以一个倍率，为 Insert 或 UpDate 做准备。
        /// </summary>
        /// <param name="value">准备存入数据库的数值</param>
        /// <param name="multiple">倍率，如果输入的数字有效时将 value 乘以此数字。如果此参数输入“0”则表示不进行倍率运算。</param>
        /// <param name="replaceNull">如果输入的数据无效或者类型转换时发生错误，将使用该值替代。默认情况下该值应为“Null”，以表示在数据库中存入一个空值。</param>
        /// <returns></returns>
        public static string SqlLong(object value, int multiple, string replaceNull)
        {
            try
            {
                if (value != System.DBNull.Value && value != null)
                {
                    if (multiple == 0)
                    {
                        return Convert.ToInt64(value).ToString();
                    }
                    else
                    {
                        return (Convert.ToInt64(value) * multiple).ToString();
                    }
                }

            }
            catch
            {

            }

            if (replaceNull != null)
            {
                return replaceNull;
            }
            else
            {
                return "Null";
            }
        }

        /// <summary>
        /// 格式化数字，为 Insert 或 UpDate 做准备。
        /// </summary>
        /// <param name="value">准备存入数据库的数字。</param>
        /// <returns></returns>
        public static string SqlDecimal(object value)
        {
            return SqlDecimal(value, 0, 0, null);
        }

        /// <summary>
        /// 格式化数字并将此数字乘以一个倍率，为 Insert 或 UpDate 做准备。
        /// </summary>
        /// <param name="value">准备存入数据库的数字。</param>
        /// <param name="multiple">倍率，如果输入的数字有效时将 value 乘以此数字。如果此参数输入“0”则表示不进行倍率运算。</param>
        /// <returns></returns>
        public static string SqlDecimal(object value, double multiple)
        {
            return SqlDecimal(value, 0, multiple, null);
        }

        /// <summary>
        /// 格式化数字并截取数字的小数位数，为 Insert 或 UpDate 做准备。
        /// </summary>
        /// <param name="value">准备存入数据库的数字。</param>
        /// <param name="decimalLength">小数部分的长度，如果该值为 0 则不对小数部分进行处理。</param>
        /// <returns></returns>
        public static string SqlDecimal(object value, int decimalLength)
        {
            return SqlDecimal(value, decimalLength, 0, null);
        }

        /// <summary>
        /// 格式化数字并将此数字乘以一个倍率后截取数字的小数位数，为 Insert 或 UpDate 做准备。
        /// </summary>
        /// <param name="value">准备存入数据库的数字。</param>
        /// <param name="decimalLength">小数部分的长度，如果该值为 0 则不对小数部分进行处理。</param>
        /// <param name="multiple">倍率，如果输入的数字有效时将 value 乘以此数字。如果此参数输入“0”则表示不进行倍率运算。</param>
        /// <returns></returns>
        public static string SqlDecimal(object value, int decimalLength, double multiple)
        {
            return SqlDecimal(value, decimalLength, multiple, null);
        }

        /// <summary>
        /// 格式化数字并将此数字乘以一个倍率后截取数字的小数位数，为 Insert 或 UpDate 做准备。
        /// </summary>
        /// <param name="value">准备存入数据库的数字。</param>
        /// <param name="decimalLength">小数部分的长度，如果该值为 0 则不对小数部分进行处理。</param>
        /// <param name="multiple">倍率，如果输入的数字有效时将 value 乘以此数字。如果此参数输入“0”则表示不进行倍率运算。</param>
        /// <param name="replaceNull">如果输入的数据无效或者类型转换时发生错误，将使用该值替代。默认情况下该值应为“Null”，以表示在数据库中存入一个空值。</param>
        /// <returns></returns>
        public static string SqlDecimal(object value, int decimalLength, double multiple, string replaceNull)
        {
            try
            {
                if (value != System.DBNull.Value && value != null)
                {
                    decimal decValue = Convert.ToDecimal(value);

                    if (multiple != 0)
                    {
                        decValue = decValue * Convert.ToDecimal(multiple);
                    }

                    if (decimalLength > 0)
                    {
                        return Convert.ToString(Math.Round(decValue, decimalLength));
                    }
                    else
                    {
                        return decValue.ToString();
                    }
                }
            }
            catch
            {

            }

            if (replaceNull != null)
            {
                return replaceNull;
            }
            else
            {
                return "Null";
            }
        }

        /// <summary>
        /// 将日期转为 YYYYMMDD 格式，为 Insert 或 UpDate 做准备。
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string SqlDateNum(DateTime date)
        {
            return ToIntDate(date).ToString();
        }

        /// <summary>
        /// 将日期转为 YYYYMMDD 格式，为 Insert 或 UpDate 做准备。
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string SqlDateNum(DateTime? date)
        {
            if (date == null)
            {
                return "Null";
            }
            else
            {
                return ToIntDate(date).ToString();
            }
        }

        /// <summary>
        /// 将时间转为 hhmmss 格式，为 Insert 或 UpDate 做准备。
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string SqlTimeNum(DateTime time)
        {
            return ToIntTime(time).ToString();
        }

        /// <summary>
        /// 将时间转为 hhmmss 格式，为 Insert 或 UpDate 做准备。
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string SqlTimeNum(DateTime? time)
        {
            if (time == null)
            {
                return "Null";
            }
            else
            {
                return ToIntTime(time).ToString();
            }
        }

        /// <summary>
        /// 处理字符串，为数据库的 Insert 或 UpDate 做准备
        /// </summary>
        /// <param name="value">准备存入字符型字段的字符串</param>
        /// <returns></returns>
        public static string SqlStr(object value)
        {
            return SqlStr(value, -1, null);
        }

        /// <summary>
        /// 处理字符串并按指定长度进行截取，为数据库的 Insert 或 UpDate 做准备
        /// </summary>
        /// <param name="value">准备存入字符型字段的字符串</param>
        /// <param name="length">该字符串的限定长度，如果设置为“-1”则表示不限定长度，否则如果字符串的字节长度超出该值则进行截取。</param>
        /// <returns></returns>
        public static string SqlStr(object value, int length)
        {
            return SqlStr(value, length, null);
        }

        /// <summary>
        /// 处理字符串（如果 value 参数为空则使用 replaceNull 替代）。为数据库的 Insert 或 UpDate 做准备
        /// </summary>
        /// <param name="value">准备存入字符型字段的字符串</param>
        /// <param name="replaceNull">如果准备存入数据库的字符串是空，则使用此值替代</param>
        /// <returns></returns>
        public static string SqlStr(object value, string replaceNull)
        {
            return SqlStr(value, -1, replaceNull);
        }

        /// <summary>
        /// 处理字符串并按指定长度进行截取（如果 value 参数为空则使用 replaceNull 替代）。为数据库的 Insert 或 UpDate 做准备
        /// </summary>
        /// <param name="value">准备存入字符型字段的字符串</param>
        /// <param name="length">该字符串的限定长度，如果设置为“-1”则表示不限定长度，否则如果字符串的字节长度超出该值则进行截取。</param>
        /// <param name="replaceNull">如果准备存入数据库的字符串是空，则使用此值替代</param>
        /// <returns></returns>
        public static string SqlStr(object value, int length, string replaceNull)
        {
            try
            {
                if (value != System.DBNull.Value && value != null)
                {
                    string strValue = Convert.ToString(value);
                    if (strValue.Trim().Length > 0)
                    {
                        if (length == 0)
                        {
                            return "'" + strValue.Replace("'", "''") + "'";
                        }
                        else
                        {
                            return "'" + StringInterceptFill(strValue, length, -1, "", false).Replace("'", "''") + "'";
                        }
                    }
                }
            }
            catch
            {
            }
            if (replaceNull == null)
            {
                return "Null";
            }
            else
            {
                return "'" + replaceNull + "'";
            }
        }

        #endregion

        #region << MD5加密 >>
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="str">加密字符</param>
        /// <returns></returns>
        public static string md5(string str)
        {
            string md5str = "";
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            for (int i = 0; i < s.Length; i++)
            {
                md5str += s[i].ToString("X2");
            }
            return md5str.Substring(8, 16);
        }
        #endregion
    }
}
