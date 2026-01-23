param location string = resourceGroup().location
param environment string = 'production'

resource appServicePlan 'Microsoft.Web/serverfarms@2022-03-01' = {
  name: 'nyhjelp24-prod-plan'
  location: location
  sku: {
    name: 'P1v2'
    tier: 'PremiumV2'
  }
}

resource webApp 'Microsoft.Web/sites@2022-03-01' = {
  name: 'nyhjelp24-prod-app'
  location: location
  properties: {
    serverFarmId: appServicePlan.id
    siteConfig: {
      appSettings: [
        { name: 'ASPNETCORE_ENVIRONMENT', value: environment }
        { name: 'DOTNET_VERSION', value: '10.0' }
      ]
      linuxFxVersion: 'DOTNETCORE|10.0'
    }
  }
}
