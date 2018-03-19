# escape=`
FROM sitecorecommercedocker_commerce

SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

ADD ./WatchDirectoryMultiple.ps1 /Scripts

ENTRYPOINT /Scripts/WatchDirectoryMultiple.ps1 -Path C:\Workspace `
            -Destinations @('C:\\inetpub\\wwwroot\\CommerceAuthoring_Sc9', `
                            'C:\\inetpub\\wwwroot\\CommerceMinions_Sc9', `
                            'C:\\inetpub\\wwwroot\\CommerceOps_Sc9', `
                            'C:\\inetpub\\wwwroot\\CommerceShops_Sc9')
