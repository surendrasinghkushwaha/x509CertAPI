*************************************************************************************************************
Procedure
Open the Visual Studio command prompt as an Administrator.
Navigate to the folder where you want to create the certificate files.
To create a certificate and a private key file, run the following command 
C:\Users\SK.DESKTOP-DUN8VHF\source\repos\x509CertAPI>makecert.exe -n "CN=mytest.com" -r -sv mytestCA.pvk mytestCA.cer
Succeeded


*************************************************************************************************************
Now create a client certificate that is signed by the first certificate:


C:\Users\SK.DESKTOP-DUN8VHF\source\repos\x509CertAPI>makecert.exe -pe -ss My -sr CurrentUser -a sha1 -sky exchange -n "CN=mytest.com"  -eku 1.3.6.1.5.5.7.3.2 -sk SignedByCA -ic mytestCA.cer -iv mytestCA.pvk
Succeeded

*************************************************************************************************************
To convert the files into a .pfx file, run the following command:
create the .pfx file that you will use to sign your deployments. Open a Command Prompt window, and type the following command:
PVK2PFX –pvk yourprivatekeyfile.pvk –spc yourcertfile.cer –pfx yourpfxfile.pfx –po yourpfxpassword
 
C:\Users\SK.DESKTOP-DUN8VHF\source\repos\x509CertAPI>PVK2PFX -pvk mytestCA.pvk -spc mytestCA.cer -pfx mytestCA.pfx -po 1
 
C:\Users\SK.DESKTOP-DUN8VHF\source\repos\x509CertAPI>

**************************************************************************************************************
Now time to use 
so host x509CertAPI in IIS
steps
1. right cliek on "sites" and  select "Add website" 
2. modal popup window give site name
                           