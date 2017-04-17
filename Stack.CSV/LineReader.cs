/*******************************************************
 * Filename: LineReader.cs
 * File description：
 * 
 * Version:	1.0
 * Created:	2016/5/17 16:01:09
 * Author:	Bruce Ma
 * 
*****************************************************/

using System.IO;
using System.Text;

namespace Stack.CSV
{
    /// <summary>
    /// LineReader
    /// </summary>
    public class LineReader
    {
        #region Variables

        private StreamReader reader;
        private bool keepCarriageReturns;

        #endregion

        #region Properties

        #endregion

        public LineReader(StreamReader reader, bool keepCarriageReturns)
        {
            this.reader = reader;
            this.keepCarriageReturns = keepCarriageReturns;
        }

        #region Methods

        public string ReadLine()
        {
            return keepCarriageReturns ? readUntilNewline() : reader.ReadLine();
        }

        #endregion

        #region Helper

        private string readUntilNewline()
        {
            StringBuilder sb = new StringBuilder();
            for (int c = reader.Read(); c > -1 && c != '\n'; c = reader.Read())
            {
                sb.Append((char)c);
            }

            return sb.Length > 0 ? sb.ToString() : null;
        }

        #endregion
    }
}