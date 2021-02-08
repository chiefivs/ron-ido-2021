rem npm i && dotnet restore
npm run build && dotnet publish -o "D:\inetpub\Pido2021" -c Release && copy "..\..\Ron.Ido2021.AppSettings\appsettings.json" "D:\inetpub\Pido2021\appsettings.json"