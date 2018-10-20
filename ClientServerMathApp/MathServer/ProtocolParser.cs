using System;

namespace MathServer
{
    /// <summary>
    /// Class for parsing protocol info.
    /// </summary>
    public static class ProtocolParser
    {
        /// <summary>
        /// Constructs protocol info from protocol text.
        /// </summary>
        /// <param name="protocolText"> Protocol text. </param>
        /// <returns> Returns protocol info. </returns>
        public static ProtocolInfo Parse(string protocolText)
        {
            //first of all we have to split protocol text with symbol ':'
            var protocolParts = protocolText.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
            double firstValue, secondValue;

            //if protocol text does not have correct format 
            //then throw an exception
            if (protocolParts.Length != 3 ||
                protocolParts[0] != "+" &&
                protocolParts[0] != "-" &&
                protocolParts[0] != "/" &&
                protocolParts[0] != "*" ||
                !double.TryParse(protocolParts[1], out firstValue) ||
                !double.TryParse(protocolParts[2], out secondValue))
            {
                throw new FormatException("Invalid protocol format.");
            }

            //if protocol format is correct then construct protocol info from protocol text
            var protocolInfo = new ProtocolInfo();
            protocolInfo.FirstValue = firstValue;
            protocolInfo.SecondValue = secondValue;

            //here we are sure that protocolparts[0] has length = 1,
            //and it only contains operator symbol
            protocolInfo.Operator = protocolParts[0][0];

            //Return protocol info;
            return protocolInfo;
        }
    }
}