using System.Threading.Tasks;
using TonSdk.Client;
using TonSdk.Core;
using Unity.VisualScripting;
using UnityEngine;

public class TonClientHandler : MonoBehaviour
{
    private TonClient client;
    private void Awake()
    {
          client = new(new() { Endpoint = "https://testnet.toncenter.com/api/v2/jsonRPC", ApiKey = "8e917567701b99d52f56599ab343121c520b8f215c05d8817d82fd9150b02a37" });
    }

    public async Task<string> GetAmount(Address address)
    {
       var reslut= await client.GetBalance(address);
       if (reslut==null)
       {
           return "0";
       }
       else
       {
           return reslut.ToString();
       }
    }

    public async Task GetNftList()
    {
        var nftResult= await client.Nft.GetNftItemData(new Address("EQDkasNWHVpjfmraVHGhQwYBQSV4ER1iQHzE58Zb2sdOXtHn"));
        Debug.Log(JsonUtility.ToJson(nftResult.Content.Parse()));
    }

    public async Task TestMint()
    {
       
    }
}
