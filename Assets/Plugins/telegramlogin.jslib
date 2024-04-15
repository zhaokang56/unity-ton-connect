mergeInto(LibraryManager.library, {

  Hello: function () {
    window.alert("Hello, world!");
  },

  HelloString: function (str) {
    window.alert(UTF8ToString(str));
  },

  LoginTelegram: function () {
           window.Telegram.Login.auth({ bot_id: '7023895386:AAEqstHmLhfxYluUh72ELjv2c-vetB7bQEA', request_access: 'write', embed: 1 }, (data) => {
           console.log(data, '这是回调数据');//这里的data和之前返回的user数据和格式无差异
           if(!data)
           {
            unityInstanceRef.SendMessage("JSManager", "OnLoginErrorCallBack", JSON.stringify(data));
           }
           else
           {
           unityInstanceRef.SendMessage("JSManager", "OnLoginSuccessCallBack", JSON.stringify(data));
           }
           });
  },
});
