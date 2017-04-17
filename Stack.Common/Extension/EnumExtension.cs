/*******************************************************
 * Filename: EnumExtension.cs
 * File description：
 * 
 * Version:	1.0
 * Created:	2017/04/17 16:02:55
 * Author:	Bruce Ma
 * 
*****************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    /// <summary>
    /// EnumExtension
    /// </summary>
    public static class EnumExtension
    {
        /// <summary>
        /// 返回枚举项的描述信息。
        /// </summary>
        /// <param name="input">要获取描述信息的枚举项。</param>
        /// <returns>枚举想的描述信息。</returns>
        public static string GetDescription(this Enum input, bool isTop = false)
        {
            Type enumType = input.GetType();
            DescriptionAttribute attr = null;
            if (isTop)
            {
                attr = (DescriptionAttribute)Attribute.GetCustomAttribute(enumType, typeof(DescriptionAttribute));
            }
            else
            {
                // 获取枚举常数名称。
                string name = Enum.GetName(enumType, input);
                if (name != null)
                {
                    // 获取枚举字段。
                    FieldInfo fieldInfo = enumType.GetField(name);
                    if (fieldInfo != null)
                    {
                        // 获取描述的属性。
                        attr = Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute), false) as DescriptionAttribute;
                    }
                }
            }

            if (attr != null && !string.IsNullOrEmpty(attr.Description))
                return attr.Description;
            else
                return string.Empty;

        }
        /// <summary>
        /// 获取枚举类型的值和描述
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Dictionary<int, string> GetValuesAndDescriptions(this Enum input)
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();

            foreach (FieldInfo fieldInfo in input.GetType().GetFields())
            {
                DescriptionAttribute attr = fieldInfo.GetCustomAttribute<DescriptionAttribute>();
                if (attr != null && !string.IsNullOrEmpty(attr.Description))
                {
                    dic.TryAdd(int.Parse(fieldInfo.GetRawConstantValue().ToString()), attr.Description);
                }
            }

            return dic;
        }
    }
}