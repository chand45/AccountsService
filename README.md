# accounts-service

##In order to run this app##
1. Create container for yourself in: https://ms.portal.azure.com/#@microsoft.onmicrosoft.com/resource/subscriptions/b571a8a7-4881-4377-94a4-7d264f5b46d3/resourceGroups/yasagrawal-cap-rg/providers/Microsoft.DocumentDB/databaseAccounts/yasagrawalcosmosacc/dataExplorer under DB: aurora-platform-db with name: Accounts-<ReplaceWithYourName>
2. Update this value in Repo.cs

## Getting access to Cosmos DB ##
### To be run in a bash cli like git bash etc ###
signed_in_user=$(az ad signed-in-user show)
echo "$signed_in_user" | jq -r .id
 
### To be run in command prompt ###
```bash
$assignee_object_id="< value from cli >"
$sql_role_definition_name="/subscriptions/b571a8a7-4881-4377-94a4-7d264f5b46d3/resourceGroups/yasagrawal-cap-rg/providers/Microsoft.DocumentDB/databaseAccounts/$yasagrawalcosmosacc/sqlRoleDefinitions/f18c7abd-ff28-40f4-bc17-197134277c90"
$account_name="yasagrawalcosmosacc"
rg_name="yasagrawal-cap-rg"

az cosmosdb sql role assignment create \
    --account-name "$account_name" \
    --resource-group "$rg_name" \
    --scope "/" \
    --principal-id "$assignee_object_id" \
    --role-definition-id "$sql_role_definition_name"
```

## Running the app ### 
1. Run the app in Visual Studio
2. Trigger the APIs via AccountsService.http
