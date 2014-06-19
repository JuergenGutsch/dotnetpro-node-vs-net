# dnp-cloud

Das Ziel der fünften Aufgabe ist, einen Service zum lokalen Hosting von statischen Webseiten zu entwickeln. Der Service besteht dabei aus zwei Teilen: Dem Deployment und dem Hosting. Im Folgenden wird angenommen, dass der Service unter `localhost:3000` beziehungsweise `localhost:4000` erreichbar ist, dies ist aber nur exemplarisch zu verstehen.

Für das Deployment einer Webseite stellt der Service einen HTTPS- / SPDY-basierten Endpunkt zur Verfügung, der kompatibel zu Git ist, so dass ein Deployment über `git push` erfolgen kann. Eine Authentifizierung ist nicht vorgesehen. Ein Deployment erfolgt dann wie folgt:

    $ git push https://localhost:3000/example master

Das Fetchen von diesem Endpunkt darf nicht möglich sein.

Ein Deployment kann beliebige statische Dateien enthalten, wie HTML, CSS, JavaScript oder Bilder. Eine Größenbegrenzung ist nicht vorgesehen. Allerdings muss eine Textdatei namens `Hostfile` im Push enthalten sein, in der zeilenweise Domains hinterlegt werden, unter der die Webseite erreichbar sein soll, beispielsweise:

    example.com
    www.example.com

Sobald der Push empfangen wurde, wird das Deployment gestartet. Ist eine Datei namens `Mobile` in dem Push enthalten, sendet der Server nach einem (erfolgreichen oder fehlgeschlagenen) Deployment eine entsprechende SMS an die darin enthaltenen Nummern. Auch hier ist das Format zeilenweise aufgebaut:

    +491773373175
    +49171...

Der Service liefert die Webseiten über SPDY aus, wobei ein Fallback auf HTTPS vorzusehen ist, falls der anfragende Webbrowser das SPDY-Protokoll nicht unterstützt. Die Verknüpfung einer Webseite zu den angegebenen Domains erfolgt über den Hostheader. Wurde das bisherige Beispiel deployed, kann die Webseite aus dem Browser über die Adresse `https://www.example.com:4000` aufgerufen werden.

Das Ganze erfordert, dass entweder der DNS-Eintrag für die jeweilige Domain bereits korrekt vorgenommen wurde, oder dass die lokale `/etc/hosts`-Datei vom Benutzer entsprechend angepasst wurde. Beides obliegt dem Benutzer, nicht dem Service.

Falls mit einem selbst-signierten Zertifikat gearbeitet wird, muss das Kommando

    $ git config http.sslVerify false

ausgeführt werden, damit das Pushen trotzdem möglich ist. Ansonsten verweigert Git den Zugriff mit der Meldung, dass das Zertifikat ungültig sei.
