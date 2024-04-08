using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TonSdk.Client;
using TonSdk.Core;
using TonSdk.Core.Boc;
using UnityEngine;

public class NftCollection 
{
    public Address OwnerAddress { get; set; }
    public float RoyaltyPercent { get; set; }
    public Address RoyaltyAddress { get; set; }
    public int NextItemIndex { get; set; }
    public string CollectionContentUrl { get; set; }
    public string CommonContentUrl { get; set; }

    public NftCollection()
    {
        
    }
    
    public NftCollection(Address ownerAddress,float royaltyPercent,Address royaltyAddress,int nextItemIndex,string collectionContentUrl,string commonContentUrl)
    {
        OwnerAddress = ownerAddress;
        RoyaltyPercent = royaltyPercent;
        RoyaltyAddress = royaltyAddress;
        NextItemIndex = nextItemIndex;
        CollectionContentUrl = collectionContentUrl;
        CommonContentUrl = commonContentUrl;
    }
    public Cell CreateCodeCell(){
        var base64String =
        "te6cckECFAEAAh8AART/APSkE/S88sgLAQIBYgkCAgEgBAMAJbyC32omh9IGmf6mpqGC3oahgsQCASAIBQIBIAcGAC209H2omh9IGmf6mpqGAovgngCOAD4AsAAvtdr9qJofSBpn+pqahg2IOhph+mH/SAYQAEO4tdMe1E0PpA0z/U1NQwECRfBNDUMdQw0HHIywcBzxbMyYAgLNDwoCASAMCwA9Ra8ARwIfAFd4AYyMsFWM8WUAT6AhPLaxLMzMlx+wCAIBIA4NABs+QB0yMsCEsoHy//J0IAAtAHIyz/4KM8WyXAgyMsBE/QA9ADLAMmAE59EGOASK3wAOhpgYC42Eit8H0gGADpj+mf9qJofSBpn+pqahhBCDSenKgpQF1HFBuvgoDoQQhUZYBWuEAIZGWCqALnixJ9AQpltQnlj+WfgOeLZMAgfYBwGyi544L5cMiS4ADxgRLgAXGBEuAB8YEYGYHgAkExIREAA8jhXU1DAQNEEwyFAFzxYTyz/MzMzJ7VTgXwSED/LwACwyNAH6QDBBRMhQBc8WE8s/zMzMye1UAKY1cAPUMI43gED0lm+lII4pBqQggQD6vpPywY/egQGTIaBTJbvy9AL6ANQwIlRLMPAGI7qTAqQC3gSSbCHis+YwMlBEQxPIUAXPFhPLP8zMzMntVABgNQLTP1MTu/LhklMTugH6ANQwKBA0WfAGjhIBpENDyFAFzxYTyz/MzMzJ7VSSXwXiN0CayQ==";

        // 将 Base64 字符串解码为字节数组
        byte[] bytes = Convert.FromBase64String(base64String);

        // 将字节数组转换为二进制字符串
       StringBuilder bitsStringBuilder = new StringBuilder();

        foreach (byte b in bytes)
        {
            // 将每个字节转换为对应的比特字符串，并附加到 StringBuilder
            bitsStringBuilder.Append(Convert.ToString(b, 2).PadLeft(8, '0'));
        }
        return Cell.From(bitsStringBuilder.ToString());
    }
    public Cell CreateDataCell() {
        CellBuilder dataCell = new CellBuilder();
        var metadataIpfsHash = "QmeLyw5sU5FyTEx9zk9DSj1dA8bLADyjSvn4EocDaE5fHV";

        dataCell.StoreAddress(OwnerAddress);
        dataCell.StoreUInt(NextItemIndex, 64); //我们需要创建一个空cell，并在其中存储集合所有者地址和将要铸造的下一个项目的索引。

        CellBuilder contentCell = new CellBuilder();//创建一个将存储我们收藏内容的空cell

        Cell collectionContent = EncodeOffChainContent(CollectionContentUrl);

        CellBuilder commonContent = new CellBuilder();
        commonContent.StoreBytes((Encoding.UTF8.GetBytes(CommonContentUrl) ));

        contentCell.StoreRef(collectionContent);
        contentCell.StoreRef(commonContent.Build());
        dataCell.StoreRef(contentCell.Build());

        //NFT项目的代码cell
        Cell NftItemCodeCell = Cell.From(
            "te6cckECDQEAAdAAART/APSkE/S88sgLAQIBYgMCAAmhH5/gBQICzgcEAgEgBgUAHQDyMs/WM8WAc8WzMntVIAA7O1E0NM/+kAg10nCAJp/AfpA1DAQJBAj4DBwWW1tgAgEgCQgAET6RDBwuvLhTYALXDIhxwCSXwPg0NMDAXGwkl8D4PpA+kAx+gAxcdch+gAx+gAw8AIEs44UMGwiNFIyxwXy4ZUB+kDUMBAj8APgBtMf0z+CEF/MPRRSMLqOhzIQN14yQBPgMDQ0NTWCEC/LJqISuuMCXwSED/LwgCwoAcnCCEIt3FzUFyMv/UATPFhAkgEBwgBDIywVQB88WUAX6AhXLahLLH8s/Im6zlFjPFwGRMuIByQH7AAH2UTXHBfLhkfpAIfAB+kDSADH6AIIK+vCAG6EhlFMVoKHeItcLAcMAIJIGoZE24iDC//LhkiGOPoIQBRONkchQCc8WUAvPFnEkSRRURqBwgBDIywVQB88WUAX6AhXLahLLH8s/Im6zlFjPFwGRMuIByQH7ABBHlBAqN1viDACCAo41JvABghDVMnbbEDdEAG1xcIAQyMsFUAfPFlAF+gIVy2oSyx/LPyJus5RYzxcBkTLiAckB+wCTMDI04lUC8ANqhGIu"
        );
        dataCell.StoreRef(NftItemCodeCell);

        var royaltyBase = 1000;//版税参数
        var royaltyFactor = Mathf.FloorToInt(RoyaltyPercent * royaltyBase);
        var royaltyCell =  new CellBuilder(); //单独的cell中存储版税数据
        royaltyCell.StoreUInt(royaltyFactor, 16);
        royaltyCell.StoreUInt(royaltyBase, 16);
        royaltyCell.StoreAddress(RoyaltyAddress);
        dataCell.StoreRef(royaltyCell.Build());

        return dataCell.Build();
    }
    
    public  Cell EncodeOffChainContent(string content)
    {
        byte[] data = Encoding.UTF8.GetBytes(content);
        byte[] offChainPrefix = { 0x01 };
        byte[] prefixedData = ConcatArrays(offChainPrefix, data);
        return MakeSnakeCell(prefixedData);
    }

    private  List<byte[]> BufferToChunks(byte[] buff, int chunkSize)
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

    private  Cell MakeSnakeCell(byte[] data)
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

    private  byte[] ConcatArrays(byte[] array1, byte[] array2)
    {
        byte[] result = new byte[array1.Length + array2.Length];
        Buffer.BlockCopy(array1, 0, result, 0, array1.Length);
        Buffer.BlockCopy(array2, 0, result, array1.Length, array2.Length);
        return result;
    }

}
