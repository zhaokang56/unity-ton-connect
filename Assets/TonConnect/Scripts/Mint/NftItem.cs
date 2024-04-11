using System.Text;
using TonSdk.Core;
using TonSdk.Core.Boc;

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
            uriContent.StoreBytes((Encoding.UTF8.GetBytes(MintParams.CommonContentUrl)));
            nftItemContent.StoreRef(uriContent.Build());
            body.StoreRef(nftItemContent.Build());
            var bodyCell = body.Build();
            return bodyCell;
        }
       
    }

}
