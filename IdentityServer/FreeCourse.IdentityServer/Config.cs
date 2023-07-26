// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Security.Principal;

namespace FreeCourse.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new ApiResource("resource_catalog"){Scopes={"catalog_fullpermission"}},
            new ApiResource("resource_photo_stock"){Scopes={ "photo_stock_fullpermission"}},
            new ApiResource("resource_basket"){Scopes={"basket_fullpermission"}},
            new ApiResource("resource_discount"){Scopes={"discount_fullpermission"}},
            new ApiResource("resource_order"){Scopes={"order_fullpermission"}},
            new ApiResource("resource_payment"){Scopes={"payment_fullpermission"}},
            new ApiResource("resource_gateway"){Scopes={"gateway_fullpermission"}},
            new ApiResource(IdentityServerConstants.LocalApi.ScopeName)

        };
        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                   new IdentityResources.Email(),
                   new IdentityResources.OpenId(),
                   new IdentityResources.Profile(),
                   new IdentityResource(){Name="Roles",DisplayName="Roles",Description="Kullanıcı rolleri",UserClaims=new[]{"Roles"} }
                   };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
        {
         new ApiScope("catalog_fullpermission","Catalog API full permission"),
            new ApiScope("photo_stock_fullpermission","Photo Stock Api full permission"),
            new ApiScope("basket_fullpermission","basket api full permission"),
            new ApiScope("discount_fullpermission","discount api full permission"),
            new ApiScope("order_fullpermission","order api full permission"),
            new ApiScope("payment_fullpermission","payment api full permission"),
            new ApiScope("gateway_fullpermission","gateway api full permission"),
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
        };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
               new Client
               {
                   ClientName="asp.net core mvc",
                   ClientId="WebMvcClient",
                   ClientSecrets={new Secret("secret".Sha256()) },
                   AllowedGrantTypes=GrantTypes.ClientCredentials,
                   AllowedScopes={ "catalog_fullpermission" , "photo_stock_fullpermission" , "gateway_fullpermission", IdentityServerConstants.LocalApi.ScopeName}
               },
               new Client
               {
                   ClientName="asp.net core mvc",
                   ClientId="WebMvcClientforUser",
                   AllowOfflineAccess=true,
                   ClientSecrets={new Secret("secret".Sha512()) },
                   AllowedGrantTypes=GrantTypes.ResourceOwnerPassword,
                   AllowedScopes={ "basket_fullpermission", "payment_fullpermission", "gateway_fullpermission", "order_fullpermission", "discount_fullpermission", IdentityServerConstants.StandardScopes.Email,IdentityServerConstants.StandardScopes.OpenId,IdentityServerConstants.StandardScopes.Profile,IdentityServerConstants.StandardScopes.OfflineAccess, IdentityServerConstants.LocalApi.ScopeName, "Roles" },
                   AccessTokenLifetime= 3600,
                   RefreshTokenExpiration=TokenExpiration.Absolute,
                   AbsoluteRefreshTokenLifetime=(int)(DateTime.Now.AddDays(60)-DateTime.Now).TotalSeconds,
                   RefreshTokenUsage=TokenUsage.ReUse   
,

               }

            };
            
    }
}