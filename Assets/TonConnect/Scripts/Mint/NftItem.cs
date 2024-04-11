using System;
using System.Text;
using Newtonsoft.Json;
using TonSdk.Core;
using TonSdk.Core.Boc;
using UnityEngine;

namespace Mint
{
    public class NftItem
    {

        public Address NftCollectionAddress;
        private MintParams MintParams;
        public NftItem(Address nftCollectionAddress,MintParams mintParams)
        {
            NftCollectionAddress = nftCollectionAddress;
            MintParams = mintParams;
        }
        public Cell CreateMintBody(){
            var body = new CellBuilder();
            body.StoreUInt(1, 32);
            body.StoreUInt(MintParams.QueryId, 64);
            body.StoreUInt(MintParams.ItemIndex, 64);
            body.StoreCoins(MintParams.Amount);
            var nftItemContent = new CellBuilder();
            nftItemContent.StoreAddress(MintParams.ItemOwnerAddress);
            var uriContent =  new CellBuilder();
            // Debug.Log($"CommonContentUrl {MintParams.CommonContentUrl}");
            var bytes = Encoding.UTF8.GetBytes(MintParams.CommonContentUrl);
            // Debug.Log($"bytes {bytes}");
            // // 将字节数组转换为十六进制字符串
            // string hexString = BitConverter.ToString(bytes).Replace("-", "");
            uriContent.StoreBytes(bytes);
            nftItemContent.StoreRef(uriContent.Build());
            body.StoreRef(nftItemContent.Build());
            var bodyCell = body.Build();
            return bodyCell;
        }
       
    }

}
