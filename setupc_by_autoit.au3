Run(".\setupc.exe --silent install PortName=COM1 PortName=COM2")
;~ WinWaitActive("Windows 安全")
WinWaitActive("Windows Security")
Send("!i")