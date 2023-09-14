# OneDrive Photo Viewer
Ccross-paltform OneDrive Photo viewer primarily targeted for Samsung Smart TV (Tizen OS)

# Dev toolchain setup
1. Install & Configure Intel HAXM (Hardware Accelerated Execution Manager)
  - Disable Windows features: Hyper-V, Windows Hypervisor Platform, Virutal Machine Platform, Guarded Host, Windows Sandbox
  - Disable Device Guard: gpedit.msc -> Computer Configuration -> Administrative Templates -> System -> Device Guard -> Turn On Virtualization Based Security -> Disabled
  - Download & install HAXM from https://github.com/intel/haxm/releases
  - Check it is running: `sc query intelhaxm` under admin
2. Install Visual Studio 2022
3. Install .NET 7.0 SDK
4. Install & configure Visual Studio Tools for Tizen extension
  - Install Visual Studio Tools for Tizen extension
  - Install Tizen SDK (Tools -> Tizen -> Tizen Package Manager)
  - Install Tizen TV Extension SDK (Tools -> Tizen -> Tizen Package Manager)
  - Configure Tizen Emulator (Tools -> Tizen -> Tizen Emulator Manager), try to start emulator
5. Install dotnet maui-tizen workload:
   dotnet workload install maui-tizen