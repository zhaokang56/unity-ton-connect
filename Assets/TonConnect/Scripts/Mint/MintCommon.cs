using System;
using System.Collections.Generic;
using System.Text;
using TonSdk.Core.Boc;

namespace Mint
{
    public static class MintCommon
    {
         public static  Cell EncodeOffChainContent(string content)
            {
                byte[] data = Encoding.UTF8.GetBytes(content);
                byte[] offChainPrefix = { 0x01 };
                byte[] prefixedData = ConcatArrays(offChainPrefix, data);
                return MakeSnakeCell(prefixedData);
            }
        
            private static  List<byte[]> BufferToChunks(byte[] buff, int chunkSize)
            {
                List<byte[]> chunks = new List<byte[]>();
                int offset = 0;
                while (offset < buff.Length)
                {
                    int length = Math.Min(chunkSize, buff.Length - offset);
                    byte[] chunk = new byte[length];
                    Array.Copy(buff, offset, chunk, 0, length);
                    chunks.Add(chunk);
                    offset += length;
                }
                return chunks;
            }
        
            private static  Cell MakeSnakeCell(byte[] data)
            {
                List<byte[]> chunks = BufferToChunks(data, 127);
        
                if (chunks.Count == 0)
                {
                    return new CellBuilder().Build();
                }
        
                if (chunks.Count == 1)
                {
                    return new CellBuilder().StoreBytes(chunks[0]).Build();
                }
        
                CellBuilder curCell = new CellBuilder();
        
                for (int i = chunks.Count - 1; i >= 0; i--)
                {
                    byte[] chunk = chunks[i];
        
                    curCell.StoreBytes(chunk);
        
                    if (i - 1 >= 0)
                    {
                        CellBuilder nextCell = new CellBuilder();
                        nextCell.StoreRef(curCell.Build()).Build();
                        curCell = nextCell;
                    }
                }
        
                return curCell.Build();
            }
        
            private static  byte[] ConcatArrays(byte[] array1, byte[] array2)
            {
                byte[] result = new byte[array1.Length + array2.Length];
                Buffer.BlockCopy(array1, 0, result, 0, array1.Length);
                Buffer.BlockCopy(array2, 0, result, array1.Length, array2.Length);
                return result;
            }
    }
}