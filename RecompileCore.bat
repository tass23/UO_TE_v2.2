@SET CURPATH=%~dp0

@SET CSCPATH=%windir%\Microsoft.NET\Framework\v4.0.30319\


@SET SRVPATH=%CURPATH%Server\

@SET SCRPATH=%CURPATH%Scripts\


@TITLE: UO-The Expanse created by Raist, using RunUO/OrbUO/ServUO hybrid
@ECHO:

@ECHO: Ready to Compile UO-The Expanse
@ECHO:


@PAUSE


@DEL "%CURPATH%server.exe"
@ECHO ON

%CSCPATH%csc.exe /win32icon:"%SRVPATH%uo_te_v2.1_ser2.ico" /r:"%CURPATH%OpenUO.Core.dll" /r:"%CURPATH%OpenUO.Ultima.dll" /r:"%CURPATH%Ultima.dll" /r:"%CURPATH%OpenUO.Ultima.Windows.Forms.dll" /target:exe /out:"%CURPATH%server.exe" /recurse:"%SRVPATH%*.cs" /d:Framework_4_0 /d:server /nowarn:0618 /debug /nologo /optimize /unsafe

@ECHO OFF


@ECHO:

@ECHO: Done!

@ECHO:


@PAUSE
@CLS



@ECHO:

@ECHO: Ready to start UO-The Expanse

@ECHO:


@PAUSE


@CLS


@ECHO OFF

server.exe
