/*******************************************************
 * Filename: BooleanExtension.cs
 * File description：
 * 
 * Version:	1.0
 * Created:	2016/5/9 19:08:57
 * Author:	Bruce Ma
 * 
*****************************************************/


namespace System
{
    /// <summary>
    /// BooleanExtension
    /// </summary>
    public static class BooleanExtension
    {
        public static int ToInt(this bool b)
        {
            return b ? 1 : 0;
        }
    }
}