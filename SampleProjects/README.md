# Sample Projects

## DesktopApp
Sample Windows Forms desktop application with a class library project. Inno Setup script file installs tha application, creates a desktop shortcut and an uninstall shortcut in Add/Remove Programs.

## OptionalFeatures
Sample Windows Forms desktop application with 3 class libraries that demostrate optional features/plugins that can installed by the setup wizard. Inno Setup script displays additinal setup wizard page for selecting optinal features.

## Windows Service
Simple Windows service project that writes a mesage to the console periodically. In debug mode it runs as a console application and in release mode it runs as a windows service. Inno Script file installs and registers the service.

## Windows Service With Login
Sample Windows service which runs with a user account instead of local system accounts. Inno Setup script displays additional setup wizard page for entering user account name and password, and sets the Windows service's user account.