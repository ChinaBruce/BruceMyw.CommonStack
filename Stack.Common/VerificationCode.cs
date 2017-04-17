/*******************************************************
 * Filename: VerificationCode.cs
 * File description：
 * 
 * Version:	1.0
 * Created:	2016/06/28 13:31:23
 * Author:	Bruce Ma
 * 
*****************************************************/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stack.Common
{
    /// <summary>
    /// 验证码生成器
    /// </summary>
    public class VerificationCode
    {
        /// <summary>
        /// 创建验证码，默认不包含干扰线和干扰点
        /// </summary>
        /// <param name="code">验证码</param>
        /// <param name="codeCount">验证码数量</param>
        /// <param name="codeType">验证码类别</param>
        /// <returns></returns>
        public static byte[] Create(out string code, int codeCount = 4, CodeType codeType = CodeType.Number)
        {
            code = CreateCode(codeCount, codeType);
            return CreateImage(code, false, false);
        }
        /// <summary>
        /// 创建验证码，包含干扰线和干扰点
        /// </summary>
        /// <param name="code">验证码</param>
        /// <param name="codeCount">验证码数量</param>
        /// <param name="codeType">验证码类别</param>
        /// <returns></returns>
        public static byte[] CreateWithDisturb(out string code, int codeCount = 4, CodeType codeType = CodeType.Number)
        {
            code = CreateCode(codeCount, codeType);
            return CreateImage(code);
        }
        /// <summary>
        /// 随机生成验证码
        /// </summary>
        /// <param name="codeCount">验证码个数</param>
        /// <param name="codeType">验证码类型</param>
        /// <returns></returns>
        public static string CreateCode(int codeCount = 4, CodeType codeType = CodeType.Number)
        {
            var verficationCode = string.Empty;
            var allChar = "";
            if((codeType & CodeType.Number) == CodeType.Number)
            {
                allChar += "0,1,2,3,4,5,6,7,8,9";
            }

            if ((codeType & CodeType.Alphabet) == CodeType.Alphabet)
            {
                if(!string.IsNullOrWhiteSpace(allChar))
                {
                    allChar += ",";
                }
                allChar += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,W,X,Y,Z,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z";
            }

            string[] allCharArray = allChar.Split(',');
            int temp = -1;
            Random rand = new Random();
            for (int i = 0; i < codeCount; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * ((int)DateTime.Now.Ticks));
                }
                int t = rand.Next(61);
                if (temp == t)
                {
                    return CreateCode(codeCount, codeType);
                }
                temp = t;
                verficationCode += allCharArray[t];
            }

            return verficationCode;
        }
        /// <summary>
        /// 生成图片，格式为png
        /// </summary>
        /// <param name="code">验证码</param>
        /// <param name="hasDisturbLine">是否包含干扰线</param>
        /// <param name="hasDisturbPoint">是否包含干扰点</param>
        /// <returns></returns>
        public static byte[] CreateImage(string code, bool hasDisturbLine = true, bool hasDisturbPoint = true)
        {
            //颜色列表，用于验证码、噪线、噪点 
            Color[] color = { Color.Black, Color.Red, Color.Blue, Color.Green, Color.Orange, Color.Brown, Color.Brown, Color.DarkBlue };
            //字体列表，用于验证码 
            string[] font = { "Times New Roman", "MS Mincho", "Calibri", "Segoe Print", "PMingLiU", "Impact" };

            Random rand = new Random();
            Bitmap bmp = new Bitmap(code.Length * 25, 40);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            if(hasDisturbLine)
            {
                //画噪线 
                for (int i = 0; i < 10; i++)
                {
                    int x1 = rand.Next(100);
                    int y1 = rand.Next(40);
                    int x2 = rand.Next(100);
                    int y2 = rand.Next(40);
                    Color clr = color[rand.Next(color.Length)];
                    g.DrawLine(new Pen(clr), x1, y1, x2, y2);
                }
            }
            
            //画验证码字符串 
            for (int i = 0; i < code.Length; i++)
            {
                string fnt = font[rand.Next(font.Length)];
                Font ft = new Font(fnt, 18);
                Color clr = color[rand.Next(color.Length)];
                g.DrawString(code[i].ToString(), ft, new SolidBrush(clr), (float)i * 20 + 10, (float)8);
            }

            if(hasDisturbPoint)
            {
                //画噪点 
                for (int i = 0; i < 100; i++)
                {
                    int x = rand.Next(bmp.Width);
                    int y = rand.Next(bmp.Height);
                    Color clr = color[rand.Next(color.Length)];
                    bmp.SetPixel(x, y, clr);
                }
            }            

            MemoryStream ms = new MemoryStream();
            try
            {
                bmp.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
            finally
            {
                bmp.Dispose();
                ms.Dispose();
                g.Dispose();
            }
        }
    }

    /// <summary>
    /// 验证码类型
    /// </summary>
    public enum CodeType
    {
        /// <summary>
        /// 数字
        /// </summary>
        Number = 1,
        /// <summary>
        /// 字母
        /// </summary>
        Alphabet = 2
    }
}