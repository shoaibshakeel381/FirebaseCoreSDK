## What is FirebaseCoreSDK

FirebaseCoreSDK is .net core library for interacting with firebase database, Cloud Messaging and storage. Library is supposed to be used in server side apps. This code is based on https://github.com/shoaibshakeel381/FirebaseCoreAdmin 
But it has improved code organization with additional fixes and enhancements. It supports
- Firebase Cloud Messaging (Tested Basic, Android and APNs Messaging modes, WebPush should work, but is untested)
- Firebase Realtime Database (Basic Crud, no Offline or Streaming support)
- Firebase Storage
- Authentication (Generate Custom Token, no user management)
- Logging (You can provide logger in `FirebaseSDKConfiguration`)
- Support for plugging custom HttpClient for supprting Mocks, `IHttpClientFactory`, Polly etc

## Initialization

Supports both json and p12 config files.
In order to give permissions to FirebaseCoreSdk to use your firebase project you need to first create private key in [service account] tab on (https://console.cloud.google.com/iam-admin/serviceaccounts). After creating service account you will be propmted to download either json file or p12 file, recommended is json file. Download that file and attach to your project.

* Json file
``` C#
var configuration = new FirebaseSDKConfiguration {
    Credentials = new JsonServiceAccountCredentials("your-file.json")
};

var firebaseClient = new FirebaseClient(configuration);
```

* Json string
``` C#
var configuration = new FirebaseSDKConfiguration {
    Credentials = new JsonServiceAccountCredentials("your-json-string", false)
};

var firebaseClient = new FirebaseClient(configuration);
```

* P12 file
``` C#
var configuration = new FirebaseSDKConfiguration {
    Credentials = new P12ServiceAccountCredentials("your-file.p12", "your-secret", "your-service-account", "your-database")
};
var firebaseClient = new FirebaseClient(configuration);
```

## Auth
Create token for some `userId` which should be used by client to authenticate against firebase database, that token could be used in client sdks by calling `firebase.auth().signInWithCustomToken(token)`

```C#
 var token = firebaseClient.Auth.CreateCustomToken(userId);
```

## Database
Getting reference on some node of database use `firebaseClient.Database.Ref("endpoint")` for example `firebaseClient.Database.Ref("users/12/details")`

### Query database
For getting data
* GetAsync
* GetWithKeyInjectedAsync

Examples:
Let's say you have this structure in firebase 
`-users/{userId}/events`

                      --EventKey1
                      ---- CodeId: 1
                      ---- IsRead: true,
                      ---- Timestamp: 1502047422150
                      --EventKey2
                      ---- CodeId: 2
                      ---- IsRead: false,
                      ---- Timestamp: 1502047422279

Let's assume we have UserHistory class
```C#
class UserHistory {
    public int CodeId { get; set; }
    public bool IsRead { get; set; }
    public long Timestamp { get; set; }
}
```

and can query via

```C#
await firebaseClient.Auth.AuthenticateAsync();
var result = await firebaseClient.Database.Ref("users/330/events/EventKey2")
                .GetAsync<UserHistory>();

var queryBuilder = QueryBuilder.New()
                .Shallow(true)
                .StartAt("startvalu")
                .OrderBy("asd")
                .LimitToLast(1);

var result1 = await client.Database.Ref("users/330/events/EventKey2").GetAsync<T>(queryBuilder);

```
We can inject key into model by inheriting `UserHistory: KeyEntity`
and instead of calling `.GetAsync<UserHistory>()` call `.GetWithKeyInjectedAsync<UserHistory>()`
like:

```C#
await firebaseClient.Auth.AuthenticateAsync();
var result = await firebaseClient.Database.Ref("users/330/eventsEventKey2")
                .GetWithKeyInjectedAsync<UserHistory>();
```


### Update database

* PushAsync
* SetAsync
* UpdateAsync
* DeleteAsync

Methods are functioning exactly like their counterparts in NodeJs or Java sdks. 

Examples:

`Push`
```C#
await firebaseClient.Auth.AuthenticateAsync();
var result = await firebaseClient.Database.Ref("/users/30/details").PushAsync(new Detail())

```

`Bulk update`
```C#
await firebaseClient.Auth.AuthenticateAsync();
var result = await firebaseClient.Database.Ref("/users/30/details").UpdateAsync(new Dictionary<string, object>() {
                { "codeId", 20 } ,
                { "info","info"} ,
                { "sub/info","subinfo"} ,
             });
```

`Set`

```C#
await firebaseClient.Auth.AuthenticateAsync();
var result = await firebaseClient.Database.Ref("/test").SetAsync(new Test1());
```

`Delete`

```C#
await firebaseClient.Auth.AuthenticateAsync();
var result = await firebaseClient.Database.Ref("/test").DeleteAsync();
```


## Storage
Following storage methods are supported

* GetPublicUrl
* GetSignedUrl
* RemoveObjectAsync
* GetObjectMetaDataAsync
* MoveObjectAsync

Examples:

```C#
await firebaseClient.Auth.AuthenticateAsync();
var result = await firebaseClient.Storage.GetObjectMetaDataAsync("test/my-image");

var publicUrl = firebaseClient.Storage.GetPublicUrl("my-image");

var signedUrl = firebaseClient.Storage.GetSignedUrl(new Firebase.Storage.SigningOption()
             {
                 Action = Firebase.Storage.SigningAction.Write,
                 Path = "my-image",
                 ContentType = "image/jpeg",
                 ExpireDate = DateTime.Now + new TimeSpan(0, 0, 0, 0, 60000000)
             });
```

## Cloud Messaging

* SendCloudMessageAsync

Example:

```C#
await firebaseClient.Auth.AuthenticateAsync();

var message = new FirebasePushMessage();

var result = await firebaseClient.CloudMessaging.SendCloudMessageAsync(message);
```

## Custom Http Request Handling

It is now possible to provide custom HTTP request handler by providing an implementation for `IHttpClientProxy` in `FirebaseSDKConfiguration`. SDK comes with a default implementation as `TransientHttpClientProxy`, clients can override it if need to. A sample implementation for using Polly is given below. In ASP.NET Core >= 2.1, it is recomended to also use `IHttpRequestFactory` to create instances of `HttpClient`.

Polly Sample Code:

```C#
public class FirebaseHttpClientProxy : TransientHttpClientProxy
{
    private readonly AsyncRetryPolicy<HttpResponseMessage> _retryPolicy;

    public FirebaseHttpClientProxy()
    {
        _retryPolicy = HttpPolicyExtensions
            .HandleTransientHttpError()
            .Or<FirebaseHttpException>(StatusCodeShouldBeRetried)
            .WaitAndRetryAsync(3, attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt))));
    }

    /// <inheritdoc />
    public override async Task<string> SendAsync(Func<HttpRequestMessage> request)
    {
        var response = await _retryPolicy.ExecuteAsync(() => SendAsync(request()));
        return await GetResponseContent(response);
    }

    private async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
    {
        var uri = request.RequestUri;

        using (var client = new HttpClient()) // TODO: Use IHttpClientFactory here
        {
            var response = await client.SendAsync(request);
            await EnsureSuccessStatusCodeAsync(response, request, null).ConfigureAwait(false);
            
            return response;
        }
    }

    private static bool StatusCodeShouldBeRetried(FirebaseHttpException r)
    {
        if (r.ResponseMessage?.StatusCode < HttpStatusCode.InternalServerError)
        {
            return r.ResponseMessage.StatusCode == HttpStatusCode.RequestTimeout;
        }

        return true;
    }
}
```

Instance of this class can be provided in `FirebaseSDKConfiguration` which will be passed to `FirebaseClient` constructor.

```c#
var sdkConfiguration = new FirebaseSDKConfiguration 
    { 
        HttpClientProxy = new FirebaseHttpClientProxy(),
        Credentials = new JsonServiceAccountCredentials("json-service-account-credentials-string", false);
    };

var firebaseClient = new FirebaseClient(sdkConfiguration);
```



