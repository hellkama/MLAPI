$myPath = (Get-Item -Path ".\").FullName;
$myPath = $myPath.Replace("\", "/")
$executablePath = -join ($myPath, "/Libraries/Internal/ApiParser/ApiParser.exe")

$argList = "./MLAPI/bin/Debug/MLAPI.dll ./MLAPI/bin/Debug/MLAPI.xml ./docs/_data/api.yml ./docs/_api/"

Start-Process -FilePath $executablePath -ArgumentList $argList