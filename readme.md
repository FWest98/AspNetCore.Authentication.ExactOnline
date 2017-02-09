# Exact online


### 1.0.0

* Initial release

## Usage 
```csharp
app.UseExactOnlineAuthentication(new ExactOnlineAuthenticationOptions()
{
    ClientId = "",
    ClientSecret = "",
    WebhookSecret = ""
});

//or to map to appsettings directly
services.Configure<ExactOnlineAuthenticationOptions>(Configuration.GetSection("ExactOnline"));
app.UseExactOnlineAuthentication();
```


## Api calls

to do api calls you can find the security tokens under claims:

* urn:exactonline:access_token_expires_at"
* urn:exactonline:access_token
* urn:exactonline:refresh_token
* urn:exactonline:division (current devision logged in with)

No rolling refresh_token is implemented yet. (Make a pull request if you like).