# dotnet-message-app

## Projektübersicht
Dieses Projekt enthält zwei .NET Core Microservices:  
- **MessageSender**: Sendet Nachrichten an den Receiver.  
- **MessageReceiver**: Empfängt und zeigt Nachrichten an.  

Beide Dienste laufen containerisiert in Docker und werden via Kubernetes mit Helm-Charts deployed.  
Die Container-Images werden in Azure Container Registry (ACR) gespeichert.  
ArgoCD sorgt für automatisches GitOps-Deployment im Kubernetes-Cluster.

---

## Voraussetzungen
- Azure CLI installiert und angemeldet (`az login`)  
- Kubernetes-Cluster mit ArgoCD und Image Updater eingerichtet  
- Helm und kubectl lokal installiert und auf das Cluster konfiguriert  
- GitHub Secrets konfiguriert:  
  - `AZURE_CREDENTIALS` (Service Principal JSON für Azure)  
  - `ACR_NAME` (Name deiner Azure Container Registry, z.B. `dotnetacrnick`)

---

## GitHub Actions
- Workflows bauen und pushen automatisch Docker-Images bei Code-Push in `main`.  
- Die Images bekommen eindeutige Tags mit Zeitstempel.  
- (Optional) Helm-Deploy direkt aus GitHub Actions, wenn Runner Zugriff auf Kubernetes hat.

---

## Lokales Deployment
Da GitHub Actions meist keinen Zugriff auf dein Kubernetes-Cluster hat, kannst du lokal deployen:

1. PowerShell-Skript `deploy-latest.ps1` nutzen, das automatisch die neuesten Image-Tags aus ACR liest und Helm-Deploys ausführt.  
2. Alternativ manuelle Helm-Befehle:  
   ```powershell
   helm upgrade --install sender ./helm/sender --namespace default --set image.tag=DEIN_TAG
   helm upgrade --install receiver ./helm/receiver --namespace default --set image.tag=DEIN_TAG
