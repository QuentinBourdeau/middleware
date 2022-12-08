# PNS-SI4-S7-Mwsoc_LetsGoBiking

## Comment exécuter le projet :

Il va falloir tout d'abord lancer les .exe générés par Visual Studio, puis ensuite lancer le client Java.

## Partie C#

Lorsque les exécutables ont été générés par Visual Studio il y a 2 solutions :

- Si vous possédez Windows Terminal (ou alors utlisez ce lien pour l'installer https://apps.microsoft.com/store/detail/windows-terminal/9N0DX20HK701?hl=en-us&gl=us),
	- lancez un Windows Terminal PowerShell en administrateur, puis exécutez le fichier ```runC#.bat```

- Sinon  
	- lancez manuellement les exécutables suivants en prenant soin de commencer par le proxyCache :        
			```SolutionsLab2\GenericProxyCache\bin\Debug\GenericProxyCache.exe```           
			puis            
			```SolutionsLab2\test\bin\Debug\test.exe```

## Partie Java

Les entrées du client sont des adresses de départ et d'arrivée, par exemple :
- ```Rue Paul Bert, Nantes```
- ```Chateau des ducs de Bretagne```
(exemple présent dans le fichier Java\LetsGoBiking\exemple.txt)

Maintenant pour lancer le client vous avez 3 solutions :
- Ouvrir le dossier Java\LetsGoBiking dans votre IDE préféré, utiliser la commande ```mvn clean package```dans un terminal puis exécuter la méthode main présente dans le fichier LetsGoBiking\src\main\java\fr\unice\polytech\BikeProject\Main.java
- Sinon si vous possédez Windows Terminal
	- lancez un Windows Terminal PowerShell en administrateur, puis exécutez le fichier ```runJava.bat```
- Sinon lancez le client avec une console de commande
	- Placez vous dans le dossier ```Java\LetsGoBiking``` avec la commande ```cd Java\HeavyClient```
	- Puis lancez le client avec la commande ```mvn compile exec:java```
