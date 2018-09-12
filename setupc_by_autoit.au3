Run(".\setupc.exe --silent install PortName=COM101 PortName=COM102")
;~ WinWaitActive("Windows 安全")
WinWaitActive("Windows Security")
Send("!i")