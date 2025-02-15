﻿using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;
using System.Reflection;
namespace CatJson
{
    public static class Util
    {
        public static StringBuilder CachedSB = new StringBuilder();
        public static bool IsFormat { get; set; }

        static Util()
        {
            IsFormat = true;
        }

        public static void AppendTab(int tabNum)
        {
            if (!IsFormat)
            {
                return;
            }
            for (int i = 0; i < tabNum; i++)
            {
                CachedSB.Append("\t");
            }
        }

        public static void Append(string str,int tabNum = 0)
        {
            if (tabNum > 0 && IsFormat)
            {
                AppendTab(tabNum);
            }
           
            CachedSB.Append(str);
        }

        public static void AppendLine(string str, int tabNum = 0)
        {
            if (tabNum > 0 && IsFormat)
            {
                AppendTab(tabNum);
            }

            if (IsFormat)
            {
                CachedSB.AppendLine(str);
            }
            else
            {
                CachedSB.Append(str);
            }
        }

        /// <summary>
        /// type是否为内置基础类型 (string char bool 数字)
        /// </summary>
        public static bool IsBaseType(Type type)
        {
            return type == typeof(string) || type == typeof(char) || type == typeof(bool) || IsNumberType(type);
        }

        /// <summary>
        /// type是否为数字类型(byte int long float double)
        /// </summary>
        public static bool IsNumberType(Type type)
        {
            return type == typeof(byte) || type == typeof(int) || type == typeof(long) || type == typeof(float) || type == typeof(double)
            ||type == typeof(uint)||type == typeof(ulong)||type == typeof(ushort)||type == typeof(short) ||type == typeof(decimal)
            ||type == typeof(sbyte);
        }

        /// <summary>
        /// obj是否为数字（byte int long float double)
        /// </summary>
        public static bool IsNumber(object obj)
        {
            return obj is byte || obj is int || obj is long || obj is float || obj is double
                   ||obj is uint||obj is ulong ||obj is ushort ||obj is short ||obj is decimal 
                   ||obj is sbyte ;
        }

        /// <summary>
        /// obj是否为数组或List
        /// </summary>
        public static bool IsArrayOrList(object obj)
        {
            return obj is Array || obj is IList;
        }

        /// <summary>
        /// type是否为array或list的type
        /// </summary>
        public static bool IsArrayOrListType(Type type)
        {
            return type.IsArray || (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>));
        }

        /// <summary>
        /// obj是否为字典
        /// </summary>
        public static bool IsDictionary(object obj)
        {
            return obj is IDictionary;
        }

        /// <summary>
        /// type是否为字典的type
        /// </summary>
        public static bool IsDictionaryType(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Dictionary<,>);
        }

        /// <summary>
        /// 获取数组和List的元素类型
        /// </summary>
        public static Type GetArrayElementType(Type type)
        {
            if (type.IsArray)
            {
                return type.GetElementType();
            }

            return type.GetGenericArguments()[0];
        }

        /// <summary>
        /// value是否为内置基础类型的默认值(null 0 false)
        /// </summary>
        public static bool IsDefaultValue(object value)
        {
            if (!(value is System.ValueType))
            {
                return value == default;
            }

            if (value is byte b)
            {
                return b == default;
            }

            if (value is int i)
            {
                return i == default;
            }
            if (value is long l)
            {
                return l == default;
            }
            if (value is float f)
            {
                return Math.Abs(f - default(float)) < 1e-6f;
            }
            if (value is double d)
            {
                return Math.Abs(d - default(double)) < 1e-15;
            }

            if (value is bool boolean)
            {
                return boolean == default;
            }
            
            if (value is sbyte sb)
            {
                return sb == default;
            }
            if (value is short s)
            {
                return s == default;
            }
            if (value is uint ui)
            {
                return ui == default;
            }
            if (value is ulong ul)
            {
                return ul == default;
            }
            if (value is ushort us)
            {
                return us == default;
            }
            if (value is decimal de)
            {
                return de == default;
            }



            return false;

        }

        /// <summary>
        /// 获取4个字符代表的unicode码点
        /// </summary>
        public static char GetUnicodeCodePoint(char c1,char c2,char c3,char c4)
        {
            int temp = UnicodeChar2Int(c1) * 0x1000 + UnicodeChar2Int(c2) *0x100 + UnicodeChar2Int(c3)*0x10 + UnicodeChar2Int(c4);
            return (char)temp;
        }

        private static int UnicodeChar2Int(char c)
        {
            //0-9
            if (char.IsDigit(c))
            {
                return c - '0';
            }
            
            //A-F
            if (c >= 65 && c <= 70)
            {
                return c - 'A' + 10;
            }

            //a-f
            if (c >= 97 && c <= 102)
            {
                return c - 'a' + 10;
            }

            throw new Exception("Char2Int调用失败，当前字符为：" + c);
        }
    

    }

}
