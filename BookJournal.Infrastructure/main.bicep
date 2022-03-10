param env string = 'dev'
param sa_sku object = { 
  name: 'Standard_LRS'
}
param location string = '${resourceGroup().location}'

// Vars can contain runtime variable values
var region = 'au'
var company = 'hectagon'
var saName = replace('${company}-sa-${env}-${region}', '-', '')
var apimName = '${company}-apim-${env}-${region}'
var aiName = '${company}-ai-${env}-${region}'

resource sa 'Microsoft.Storage/storageAccounts@2021-08-01' = {
  name: saName
  location: location
  kind: 'StorageV2'
  sku: sa_sku
  properties:{
    supportsHttpsTrafficOnly: true
  }
}

// Link container to storage account by providing path as name
resource saContainerApim 'Microsoft.Storage/storageAccounts/blobServices/containers@2019-06-01' = {
  name: '${sa.name}/default/apim-files'
}
resource saContainerApi 'Microsoft.Storage/storageAccounts/blobServices/containers@2019-06-01' = {
  name: '${sa.name}/default/api-files'
}

resource saBookCovers 'Microsoft.Storage/storageAccounts/blobServices/containers@2019-06-01' = {
  name: '${sa.name}/default/book-covers'
}

resource apim 'Microsoft.ApiManagement/service@2021-08-01' = {
  name: apimName
  location: location
  sku: {
    capacity: 0
    name: 'Consumption'
  }
  identity: {
    type: 'SystemAssigned'
  }
  properties:{
    publisherName: 'Nick Ward'
    publisherEmail: 'nickward@microsoft.com'
  }
}

resource apimPolicy 'Microsoft.ApiManagement/service/policies@2019-12-01' = {
  name: '${apim.name}/policy'
  properties:{
    format: 'rawxml'
    value: '<policies><inbound /><backend><forward-request /></backend><outbound /><on-error /></policies>'
  }
}

resource ai 'Microsoft.Insights/components@2015-05-01' = {
  name: aiName
  location: location
  kind: 'web'
  properties:{
    Application_Type:'web'
  }
}

resource apimLogger 'Microsoft.ApiManagement/service/loggers@2019-12-01' = {
  name: '${apim.name}/${apim.name}-logger'
  properties:{
    resourceId: '${ai.id}'
    loggerType: 'applicationInsights'
    credentials:{
      instrumentationKey: '${ai.properties.InstrumentationKey}'
    }
  }
}
