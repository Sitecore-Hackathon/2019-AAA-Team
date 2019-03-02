# Sitecore Hackathon AAA Team. 

## Hackathon Category: Enhancement to Sitecore Commerce Business Tools UI. 

## Module: Advanced breadcrumbs for Merchandising Dashboard.


#### Problem:
When using merchandising dashboard, the navigation between Catalogs, Categories, Sellable items, Variants is very confusing. The default breadcrumbs component shows links to the ecommerce site root (Commerce) and to the current item, no matter where you have navigated to:

![Image](images/IMG1.png?raw=true)


#### Purpose of the module:
Advanced BizFx breadcrumbs component to help navigate in the Merchandising Dashboard. This component will show full hierarchical path to the current item + plus will be links to the over items on each hierarchical level (to switch catalogs, categories etc.). 

#### End user: 
 Can easily navigate between items of the merchandising dashboard and can easily understand where he is located. 


#### Pre-requisites:
 Installed Sitecore Ecommerce 9.0.3. 


 ## Installation

 Solution contains 2 parts to be deployed: 

 ### 1. The Copy of the "BizFx Sitecore Commerce Business Tools SDK" with advanced breadcrumbs component (sc-bizfx-breadcrumbs-new) added and integrated in the `./Sitecore.BizFx` folder.

 The source code for the new breadcrumb BizFx component files could be found under `./Sitecore.BizFx/src/app/components/actions` folder:

[sc-bizfx-breadcrumb-new.component.css](../Sitecore.BizFx/src/app/components/actions/sc-bizfx-breadcrumb-new.component.css)

[sc-bizfx-breadcrumb-new.component.html](../Sitecore.BizFx/src/app/components/actions/sc-bizfx-breadcrumb-new.component.html)

[sc-bizfx-breadcrumb-new.component.ts](../Sitecore.BizFx/src/app/components/actions/sc-bizfx-breadcrumb-new.component.ts)


 This SDK should be deployed according to the regular Sitecore installation instructions (copy located in the git path: [Sitecore.BizFx/README.md](Sitecore.BizFx/README.md):

 Before start developing make sure you have the Sitecore feed in your user's npm config.

Go to `C:\Users\[your user]\.npmrc` and add the following lines to the file:

`@speak:registry=https://sitecore.myget.org/F/sc-npm-packages/npm/`
`@sitecore:registry=https://sitecore.myget.org/F/sc-npm-packages/npm/`

If the file doesn't exists, run

```npm
npm config set @speak:registry=https://sitecore.myget.org/F/sc-npm-packages/npm/
npm config set @sitecore:registry=https://sitecore.myget.org/F/sc-npm-packages/npm/
```

Install the SPEAK and BizFx packages, run

**You can find the SPEAK tarball files at the root of the Sitecore.Commerce.[version].zip file.**

**When installing the `@sitecore/bizfx` package, make sure you are installing the version that shipped with `Sitecore Experience Commerce` that you are using.**

```npm
npm install speak-ng-bcl-0.8.0.tgz
npm install speak-styling-0.9.0-r00078.tgz
npm install @sitecore/bizfx
```

Run `npm install`

#### Build

Run `ng build` to build the project. The build artifacts will be stored in the `dist/` directory. Use the `-prod` flag for a production build.

#### Copy files from `/Sitecore.BizFx/dist/` to your SitecoreBizFx web site (C:\inetpub\wwwroot\SitecoreBizFx for example).

### 2. Visual Studio Helix based solution [Hackathon.AAATeam.sln](Hackathon.AAATeam.sln).

Publish Sitecore.Commerce.Engine solution to the CommerceAuthoring web site (C:\inetpub\wwwroot\CommerceAuthoring for example).

![Image](images/IMG2.png?raw=true)

After publish, please clean by postman web request habitat environment and then initialize environment (because we need new fields in the catalog items).


### YouTube link

[video](https://www.youtube.com/watch?v=OUitKiIPuz4)

