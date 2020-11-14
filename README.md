# Unity Input Validation
Unity Requirement: 2019.1+

Validates the Legacy Input System

-----
## Screenshots

Scene 'detectgamepad':
![Showing the controller layout](https://user-images.githubusercontent.com/43559/99154159-83834200-2662-11eb-93e0-b87b6fa568a1.png)

Scene 'showinput':
![Showing the raw input detection](https://user-images.githubusercontent.com/43559/99154239-1ae89500-2663-11eb-9c7e-45f96b53da94.png)

-----
## Known Issue
1. If connect the controller using Bluetooth, not dongle (ie: DualShock 4 -> CUH-ZWA1G) or USB cable. Input System can't detect whether or not the controller is disconnected by power off.
1. Input System can detect Joycon, but it doesn't detect as Gamepad(???)
1. runInBackground doesn't work. Unity window must be focus. (because direct access??)


-----
## TODO

* Add Switch PRO image and switch image by current select. (ADD)

-----
## Update
###### 2020-11-13
* Convert to Legacy Input System

###### 2019.04.28
* Delete junk code
* Fix unstable indecator button
* Add Xbox controller indecator image

###### 2019.04.26
* Multiple Controller Support

###### 2019.04.25
* Project Created!


## Credits

Based on Unity-Input-System-Controller-Test by Valxe
www.valxe.idv.tw
kculwp@gmail.com
