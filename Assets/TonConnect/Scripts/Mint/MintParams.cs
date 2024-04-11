using TonSdk.Core;

namespace Mint
{
    public class MintParams
    {
        public ulong QueryId;
        public Address ItemOwnerAddress;
        public ulong ItemIndex;
        public Coins Amount;
        public string CommonContentUrl;

        public MintParams(ulong queryId,Address itemOwnerAddress,ulong itemIndex,string commonContentUrl)
        {
            ItemOwnerAddress = itemOwnerAddress;
            ItemIndex = itemIndex;
            CommonContentUrl = commonContentUrl;
            Amount = new Coins(0.05);
            QueryId = queryId;
        }
    }
}