# Variablen
$acrName = "dotnetacrnick"
$resourceGroup = "dotnet-rg"
$namespace = "default"

# 1. Neuesten Image-Tag vom Sender aus ACR auslesen
$senderTag = az acr repository show-tags --name $acrName --repository dotnet-sender --orderby time_desc --top 1 --output tsv

# 2. Neuesten Image-Tag vom Receiver aus ACR auslesen
$receiverTag = az acr repository show-tags --name $acrName --repository dotnet-receiver --orderby time_desc --top 1 --output tsv

Write-Host "Sender Tag: $senderTag"
Write-Host "Receiver Tag: $receiverTag"

# 3. Helm Upgrade / Install für Sender mit dem neuesten Tag
helm upgrade --install sender ./helm/sender --namespace $namespace --set image.tag=$senderTag

# 4. Helm Upgrade / Install für Receiver mit dem neuesten Tag
helm upgrade --install receiver ./helm/receiver --namespace $namespace --set image.tag=$receiverTag

Write-Host "Deployment abgeschlossen."