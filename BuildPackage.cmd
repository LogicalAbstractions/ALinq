C:\Windows\Microsoft.NET\Framework64\v4.0.30319\msbuild Source\ALinq.sln /property:Configuration=Release
md Packages
cd Source\ALinq
..\.nuget\nuget pack -OutputDirectory ..\..\Packages -Properties Configuration=Release
cd ..\..