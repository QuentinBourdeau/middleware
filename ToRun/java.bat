@ECHO off
cd .\Java\LetsGoBiking
call mvn clean package
call mvn compile exec:java
pause