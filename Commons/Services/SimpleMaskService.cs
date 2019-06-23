using System.Collections.Generic;

namespace Commons
{
    public class SimpleMaskService : ISimpleMaskService
    {
        public byte[] MaskUnmask(byte[] buffer)
        {
            byte tempValue = 0;
            var resultList = new List<byte>();
            foreach (var item in buffer)
            {
                tempValue = (byte)(item ^ 13);
                resultList.Add(tempValue);
            }
            return resultList.ToArray();
        }
    }
}
