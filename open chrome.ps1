$pathToChrome = 'C:\Program Files (x86)\Google\Chrome\Application\chrome.exe'
$tempFolder = '--user-data-dir=c:\temp' # pick a temp folder for user data
$startmode = '--start-fullscreen' # '--kiosk' is another option
$startPage = 'file:///C:/Users/Samvel-Laptop/source/repos/Glow%20Text/WebTemplate/SongLive.html'

Start-Process -FilePath $pathToChrome -ArgumentList $tempFolder, $startmode, $startPage, --window-position=1025,0 --window-size=1280,720 





